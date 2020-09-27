using BusinessLogic.Services;
using BusinessLogic.Services.DeliveryCostService;
using DataAccess.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessLogic.Test
{
    public class CartServiceTest
    {
        ICartService _cartService;
        Mock<IDeliveryCostService> _deliveryCostService;

        [SetUp]
        public void Setup()
        {
            _deliveryCostService = new Mock<IDeliveryCostService>();
            _cartService = new CartService(new Cart(), _deliveryCostService.Object);
        }


        [Test]
        public void GetTotalPaymentAmount_SuccessfullyResult_ReturnExpectedValue()
        {
            var electronicCategory = new Category("Electronic");

            var computerCategory = new Category("Computer", electronicCategory);

            var clothes = new Category("Clothes");


            var macbookPro = new Product("Macbook Pro", 18500.00, computerCategory);

            var tshirt = new Product("Basic Tshirt", 50.00, clothes);


            var computerCampaing = new Campaign("All computers %20 discount", 1, 20, computerCategory);

            var clothesCampaing = new Campaign("Minumum two clothes has get %40 discount", 2, 40, clothes);
            var coupon = new Coupon("ABCDEF1", 2000, 500);


            _cartService.AddCampaing(clothesCampaing);
            _cartService.AddCampaing(computerCampaing);
            _cartService.AddCoupon(coupon);

            _cartService.AddProduct(macbookPro, 1);

            _cartService.AddProduct(tshirt, 10);

            _deliveryCostService.Setup(x => x.CostCalculate(_cartService)).Returns(10.00);

            var totalAmount = _cartService.GetTotalPaymentAmount();

            Assert.AreEqual(14610.00, totalAmount, "Beklenen kampaylar ve kupon ile birlikte gelen toplam ödenecek tutar");
        }


        #region AddProduct

        [Test]
        public void AddProduct_SuccessfullyAdd_ReturnTrue()
        {
            var product = new Product("Apple", 5.00, new Category("fruit"));

            var result = _cartService.AddProduct(product, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddProduct_NegativeQuantity_ReturnFalse()
        {
            var product = new Product("Apple", 5.00, new Category("fruit"));

            var result = _cartService.AddProduct(product, -1);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddProduct_AlreadyExistProduct_ReturnFalse()
        {
            var product = new Product("Apple", 5.00, new Category("fruit"));

            var product2 = new Product("Apple", 3.00, new Category("fruit"));

            _cartService.AddProduct(product, 2);
            var result = _cartService.AddProduct(product, 1);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddProduct_NullCategory_ReturnFalse()
        {
            var product = new Product("Apple", 5.00, null);

            var result = _cartService.AddProduct(product, 1);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddProduct_NullProduct_ReturnFalse()
        {
            Product product = null;
            var result = _cartService.AddProduct(product, 1);
            Assert.IsFalse(result);
        }
        #endregion

        #region CreateNewInstanceIfProductsWithQuantityIsNullInCart
        [Test]
        public void CreateNewInstanceIfProductsWithQuantityIsNullInCart_Always_ReturnListCountZero()
        {
            _cartService.CreateNewInstanceIfProductsWithQuantityIsNullInCart();
            Assert.AreEqual(_cartService.GetNumberOfProducts(), 0);
        }

        [Test]
        public void CreateNewInstanceIfProductsWithQuantityIsNullInCart_Always_ReturnNull()
        {
            Assert.AreEqual(_cartService.GetNumberOfProducts(), null);
        }
        #endregion

        #region AddCampaing

        [Test]
        public void AddCampaing_NullCampaing_ReturnTrue()
        {
            Campaign campaign = null;
            var result = _cartService.AddCampaing(campaign);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddCampaing_SuccessfullyAdd_ReturnTrue()
        {
            Campaign campaign = new Campaign("%10 discount", 1, 10, new Category("Computer"));
            var result = _cartService.AddCampaing(campaign);
            Assert.IsTrue(result);
        }
        #endregion

        #region AddCoupon
        [Test]
        public void AddCoupon_NullCoupon_ReturnTrue()
        {
            Coupon coupon = null;
            var result = _cartService.AddCoupon(coupon);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddCoupon_SuccessfullyAdd_ReturnTrue()
        {
            Coupon coupon = new Coupon("20 Discount", 100.00, 20);
            var result = _cartService.AddCoupon(coupon);
            Assert.IsTrue(result);
        }
        #endregion

        [Test]
        public void GetNumberOfDeliveries_Successfull_ReturnTwo()
        {
            var clothes = new Category("Clothes");
            var electronicCategory = new Category("Electronic");

            var macbookPro = new Product("Macbook Pro", 18500.00, electronicCategory);

            var tshirt = new Product("Basic Tshirt", 50.00, clothes);

            _cartService.AddProduct(macbookPro, 2);
            _cartService.AddProduct(tshirt, 4);

            var result = _cartService.GetNumberOfDeliveries();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetNumberOfProducts_Successfull_ReturnThree()
        {
            var clothes = new Category("Clothes");
            var electronicCategory = new Category("Electronic");

            var macbookPro = new Product("Macbook Pro", 18500.00, electronicCategory);
            var lenovaThinkpad = new Product("Lenova ThinkPad", 7500.00, electronicCategory);
            var tshirt = new Product("Basic Tshirt", 50.00, clothes);

            _cartService.AddProduct(macbookPro, 2);
            _cartService.AddProduct(tshirt, 4);
            _cartService.AddProduct(lenovaThinkpad, 1);

            var result = _cartService.GetNumberOfProducts();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void GetDeliveryCost_SuccessfullyResult_ReturnOneHundred()
        {
            var electronicCategory = new Category("Electronic");
            var macbookPro = new Product("Macbook Pro", 18500.00, electronicCategory);
            _cartService.AddProduct(macbookPro, 2);

            _deliveryCostService.Setup(x => x.CostCalculate(_cartService)).Returns(100.00);
            var result = _cartService.GetDeliveryCost();
            Assert.AreEqual(100.00, result);
        }

        [Test]
        public void GetTotalAmountWithoutCampaingAndCoupon_SuccessfullyResult_ReturnExpectedValue()
        {
            var electronicCategory = new Category("Electronic");

            var computerCategory = new Category("Computer", electronicCategory);

            var clothes = new Category("Clothes");


            var macbookPro = new Product("Macbook Pro", 18500.00, computerCategory);

            var tshirt = new Product("Basic Tshirt", 50.00, clothes);

            _cartService.AddProduct(macbookPro, 1);

            _cartService.AddProduct(tshirt, 10);

            _deliveryCostService.Setup(x => x.CostCalculate(_cartService)).Returns(10.00);

            var result = _cartService.GetTotalAmountWithoutCampaingAndCoupon();


            Assert.AreEqual(19000.00, result);
        }

        [Test]
        public void GetTotalDiscountWithCoupon_SuccessfullyResult_ReturnExpectedValue()
        {
            var electronicCategory = new Category("Electronic");

            var computerCategory = new Category("Computer", electronicCategory);


            var macbookPro = new Product("Macbook Pro", 18500.00, computerCategory);


            var coupon = new Coupon("ABCDEF1", 2000, 500);

            _cartService.AddProduct(macbookPro, 1);

            _cartService.AddCoupon(coupon);

            var totalAmount = _cartService.GetTotalAmountWithoutCampaingAndCoupon();
            var result = _cartService.GetTotalDiscountWithCoupon(totalAmount);

            Assert.AreEqual(500, result);
        }

        [Test]
        public void GetTotalDiscountWithCampaing_SuccessfullyResult_ReturnExpectedValue()
        {
            var electronicCategory = new Category("Electronic");

            var computerCategory = new Category("Computer", electronicCategory);

            var clothes = new Category("Clothes");


            var macbookPro = new Product("Macbook Pro", 18500.00, computerCategory);

            var tshirt = new Product("Basic Tshirt", 50.00, clothes);


            var computerCampaing = new Campaign("All computers %20 discount", 1, 20, computerCategory);

            var clothesCampaing = new Campaign("Minumum two clothes has get %40 discount", 2, 40, clothes);


            _cartService.AddCampaing(clothesCampaing);
            _cartService.AddCampaing(computerCampaing);

            _cartService.AddProduct(macbookPro, 1);

            _cartService.AddProduct(tshirt, 10);

            var result = _cartService.GetTotalDiscountWithCampaing();

            Assert.AreEqual(3900.00, result);
        }

        [Test]
        public void IsChildCategory_SuccessfullyResult_ReturnTrue()
        {
            var electronicCategory = new Category("Electronic");

            var computerCategory = new Category("Computer", electronicCategory);
            var result = _cartService.IsChildCategory(electronicCategory, computerCategory);

            Assert.IsTrue(result);

        }
    }
}