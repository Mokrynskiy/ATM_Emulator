using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Emulator.Models.DbModel
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [Column("ID")]
        public int TransactionId { get; set; }        
        public virtual Account Account { get; set; }
        public DateTime Date { get; set; }
        public string Discription { get; set; }
        public decimal Amount { get; set; }
    }
}
