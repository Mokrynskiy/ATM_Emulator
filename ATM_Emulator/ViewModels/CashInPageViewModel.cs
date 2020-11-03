using ATM_Emulator.Models.DbModel;
using System;
using System.Linq;
using ATM_Emulator.Models;
using System.Windows.Input;
using DevExpress.Mvvm;
using System.Windows;

namespace ATM_Emulator.ViewModels
{
    public class CashInPageViewModel: BaseViewModel
    {
        public NominalsModel Nominals { get; set; }
        AtmContext context = new AtmContext();
        public decimal Sum { get; set; } = 0;        
        public bool NotNullValue { get; set; }
        public bool MainContentVisibility { get; set; } = true;
        public bool SecondContentVisibility { get; set; } = false;
        public CashInPageViewModel()
        {
            Nominals = new NominalsModel();            
        }
        public ICommand Deposit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    try
                    {
                        foreach (var item in context.Banknotes)
                        {
                            switch (item.NominalValue)
                            {
                                case 50:
                                    item.Count += Nominals.Nom50Count;
                                    break;
                                case 100:
                                    item.Count += Nominals.Nom100Count;
                                    break;
                                case 200:
                                    item.Count += Nominals.Nom200Count;
                                    break;
                                case 500:
                                    item.Count += Nominals.Nom500Count;
                                    break;
                                case 1000:
                                    item.Count += Nominals.Nom1000Count;
                                    break;
                                case 2000:
                                    item.Count += Nominals.Nom2000Count;
                                    break;
                                case 5000:
                                    item.Count += Nominals.Nom5000Count;
                                    break;
                                default:
                                    break;
                            }

                        }
                        Transaction transaction = new Transaction();
                        transaction.Account = (from a in context.Accounts where a.AccountId == PublcElements.AccountId select a).First();
                        transaction.Date = DateTime.Now;
                        transaction.Discription = "Поступление. Внесение средств в банкомате";
                        transaction.Amount = Sum;
                        context.Transactions.Add(transaction);                        
                        context.SaveChanges();
                        MainContentVisibility = false;
                        SecondContentVisibility = true;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    
                    
                });
            }
        }
        public ICommand SumCalculate
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Sum = Nominals.Nom50Count*50+Nominals.Nom100Count*100+Nominals.Nom200Count*200+Nominals.Nom500Count*500+
                            Nominals.Nom1000Count*1000+Nominals.Nom2000Count*2000+Nominals.Nom5000Count*5000;                   
                   
                    if (Sum == 0)
                        NotNullValue = false;
                    else
                    {
                        if (NotNullValue != true)
                        {
                            NotNullValue = true;
                        }
                    }
                });
            }
        }

       
    }
}
