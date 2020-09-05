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

namespace WpfApp2.Forms.Rooms
{
    /// <summary>
    /// Логика взаимодействия для Form_createRoom.xaml
    /// </summary>
    public partial class Form_createRoom : Window
    {
        ApplicationContext db;
        List<Entity.Rooms> roomsList;
        List<Entity.RoomTypes> roomTypesList;

        public Form_createRoom()
        {
            InitializeComponent();

            db = new ApplicationContext();

            // создаем репозиторий комнат, для работы с бд
            db.Rooms.Load();
            EFGenericRepository<Entity.Rooms> roomsRepo = new EFGenericRepository<Entity.Rooms>(db);
            roomsList = (List<Entity.Rooms>)roomsRepo.Get();

            // создаем репозиторий типов комнат, для работы с бд
            db.RoomTypes.Load();
            EFGenericRepository<Entity.RoomTypes> roomTypesRepo = new EFGenericRepository<Entity.RoomTypes>(db);
            roomTypesList = (List<Entity.RoomTypes>)roomTypesRepo.Get();

            // инициализируем комбобокс с выбором типа комнаты
            foreach( RoomTypes type in roomTypesList)
            {
                Combobox_roomType.Items.Add(type);
            }

            // Инициализируем комбобокс выбора размера комнаты значениями
            Entity.Rooms.initComboboxSize(Combobox_size);

        }

        private void Button_createRoom_click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
