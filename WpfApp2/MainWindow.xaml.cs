﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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
using WpfApp2.Utils;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationContext db;

        private List<Entity.Rooms> roomsList;
        private List<Entity.RoomTypes> roomTypesList;
        private List<Entity.Clients> clientsList;
        private List<Entity.Order_entity> ordersList;
        private List<Entity.Order_entity> ordersList_table;
        
        private string emptyComboBoxItem = "--\\--";
        
        private string text_number = "";
        private string text_priceFrom = "";
        private string text_priceTo = "";
        
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                ComboBox_Size.Items.Add(emptyComboBoxItem);
                Entity.Rooms.initComboboxSize(ComboBox_Size);

                updateDataGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                using (StreamWriter writetext = new StreamWriter("_logs.txt"))
                {
                    writetext.WriteLine(ex.Message);
                    writetext.WriteLine("\n");
                    writetext.WriteLine(ex.ToString());
                }
            }
            
        }

        public void updateDataGrid()
        {
            db = new ApplicationContext();
            
            roomTypesList =  RoomTypes.init_RoomTypes(db);
            roomsList = Entity.Rooms.init_Rooms(db);
            clientsList = Clients.init_Clients(db);
            ordersList = Order_entity.init_Orders(db);

            ordersList_table = new List<Order_entity>();
            foreach (var order in ordersList)
            {
                ordersList_table.Add(order);
            }
            
            DateTime now = DateTime.Now;

            ObservableCollection<Order_entity> items = new ObservableCollection<Order_entity>(ordersList) { };
            
            DataGrid_orders.ItemsSource = ordersList_table;
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
            form.Closed += new EventHandler((o, args) => updateDataGrid());
            form.ShowDialog();
        }
        // кнопка меню комнаты
        private void MenuItem_rooms_click(object sender, RoutedEventArgs e)
        {
            Form_rooms form = new Form_rooms();

            form.Owner = this;
            form.Closed += new EventHandler((o, args) => updateDataGrid());
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
            form.Closed += new EventHandler((o, args) => updateDataGrid());
            form.ShowDialog();
        }
        // кнопка меню справка
        private void MenuItem_info_click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string[] lines = File.ReadAllLines("_data/data.txt");
                MessageBox.Show("author: " + lines[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                using (StreamWriter writetext = new StreamWriter("_logs.txt"))
                {
                    writetext.WriteLine(ex.Message);
                    writetext.WriteLine("\n");
                    writetext.WriteLine(ex.ToString());
                }
            }
        }

        private void Button_createOrder_click(object sender, RoutedEventArgs e)
        {
            Form_createOrder form = new Form_createOrder(null);

            form.Owner = this;
            form.Closed += new EventHandler((o, args) => updateDataGrid());

            form.ShowDialog();
        }

        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {
            Order_entity order = ((FrameworkElement)sender).DataContext as Order_entity;
            Console.WriteLine("clicked edit row  id = " + order.Id);

            Form_createOrder form = new Form_createOrder(order);
            form.Owner = this;
            form.Closed += new EventHandler((o, args) => updateDataGrid());
            form.ShowDialog();
        }

        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {
            Order_entity order = ((FrameworkElement)sender).DataContext as Order_entity;

            MessageBoxResult messageBoxResult = 
                System.Windows.MessageBox.Show(
                    "Вы действительно хотите удалить комнату №" + order.Id,
                    "Delete Confirmation",
                    System.Windows.MessageBoxButton.YesNo
                );

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Console.WriteLine("clicked delete row id = " + order.Id);

                // Проверяем, нет ли зависимых от это строки записей
                using (var context = new ApplicationContext())
                {
                    var deletedCustomer = context.Orders.Where(c => c.Id == order.Id).FirstOrDefault();
                    context.Orders.Remove(deletedCustomer);
                    context.SaveChanges();
                }

                db.SaveChanges();

                this.updateDataGrid();
            }
        }

        private void MenuItem_mainPage(object sender, RoutedEventArgs e)
        {
            updateDataGrid();
        }

        private void Button_resetFilter_click(object sender, RoutedEventArgs e)
        {
            TextBox_Number.Text = text_number = "";
            
            ComboBox_Size.SelectedIndex = -1;
            
            TextBox_PriceFrom.Text = text_priceFrom = "";
            
            TextBox_PriceTo.Text = text_priceTo = "";
            
            DatePicker_StartFrom.SelectedDate = null;
            DatePicker_StartTo.SelectedDate = null;
            DatePicker_EndFrom.SelectedDate = null;
            DatePicker_EndTo.SelectedDate = null;
        }

        // Дата заселения ОТ
        private void DatePicker_StartFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();
        }

        // Дата заселения ДО
        private void DatePicker_StartTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();
        }

        // Дата выезда ОТ
        private void DatePicker_EndFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();
        }
        // Дата выезда ДО
        private void DatePicker_EndTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();
        }


        private void TextBox_PriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_PriceFrom.Text = text_priceFrom = Helper.removeSymbolIfNotNumber(TextBox_PriceFrom, text_priceFrom, e);
            TextBox_PriceFrom.CaretIndex = TextBox_PriceFrom.Text.Length;
            findRowsByFilter();
        }

        

        private void TextBox_PriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_PriceTo.Text = text_priceTo = Helper.removeSymbolIfNotNumber(TextBox_PriceTo, text_priceTo, e);
            TextBox_PriceTo.CaretIndex = TextBox_PriceTo.Text.Length;
            findRowsByFilter();
        }

        private void ComboBox_Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Size.SelectedItem is string && (string)ComboBox_Size.SelectedItem == this.emptyComboBoxItem)
                ComboBox_Size.SelectedIndex = -1;
            findRowsByFilter();
        }

        private void TextBox_Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_Number.Text = text_number = Helper.removeSymbolIfNotNumber(TextBox_Number, text_number, e);
            TextBox_Number.CaretIndex = TextBox_Number.Text.Length;
            findRowsByFilter();
        }

        public  void findRowsByFilter()
        {
            int find_priceFrom = TextBox_PriceFrom.Text == "" ? -1 : Int32.Parse(TextBox_PriceFrom.Text);
            int find_priceTo = TextBox_PriceTo.Text == "" ? Int32.MaxValue : Int32.Parse(TextBox_PriceTo.Text);
            int find_size = ComboBox_Size.SelectedIndex > -1 ? (int)ComboBox_Size.SelectedItem : -1;
            int find_number = TextBox_Number.Text == "" ? -1 : Int32.Parse(TextBox_Number.Text);

            string find_firstName = TextBox_FirstName.Text;
            string find_secondName = TextBox_SecondName.Text;
            string find_patronymic = TextBox_Patronymic.Text;

            DateTime? dateStartFrom = DatePicker_StartFrom.SelectedDate;
            DateTime? dateStartTo = DatePicker_StartTo.SelectedDate;

            DateTime? dateEndFrom = DatePicker_EndFrom.SelectedDate;
            DateTime? dateEndTo = DatePicker_EndTo.SelectedDate;
            
            ordersList_table  = new List<Order_entity> { };

            foreach (var order in ordersList)
            {
                bool isAdding = true;


                if (!(find_priceFrom <= order.Rooms.Price && order.Rooms.Price <= find_priceTo))
                    isAdding = false;

                if (find_size >= 0 && order.Rooms.Size != find_size)
                    isAdding = false;

                if (find_number >= 0 && find_number != order.Rooms.Number)
                    isAdding = false;

                if (find_firstName.Length != 0 && !order.Clients.FirstName.ToLower().Contains(find_firstName.ToLower()))
                    isAdding = false;
                
                if (find_secondName.Length != 0 && !order.Clients.SecondName.ToLower().Contains(find_secondName.ToLower()))
                    isAdding = false;
                
                if (find_patronymic.Length != 0 && !order.Clients.Patronymic.ToLower().Contains(find_patronymic.ToLower()))
                    isAdding = false;

                if (dateStartFrom != null && order.DateStart < dateStartFrom )
                    isAdding = false;
                
                if (dateStartTo != null && dateStartTo < order.DateStart )
                    isAdding = false;
                
                if (dateEndFrom != null && order.DateEnd < dateEndFrom )
                    isAdding = false;
                
                if (dateEndTo != null && dateEndTo < order.DateEnd )
                    isAdding = false;

                if (isAdding)
                    ordersList_table.Add(order);
            }
            DataGrid_orders.ItemsSource = ordersList_table;

        }

        private void TextBox_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }

        private void TextBox_SecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }

        private void TextBox_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }

        private void Button_resetDateFilter_Click(object sender, RoutedEventArgs e)
        {
            DatePicker_StartFrom.SelectedDate = null;
            DatePicker_StartTo.SelectedDate = null;
            DatePicker_EndFrom.SelectedDate = null;
            DatePicker_EndTo.SelectedDate = null;
        }
    }
}
