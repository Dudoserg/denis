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
using WpfApp2.Forms.Rooms;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

           db = new ApplicationContext();
           db.Phones.Load();
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
            //Form_createClient form = new Form_createClient(null);
            //
            //form.Owner = this;
            //form.ShowDialog();
        }

        // кнопка меню клиенты
        private void MenuItem_2_click(object sender, RoutedEventArgs e)
        {
            Form_clients form = new Form_clients();

            form.Owner = this;
            form.ShowDialog();
        }
        // кнопка меню комнаты
        private void MenuItem_rooms_click(object sender, RoutedEventArgs e)
        {
            Form_rooms form = new Form_rooms();

            form.Owner = this;
            form.ShowDialog();
        }

        // кнопка выйти подменю программы
        private void MenuItem_programExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // кнопка меню оформить заказ
        private void MenuItem_createOrder_click(object sender, RoutedEventArgs e)
        {

        }
        // кнопка меню справка
        private void MenuItem_info_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("created By ...");
        }
    }
}
