using ComputerApi.Controllers;
using ComputerApi.Models;
using ComputerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ComputerApiTests;

public class ComputerControllerTests
{
    private readonly Mock<IComputerService> _computerServiceMock;
    private readonly ComputerController _controller;

    public ComputerControllerTests()
    {
        _computerServiceMock = new Mock<IComputerService>();

        _controller = new ComputerController(
            _computerServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithComputers()
    {
        // Arrange
        var computers = new List<ComputerDto>
        {
            new()
            {
                Id = 1,
                Model = "MacBook Pro 14",
                Processor = "Apple M4 Pro",
                RamGb = 24,
                StorageGb = 512
            },
            new()
            {
                Id = 2,
                Model = "ThinkPad X1 Carbon",
                Processor = "Intel Core Ultra 7",
                RamGb = 32,
                StorageGb = 1000
            }
        };

        _computerServiceMock
            .Setup(service => service.GetAllAsync())
            .ReturnsAsync(computers);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedComputers =
            Assert.IsType<List<ComputerDto>>(okResult.Value);

        Assert.Equal(computers, returnedComputers);

        _computerServiceMock.Verify(
            service => service.GetAllAsync(),
            Times.Once);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithEmptyList()
    {
        // Arrange
        var computers = new List<ComputerDto>();

        _computerServiceMock
            .Setup(service => service.GetAllAsync())
            .ReturnsAsync(computers);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedComputers =
            Assert.IsType<List<ComputerDto>>(okResult.Value);

        Assert.Empty(returnedComputers);

        _computerServiceMock.Verify(
            service => service.GetAllAsync(),
            Times.Once);
    }
}
