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
    /// <summary>
    /// Сущность базы данных - комнаты
    /// </summary>
    public class Rooms : INotifyPropertyChanged
    {
        // максимально возможная вместимост номеров
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

        // первичный ключ, идентификатор комнат
        public int Id { get; set; }

        // внешний ключ на тип комнаты
        public int? RoomTypesId { get; set; }      // внешний ключ
        public RoomTypes RoomTypes { get; set; }
        // Вместомость комнаты
        public int Size { get; set; }
        // Цена за сутки
        public int Price { get; set; }
        // Номер комнаты
        public int Number { get; set; }

        // Инициализируем комнату(вручную подставляем тип комнаты, который так же достали из бд)
        public void customInit(DbSet<RoomTypes> dbSet)
        {
            // достаем соответствующей данной комнате Тип комнаты
            RoomTypes tmp_roomType = dbSet.Where(c => c.Id == this.RoomTypesId).ToList()[0];
            // устанавливаем его
            this.RoomTypes = tmp_roomType;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        // инициализируем комбобокс с выбором вместимости комнат
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
