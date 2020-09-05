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
using System.Windows.Shapes;
using WpfApp2.Entity;
using WpfApp2.Repos;

namespace WpfApp2.Forms.Order
{
    /// <summary>
    /// Логика взаимодействия для Form_createOrder.xaml
    /// </summary>
    public partial class Form_createOrder : Window
    {
        ApplicationContext db;
        List<Entity.Rooms> roomsList;
        List<Entity.RoomTypes> roomTypesList;
        string emptyComboBoxItem = "--\\--";

        public Form_createOrder()
        {
            InitializeComponent();

            db = new ApplicationContext();


            roomTypesList = init_RoomTypes();
            roomsList = init_Rooms();

            // инициализируем комбобокс с выбором типа комнаты
            ComboBox_roomType.Items.Add(emptyComboBoxItem);
            foreach (RoomTypes type in roomTypesList)
            {
                ComboBox_roomType.Items.Add(type);
            }

            // Инициализируем комбобокс выбора размера комнаты значениями
            Combobox_size.Items.Add(emptyComboBoxItem);
            Entity.Rooms.initComboboxSize(Combobox_size);

            updateDataGridRooms();
        }

        public List<RoomTypes> init_RoomTypes()
        {
            // создаем репозиторий типов комнат, для работы с бд
            db.RoomTypes.Load();
            EFGenericRepository<Entity.RoomTypes> roomTypesRepo = new EFGenericRepository<Entity.RoomTypes>(db);
            return (List<Entity.RoomTypes>)roomTypesRepo.Get();
        }
        public List<Entity.Rooms> init_Rooms()
        {
            // создаем репозиторий комнат, для работы с бд
            db.Rooms.Load();
            EFGenericRepository<Entity.Rooms> roomsRepo = new EFGenericRepository<Entity.Rooms>(db);
            List<Entity.Rooms> tmpList = (List<Entity.Rooms>)roomsRepo.Get();
            // Т.к. EF не хочет устанавливать внешние связи, придется делать связи вручную
            foreach (Entity.Rooms room in tmpList)
            {
                room.customInit(db.RoomTypes);
            }
            return tmpList;
        }

        // Фильтруем доступные комнаты по фильтру
        private void findRoomsByFilter()
        {
            RoomTypes findRoomType = ComboBox_roomType.SelectedIndex > -1 ? (RoomTypes)ComboBox_roomType.SelectedItem : null;

            int find_priceFrom = TextBox_priceFrom.Text == "" ? -1 : Int32.Parse(TextBox_priceFrom.Text);
            int find_priceTo = TextBox_priceTo.Text == "" ? Int32.MaxValue : Int32.Parse(TextBox_priceTo.Text);
            int find_size = Combobox_size.SelectedIndex > -1 ? (int)Combobox_size.SelectedItem : -1;

            db.Clients.Load();

            roomsList = new List<Entity.Rooms> { };

            foreach (Entity.Rooms room in db.Rooms)
            {
                bool isAdding = true;

                if (findRoomType != null && room.TypeId != findRoomType.Id)
                    isAdding = false;

                if (!(find_priceFrom <= room.Price && room.Price <= find_priceTo))
                    isAdding = false;

                if (find_size >= 0 && room.Size != find_size)
                    isAdding = false;

                if (isAdding)
                    roomsList.Add(room);
            }
            DataGrid_rooms.ItemsSource = roomsList;
        }
        
        // Обновляем списко доступных комнат
        public void updateDataGridRooms()
        {
            db = new ApplicationContext();

            roomTypesList = init_RoomTypes();
            roomsList = init_Rooms();

            DataGrid_rooms.ItemsSource = roomsList;

            findRoomsByFilter();
        }

        // найти свободные комнаты в соответствии с заданным фильтром
        private void Button_findFreeNumber_Click(object sender, RoutedEventArgs e)
        {
            findRoomsByFilter();
        }

        private void ComboBox_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_roomType.SelectedItem is string && (string)ComboBox_roomType.SelectedItem == this.emptyComboBoxItem)
                ComboBox_roomType.SelectedIndex = -1;
            findRoomsByFilter();
        }

        private void ComboBox_size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox_size.SelectedItem is string && (string)Combobox_size.SelectedItem == this.emptyComboBoxItem)
                Combobox_size.SelectedIndex = -1;
            findRoomsByFilter();
        }

        private void Button_calendarReset_Click(object sender, RoutedEventArgs e)
        {
            Calendar_.SelectedDates.Clear();
            findRoomsByFilter();
        }

        private void Button_resetRoomsFilter_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_roomType.SelectedIndex = -1;
            Combobox_size.SelectedIndex = -1;
            TextBox_priceFrom.Text = "";
            TextBox_priceTo.Text = "";
            Calendar_.SelectedDates.Clear();
            findRoomsByFilter();
        }

        private void Button_Price_Click(object sender, RoutedEventArgs e)
        {
            TextBox_priceFrom.Text = "";
            TextBox_priceTo.Text = "";
            findRoomsByFilter();
        }

        private void DataGrid_rooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Entity.Rooms selectedRoom = (Entity.Rooms)DataGrid_rooms.SelectedItem;
            MessageBox.Show("double click #" + selectedRoom.Price);
            Label_reservedTitle.Content = "Комната №" + selectedRoom.Number + "\tтип: " + selectedRoom.Type;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Clients
        private void Button_resetClientFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_findClient_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
