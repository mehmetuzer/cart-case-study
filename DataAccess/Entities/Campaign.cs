using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Campaign
    {
        
        /// <summary>
        /// Kampanya Adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Sepetteki İlgili kategoriye ait Minumum Ürün Adeti
        /// </summary>
        public int MinumumNumberOfItemsInCart { get; set; }

        /// <summary>
        /// İndirim Oranı
        /// </summary>
        public int DiscountRate { get; set; }

        /// <summary>
        /// Kategori
        /// </summary>
        public Category Category { get; set; }

        public Campaign(string name, int minumumNumberOfItemsInCart, int discountRate, Category category)
        {
            Name = name;
            MinumumNumberOfItemsInCart = minumumNumberOfItemsInCart;
            DiscountRate = discountRate;
            Category = category;
        }

    }
}
