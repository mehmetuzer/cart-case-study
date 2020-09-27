using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Category
    {
        [StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        [Required]
        public string Title { get; set; }

        public virtual Category ParentCategory { get; set; } = null;

        public Category(string title, Category parentCategory=null)
        {
            Title = title;
            ParentCategory = parentCategory;
        }


    }
}
