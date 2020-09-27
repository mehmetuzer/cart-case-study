using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Cart
    {
        /// <summary>
        /// Ürünler
        /// </summary>
        public Dictionary<Product, int> ProductsWithQuantity { get; set; }

        /// <summary>
        /// Kupon
        /// </summary>
        public Coupon Coupon { get; set; } = null;

        /// <summary>
        /// Kampanyalar
        /// </summary>
        public List<Campaign> Campaigns { get; set; } = null;
    }
}
