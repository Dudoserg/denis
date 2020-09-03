using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Forms;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            //db = new ApplicationContext();
            //db.Phones.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //foreach( Phone tmp in db.Phones)
            //{
            //    Console.WriteLine(tmp.Title);
            //}
        }

        private void MenuItem_clients_click(object sender, RoutedEventArgs e)
        {
            Form_createClient form = new Form_createClient();

            form.Owner = this;
            form.ShowDialog();
        }

        private void MenuItem_2_click(object sender, RoutedEventArgs e)
        {
            Form_clients form = new Form_clients();

            form.Owner = this;
            form.ShowDialog();
        }
    }
}
