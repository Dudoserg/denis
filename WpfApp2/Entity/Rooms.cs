using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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

        public Rooms( int size, int price)
        {
            //this.type = type;
            this.Size = size;
            this.Price = price;
        }

        public Rooms()
        {
        }

        public int Id { get; set; }

        public int? TypeId { get; set; }      // внешний ключ
        public RoomTypes Type { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }

        public void customInit(DbSet<RoomTypes> dbSet)
        {
            RoomTypes tmp_roomType = dbSet.Where(c => c.Id == this.TypeId).ToList()[0];
            this.Type = tmp_roomType;
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
