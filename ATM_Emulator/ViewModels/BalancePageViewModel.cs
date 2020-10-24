using ATM_Emulator.Models.DbModel;
using System.Linq;

namespace ATM_Emulator.ViewModels
{
    public class BalancePageViewModel: BaseViewModel
    {      
        public decimal Amount { get; set; }

        AtmContext context = new AtmContext();

        public BalancePageViewModel()
        {
            Amount = GetBalance(PublcElements.AccountId);
        }
        public decimal GetBalance(int accoontId)
        {
            var transactions = from t in context.Transactions where t.Account.AccountId == accoontId select t;
            decimal amount = 0;
            foreach (var item in transactions)
            {
                amount += item.Amount;
            }
            return amount;
        }
    }
}
