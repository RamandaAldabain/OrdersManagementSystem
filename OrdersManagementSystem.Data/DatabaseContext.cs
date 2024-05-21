using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Data.SeedData;
using OrdersManagementSystem.Entities;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Entities.Lookups;

namespace OrdersManagementSystem.Data
{
	public class DatabaseContext : IdentityDbContext<User>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Seed Data
			modelBuilder.RoleSeed();
			modelBuilder.CategorySeed();
			modelBuilder.ItemsSeed();

			base.OnModelCreating(modelBuilder);	
		
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            modelBuilder.Entity<Item>()
            .Property(u => u.Description)
            .HasConversion(
             Description =>DataEncryptor.EncryptData<String>(Description),
             encryptedDescription => DataEncryptor.DecryptData<String>(encryptedDescription));

            modelBuilder.Entity<Item>()
            .Property(u => u.Price)
            .HasConversion(
                Price => DataEncryptor.EncryptData<double>(Price),
                encryptedPrice => DataEncryptor.DecryptData<double>(encryptedPrice)
				);
            modelBuilder.Entity<OrderItems>()
			   .HasOne(oi => oi.Order)
			   .WithMany(o => o.OrderItems)
			   .HasForeignKey(oi => oi.OrderId);

			modelBuilder.Entity<OrderItems>()
				.HasOne(oi => oi.Item)
				.WithMany(i => i.OrderItems)
				.HasForeignKey(oi => oi.ItemId);

			modelBuilder.Entity<Item>()
			   .HasOne(oi => oi.Category);
		}
       
        public DbSet<User> Users { get; set; }
		public DbSet<OrderItems> OrderItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<CategoryLookup> CategoryLookup { get; set; }
	}
}
