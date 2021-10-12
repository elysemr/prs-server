using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prs_server.Models;

namespace prs_server.Models
{
    public class PrsCapstoneDbContext : DbContext 
    {

        public DbSet<prs_server.Models.User> User { get; set; }

        public DbSet<prs_server.Models.Product> Product { get; set; }

        public DbSet<prs_server.Models.Vendor> Vendor { get; set; }

        public DbSet<prs_server.Models.Request> Request { get; set; }

        public DbSet<prs_server.Models.RequestLine> RequestLine { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)

        {
            builder.Entity<User>(e =>
            {

                e.ToTable("Users");
                e.HasKey(p => p.Id);
                e.Property(p => p.Username).HasMaxLength(30).IsRequired(true);
                e.HasIndex(p => p.Username).IsUnique(true);
                e.Property(p => p.Password).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Firstname).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Lastname).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Phone).HasMaxLength(12).IsRequired(false);
                e.Property(p => p.Email).HasMaxLength(255).IsRequired(false);
                e.Property(p => p.IsReviewer).IsRequired(true);

            });

            builder.Entity<Vendor>(e =>
            {
                e.ToTable("Vendors");
                e.HasKey(p => p.Id);
                e.Property(p => p.Code).HasMaxLength(30).IsRequired(true);
                e.HasIndex(p => p.Code).IsUnique(true);
                e.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Address).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.City).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.State).HasMaxLength(2).IsRequired(true);
                e.Property(p => p.Zip).HasMaxLength(5).IsRequired(true);
                e.Property(p => p.Phone).HasMaxLength(12).IsRequired(false);
                e.Property(p => p.Email).HasMaxLength(255).IsRequired(false);

            });

            builder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(p => p.Id);
                e.Property(p => p.PartNbr).HasMaxLength(30).IsRequired(true);
                e.HasIndex(p => p.PartNbr).IsUnique(true);
                e.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Price).HasColumnType("decimal(11,2)").IsRequired(true);
                e.Property(p => p.Unit).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.PhotoPath).HasMaxLength(255).IsRequired(false);
                e.HasOne(p => p.Vendor).WithMany(p => p.Products).HasForeignKey(p => p.VendorId).OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Request>(e =>
            {
                e.ToTable("Requests");
                e.HasKey(p => p.Id);
                e.Property(p => p.Description).HasMaxLength(80).IsRequired(true);
                e.Property(p => p.Justification).HasMaxLength(80).IsRequired(true);
                e.Property(p => p.RejectionReason).HasMaxLength(80).IsRequired(false);
                e.Property(p => p.DeliveryMode).HasMaxLength(20).IsRequired(true);
                e.Property(p => p.Status).HasMaxLength(10).IsRequired(true);
                e.Property(p => p.Total).HasColumnType("decimal(11,2)").IsRequired(true);
                e.HasOne(p => p.User).WithMany(p => p.Requests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<RequestLine>(e =>
            {
                e.ToTable("RequestLines");
                e.HasKey(p => p.Id);
                e.HasOne(p => p.Request).WithMany(p => p.RequestLines).HasForeignKey(p => p.RequestId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(p => p.Product).WithMany(p => p.RequestLines).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);
                e.Property(p => p.Quantity).IsRequired(true);

            });
        
        }

        public PrsCapstoneDbContext(DbContextOptions<PrsCapstoneDbContext> options)
        : base(options)
        { }


    }
}
