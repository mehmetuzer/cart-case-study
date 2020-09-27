using System;
namespace BusinessLogic.Services.DeliveryCostService
{
    public interface IDeliveryCostService
    {
        /// <summary>
        /// Teslimat Maaliyetini Hesaplar
        /// </summary>
        /// <param name="cartService">Sepet servisi</param>
        /// <returns>Teslimat maaliyeti tutarı</returns>
        double CostCalculate(ICartService cartService);
    }
}
