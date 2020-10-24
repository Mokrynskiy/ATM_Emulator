using ATM_Emulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ATM_Emulator.Views;

using System.Data.Entity;

namespace ATM_Emulator.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
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
