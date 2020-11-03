using DevExpress.Mvvm;
using System.Windows.Input;

namespace ATM_Emulator.ViewModels
{
    public class OperationResultViewModel:BaseViewModel
    {
        public string Message { get; set; } = PublcElements.OperationMessage;

        public ICommand GoBack
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PublcElements.OperationMessage = string.Empty;
                    PublcElements.MainFrame.GoBack();
                });
            }

        }
    }
}
