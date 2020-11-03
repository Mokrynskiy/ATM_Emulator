using ATM_Emulator.Models;
using ATM_Emulator.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM_Emulator
{
    public static class CashOutVariants
    {
        public static CashOutVariantsModel GetCashOutVariants (int sum, IQueryable<Banknote> banknotes)
        {
            CashOutVariantsModel cashOutVariantsModel = new CashOutVariantsModel();
            cashOutVariantsModel.CashOutBancnotes = new Dictionary<int, int>();
            cashOutVariantsModel.IsPossibly = false;
            cashOutVariantsModel.Remainder = 0;
            var banknotesSorted = (from b in banknotes orderby b.NominalValue descending select b).ToList();
            int count = 0;
            
            foreach (var item in banknotesSorted)
            {                
                if (item.Count > 0)
                    {
                    count = sum / item.NominalValue;
                    if (count > 0)
                    {
                        if (item.Count >= count)
                        {
                            sum -= item.NominalValue * count;
                            cashOutVariantsModel.CashOutBancnotes.Add(item.NominalValue, count);

                        }
                        else
                        {
                            count = item.Count;
                            sum -= item.NominalValue * count;
                            cashOutVariantsModel.CashOutBancnotes.Add(item.NominalValue, count);
                        }
                    }
                }
                Console.WriteLine(item.NominalValue + " " + item.Count);
            }
            if (sum == 0)
                cashOutVariantsModel.IsPossibly = true;

            cashOutVariantsModel.Remainder = sum;
            return cashOutVariantsModel;
        }
    }
}
