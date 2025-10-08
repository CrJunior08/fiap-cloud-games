using FCG.Domain.Interfaces.Repository;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Application.Requests;

namespace FCG.Application.Tests;

[TestFixture]
public class GameServiceTests {
    private GameService _service;
    private Mock<IGameRepository> _repoMock;

    [SetUp]
    public void Setup() {
        // DADO: um repositório mockado
        _repoMock = new Mock<IGameRepository>();
        _service = new GameService(_repoMock.Object);
    }

    [Test]
    public async Task CreateGame_WithValidData_ReturnsCreatedGame() {
        // DADO: dados válidos para um jogo
        var request = new CreateGameRequest {
            Name = "Sonic",
            Gender = "Ação",
            Price = 199
        };
        var expected = new Game {
            Name = request.Name,
            Gender = request.Gender,
            Price = request.Price,
        };
        _repoMock.Setup(x => x.CreateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
        // QUANDO: criamos um jogo via serviço
        var result = await _service.CreateGameAsync(request);
        // ENTÃO: o jogo retornado deve conter os dados esperados
        Assert.IsNotNull(result);
        Assert.AreEqual("Sonic", result.Name);
        Assert.AreEqual("Ação", result.Gender);
        Assert.AreEqual(199, result.Price);
    }

    [Test]
    public async Task CreateGame_WithEmptyName_ThrowsException() {
        var request = new CreateGameRequest {
            Name = "",
            Gender = "Ação",
            Price = 199
        };
        var ex =  Assert.ThrowsAsync<ArgumentException>(async () => await _service.CreateGameAsync(request));
        Assert.That(ex.Message, Is.EqualTo("Nome do jogo é obrigatório")
        );
    }
}
