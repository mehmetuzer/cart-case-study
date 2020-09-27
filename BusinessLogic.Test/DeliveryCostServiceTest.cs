using System;
using BusinessLogic.Services;
using BusinessLogic.Services.DeliveryCostService;
using Moq;
using NUnit.Framework;

namespace BusinessLogic.Test
{
    public class DeliveryCostServiceTest
    {
        IDeliveryCostService _deliveryCostService;
        Mock<ICartService> _cartService;

        [SetUp]
        public void Setup()
        {
            _deliveryCostService = new DeliveryCostService(5, 1);
            _cartService = new Mock<ICartService>();
        }

        [Test]
        public void CostCalculate_NullCartService_ReturnsException()
        {
            Assert.Throws<NullReferenceException>(() => _deliveryCostService.CostCalculate(null));
        }

        [Test]
        public void CostCalculate_SuccessfullyResult_ReturnExpectedValue()
        {
            _cartService.Setup(m => m.GetNumberOfProducts()).Returns(10);
            _cartService.Setup(m => m.GetNumberOfDeliveries()).Returns(2);

            Assert.That(_deliveryCostService.CostCalculate(_cartService.Object) == 20.00);
        }

    }
}
