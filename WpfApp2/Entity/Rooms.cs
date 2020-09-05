using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2.Entity
{
    public class Rooms : INotifyPropertyChanged
    {
        private static int MAX_ROOM_SIZE = 6;

        public int Id { get; set; }

        private RoomTypes type;

        private int size;

        private int  price;

        public Rooms(RoomTypes type, int size, int price)
        {
            this.type = type;
            this.size = size;
            this.price = price;
        }

        public Rooms()
        {
        }

        public RoomTypes Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public static void initComboboxSize(ComboBox c)
        {
            for(int i = 1; i < MAX_ROOM_SIZE; i++)
            {
                c.Items.Add(i);
            }
        }
    }
}
