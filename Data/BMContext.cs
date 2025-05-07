using Microsoft.EntityFrameworkCore;
using PBL3.Entities;

namespace PBL3.Data
{
    public class BMContext: DbContext
    {
        public BMContext(DbContextOptions<BMContext> bmc):base(bmc) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Trans> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RegularAccount>().HasBaseType<BankAccount>();
            modelBuilder.Entity<LoanAccount>().HasBaseType<BankAccount>();
            modelBuilder.Entity<SavingAccount>().HasBaseType<BankAccount>();

            modelBuilder.Entity<Trans>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.SentTransactions)
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trans>()
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.ReceivedTransactions)
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccount>()
            .HasDiscriminator<string>("AccountType")
            .HasValue<RegularAccount>("Regular")
            .HasValue<LoanAccount>("Loan")
            .HasValue<SavingAccount>("Saving");
        }
    }
}