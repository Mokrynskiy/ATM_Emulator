using DevExpress.Mvvm;
using System.Collections.Generic;

namespace ATM_Emulator.Models
{
    public class CashOutVariantsModel:ViewModelBase
    {
        public Dictionary<int,int> CashOutBancnotes { get; set; }
        public decimal Remainder { get; set; }
        public bool IsPossibly { get; set; }
    }
}
