using System;
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
        // Контекст для работы с базой данных
        private ApplicationContext db;

        // Список записей о комнатах
        private List<Entity.Rooms> roomsList;
        // Список записей о типах комнатах
        private List<Entity.RoomTypes> roomTypesList;
        // Список записей о клиентах
        private List<Entity.Clients> clientsList;
        // Список записей о заказах
        private List<Entity.Order_entity> ordersList;
        // Список записей о заказах, для отображения в таблице
        private List<Entity.Order_entity> ordersList_table;
        
        // пустой объект строка для сброса значения ComboBox в фильтре
        private string emptyComboBoxItem = "--\\--";
        
        // Текущее значение фильтра по номерам комнат
        private string text_number = "";
        // Текущее значение фильтра по цене ОТ
        private string text_priceFrom = "";
        // Текущее значение фильтра по цене ДО
        private string text_priceTo = "";
        
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                // Инициализируем комбобокс выбора вместимости
                ComboBox_Size.Items.Add(emptyComboBoxItem);          // добавляем пустой элемент для сброса значения
                Entity.Rooms.initComboboxSize(ComboBox_Size);        // инициализируем значение комбобокса

                updateDataGrid();
            }
            catch (Exception ex)
            {
                // Логирование ошибок
                Console.WriteLine(ex.Message);
                using (StreamWriter writetext = new StreamWriter("_logs.txt"))
                {
                    writetext.WriteLine(ex.Message);
                    writetext.WriteLine("\n");
                    writetext.WriteLine(ex.ToString());
                }
            }
            
        }

        // Обновляем значение таблицы
        public void updateDataGrid()
        {
            // создаем контакст базы
            db = new ApplicationContext();
            // получаем с базы запии таблиц
            roomTypesList =  RoomTypes.init_RoomTypes(db);
            roomsList = Entity.Rooms.init_Rooms(db);
            clientsList = Clients.init_Clients(db);
            ordersList = Order_entity.init_Orders(db);

            // создаем список записей, отображаемых в таблице
            ordersList_table = new List<Order_entity>();
            foreach (var order in ordersList)
            {
                ordersList_table.Add(order);
            }
            
            DateTime now = DateTime.Now;

            ObservableCollection<Order_entity> items = new ObservableCollection<Order_entity>(ordersList) { };
            
            DataGrid_orders.ItemsSource = ordersList_table;

            findRowsByFilter();
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
            // Создаем форму
            Form_clients form = new Form_clients();
            // устанавливаем владельца формы( какая форма запустила дочернюю)
            form.Owner = this;    // устанавливаем текущую форму
            // при закрытии формы выполняем обновление таблицы
            form.Closed += new EventHandler((o, args) => updateDataGrid());
            // показываем форму по работе с клиентами
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
                // достаем имя автора из файла, из первой строки
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

        // кнопка редактирование записи
        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {
            // получаем запись которую собираемся редактировать
            Order_entity order = ((FrameworkElement)sender).DataContext as Order_entity;
            Console.WriteLine("clicked edit row  id = " + order.Id);

            // открываем окно по редактированию записи
            Form_createOrder form = new Form_createOrder(order);
            form.Owner = this;
            form.Closed += new EventHandler((o, args) => updateDataGrid());
            form.ShowDialog();
        }

        // кнопка удалить запись
        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {
            // получаем запись, которую собираемся удалить
            Order_entity order = ((FrameworkElement)sender).DataContext as Order_entity;
            // поулчаем подтверждение на удаление
            MessageBoxResult messageBoxResult = 
                System.Windows.MessageBox.Show(
                    "Вы действительно хотите удалить заказ №" + order.Id,
                    "Delete Confirmation",
                    System.Windows.MessageBoxButton.YesNo
                );
            
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Console.WriteLine("clicked delete row id = " + order.Id);

                // Проверяем, нет ли зависимых от это строки записей
                using (var context = new ApplicationContext())
                {
                    // получаем запись по первичному ключу, которую собираемся удалить
                    var deletedRoom = context.Orders.Where(c => c.Id == order.Id).FirstOrDefault();
                    // удаляем запись
                    context.Orders.Remove(deletedRoom);
                    // применяем изменения в базе
                    context.SaveChanges();
                }
                // применяем изменения в бд                    
                db.SaveChanges();
                // обновляем таблицу с заказами
                this.updateDataGrid();
            }
        }
        // переход на главную страницу с заказами
        private void MenuItem_mainPage(object sender, RoutedEventArgs e)
        {
            updateDataGrid();
        }
        // сбрасываем фильтр поиска
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
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // Дата заселения ДО
        private void DatePicker_StartTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // Дата выезда ОТ
        private void DatePicker_EndFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }
        // Дата выезда ДО
        private void DatePicker_EndTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }


        // Изменился текст в поле "ЦЕНА ОТ" фильтра 
        private void TextBox_PriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            // выполняем игнорирование введенного символа, Если это не цифра
            TextBox_PriceFrom.Text = text_priceFrom = Helper.removeSymbolIfNotNumber(TextBox_PriceFrom, text_priceFrom, e);
            // Переставляем курсор за последний символ
            TextBox_PriceFrom.CaretIndex = TextBox_PriceFrom.Text.Length;
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // Изменился текст в поле "ЦЕНА ДО" фильтра 
        private void TextBox_PriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            // выполняем игнорирование введенного символа, Если это не цифра
            TextBox_PriceTo.Text = text_priceTo = Helper.removeSymbolIfNotNumber(TextBox_PriceTo, text_priceTo, e);
            // Переставляем курсор за последний символ
            TextBox_PriceTo.CaretIndex = TextBox_PriceTo.Text.Length;
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // Изменилось значение фильтра в комбобоксе по выбору вместительности комнаты
        private void ComboBox_Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Если выбрали пустое значение, то сбрасываем выбор в этом комбобоксе
            if (ComboBox_Size.SelectedItem is string && (string)ComboBox_Size.SelectedItem == this.emptyComboBoxItem)
                ComboBox_Size.SelectedIndex = -1;
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // Изменился текст в поле "НОМЕРА" фильтра 
        private void TextBox_Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            // выполняем игнорирование введенного символа, Если это не цифра
            TextBox_Number.Text = text_number = Helper.removeSymbolIfNotNumber(TextBox_Number, text_number, e);
            // Переставляем курсор за последний символ
            TextBox_Number.CaretIndex = TextBox_Number.Text.Length;
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        public  void findRowsByFilter()
        {
            // Получаем введенные поля фильтра
            // Проверяем, Если в текстбоксе есть введенная строка, то кастим ее в цифру, иначе присваиваем -1
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
            
            // записи базы, которые будут соответствовать фильтру
            ordersList_table  = new List<Order_entity> { };

            // перебираем все записи ЗАКАЗЫ базы
            foreach (var order in ordersList)
            {
                bool isAdding = true;    // флаг, добавлять ли запись
                
                // выполняем проверку соответсвтия всех полей фильтра рассматирваемой записи
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

                // Если запись соответствует фильтру, добавляем ее в список
                if (isAdding)
                    ordersList_table.Add(order);
            }
            // обновляем таблицу
            DataGrid_orders.ItemsSource = ordersList_table;
        }

        private void TextBox_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        private void TextBox_SecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        private void TextBox_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            findRowsByFilter();     // ищем записи в соответствии с заданным фильтром
        }

        // сбрасываем выбранные даты фильтре
        private void Button_resetDateFilter_Click(object sender, RoutedEventArgs e)
        {
            DatePicker_StartFrom.SelectedDate = null;
            DatePicker_StartTo.SelectedDate = null;
            DatePicker_EndFrom.SelectedDate = null;
            DatePicker_EndTo.SelectedDate = null;
        }
    }
}
