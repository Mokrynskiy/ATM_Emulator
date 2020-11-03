using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
