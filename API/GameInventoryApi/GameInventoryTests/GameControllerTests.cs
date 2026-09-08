using GameInventoryApi.Controllers;
using GameInventoryApi.Models;
using GameInventoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GameInventoryTests;

public class GameControllerTests
{
    private readonly Mock<IGameService> _gameServiceMock;
    private readonly GameController _controller;
    
    public GameControllerTests()
    {
        _gameServiceMock = new Mock<IGameService>();
        _controller = new GameController(_gameServiceMock.Object);
    }
    
    [Fact]
    public async Task GetGames_ReturnsOk_WithGames()
    {
        // Arrange
        var games = new List<Game>
        {
            new()
            {
                Id = 1,
                Title = "Elden Ring",
                Genre = "RPG",
                HoursPlayed = 50,
                Installed = true
            },
            new()
            {
                Id = 2,
                Title = "Halo Infinite",
                Genre = "FPS",
                HoursPlayed = 20,
                Installed = true
            }
        };
        
        _gameServiceMock
            .Setup(x => x.GetGames())
            .ReturnsAsync(games);
        
        // Act
        var result = await _controller.GetGames();
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedGames = Assert.IsType<IEnumerable<Game>>(okResult.Value, exactMatch: false);
        
        Assert.Equal(games, returnedGames);
        
        _gameServiceMock.Verify(
            x => x.GetGames(),
            Times.Once);
    }
    
    
    [Fact]
    public async Task GetGame_WhenGameExists_ReturnsOk_WithGame()
    {
        // Arrange
        const int gameId = 1;

        var game = new Game
        {
            Id = gameId,
            Title = "Elden Ring",
            Genre = "RPG",
            HoursPlayed = 50,
            Installed = true
        };

        _gameServiceMock
            .Setup(x => x.GetGame(gameId))
            .ReturnsAsync(game);

        // Act
        var result = await _controller.GetGame(gameId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedGame = Assert.IsType<Game>(okResult.Value);

        Assert.Equal(gameId, returnedGame.Id);
        Assert.Equal("Elden Ring", returnedGame.Title);
        Assert.Equal("RPG", returnedGame.Genre);
        Assert.Equal(50, returnedGame.HoursPlayed);
        Assert.True(returnedGame.Installed);

        _gameServiceMock.Verify(
            x => x.GetGame(gameId),
            Times.Once);
    }

    [Fact]
    public async Task GetGame_WhenGameDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        const int gameId = 999;

        _gameServiceMock
            .Setup(x => x.GetGame(gameId))
            .ReturnsAsync((Game?)null);

        // Act
        var result = await _controller.GetGame(gameId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

        _gameServiceMock.Verify(
            x => x.GetGame(gameId),
            Times.Once);
    }

    [Fact]
    public async Task AddGame_ReturnsCreatedAtAction_WithCreatedGame()
    {
        // Arrange
        var gameDto = new GameDto
        {
            Title = "Elden Ring",
            Genre = "RPG",
            HoursPlayed = 50,
            Installed = true
        };

        var game = new Game
        {
            Id = 1,
            Title = gameDto.Title,
            Genre = gameDto.Genre,
            HoursPlayed = gameDto.HoursPlayed,
            Installed = gameDto.Installed
        };

        _gameServiceMock
            .Setup(x => x.AddGame(gameDto))
            .ReturnsAsync(game);

        // Act
        var result = await _controller.AddGame(gameDto);

        // Assert
        var createdResult =
            Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(
            nameof(GameController.GetGame),
            createdResult.ActionName);

        Assert.Equal(game, createdResult.Value);

        Assert.NotNull(createdResult.RouteValues);
        Assert.Equal(game.Id, createdResult.RouteValues["id"]);

        _gameServiceMock.Verify(
            x => x.AddGame(gameDto),
            Times.Once);
    }

    [Fact]
    public async Task UpdateGame_WhenGameExists_ReturnsNoContent()
    {
        // Arrange
        const int gameId = 1;

        var gameDto = new GameDto
        {
            Title = "Elden Ring",
            Genre = "RPG",
            HoursPlayed = 75,
            Installed = true
        };

        _gameServiceMock
            .Setup(x => x.UpdateGame(gameId, gameDto))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateGame(gameId, gameDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        _gameServiceMock.Verify(
            x => x.UpdateGame(gameId, gameDto),
            Times.Once);
    }

    [Fact]
    public async Task UpdateGame_WhenGameDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        const int gameId = 999;

        var gameDto = new GameDto
        {
            Title = "Elden Ring",
            Genre = "RPG",
            HoursPlayed = 50,
            Installed = true
        };

        _gameServiceMock
            .Setup(x => x.UpdateGame(gameId, gameDto))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateGame(gameId, gameDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        _gameServiceMock.Verify(
            x => x.UpdateGame(gameId, gameDto),
            Times.Once);
    }

    [Fact]
    public async Task DeleteGame_WhenGameExists_ReturnsNoContent()
    {
        // Arrange
        const int gameId = 1;

        _gameServiceMock
            .Setup(x => x.DeleteGame(gameId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteGame(gameId);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        _gameServiceMock.Verify(
            x => x.DeleteGame(gameId),
            Times.Once);
    }

    [Fact]
    public async Task DeleteGame_WhenGameDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        const int gameId = 999;

        _gameServiceMock
            .Setup(x => x.DeleteGame(gameId))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteGame(gameId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        _gameServiceMock.Verify(
            x => x.DeleteGame(gameId),
            Times.Once);
    }
}