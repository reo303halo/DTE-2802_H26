"""
Make sure GameInventoryApi is running first (http://localhost:5095)
"""

from flask import Flask, render_template, request, redirect, url_for, flash
import requests

app = Flask(__name__)
app.secret_key = "just-for-flash-messages-in-this-demo"  # only needed for flash()

API_BASE = "http://localhost:5095/Game"


@app.route("/")
def index():
    try:
        response = requests.get(API_BASE, timeout = 5)
        response.raise_for_status()
        games = response.json()
    except requests.exceptions.RequestException:
        flash("Could not reach the GameInventoryApi. Is it running?")
        games = []

    return render_template("index.html", games = games)


@app.route("/game/<int:game_id>")
def game_detail(game_id):
    response = requests.get(f"{API_BASE}/{game_id}", timeout = 5)

    if response.status_code == 404:
        flash(f"No game found with ID {game_id}.")
        return redirect(url_for("index"))

    game = response.json()
    return render_template("detail.html", game=game)


@app.route("/add", methods=["GET", "POST"])
def add_game():
    if request.method == "POST":
        new_game = {
            "title": request.form["title"],
            "genre": request.form["genre"],
            "hoursPlayed": 0,
            "installed": "installed" in request.form
        }

        response = requests.post(API_BASE, json = new_game, timeout = 5)

        if response.status_code == 201:
            flash(f"Added '{new_game['title']}' successfully!")
            return redirect(url_for("index"))
        else:
            flash(f"Failed to add game (status {response.status_code}).")

    return render_template("add.html")


if __name__ == "__main__":
    app.run(debug = True, port = 5000)
