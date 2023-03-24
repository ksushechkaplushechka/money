using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace учет_буджета
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ExampleDtp.DisplayDateStart = new DateTime(2023, 03, 24);
            DateTime date = Convert.ToDateTime(ExampleDtp.Text);
            MessageBox.Show(date.ToShortDateString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page win = new Page();
            win.Show();
        }
    }
}
