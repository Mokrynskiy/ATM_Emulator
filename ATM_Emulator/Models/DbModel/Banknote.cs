using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.Models.DbModel
{   
    [Table("Banknotes")]
    public class Banknote
    {      
        [Key]
        [Column("ID")]
        public int BanknoteId { get; set; }
        public int NominalValue { get; set; }
        public int Count { get; set; }

    }
}
