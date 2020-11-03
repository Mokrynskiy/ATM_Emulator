using DevExpress.Mvvm;
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
