using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ATM_Emulator.Models.DbModel
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [Column("ID")]        
        public int AccountId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }      
        public virtual IQueryable<Transaction> Transactions { get; set; }
    }
}
