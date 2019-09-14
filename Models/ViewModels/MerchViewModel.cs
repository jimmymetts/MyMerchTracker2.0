using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMerchTrack2.Models.ViewModels
{
    public class MerchViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Merch Description")]
        [RegularExpression("^[a-zA-Z0-9 ]*$")]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string MerchDescription { get; set; }

        [Required]
        [Display(Name = "Merch Price")]
        public double MerchPrice { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Small")]
        public int Small { get; set; }

        [Display(Name = "Medium")]
        public int Medium { get; set; }

        [Display(Name = "Large")]
        public int Large { get; set; }

        [Display(Name = "XLarge")]
        public int Xlarge { get; set; }

        [Display(Name = "XXLarge")]
        public int Xxlarge { get; set; }

        [Display(Name = "All Size")]
        public string AllSize { get; set; }

        public int MerchTypeId { get; set; }






    }
}
