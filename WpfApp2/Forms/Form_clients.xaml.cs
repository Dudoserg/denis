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

namespace WpfApp2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Form_clients.xaml
    /// </summary>
    public partial class Form_clients : Window
    {
        public Form_clients()
        {
            InitializeComponent();
        }

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void Button_(object sender, RoutedEventArgs e)
        {

        }

        private void Button_createClient_click(object sender, RoutedEventArgs e)
        {
            Form_createClient form = new Form_createClient();

            form.Owner = this;
            form.ShowDialog();
        }

        private void Button_findClient_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
