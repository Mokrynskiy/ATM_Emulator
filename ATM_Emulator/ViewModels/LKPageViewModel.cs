using ATM_Emulator.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.ViewModels
{
    class LKPageViewModel: BaseViewModel
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public Account CurrentAccount { get; set; } = new Account();
        public LKPageViewModel()
        {
            AtmContext context = new AtmContext();
            Transactions.AddRange(from t in context.Transactions where t.Account.AccountId == PublcElements.AccountId select t);
            CurrentAccount = (from a in context.Accounts where a.AccountId == PublcElements.AccountId select a).First();

        }
    }
}
