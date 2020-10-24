using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.Models.DbModel
{
    public class AtmDbInitializer: DropCreateDatabaseAlways<AtmContext>
    {
        protected override void Seed(AtmContext context)
        {
            context.Banknotes.Add(new Banknote { NominalValue = 50, Count = 50});
            context.Banknotes.Add(new Banknote { NominalValue = 100, Count = 50 });
            context.Banknotes.Add(new Banknote { NominalValue = 200, Count = 20});
            context.Banknotes.Add(new Banknote { NominalValue = 500, Count = 20 });
            context.Banknotes.Add(new Banknote { NominalValue = 1000, Count = 20 });
            context.Banknotes.Add(new Banknote { NominalValue = 2000, Count = 20 });
            context.Banknotes.Add(new Banknote { NominalValue = 5000, Count = 10 });
            Account account = new Account { AccountNumber = "40702810967100000724", CardNumber = "2364-4567-4836-1247", LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович" };
            context.Accounts.Add(account);
            context.Transactions.Add(new Transaction { Account = account, Date = DateTime.Now, Amount = 50000, Discription = "Поступление. Начальный капитал." });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
