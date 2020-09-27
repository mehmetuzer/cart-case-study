using System.Collections.Generic;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Sepete adediyle birlikte ürün ekler.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <param name="productQuantity">Ürün Adedi</param>
        bool AddProduct(Product product, int productQuantity);

        /// <summary>
        /// Eğer Ürün ve adetlerini tutan liste boş ise yeni bir örnek oluşturur.
        /// </summary>
        void CreateNewInstanceIfProductsWithQuantityIsNullInCart();


        /// <summary>
        /// Ürün objesi ve ürün adet sayısı geçerli mi kontrolü yapılır.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <param name="productQuantity">Ürün adeti</param>
        /// <returns>Geçerli mi değilmi</returns>
        bool IsValidProductAndQuantity(Product product, int productQuantity);

        /// <summary>
        /// Ürün objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns>Boş değil mi ?</returns>
        bool IsNotNullCheckProduct(Product product);

        /// <summary>
        /// Ürün içinde ki kategori objesinin 
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns></returns>
        bool IsNotNullCheckCategoryInProduct(Product product);

        /// <summary>
        /// Ürün Adetlerinin geçerli olup olmadığı kontrol edilir.
        /// </summary>
        /// <param name="productQuantity">Ürün adet bilgisii</param>
        /// <returns>Geçerlimi ?</returns>
        bool IsValidQuantity(int productQuantity);

        /// <summary>
        /// Sepette aynı isme sahip ürün olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns>Aynı isme sahip ürün varmı ?</returns>
        bool IsNotExistSameProductInCart(Product product);

        /// <summary>
        /// Sepete Kampanya Ekle
        /// </summary>
        /// <param name="campaign">Kampanya</param>
        bool AddCampaing(Campaign campaign);


        /// <summary>
        /// Kampanya objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="campaign">Kampanya</param>
        /// <returns>Boş değil mi ?</returns>
        bool IsNotNullCheckCampaing(Campaign campaign);

        /// <summary>
        /// Eğer sepetteki kampayalar listesi boş ise yeni bir örnek oluşturur.
        /// </summary>
        void CreateNewInstanceIfCampaignListIsNullInCart();

        /// <summary>
        /// Sepete Kupon Ekler.
        /// </summary>
        /// <param name="coupon">Kupon</param>
        /// <returns>Başarılı kayıtmı?</returns>
        bool AddCoupon(Coupon coupon);

        /// <summary>
        /// Kupon objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="campaign">Kupon</param>
        /// <returns>Boş değil mi ?</returns>
        bool IsNotNullCheckCoupon(Coupon coupon);


        /// <summary>
        /// Kupon ve Kampanyalar olmadan önce toplam tutar.
        /// </summary>
        /// <returns>Toplam Tutar</returns>
        public double GetTotalAmountWithoutCampaingAndCoupon();


        /// <summary>
        /// İlgili Toplam tutar üzerinden kupon'un uyguladığı indirimi verir.
        /// </summary>
        /// <param name="totalAmount">Toplam Tuatar</param>
        /// <returns>İndirim Tutarı</returns>
        double GetTotalDiscountWithCoupon(double totalAmount);

        /// <summary>
        /// Kampalar sonrası sepet'te yapılacak olan toplam indirim miktarını verir.
        /// </summary>
        /// <returns>İndirim Tutarı</returns>
        double GetTotalDiscountWithCampaing();

        /// <summary>
        /// İlgili Kategoriye ait tüm ürünler adet sayılarıyla birlikte döner.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Adetleiyle birlikte ürün listesi</returns>
        Dictionary<Product, int> GetAllProductByCategory(Category category);

        /// <summary>
        /// Alt kategori üst kategorinin bir üyesimi diye kontrol sağlıyor.
        /// </summary>
        /// <param name="parent">Ana kategori</param>
        /// <param name="child">Alt kategori</param>
        /// <returns><Alt kateogirsimi bilgisi/returns>
        bool IsChildCategory(Category parent, Category child);

        /// <summary>
        /// Sepetteki ürünlerin kategorisine göre toplam teslimat sayısını bulur.
        /// </summary>
        /// <returns><Teslimat sayısı/returns>
        int? GetNumberOfDeliveries();


        /// <summary>
        /// Sepetteki toplam ürün sayısını bulur.
        /// </summary>
        /// <returns>Ürün Sayısı</returns>
        int? GetNumberOfProducts();

        // <summary>
        /// Sepetteki Ürün sayısı ve teslimat sayısına göre testlimat maaliyetini hesaplar.
        /// </summary>
        /// <returns>Teslimat Maaliyeti</returns>
        double GetDeliveryCost();

        /// <summary>
        /// Var ise kampaya ve kupon indirimleri uygulanmış ve teslimat maaliyeti eklenmiş toplam ödeme tutarını verir. 
        /// </summary>
        /// <returns>Toplam Ödeme Tutarı</returns>
        double GetTotalPaymentAmount();
    }
}
