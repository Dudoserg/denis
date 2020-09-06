using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.Entity;
using WpfApp2.Repos;

namespace WpfApp2.Forms.Rooms
{
    /// <summary>
    /// Логика взаимодействия для Form_rooms.xaml
    /// </summary>
    public partial class Form_rooms : Window
    {
        ApplicationContext db;

        List<RoomTypes> roomTypesList;
        List<Entity.Rooms> roomsList;
        string emptyComboBoxItem = "--\\--";

        public Form_rooms()
        {
            InitializeComponent();

            updateDataGrid();


            //emptyRoomType = new RoomTypes();
            //emptyRoomType.Id = -1;
            //emptyRoomType.Type = "";

            //ComboBox_roomType.Items.Add(emptyRoomType);

            ComboBox_roomType.Items.Add(emptyComboBoxItem);
            foreach (RoomTypes roomType in roomTypesList)
            {
                ComboBox_roomType.Items.Add(roomType);
            }

            ComboBox_size.Items.Add(emptyComboBoxItem);
            Entity.Rooms.initComboboxSize(ComboBox_size);
        }

        public void updateDataGrid()
        {
            db = new ApplicationContext();


            // создаем репозиторий типов комнат, для работы с бд
            db.RoomTypes.Load();
            EFGenericRepository<Entity.RoomTypes> roomTypesRepo = new EFGenericRepository<Entity.RoomTypes>(db);
            roomTypesList = (List<Entity.RoomTypes>)roomTypesRepo.Get();

            // создаем репозиторий комнат, для работы с бд
            db.Rooms.Load();
            EFGenericRepository<Entity.Rooms> roomsRepo = new EFGenericRepository<Entity.Rooms>(db);
            roomsList = (List<Entity.Rooms>)roomsRepo.Get();
            // Т.к. EF не хочет устанавливать внешние связи, придется делать связи вручную
            foreach (Entity.Rooms room in roomsList)
            {
                room.customInit(db.RoomTypes);
            }
        
            DataGrid_rooms.ItemsSource = roomsList;

            findRowsByFilter();
        }

        private void Button_createRoom_click(object sender, RoutedEventArgs e)
        {
            Form_createRoom form = new Form_createRoom(null);

            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_findRoom_click(object sender, RoutedEventArgs e)
        {
            findRowsByFilter();
        }
        private void findRowsByFilter()
        {
            RoomTypes findRoomType = ComboBox_roomType.SelectedIndex > -1 ? (RoomTypes)ComboBox_roomType.SelectedItem : null;

            int find_priceFrom = TextBox_priceFrom.Text == "" ? -1 : Int32.Parse(TextBox_priceFrom.Text);
            int find_priceTo = TextBox_priceTo.Text == "" ? Int32.MaxValue : Int32.Parse(TextBox_priceTo.Text);
            int find_size = ComboBox_size.SelectedIndex > -1 ? (int)ComboBox_size.SelectedItem : -1;

            int find_number = TextBox_roomNumber.Text == "" ? -1 : Int32.Parse(TextBox_roomNumber.Text);

            db.Clients.Load();

            roomsList = new List<Entity.Rooms> { };

            foreach (Entity.Rooms room in db.Rooms)
            {
                bool isAdding = true;

                if (findRoomType != null && room.RoomTypesId != findRoomType.Id)
                    isAdding = false;

                if (!(find_priceFrom <= room.Price && room.Price <= find_priceTo))
                    isAdding = false;

                if (find_size >= 0 && room.Size != find_size)
                    isAdding = false;

                if (find_number >= 0 && find_number != room.Number)
                    isAdding = false;

                if (isAdding)
                    roomsList.Add(room);
            }
            DataGrid_rooms.ItemsSource = roomsList;
        }

        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {
            Entity.Rooms room = ((FrameworkElement)sender).DataContext as Entity.Rooms;
            Console.WriteLine("clicked edit row  id = " + room.Id);

            Form_createRoom form = new Form_createRoom(room);
            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {
            Entity.Rooms room = ((FrameworkElement)sender).DataContext as Entity.Rooms;

            MessageBoxResult messageBoxResult = 
                System.Windows.MessageBox.Show(
                    "Вы действительно хотите удалить комнату №" + room.Number,
                    "Delete Confirmation",
                    System.Windows.MessageBoxButton.YesNo
                );

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Console.WriteLine("clicked delete row id = " + room.Id);

                // Проверяем, нет ли зависимых от это строки записей
                OrderRepos orderRepo = new OrderRepos(db);
                List<Order_entity> dependentOrders = orderRepo.GetByRoomId(room.Id);
                if (dependentOrders.Count != 0)
                {
                    MessageBox.Show(
                        "Невозможно удалить эту запись, т.к. от нее зависят " + dependentOrders.Count + " заказов");
                    return;
                }
                
                using (var context = new ApplicationContext())
                {
                    var deletedCustomer = context.Rooms.Where(c => c.Id == room.Id).FirstOrDefault();
                    context.Rooms.Remove(deletedCustomer);
                    context.SaveChanges();
                }

                db.SaveChanges();

                this.updateDataGrid();
            }

        }

        private void Button_resetFilter_click(object sender, RoutedEventArgs e)
        {
            ComboBox_roomType.SelectedIndex = -1;
            ComboBox_size.SelectedIndex = -1;

            TextBox_priceFrom.Text = "";
            TextBox_priceTo.Text = "";

            TextBox_roomNumber.Text = "";

            ButtonAutomationPeer peer = new ButtonAutomationPeer(Button_findRoom);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void ComboBox_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_roomType.SelectedItem is string && (string)ComboBox_roomType.SelectedItem == this.emptyComboBoxItem)
                ComboBox_roomType.SelectedIndex = -1;
            findRowsByFilter();
        }

        private void ComboBox_size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_size.SelectedItem is string && (string)ComboBox_size.SelectedItem == this.emptyComboBoxItem)
                ComboBox_size.SelectedIndex = -1;
            findRowsByFilter();
        }

        private void TextBox_roomNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }

        private void TextBox_priceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }

        private void TextBox_priceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();
        }
    }
}
