using Microsoft.EntityFrameworkCore.Internal;
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
        List<Entity.Clients> clientsList;

        string emptyComboBoxItem = "--\\--";

        public Form_createOrder()
        {
            InitializeComponent();

            db = new ApplicationContext();


            roomTypesList = init_RoomTypes();
            roomsList = init_Rooms();
            clientsList = init_Clients();

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

            updateDataGridClients();
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
        public List<Entity.Clients> init_Clients()
        {
            // создаем репозиторий комнат, для работы с бд
            db.Clients.Load();
            EFGenericRepository<Entity.Clients> clientsRepo = new EFGenericRepository<Entity.Clients>(db);
            List<Entity.Clients> tmpList = (List<Entity.Clients>)clientsRepo.Get();
            return tmpList;
        }

        // Фильтруем доступные комнаты по фильтру
        private void findRoomsByFilter()
        {
            RoomTypes findRoomType = ComboBox_roomType.SelectedIndex > -1 ? (RoomTypes)ComboBox_roomType.SelectedItem : null;

            int find_priceFrom = TextBox_priceFrom.Text == "" ? -1 : Int32.Parse(TextBox_priceFrom.Text);
            int find_priceTo = TextBox_priceTo.Text == "" ? Int32.MaxValue : Int32.Parse(TextBox_priceTo.Text);
            int find_size = Combobox_size.SelectedIndex > -1 ? (int)Combobox_size.SelectedItem : -1;


            roomsList = new List<Entity.Rooms> { };

            List<Entity.Rooms> tmp_roomsList = init_Rooms();
            tmp_roomsList = init_Rooms();

            foreach (Entity.Rooms room in tmp_roomsList)
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

        private void TextBox_priceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRoomsByFilter();
        }
        private void TextBox_priceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRoomsByFilter();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Clients
        private void Button_resetClientFilter_Click(object sender, RoutedEventArgs e)
        {
            TextBox_passport.Text = "";
            TextBox_firstName.Text = "";
            TextBox_secondName.Text = "";
            TextBox_patronymic.Text = "";
            TextBox_phone.Text = "";
            findClientsByFilter();
        }


        private void findClientsByFilter()
        {
            string findPassport = TextBox_passport.Text;
            string findFirstName = TextBox_firstName.Text;
            string findSecondName = TextBox_secondName.Text;
            string findPatronymic = TextBox_patronymic.Text;
            string findPhone = TextBox_phone.Text;

            db.Clients.Load();

            clientsList = new List<Clients> { };
            foreach (Clients client in db.Clients)
            {
                bool isAdding = true;

                if (findPassport.Length != 0 && client.Passport.ToLower().IndexOf(findPassport.ToLower()) == -1)
                    isAdding = false;

                if (findFirstName.Length != 0 && client.FirstName.ToLower().IndexOf(findFirstName.ToLower()) == -1)
                    isAdding = false;

                if (findSecondName.Length != 0 && client.SecondName.ToLower().IndexOf(findSecondName.ToLower()) == -1)
                    isAdding = false;

                if (findPatronymic.Length != 0 && client.Patronymic.ToLower().IndexOf(findPatronymic.ToLower()) == -1)
                    isAdding = false;

                if (findPhone.Length != 0 && client.Phone.ToLower().IndexOf(findPhone.ToLower()) == -1)
                    isAdding = false;

                if (isAdding)
                    clientsList.Add(client);
            }
            DataGrid_clients.ItemsSource = clientsList;

            // проверяем, чтобы пользователя с таким паспортом не было, т.к. паспорт должен быть уникален
            Clients tmp_client = db.Clients.Where(c => c.Passport.Contains(findPassport) ).FirstOr(null);
            if (tmp_client != null && clientsList.Count == 0)
            {
                Label_statusClients.Content = "Клиент с таким паспортом, но c другими данными, уже существуе";
            }
            else
            {
                Label_statusClients.Content = "Клиент будет добавлен в базу";
            }
            
        }
        public void updateDataGridClients()
        {
            db = new ApplicationContext();

            clientsList = init_Clients();

            DataGrid_clients.ItemsSource = clientsList;

            findClientsByFilter();
        }

        private void TextBox_passport_TextChanged(object sender, TextChangedEventArgs e)
        {
            findClientsByFilter();
        }

        private void TextBox_firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findClientsByFilter();
        }

        private void TextBox_secondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findClientsByFilter();
        }

        private void TextBox_patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            findClientsByFilter();
        }

        private void TextBox_phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            findClientsByFilter();
        }

        private void DataGrid_clients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clients selectedClient = (Clients)DataGrid_clients.SelectedItem;
            MessageBox.Show("double click #" + selectedClient.Passport);

            Label_statusClients.Content = "Клиент выбран из базы";

            TextBox_passport.Text = selectedClient.Passport;
            TextBox_firstName.Text = selectedClient.FirstName;
            TextBox_secondName.Text = selectedClient.SecondName;
            TextBox_patronymic.Text = selectedClient.Patronymic;
            TextBox_phone.Text = selectedClient.Phone;
        }
    }
}
