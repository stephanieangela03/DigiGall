using DigiGall.Models;
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
        public DbSet<PemberianQuest> PemberianQuests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        public DbSet<RiwayatTransaksi> RiwayatTransaksis { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
