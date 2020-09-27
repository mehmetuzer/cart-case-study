using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Services.DeliveryCostService;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Sepet servisi
    /// </summary>
    public class CartService : ICartService
    {
        /// <summary>
        /// Teslim maaliyetini hesaplayan servis
        /// </summary>
        private readonly IDeliveryCostService _deliveryCostService;

        /// <summary>
        /// Sepet
        /// </summary>
        private Cart Cart;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deliveryCostService">Teslim maaliyet'i hesaplayıcı servis</param>
        public CartService(Cart cart, IDeliveryCostService deliveryCostService)
        {
            _deliveryCostService = deliveryCostService;
            Cart = cart;
        }

        /// <summary>
        /// Sepete adediyle birlikte ürün ekler.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <param name="productQuantity">Ürün Adedi</param>
        public bool AddProduct(Product product, int productQuantity)
        {
            if (IsValidProductAndQuantity(product, productQuantity))
            {
                CreateNewInstanceIfProductsWithQuantityIsNullInCart();
                Cart.ProductsWithQuantity.Add(product, productQuantity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Eğer Ürün ve adetlerini tutan liste boş ise yeni bir örnek oluşturur.
        /// </summary>
        public void CreateNewInstanceIfProductsWithQuantityIsNullInCart()
        {
            if (Cart.ProductsWithQuantity == null)
            {
                Cart.ProductsWithQuantity = new Dictionary<Product, int>();
            }
        }

        /// <summary>
        /// Ürün objesi ve ürün adet sayısı geçerli mi kontrolü yapılır.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <param name="productQuantity">Ürün adeti</param>
        /// <returns>Geçerli mi değilmi</returns>
        public bool IsValidProductAndQuantity(Product product, int productQuantity)
        {
            if (!IsNotNullCheckProduct(product)
                || !IsNotNullCheckCategoryInProduct(product)
                || !IsValidQuantity(productQuantity)
                || !IsNotExistSameProductInCart(product))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Ürün objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns>Boş değil mi ?</returns>
        public bool IsNotNullCheckProduct(Product product) => product != null;

        /// <summary>
        /// Ürün içinde ki kategori objesinin 
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns></returns>
        public bool IsNotNullCheckCategoryInProduct(Product product) => product.Category != null;

        /// <summary>
        /// Ürün Adetlerinin geçerli olup olmadığı kontrol edilir.
        /// </summary>
        /// <param name="productQuantity">Ürün adet bilgisii</param>
        /// <returns>Geçerlimi ?</returns>
        public bool IsValidQuantity(int productQuantity) => productQuantity > 0 && productQuantity < 100 == true;

        /// <summary>
        /// Sepette aynı isme sahip ürün olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="product">Ürün</param>
        /// <returns>Aynı isme sahip ürün varmı ?</returns>
        public bool IsNotExistSameProductInCart(Product product)
        {
            if (IsNotNullCheckProductsWithQuantityInCart)
            {
                return !Cart.ProductsWithQuantity.Any(x => x.Key.Title == product.Title);
            }
            return true;
        }

        /// <summary>
        /// Sepete Kampanya Ekle
        /// </summary>
        /// <param name="campaign">Kampanya</param>
        public bool AddCampaing(Campaign campaign)
        {
            if (IsNotNullCheckCampaing(campaign))
            {
                CreateNewInstanceIfCampaignListIsNullInCart();
                Cart.Campaigns.Add(campaign);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Kampanya objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="campaign">Kampanya</param>
        /// <returns>Boş değil mi ?</returns>
        public bool IsNotNullCheckCampaing(Campaign campaign) => campaign != null;

        /// <summary>
        /// Eğer sepetteki kampayalar listesi boş ise yeni bir örnek oluşturur.
        /// </summary>
        public void CreateNewInstanceIfCampaignListIsNullInCart()
        {
            if (Cart.Campaigns == null)
            {
                Cart.Campaigns = new List<Campaign>();
            }
        }


        /// <summary>
        /// Sepete Kupon Ekler.
        /// </summary>
        /// <param name="coupon">Kupon</param>
        /// <returns>Başarılı kayıtmı?</returns>
        public bool AddCoupon(Coupon coupon)
        {
            if (IsNotNullCheckCoupon(coupon))
            {
                Cart.Coupon = coupon;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kupon objesinin boş olup olmadığını kontrolünü sağlar.
        /// </summary>
        /// <param name="campaign">Kupon</param>
        /// <returns>Boş değil mi ?</returns>
        public bool IsNotNullCheckCoupon(Coupon coupon) => coupon != null;

        /// <summary>
        /// Sepetteki ürünlerin kategorisine göre toplam teslimat sayısını bulur.
        /// </summary>
        /// <returns><Teslimat sayısı/returns>
        public int? GetNumberOfDeliveries() => IsNotNullCheckProductsWithQuantityInCart && IsAnyProductsWithQuantityListInCart
                ? Cart.ProductsWithQuantity.GroupBy(x => x.Key.Category.Title).Count()
                : (int?)null;

        /// <summary>
        /// Sepette Ürün vce adet bilgisi listesi boşmu kontrolü sağlar.
        /// </summary>
        public bool IsNotNullCheckProductsWithQuantityInCart => Cart.ProductsWithQuantity != null;

        /// <summary>
        /// Sepette Hiç Ürün ve adet bilgisi kayıdı varmı kontrolünü sağlar.
        /// </summary>
        public bool IsAnyProductsWithQuantityListInCart => Cart.ProductsWithQuantity.Any();

        /// <summary>
        /// Sepetteki toplam ürün sayısını bulur.
        /// </summary>
        /// <returns>Ürün Sayısı</returns>
        public int? GetNumberOfProducts() => Cart.ProductsWithQuantity?.Count();

        /// <summary>
        /// Sepetteki Ürün sayısı ve teslimat sayısına göre testlimat maaliyetini hesaplar.
        /// </summary>
        /// <returns>Teslimat Maaliyeti</returns>
        public double GetDeliveryCost() => _deliveryCostService.CostCalculate(this);

        /// <summary>
        /// Kupon ve Kampanyalar olmadan önce toplam tutar.
        /// </summary>
        /// <returns>Toplam Tutar</returns>
        public double GetTotalAmountWithoutCampaingAndCoupon() => IsNotNullCheckProductsWithQuantityInCart == true ? Cart.ProductsWithQuantity.Sum(x => x.Key.Price * x.Value) : 0;

        /// <summary>
        /// Var ise kampaya indirimleriyle oluşan toplam tutar üzerinden kupon'un uyguladığı indirimi verir.
        /// </summary>
        /// <param name="totalAmount">Toplam Tuatar</param>
        /// <returns>İndirim Tutarı</returns>
        public double GetTotalDiscountWithCoupon(double totalAmount) => IsNotNullCheckCouponInCart && totalAmount >= Cart.Coupon.MinumumCartAmount ? Cart.Coupon.DiscountAmount : 0;

        /// <summary>
        /// Sepetteki kupon objesinin boş olup olmadığı kontrol eder.
        /// </summary>
        public bool IsNotNullCheckCouponInCart => Cart.Coupon != null;

        /// <summary>
        /// Kampalar sonrası sepet'te yapılacak olan toplam indirim miktarını verir.
        /// </summary>
        /// <returns>İndirim Tutarı</returns>
        public double GetTotalDiscountWithCampaing()
        {
            var totalDiscount = 0.0;
            if (IsAnyCampaingInCart)
            {
                foreach (var campaign in Cart.Campaigns)
                {
                    var products = GetAllProductByCategory(campaign.Category);
                    if (products.Sum(x => x.Value) >= campaign.MinumumNumberOfItemsInCart)
                    {
                        var categoryAmount = 0.0;
                        foreach (var product in products.Keys)
                        {
                            categoryAmount += product.Price * products[product];
                        }
                        totalDiscount += categoryAmount * (campaign.DiscountRate / 100.00);
                    }
                }
            }
            return totalDiscount;
        }

        /// <summary>
        /// Sepette Hiç kampanya varmı kontrolü sağlar.
        /// </summary>
        private bool IsAnyCampaingInCart => Cart.Campaigns.Any();


        /// <summary>
        /// İlgili Kategoriye ait tüm ürünler adet sayılarıyla birlikte döner.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Adetleiyle birlikte ürün listesi</returns>
        public Dictionary<Product, int> GetAllProductByCategory(Category category)
        {
            return Cart.ProductsWithQuantity
                .Where(x => x.Key.Category == category || IsChildCategory(category, x.Key.Category))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Alt kategori üst kategorinin bir üyesimi diye kontrol sağlıyor.
        /// </summary>
        /// <param name="parent">Ana kategori</param>
        /// <param name="child">Alt kategori</param>
        /// <returns><Alt kateogirsimi bilgisi/returns>
        public bool IsChildCategory(Category parent, Category child)
        {
            var category = child.ParentCategory;
            while (category != null)
            {
                if (category == parent)
                {
                    return true;
                }
                category = category.ParentCategory;
            }
            return false;
        }

        /// <summary>
        /// Var ise kampaya ve kupon indirimleri uygulanmış ve teslimat maaliyeti eklenmiş toplam ödeme tutarını verir. 
        /// </summary>
        /// <returns>Toplam Ödeme Tutarı</returns>
        public double GetTotalPaymentAmount()
        {
            var totalAmountWithoutCampaingAndCoupon = GetTotalAmountWithoutCampaingAndCoupon();
            var totalDiscountWithCampaing = GetTotalDiscountWithCampaing();
            var totalDiscountWithCoupon = GetTotalDiscountWithCoupon(totalAmountWithoutCampaingAndCoupon);
            var deliveryCost = GetDeliveryCost();
            return totalAmountWithoutCampaingAndCoupon - (totalDiscountWithCampaing + totalDiscountWithCoupon) + deliveryCost;
        }
    }
}
