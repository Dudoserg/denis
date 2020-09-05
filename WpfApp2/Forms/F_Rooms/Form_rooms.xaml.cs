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
using WpfApp2.Forms.Rooms;
using WpfApp2.Repos;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Form_rooms.xaml
    /// </summary>
    public partial class Form_rooms : Window
    {
        ApplicationContext db;

        List<RoomTypes> roomTypesList;
        public Form_rooms()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.RoomTypes.Load();

            EFGenericRepository<RoomTypes> roomTypesRepo = new EFGenericRepository<RoomTypes>(db);

            roomTypesList = (List<RoomTypes>)roomTypesRepo.Get();

    
            foreach (RoomTypes roomType in roomTypesList)
            {
                ComboBox_roomType.Items.Add(roomType);
            }
        }

        private void Button_createRoom_click(object sender, RoutedEventArgs e)
        {
            Form_createRoom form = new Form_createRoom();

            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_findRoom_click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_resetFilter_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
