using System;
using System.Collections.Generic;
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
using System.Linq;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Form_createClient.xaml
    /// </summary>
    public partial class Form_createClient : Window
    {
        ApplicationContext db;
        Clients client;
        public Form_createClient(Clients client)
        {
            this.client = client;
            InitializeComponent();

            // создаем контекст базы данных
            db = new ApplicationContext();
            // подгружаемм данные
            db.Clients.Load();

            //List<Clients> clientsList = new List<Clients> { };

            //foreach (Clients client in db.Clients)
            //{
            //    clientsList.Add(client);
            //}

            if(client != null)
            {
                Button_createClient.Content = "Изменить";
                this.Title = "Изменение информации о клиенте";
                TextBox_passport.Text = client.Passport;
                TextBox_firstName.Text = client.FirstName;
                TextBox_secondName.Text = client.SecondName;
                TextBox_patronymic.Text = client.Patronymic;
                TextBox_phone.Text = client.Phone;
            }
        }

        private void Button_createClient_click(object sender, RoutedEventArgs e)
        {
            // создаем нового пользователя
            if (this.client == null)
            {
                string passport = TextBox_passport.Text;

                List<Clients> phones = db.Clients.Where(c => c.Passport == passport).ToList();
                
                if (phones.Count != 0)
                {
                    MessageBox.Show("Клиент с пасспортом '" + passport + "' уже существует");
                    return;
                }

                if (!Clients.verificate_Passport(passport))
                    return;

                string firstName = TextBox_firstName.Text;
                if (!Clients.verificate_FirstName(firstName))
                    return ;

                string secondName = TextBox_secondName.Text;
                if (!Clients.verificate_SecondName(secondName))
                    return ;

                string patronymic = TextBox_patronymic.Text;

                string phone = TextBox_phone.Text;
                if (!Clients.verificate_Phone(phone))
                    return;


                Clients client = new Clients(passport, firstName, secondName, patronymic, phone);
                db.Clients.Add(client);
                db.SaveChanges();

                ((Form_clients)this.Owner).updateDataGrid();

                this.Close();
            }
            else
            {
                // редактируем существующего

                // верификация данных
                string passport = TextBox_passport.Text;

                // получаем из базы клиентов, у которых паспорт равен введенному в форме
                List<Clients> clients = db.Clients.Where(c => c.Passport == passport).ToList();
                // Если такие клиенты есть ( не считая текущего переданного ), значит ошибка
                if (clients.Count != 0 && client.Passport != passport)
                {
                    MessageBox.Show("Клиент с пасспортом '" + passport + "' уже существует");
                    return;
                }
                if(!Clients.verificate_Passport(passport))
                    return;

                string firstName = TextBox_firstName.Text;
                if (!Clients.verificate_FirstName(firstName))
                    return;

                string secondName = TextBox_secondName.Text;
                if (!Clients.verificate_SecondName(secondName))
                    return;

                string patronymic = TextBox_patronymic.Text;

                string phone = TextBox_phone.Text;
                if (!Clients.verificate_Phone(phone))
                    return;

                client.Passport = passport;
                client.FirstName = firstName;
                client.SecondName = secondName;
                client.Patronymic = patronymic;
                client.Phone = phone;

                // обновление записи в бд
                using (var db = new ApplicationContext())
                {
                    db.Clients.Attach(client);

                    db.Entry(client).Property(x => x.Passport).IsModified = true;
                    db.Entry(client).Property(x => x.FirstName).IsModified = true;
                    db.Entry(client).Property(x => x.SecondName).IsModified = true;
                    db.Entry(client).Property(x => x.Patronymic).IsModified = true;
                    db.Entry(client).Property(x => x.Phone).IsModified = true;

                    db.SaveChanges();
                }
                // обновляем таблицу на родительской форме
                ((Form_clients)this.Owner).updateDataGrid();

                // закрываем текущую форму
                this.Close();
            }
        }
    }
}
