using System;
using System.Windows;

namespace ATM_Emulator.Views
{  
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            ATM_Emulator.Models.DbModel.AtmContext context = new Models.DbModel.AtmContext();
            foreach (var item in context.Banknotes)
            {
                Console.WriteLine(item.NominalValue);
            }            
        }        
    }
}
