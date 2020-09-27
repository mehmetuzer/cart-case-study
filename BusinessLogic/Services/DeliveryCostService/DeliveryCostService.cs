using System;
namespace BusinessLogic.Services.DeliveryCostService
{
    public class DeliveryCostService : IDeliveryCostService
    {
        public double PerDeliveryCost { get; set; }
        public double PerProductCost { get; set; }

        public DeliveryCostService(double perDeliveryCost, double perProductCost)
        {
            PerDeliveryCost = perDeliveryCost;
            PerProductCost = perProductCost;
        }



        /// <summary>
        /// Teslimat Maaliyetini Hesaplar
        /// </summary>
        /// <param name="cartService">Sepet servisi</param>
        /// <returns>Teslimat maaliyeti tutarı</returns>
        public double CostCalculate(ICartService cartService)
        {
            if (cartService == null)
            {
                throw new NullReferenceException($"{nameof(cartService)} is Null");
            }
            var numberOfDeliveries = cartService.GetNumberOfDeliveries();

            var numberOfProducts = cartService.GetNumberOfProducts();

            return (PerDeliveryCost * (int)numberOfDeliveries) + (PerProductCost * (int)numberOfProducts);
        }

    }
}
