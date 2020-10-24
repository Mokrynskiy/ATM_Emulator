namespace ATM_Emulator.Models.DbModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AtmContext : DbContext
    {
        public AtmContext()
            : base("name=AtmContext")
        {
            Database.SetInitializer<AtmContext>(new AtmDbInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Banknote> Banknotes { get; set; }
    }
}