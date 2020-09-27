using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Coupon
    {
        /// <summary>
        /// İndirim Kodu
        /// </summary>
        [StringLength(7, ErrorMessage = "En az ve En fazla {1} karakter olmak zorunda.", MinimumLength = 7)]
        public string Code { get; set; }

        /// <summary>
        /// Minumum Sepet Miktarı
        /// </summary>
        public double MinumumCartAmount { get; set; }

        /// <summary>
        /// İndirim Miktarı
        /// </summary>
        public int DiscountAmount { get; set; }

        public Coupon(string code, double minumumCartAmount, int discountAmount)
        {
            Code = code;
            MinumumCartAmount = minumumCartAmount;
            DiscountAmount = discountAmount;
        }
    }
}
