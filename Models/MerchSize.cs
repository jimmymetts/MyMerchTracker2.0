using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMerchTrack2.Models
{
    public class MerchSize
    {
        [Key]
        public int Id { get; set; }

        public string Size { get; set; }

        public int MerchId { get; set; }

        public Merch Merch { get; set; }

        public int Quantity { get; set; }
    }
}
