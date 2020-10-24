using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATM_Emulator.ViewModels
{
    public class StartPageViewModel:BaseViewModel
    {
        public ICommand ShowMessagePage
        {
            get
            {
                return new DelegateCommand<string>((s) =>
                {
                    PublcElements.OperationMessage = s;                    
                    SetPage(@"\Views\OperationResultPage.xaml");
                });
            }
        }
    }
}
