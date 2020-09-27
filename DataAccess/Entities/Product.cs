using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Product
    {
        [StringLength(200, ErrorMessage = "Karakter uzunlu {2} ve {1} aralığında olmak zorunda.", MinimumLength = 10)]
        [Required]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Category Category { get; set; }

        public Product(string title, double price,Category category)
        {
            Title = title;
            Price = price;
            Category = category;
        }
    }
}
