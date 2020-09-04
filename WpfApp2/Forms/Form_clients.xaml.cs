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

namespace WpfApp2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Form_clients.xaml
    /// </summary>
    public partial class Form_clients : Window
    {
        ApplicationContext db;
        List<Clients> clientsList;
        public Form_clients()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Clients.Load();

            clientsList = new List<Clients> { };

            foreach (Clients client in db.Clients)
            {
                clientsList.Add(client);
            }
            DataGrid_clients.ItemsSource = clientsList;

            DataGrid_clients.CellEditEnding += myDG_CellEditEnding;

        }

        private void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
               
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    switch (bindingPath)
                    {
                        case "Passport":
                            {
                                int rowIndex = e.Row.GetIndex();
                                var el = e.EditingElement as TextBox;
                                Console.WriteLine("Passport rowIndex = " + rowIndex + "\t" + "el = " + el.Text);
                                break;
                            }
                        case "FirstName":
                            {
                                int rowIndex = e.Row.GetIndex();
                                var el = e.EditingElement as TextBox;
                                Console.WriteLine("FirstName rowIndex = " + rowIndex + "\t" + "el = " + el.Text);
                                break;
                            }
                        case "SecondName":
                            {
                                int rowIndex = e.Row.GetIndex();
                                TextBox el = e.EditingElement as TextBox;
                                Console.WriteLine("SecondName rowIndex = " + rowIndex + "\t" + "el = " + el.Text);
                                break;
                            }
                        case "Patronymic":
                            {
                                int rowIndex = e.Row.GetIndex();
                                var el = e.EditingElement as TextBox;
                                Console.WriteLine("Patronymic rowIndex = " + rowIndex + "\t" + "el = " + el.Text);
                                break;
                            }
                        case "Phone":
                            {
                                int rowIndex = e.Row.GetIndex();
                                var el = e.EditingElement as TextBox;
                                Console.WriteLine("Phone rowIndex = " + rowIndex + "\t" + "el = " + el.Text);
                                break;
                            }
                    }



                    if (bindingPath == "Col2")
                    {
                        int rowIndex = e.Row.GetIndex();
                        var el = e.EditingElement as TextBox;
                        // rowIndex has the row index
                        // bindingPath has the column's binding
                        // el.Text has the new, user-entered value
                    }
                }
            }
        }
        

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void Button_(object sender, RoutedEventArgs e)
        {

        }

        private void Button_createClient_click(object sender, RoutedEventArgs e)
        {
            Form_createClient form = new Form_createClient(null);

            form.Owner = this;
            form.ShowDialog();
        }

        // обновляем таблицу 
        public void updateAfterAdd()
        {
            db.Clients.Load();

            clientsList = new List<Clients> { };

            foreach (Clients client in db.Clients)
            {
                clientsList.Add(client);
            }
            DataGrid_clients.ItemsSource = clientsList;
        }

        private void Button_findClient_click(object sender, RoutedEventArgs e)
        {
            string findPassport = TextBox_findPassport.Text;
            string findFirstName = TextBox_findFirstName.Text;
            string findSecondName = TextBox_findSecondName.Text;
            string findPatronymic = TextBox_findPatronymic.Text;
            string findPhone = TextBox_findPhone.Text;

            db.Clients.Load();

            clientsList = new List<Clients> { };
            foreach (Clients client in db.Clients)
            {
                bool isAdding = true;

                if(findPassport.Length != 0 && client.Passport.IndexOf(findPassport) == -1)
                    isAdding = false;

                if (findFirstName.Length != 0 && client.FirstName.IndexOf(findFirstName) == -1)
                    isAdding = false;

                if (findSecondName.Length != 0 && client.SecondName.IndexOf(findSecondName) == -1)
                    isAdding = false;

                if (findPatronymic.Length != 0 && client.Patronymic.IndexOf(findPatronymic) == -1)
                    isAdding = false;

                if (findPhone.Length != 0 && client.Phone.IndexOf(findPhone) == -1)
                    isAdding = false;

                if(isAdding)
                    clientsList.Add(client);
            }
            DataGrid_clients.ItemsSource = clientsList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("clientsList:");
            foreach (Clients client in clientsList)
            {
                Console.WriteLine(client.Passport + "\t" + client.FirstName + "\t" + client.SecondName + "\t" + client.Patronymic);
            }
            Console.WriteLine("");

            Console.WriteLine("db.Clients:");
            foreach (Clients client in db.Clients)
            {
                Console.WriteLine(client.Passport + "\t" + client.FirstName + "\t" + client.SecondName + "\t" + client.Patronymic);
            }
            Console.WriteLine("");

            db.SaveChanges();
        }

        private void Button_editRow_click(object sender, RoutedEventArgs e)
        {
            Clients client = ((FrameworkElement)sender).DataContext as Clients;
            Console.WriteLine("clicked row  id = " + client.Id);

            Form_createClient form = new Form_createClient(client);
            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_delRow_click(object sender, RoutedEventArgs e)
        {
            Clients client = ((FrameworkElement)sender).DataContext as Clients;
            Console.WriteLine("clicked row id = " + client.Id);
        }

        private void Button_resetFilter_click(object sender, RoutedEventArgs e)
        {
            TextBox_findPassport.Text = "";
            TextBox_findFirstName.Text = "";
            TextBox_findSecondName.Text = "";
            TextBox_findPatronymic.Text = "";
            TextBox_findPhone.Text = "";

            
            ButtonAutomationPeer peer = new ButtonAutomationPeer(Button_findClient);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();

        }
    }
}
