using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp2.Entity;
using WpfApp2.Forms;
using WpfApp2.Forms.Order;
using WpfApp2.Forms.Rooms;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        List<Entity.Rooms> roomsList;
        List<Entity.RoomTypes> roomTypesList;
        List<Entity.Clients> clientsList;
        List<Entity.Order_entity> ordersList;
        
        public MainWindow()
        {
            InitializeComponent();

            updateDataGrid();
        }

        public void updateDataGrid()
        {
            db = new ApplicationContext();
            
            roomTypesList =  RoomTypes.init_RoomTypes(db);
            roomsList = Entity.Rooms.init_Rooms(db);
            clientsList = Clients.init_Clients(db);
            ordersList = Order_entity.init_Orders(db);
            
            DateTime now = DateTime.Now;

            ObservableCollection<Order_entity> items = new ObservableCollection<Order_entity>(ordersList) { };
            
            DataGrid_orders.ItemsSource = items;
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
            Form_createOrder form = new Form_createOrder(null);

            form.Owner = this;
            form.ShowDialog();
        }
        // кнопка меню справка
        private void MenuItem_info_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("author: Nagibator228");
        }

        private void Button_createOrder_click(object sender, RoutedEventArgs e)
        {
            Form_createOrder form = new Form_createOrder(null);

            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {
            Order_entity order = ((FrameworkElement)sender).DataContext as Order_entity;
            Console.WriteLine("clicked edit row  id = " + order.Id);

            Form_createOrder form = new Form_createOrder(order);
            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_mainPage(object sender, RoutedEventArgs e)
        {
            updateDataGrid();
        }
    }
}
