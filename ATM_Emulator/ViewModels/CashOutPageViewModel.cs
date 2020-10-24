using ATM_Emulator.Models;
using ATM_Emulator.Models.DbModel;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ATM_Emulator.ViewModels
{
    public class CashOutPageViewModel: BaseViewModel
    {
        public CashOutVariantsModel Sum500Variant { get; set; }
        public CashOutVariantsModel Sum1000Variant { get; set; }
        public CashOutVariantsModel Sum2000Variant { get; set; }
        public CashOutVariantsModel Sum3000Variant { get; set; }
        public CashOutVariantsModel Sum4000Variant { get; set; }
        public CashOutVariantsModel Sum5000Variant { get; set; }


        AtmContext context = new AtmContext();        

        public CashOutPageViewModel()
        {
            var bancnotes = from b in context.Banknotes select b;
            Sum500Variant = CashOutVariants.GetCashOutVariants(500, bancnotes);            
            Sum1000Variant = CashOutVariants.GetCashOutVariants(1000, bancnotes);
            Sum2000Variant = CashOutVariants.GetCashOutVariants(2000, bancnotes);
            Sum3000Variant = CashOutVariants.GetCashOutVariants(3000, bancnotes);
            Sum4000Variant = CashOutVariants.GetCashOutVariants(4000, bancnotes);
            Sum5000Variant = CashOutVariants.GetCashOutVariants(5000, bancnotes);
        }

        

        public ICommand GetCash
        {
            get
            {
                return new DelegateCommand<string>((s) =>
                {
                    decimal Balance = (from t in context.Transactions where t.Account.AccountId == PublcElements.AccountId select t.Amount).ToArray().Sum();
                    if (Balance >= int.Parse(s))
                    {
                        var bancnotes = from b in context.Banknotes where b.Count>0 select b;                        
                        CashOutVariantsModel Cash = new CashOutVariantsModel();
                        Cash = CashOutVariants.GetCashOutVariants(int.Parse(s), bancnotes);
                        decimal operationSum = 0;
                        string stringOperatonSum = string.Empty;
                        string message = string.Empty;
                        if (Cash.IsPossibly)
                        {                            
                            foreach (var item in Cash.CashOutBancnotes)
                            {
                                operationSum += item.Key * item.Value;
                                var bancnote = (from b in context.Banknotes where b.NominalValue == item.Key select b).First();
                                bancnote.Count -= item.Value;
                                context.SaveChanges();
                                message += string.Format("{0:C2} - {1} шт. \r\n", item.Key, item.Value);
                            }
                            Transaction transaction = new Transaction();
                            transaction.Account = (from a in context.Accounts where a.AccountId == PublcElements.AccountId select a).First();
                            transaction.Amount = operationSum * -1;
                            transaction.Date = DateTime.Now;
                            transaction.Discription = "Списание. Снятие наличных";
                            context.Transactions.Add(transaction);
                            context.SaveChanges();
                            PublcElements.OperationMessage = $"УСПЕШНАЯ ОПЕРАЦИЯ!\r\nВАМ ВЫДАНО {string.Format("{0:C}", operationSum)}\r\n{message}";                            
                        }
                    }
                    else
                        PublcElements.OperationMessage = $"НЕ ДОСТАТОЧНО СРЕДСТВ НА СЧЕТЕ!";
                    SetPage(@"\Views\OperationResultPage.xaml");

                });
            }
        }

        
    }
}
