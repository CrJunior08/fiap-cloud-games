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

namespace FCG.Application.Tests {

    [TestFixture]
    public class PromotionServiceTests {
        private PromotionService _service;
        private Mock<IGameRepository> _repoMock;

        [SetUp]
        public void Setup() {
            _repoMock = new Mock<IGameRepository>();
            _service = new PromotionService(_repoMock.Object);
        }

        [Test]
        public async Task ApplyDiscount_ShouldReturnUpdatedGame() {
            var game = new Game {
                Id = 1,
                Name = "Avatar",
                Price = 100,
                OriginalPrice = 100,
                Gender = "Ação",
                IsOnPromotion = false
            };
            _repoMock.Setup(x => x.GetGameByIdAsync(It.IsAny<int>())).ReturnsAsync(game);
            _repoMock.Setup(x => x.UpdateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            var result = await _service.ApplyDiscountAsync(1, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(90, result.Price);
        }
    }
}