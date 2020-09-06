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
using WpfApp2.Repos;

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

        public int? RoomTypesId { get; set; }      // внешний ключ
        public RoomTypes RoomTypes { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }

        public void customInit(DbSet<RoomTypes> dbSet)
        {
            RoomTypes tmp_roomType = dbSet.Where(c => c.Id == this.RoomTypesId).ToList()[0];
            this.RoomTypes = tmp_roomType;
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
      
        public static List<Entity.Rooms> init_Rooms(DbContext db)
        {
            // создаем репозиторий комнат, для работы с бд
            db.Set<Rooms>().Load();
            EFGenericRepository<Entity.Rooms> roomsRepo = new EFGenericRepository<Entity.Rooms>(db);
            List<Entity.Rooms> tmpList = (List<Entity.Rooms>) roomsRepo.Get();
            // Т.к. EF не хочет устанавливать внешние связи, придется делать связи вручную
            foreach (Entity.Rooms room in tmpList)
            {
                room.customInit(db.Set<RoomTypes>());
            }

            return tmpList;
        }
    }
}
