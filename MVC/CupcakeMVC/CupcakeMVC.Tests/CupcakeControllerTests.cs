using System.Reflection;
using System.Security.Claims;
using CupcakeMVC.Controllers;
using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;
using CupcakeMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace CupcakeMVC.Tests;

public class CupcakeControllerTests
{
    private readonly Mock<ICupcakeRepository> _repository = new ();
    private readonly ClaimsPrincipal _user;
    private readonly CupcakeController _controller;

    public CupcakeControllerTests()
    {
        _user = new ClaimsPrincipal(new ClaimsIdentity(
        [new Claim(ClaimTypes.Name, "test@uit.no")], authenticationType: "TestAuth"));
        
        var httpContext = new DefaultHttpContext { User = _user };

        _controller = new CupcakeController(_repository.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
            TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
        };
    }
    
    [Fact]
    public void Index_ReturnsViewWithAllCupcakes()
    {
        // Arrange
        var cupcakes = new List<Cupcake>
        {
            new() { Id = 1, Name = "Vanilla", Description = "Classic", SizeId = 1, CategoryId = 1 },
            new() { Id = 2, Name = "Chocolate", Description = "Rich", SizeId = 2, CategoryId = 1 }
        };
        _repository.Setup(r => r.GetAll()).Returns(cupcakes);
        
        // Act
        var result = _controller.Index();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Cupcake>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count);
        _repository.Verify(r => r.GetAll(), Times.Once);
    }
    
    
    [Fact]
    public void Create_Get_ReturnsViewWithEditViewModel()
    {
        // Arrange
        var vm = new CupcakeEditViewModel();
        _repository.Setup(r => r.GetCupcakeEditViewModel()).Returns(vm);

        // Act
        var result = _controller.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(vm, viewResult.Model);
    }
    

    [Fact]
    public void Create_Post_ValidModel_SavesCupcakeAndRedirectsToIndex()
    {
        // Arrange
        var vm = new CupcakeEditViewModel
        {
            Name = "Lemon",
            Description = "Fresh and sour",
            SizeId = 2,
            CategoryId = 3
        };

        // Act
        var result = _controller.Create(vm);

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        
        _repository.Verify(r => r.Save(
                It.Is<Cupcake>(c =>
                    c.Name == "Lemon" &&
                    c.Description == "Fresh and sour" &&
                    c.SizeId == 2 &&
                    c.CategoryId == 3),
                _user),
            Times.Once);

        Assert.Equal("Cupcake with name 'Lemon' created successfully", _controller.TempData["Message"]);
    }

    [Fact]
    public void Create_Post_InvalidModel_ReturnsViewAndDoesNotSave()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");
        var vm = new CupcakeEditViewModel { Description = "No name" };

        // Act
        var result = _controller.Create(vm);

        // Assert
        Assert.IsType<ViewResult>(result);
        _repository.Verify(r => r.Save(It.IsAny<Cupcake>(), It.IsAny<ClaimsPrincipal>()), Times.Never);
    }

    [Fact]
    public void Create_Post_RepositoryThrows_SetsErrorMessageAndRedirectsToIndex()
    {
        // Arrange
        _repository
            .Setup(r => r.Save(It.IsAny<Cupcake>(), It.IsAny<ClaimsPrincipal>()))
            .Throws(new InvalidOperationException("Database is down"));

        var vm = new CupcakeEditViewModel { Name = "Lemon", Description = "x", SizeId = 1, CategoryId = 1 };

        // Act
        var result = _controller.Create(vm);

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Obs something went wrong", _controller.TempData["Message"]);
    }
    

    [Fact]
    public void Index_DoesNotRequireAuthorization()
    {
        var method = typeof(CupcakeController).GetMethod(nameof(CupcakeController.Index))!;
        Assert.Null(method.GetCustomAttribute<AuthorizeAttribute>());
    }

    [Fact]
    public void Create_Get_RequiresAuthorization()
    {
        var method = typeof(CupcakeController).GetMethod(nameof(CupcakeController.Create), Type.EmptyTypes)!;
        Assert.NotNull(method.GetCustomAttribute<AuthorizeAttribute>());
    }

    [Fact]
    public void Create_Post_RequiresAuthorizationAndHttpPost()
    {
        var method = typeof(CupcakeController).GetMethod(
            nameof(CupcakeController.Create), [typeof(CupcakeEditViewModel)])!;

        Assert.NotNull(method.GetCustomAttribute<AuthorizeAttribute>());
        Assert.NotNull(method.GetCustomAttribute<HttpPostAttribute>());
    }
    
}