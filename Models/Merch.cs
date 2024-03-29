﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMerchTrack2.Models
{
    public class Merch
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

        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Merch Type")]
        public int MerchTypeId { get; set; }

        public  MerchType MerchType { get; set; }

        public ICollection<MerchSize> MerchSizes { get; set; } 


    }
}