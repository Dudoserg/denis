using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2.Utils
{
    class Helper
    {
        // проверка введенных символов в текстбокс на ЦИФРУ, если не цифра, игнорируем
        public static string removeSymbolIfNotNumber(TextBox textBox, string oldValue, TextChangedEventArgs e)
        {
            string value = textBox.Text;
            if (value.Length == 0)
                return value;
            int num;
            bool isNumeric = int.TryParse(value, out num);
            if (!isNumeric)
            {
                value = oldValue;
            }

            return value;
        }
    }
}