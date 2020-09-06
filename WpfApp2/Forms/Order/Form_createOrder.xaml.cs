using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
        List<Order_entity> ordersList;

        string emptyComboBoxItem = "--\\--";

        Order_entity order_editing = null;

        public Form_createOrder(Order_entity o)
        {
            this.order_editing = o;

            InitializeComponent();

            db = new ApplicationContext();


            roomTypesList = RoomTypes.init_RoomTypes(db);
            roomsList = Entity.Rooms.init_Rooms(db);
            clientsList = Clients.init_Clients(db);
            ordersList = Order_entity.init_Orders(db);

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

            try
            {
                updateDataGridClients();
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

            DateTime now = DateTime.Now;
            Calendar_.SelectionMode = CalendarSelectionMode.MultipleRange;
            Calendar_.DisplayDateStart = new DateTime(
                now.Year,
                now.Month - 1 < 1 ? 1 : now.Month - 1,
                1);

            if (order_editing != null)
            {
                // edit mod
                this.Title = "Редактирование информации о заказе";
                this.Button_confirmOrder.Content = "Обновить";
                this.selectedRoom = order_editing.Rooms;
                
                // инфа о выбранном пользователе
                TextBox_passport.Text = order_editing.Clients.Passport;
                TextBox_firstName.Text = order_editing.Clients.FirstName;
                TextBox_secondName.Text = order_editing.Clients.SecondName;
                TextBox_patronymic.Text = order_editing.Clients.Patronymic;
                TextBox_phone.Text = order_editing.Clients.Phone;
                
                // достаем заказы с выбранной комнатой
                OrderRepos orderRepos = new OrderRepos(db);
                List<Order_entity> selectedRoomOrderList = orderRepos.GetByRoomId(order_editing.RoomsId);

                // помечаем календарь крестиками
                Calendar_.BlackoutDates.Clear();
                Calendar_.SelectedDates.Clear();
                foreach (Order_entity order in selectedRoomOrderList)
                {
                    if(order.Id != order_editing.Id)
                    {
                        Calendar_.BlackoutDates.Add(
                            new CalendarDateRange(order.DateStart, order.DateEnd)
                        );
                    }
                    
                }
                // помечаем на календаре выбранные дни для текущего заказа
                Calendar_.SelectedDates.Clear();
                for (DateTime date = order_editing.DateStart;
                    order_editing.DateEnd.AddDays(1).CompareTo(date) > 0;
                    date = date.AddDays(1.0))
                {
                    Calendar_.SelectedDates.Add(date);
                }
            }
        }

        // Фильтруем доступные комнаты по фильтру
        private void findRoomsByFilter()
        {
            RoomTypes findRoomType = ComboBox_roomType.SelectedIndex > -1
                ? (RoomTypes) ComboBox_roomType.SelectedItem
                : null;

            int find_priceFrom = TextBox_priceFrom.Text == "" ? -1 : Int32.Parse(TextBox_priceFrom.Text);
            int find_priceTo = TextBox_priceTo.Text == "" ? Int32.MaxValue : Int32.Parse(TextBox_priceTo.Text);
            int find_size = Combobox_size.SelectedIndex > -1 ? (int) Combobox_size.SelectedItem : -1;


            roomsList = new List<Entity.Rooms> { };

            List<Entity.Rooms> tmp_roomsList = Entity.Rooms.init_Rooms(db);
            tmp_roomsList = Entity.Rooms.init_Rooms(db);

            foreach (Entity.Rooms room in tmp_roomsList)
            {
                bool isAdding = true;

                if (findRoomType != null && room.RoomTypesId != findRoomType.Id)
                    isAdding = false;

                if (!(find_priceFrom <= room.Price && room.Price <= find_priceTo))
                    isAdding = false;

                if (find_size >= 0 && room.Size != find_size)
                    isAdding = false;

                if (isAdding)
                    roomsList.Add(room);
            }

            DataGrid_rooms.ItemsSource = roomsList;
            Label_statusRoom.Content = "";
        }

        // Обновляем списко доступных комнат
        public void updateDataGridRooms()
        {
            db = new ApplicationContext();

            roomTypesList = RoomTypes.init_RoomTypes(db);
            roomsList = Entity.Rooms.init_Rooms(db);

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
            if (ComboBox_roomType.SelectedItem is string &&
                (string) ComboBox_roomType.SelectedItem == this.emptyComboBoxItem)
                ComboBox_roomType.SelectedIndex = -1;
            findRoomsByFilter();
        }

        private void ComboBox_size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox_size.SelectedItem is string && (string) Combobox_size.SelectedItem == this.emptyComboBoxItem)
                Combobox_size.SelectedIndex = -1;
            findRoomsByFilter();
        }

        private void Button_calendarReset_Click(object sender, RoutedEventArgs e)
        {
            Calendar_.SelectedDates.Clear();
            Calendar_.BlackoutDates.Clear();
            findRoomsByFilter();
        }

        private void Button_resetRoomsFilter_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_roomType.SelectedIndex = -1;
            Combobox_size.SelectedIndex = -1;
            TextBox_priceFrom.Text = "";
            TextBox_priceTo.Text = "";
            Calendar_.SelectedDates.Clear();
            Calendar_.BlackoutDates.Clear();
            findRoomsByFilter();
        }

        private void Button_Price_Click(object sender, RoutedEventArgs e)
        {
            TextBox_priceFrom.Text = "";
            TextBox_priceTo.Text = "";
            findRoomsByFilter();
        }

        private Entity.Rooms selectedRoom = null;

        private void DataGrid_rooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Entity.Rooms currentRoom = (Entity.Rooms) DataGrid_rooms.SelectedItem;
            this.selectedRoom = currentRoom;
