using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMerchTrack2.Models;

namespace MyMerchTrack2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         public DbSet<ApplicationUser> ApplicationUser { get; set; }
         public DbSet<MyMerchTrack2.Models.Merch> Merch { get; set; }
         public DbSet<MyMerchTrack2.Models.MerchType> MerchType { get; set; }
         public DbSet<MyMerchTrack2.Models.MerchSize> MerchSize { get; set; }
    }
}
