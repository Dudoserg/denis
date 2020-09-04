using System;
using System.Collections.Generic;
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
using System.Data.Entity;
using WpfApp2.Entity;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Form_createClient.xaml
    /// </summary>
    public partial class Form_createClient : Window
    {
        ApplicationContext db;
        public Form_createClient()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Clients.Load();

            //List<Clients> clientsList = new List<Clients> { };

            //foreach (Clients client in db.Clients)
            //{
            //    clientsList.Add(client);
            //}
        }

        private void Button_createClient_click(object sender, RoutedEventArgs e)
        {
            string passport = TextBox_passport.Text;
            string firstName = TextBox_firstName.Text;
            string secondName = TextBox_secondName.Text;
            string patronymic = TextBox_patronymic.Text;
            string phone = TextBox_phone.Text;

            Clients client = new Clients(passport, firstName, secondName, patronymic, phone);
            db.Clients.Add(client);
            db.SaveChanges();

            this.Close();
        }
    }
}
