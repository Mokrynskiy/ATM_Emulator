using ATM_Emulator.Models;
using ATM_Emulator.Models.DbModel;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATM_Emulator.ViewModels
{
    public class EnterAmountPageViewModel: BaseViewModel
    {
        string StringAmount = "0";
        public decimal Amount { get; set; } = 0;
        AtmContext context = new AtmContext();
        public EnterAmountPageViewModel()
        {

        }
        public ICommand ChangeAmount
        {
            get
            {
                return new DelegateCommand<string>((s) => {
                    StringAmount += s;
                    Amount = decimal.Parse(StringAmount);
                });
            }
        }
        public ICommand ClearAmount
        {
            get
            {
                return new DelegateCommand<string>((s) => {
                    StringAmount = "0";
                    Amount = decimal.Parse(StringAmount);
                });
            }
        }

        public ICommand GetCash
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    decimal Balance = (from t in context.Transactions where t.Account.AccountId == PublcElements.AccountId select t.Amount).ToArray().Sum();
                    if (Balance >= Amount)
                    {
                        var bancnotes = from b in context.Banknotes where b.Count>0 select b;
                        int minSum = (from b in bancnotes select b.NominalValue).Min();                       
                        CashOutVariantsModel Cash = new CashOutVariantsModel();
                        Cash = CashOutVariants.GetCashOutVariants((int)Amount, bancnotes);
                        decimal operationSum = 0;
                        string stringOperatonSum = string.Empty;
                        string message = string.Empty;
                        if (Amount >= minSum)
                        {
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
                            else
                            {
                                PublcElements.OperationMessage = $"НЕВОЗМОЖНО ВЫДАТЬ УКАЗАННУЮ СУММУ!\r\nБЛИЖЫЙШАЯ СУММА ВОЗМОЖНАЯ ДЛЯ ВЫДАЧИ\r\n{string.Format("{0:C}", Amount - Cash.Remainder)}";
                            }
                        }
                        else
                        {
                            PublcElements.OperationMessage = $"СУММА ДЛЯ СНЯТИЯ ДОЛЖНА БЫТЬ\r\nНЕ МЕНЬШЕ {string.Format("{0:C}",minSum)}";
                        }
                        
                    }
                    else
                    {
                        PublcElements.OperationMessage = $"НЕ ДОСТАТОЧНО СРЕДСТВ НА СЧЕТЕ!";
                    }
                        
                    SetPage(@"\Views\OperationResultPage.xaml");

                });
            }
        }
    }
}
