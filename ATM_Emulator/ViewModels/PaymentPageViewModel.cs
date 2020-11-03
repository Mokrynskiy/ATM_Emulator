using ATM_Emulator.Models.DbModel;
using DevExpress.Mvvm;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ATM_Emulator.ViewModels
{
    class PaymentPageViewModel:BaseViewModel
    {
        string StringAmount = "0";
        public decimal Amount { get; set; } = 0;
        public string PhoneNumber { get; set; } = "";
        public bool ValidPhoneNumber { get; set; } = false;
        public bool ValidAllData { get; set; } = false;
        AtmContext context = new AtmContext();

        public ICommand ChangeAmount
        {
            get
            {
                return new DelegateCommand<string>((s) => {
                    StringAmount += s;
                    Amount = decimal.Parse(StringAmount);
                    ValidData();
                });
            }
        }
        public ICommand ChangePhoneNumber
        {
            get
            {
                return new DelegateCommand<string>((s) => {
                    PhoneNumber += s;
                    ValidData();
                });
            }
        }
        public ICommand CorrectPhoneNumber
        {
            get
            {
                return new DelegateCommand(() => {
                    if (PhoneNumber.Length >0)
                    {
                        int charIndex = PhoneNumber.Length - 1;
                        PhoneNumber = PhoneNumber.Remove(charIndex);
                    }
                    ValidData();
                });
            }
        }
        public ICommand CorrectSum
        {
            get
            {
                return new DelegateCommand(() => {
                    if (Amount >= 10)
                    {
                        int charIndex = StringAmount.Length - 1;
                        StringAmount = StringAmount.Remove(charIndex);
                        Amount = decimal.Parse(StringAmount);
                    }
                    else
                    {
                        StringAmount = "0";
                        Amount = decimal.Parse(StringAmount);
                    }
                    ValidData();                       
                });
            }
        }
        public ICommand Payment
        {
            get
            {
                return new DelegateCommand(() => {
                    decimal AccountAmount = (from a in context.Transactions where a.Account.AccountId == PublcElements.AccountId select a.Amount).ToArray().Sum();
                    if (AccountAmount >=Amount)
                    {
                        Transaction transaction = new Transaction();
                        transaction.Account = (from a in context.Accounts where a.AccountId == PublcElements.AccountId select a).First();
                        transaction.Amount = Amount * (-1);
                        transaction.Date = DateTime.Now;
                        transaction.Discription = $"Списание. Оплата связи {PhoneNumber}";
                        context.Transactions.Add(transaction);
                        context.SaveChanges();
                        PublcElements.OperationMessage = string.Format("УСПЕШНАЯ ОПЕРАЦИЯ \r\n НА НОМЕР {0} ЗАЧИСЛЕНО \r\n {1}",PhoneNumber,string.Format("{0:C}",Amount));                        
                    }
                    else
                    {
                        PublcElements.OperationMessage = $"НЕ ДОСТАТОЧНО СРЕДСТВ НА СЧЕТЕ!";
                    }
                    SetPage(@"\Views\OperationResultPage.xaml");
                });
            }
        }

        bool PhonNumberValidator(string phonNumber)
        {
            bool validPhoneNumber = false;           
            Regex regex = new Regex(@"^\+\d{11}");

            if (regex.IsMatch(phonNumber))
            {
                validPhoneNumber = true;
            }
            else
                validPhoneNumber = false;
            return validPhoneNumber;
        }
        void ValidData()
        {
            ValidPhoneNumber = PhonNumberValidator(PhoneNumber);
            if (ValidPhoneNumber || Amount < 0)
            {
                ValidAllData = true;
            }
        }        

    }
}
