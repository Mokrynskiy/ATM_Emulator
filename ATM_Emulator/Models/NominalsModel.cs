using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.Models
{
    public class NominalsModel:ViewModelBase
    {
        public int Nom50Count { get; set; }
        public int Nom100Count { get; set; } 
        public int Nom200Count { get; set; }
        public int Nom500Count { get; set; } 
        public int Nom1000Count { get; set; }
        public int Nom2000Count { get; set; }
        public int Nom5000Count { get; set; }

    }
}
