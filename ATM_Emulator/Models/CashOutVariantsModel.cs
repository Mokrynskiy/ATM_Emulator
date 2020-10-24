using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.Models
{
    public class CashOutVariantsModel:ViewModelBase
    {
        public Dictionary<int,int> CashOutBancnotes { get; set; }
        public decimal Remainder { get; set; }
        public bool IsPossibly { get; set; }
    }
}