//            MessageBox.Show("double click #" + currentRoom.Price);
            updateLabelStatusRoom();
            UpdateCalendar();
        }

        private void updateLabelStatusRoom()
        {
            if (selectedRoom == null)
            {
                Label_statusRoom.Content = "Комната не выбрана";
                return;
            }

            Label_statusRoom.Content = "Комната №" + selectedRoom.Number + "\tтип: " + selectedRoom.RoomTypes;
        }

        private void UpdateCalendar()
        {
            Entity.Rooms selectedRoom = (Entity.Rooms) DataGrid_rooms.SelectedItem;
            List<Order_entity> ordersList = Order_entity.init_Orders(db);

            // достаем заказы с выбранной комнатой
            OrderRepos orderRepos = new OrderRepos(db);
            List<Order_entity> selectedRoomOrderList = orderRepos.GetByRoomId(selectedRoom.Id);

            Calendar_.BlackoutDates.Clear();
            foreach (Order_entity order in selectedRoomOrderList)
            {
                Calendar_.BlackoutDates.Add(
                    new CalendarDateRange(order.DateStart, order.DateEnd)
                );
            }

            int a = 2;
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


        private bool isNewClient = false;
        private bool isClientWithPassportExist = false;
        private bool isOldClient = false;
        private Clients oldClient = null;

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

            isClientWithPassportExist = false;
            isNewClient = false;
            isOldClient = false;
            // проверяем, чтобы пользователя с таким паспортом не было, т.к. паспорт должен быть уникален
            Clients tmp_clientWithExistingPassport = db.Clients.Where(c => c.Passport == findPassport).FirstOrDefault();
            if (tmp_clientWithExistingPassport != null)
            {
                // Клиент с таким же паспортом уже существует
                // проверяем, другие ли данные о нем сейчас в текстбоксе, если да то сообщаем об этом
                if (!(tmp_clientWithExistingPassport.Passport == TextBox_passport.Text &&
                      tmp_clientWithExistingPassport.FirstName == TextBox_firstName.Text &&
                      tmp_clientWithExistingPassport.SecondName == TextBox_secondName.Text &&
                      tmp_clientWithExistingPassport.Patronymic == TextBox_patronymic.Text &&
                      tmp_clientWithExistingPassport.Phone == TextBox_phone.Text))
                {
                    // паспорт тот же, данные другие
                    Label_statusClients.Content = "Клиент с таким паспортом, но c другими данными, уже существует";
                    isClientWithPassportExist = true;
                    isNewClient = false;
                    isOldClient = false;
                }
                else
                {
                    // паспорт тот же, Данные те же, КЛИЕНТ ИЗ БАЗЫ
                    Label_statusClients.Content = "Клиент выбран из базы";
                    isClientWithPassportExist = false;
                    isNewClient = false;
                    isOldClient = true;
                    oldClient = tmp_clientWithExistingPassport;
                }
            }
            else
            {
                Label_statusClients.Content = "Новый клиент будет добавлен в базу";
                isClientWithPassportExist = false;
                isNewClient = true;
                isOldClient = false;
            }

            // проверяем на полное совпадение
//            Clients selectedItem = (Clients) DataGrid_clients.SelectedItem;
//            if (selectedItem != null &&
//                selectedItem.Passport == TextBox_passport.Text &&
//                selectedItem.FirstName == TextBox_firstName.Text &&
//                selectedItem.SecondName == TextBox_secondName.Text &&
//                selectedItem.Patronymic == TextBox_patronymic.Text &&
//                selectedItem.Phone == TextBox_phone.Text)
//            {
//                Label_statusClients.Content = "Клиент выбран из базы";
//                isClientWithPassportExist = false;
//                isNewClient = false;
//                isOldClient = true;
//            }
        }

        public void updateDataGridClients()
        {
            db = new ApplicationContext();

            clientsList = Clients.init_Clients(db);

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
            Clients selectedClient = (Clients) DataGrid_clients.SelectedItem;
            //MessageBox.Show("double click #" + selectedClient.Passport);

            Label_statusClients.Content = "Клиент выбран из базы";

            TextBox_passport.Text = selectedClient.Passport;
            TextBox_firstName.Text = selectedClient.FirstName;
            TextBox_secondName.Text = selectedClient.SecondName;
            TextBox_patronymic.Text = selectedClient.Patronymic;
            TextBox_phone.Text = selectedClient.Phone;
        }


        private bool verificateClient()
        {
            if (!(isNewClient || isOldClient || isClientWithPassportExist))
            {
                // косяк с выбором клиента
                Label_statusMain.Content = "Ошибка при выборе клиента";
                return false;
            }

            if (isClientWithPassportExist && !isNewClient && !isOldClient)
            {
                Label_statusMain.Content = "Клиент с таким паспортом, но другими данными уже существует";
                return false;
            }

            // Верефицируем пользователя
            string passport = TextBox_passport.Text;
            string firstName = TextBox_firstName.Text;
            string secondName = TextBox_secondName.Text;
            string patronymic = TextBox_patronymic.Text;
            string phone = TextBox_phone.Text;

            if (!Clients.verificate_Passport(passport))
                return false;

            if (!Clients.verificate_FirstName(firstName))
                return false;

            if (!Clients.verificate_SecondName(secondName))
                return false;

            if (!Clients.verificate_Phone(phone))
                return true;

            return true;
        }

        private bool verificateRoom()
        {
            Console.WriteLine("verificateRoom");

            if (this.selectedRoom == null)
            {
                Label_statusMain.Content = "Вы не выбрали номер";
                return false;
            }

            SelectedDatesCollection dates = Calendar_.SelectedDates;
            bool isIntervalVerificate = true;
            if (dates.Count > 1)
            {
                for (int i = 0; i < dates.Count - 1; i++)
                {
                    if ((int) (dates[i + 1] - dates[i]).TotalDays != 1)
                    {
                        isIntervalVerificate = false;
                        break;
                    }
                }
            }
            else if (dates.Count < 1)
            {
                Label_statusMain.Content = "Вы не выбрали дату заселения";
                return false;
            }

            if (!isIntervalVerificate)
            {
                Label_statusMain.Content = "Вы выбрали несколько прерывающихся временных отрезков";
                return false;
            }

            return true;
        }

        private void Button_confirmOrder_Click(object sender, RoutedEventArgs e)
        {
            Label_statusMain.Content = "";
            if (!verificateClient())
                return;

            if (!verificateRoom())
                return;

            Label_statusMain.Content = "Вроде все ок, можно заказывать";

            // Разбираемся с выбранным клиентом
            Clients client = null;
            if (isOldClient)
            {
                client = this.oldClient;
            }
            else
            {
                string findPassport = TextBox_passport.Text;
                string findFirstName = TextBox_firstName.Text;
                string findSecondName = TextBox_secondName.Text;
                string findPatronymic = TextBox_patronymic.Text;
                string findPhone = TextBox_phone.Text;

                client = new Clients();
                client.Passport = findPassport;
                client.FirstName = findFirstName;
                client.SecondName = findSecondName;
                client.Patronymic = findPatronymic;
                client.Phone = findPhone;
                client = db.Clients.Add(client);
                db.SaveChanges();
            }

            if (this.order_editing == null)
            {
                // create mod
                Order_entity order = new Order_entity();
                order.ClientsId = client.Id;
                order.Clients = client;

                order.RoomsId = selectedRoom.Id;
                order.Rooms = selectedRoom;

                SelectedDatesCollection calendarSelectedDates = Calendar_.SelectedDates;
                order.DateStart = calendarSelectedDates[0];
                order.DateEnd = calendarSelectedDates[calendarSelectedDates.Count - 1];

                order = db.Orders.Add(order);
            }
            else
            {
                // edit mod
                order_editing.ClientsId = client.Id;
                order_editing.Clients = client;
                
                order_editing.RoomsId = selectedRoom.Id;
                order_editing.Rooms = selectedRoom;
                
                SelectedDatesCollection calendarSelectedDates = Calendar_.SelectedDates;
                order_editing.DateStart = calendarSelectedDates[0];
                order_editing.DateEnd = calendarSelectedDates[calendarSelectedDates.Count - 1];
                
                // обновление записи в бд
                using (var db = new ApplicationContext())
                {
                    db.Orders.Attach(order_editing);

                    db.Entry(order_editing).Property(x => x.ClientsId).IsModified = true;
                    db.Entry(order_editing).Property(x => x.RoomsId).IsModified = true;
                    db.Entry(order_editing).Property(x => x.DateStart).IsModified = true;
                    db.Entry(order_editing).Property(x => x.DateEnd).IsModified = true;

                    db.SaveChanges();
                }
                // обновляем таблицу на родительской форме
                ((MainWindow)this.Owner).updateDataGrid();
            }
         
            db.SaveChanges();
            // закрываем текущую форму
            this.Close();
        }
    }
}