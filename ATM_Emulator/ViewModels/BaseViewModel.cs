using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ATM_Emulator;
using ATM_Emulator.Views;

namespace ATM_Emulator.ViewModels
{
    public class BaseViewModel:ViewModelBase
    {
        public ICommand StartAppCommand { get; set; }
        public ICommand GoToPage { get; set; }
        public ICommand SetAccountId { get; set; }
        public BaseViewModel()
        {
            this.StartAppCommand = new DelegateCommand<Frame>(RunStartPage);
            this.GoToPage = new DelegateCommand<string>(SetPage);
            this.SetAccountId = new DelegateCommand<int>(setAccountId);
        }
        public virtual void SetPage(string uriString)
        {            
            Uri uri = new Uri(uriString, UriKind.Relative);
            PublcElements.MainFrame.NavigationService.Navigate(uri);
        }
        public void setAccountId(int accountId)
        {
            PublcElements.AccountId = accountId;
        }
        private void RunStartPage(Frame obj)
        {
            PublcElements.MainFrame = obj;
            PublcElements.MainFrame.Content = new StartPage();
            //временный код
            setAccountId(1);
        }
    }
}

