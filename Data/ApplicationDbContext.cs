using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiGall.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PemberianQuest> PemberianQuests { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        public DbSet<RiwayatTransaksi> RiwayatTransaksis { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mengatur DeleteBehavior untuk User
            modelBuilder.Entity<RiwayatTransaksi>()
                .HasOne(r => r.User)
                .WithMany()  // Menyesuaikan dengan relasi Anda
                .HasForeignKey(r => r.Email)
                .OnDelete(DeleteBehavior.Restrict);  // Menetapkan delete behavior

            // Mengatur DeleteBehavior untuk Transaksi
            modelBuilder.Entity<RiwayatTransaksi>()
                .HasOne(r => r.Transaksi)
                .WithMany()  // Menyesuaikan dengan relasi Anda
                .HasForeignKey(r => r.TanggalTransaksi)
                .OnDelete(DeleteBehavior.Restrict);  // Menetapkan delete behavior
        }
        
    }
}
