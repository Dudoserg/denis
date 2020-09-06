using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Repos;

namespace WpfApp2.Entity
{
    [Table("Orders")]

    public class Order_entity: INotifyPropertyChanged
    {
        public Order_entity()
        {
        }

        public int Id { get; set; }

        public int? ClientsId { get; set; }      // внешний ключ
        [NotMapped]
        public Clients Clients { get; set; }
        public int RoomsId { get; set; }      // внешний ключ
        [NotMapped]
        public Rooms Rooms { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        // Вычисляемое свойство для таблицы
        [NotMapped]
        public string FIO
        {
            get{
                String s_1 = this.Clients != null ? this.Clients.FirstName : "";
                String s_2 = this.Clients != null ? this.Clients.SecondName : "";
                return s_1 + " " + s_2; 
            }
        }

        // Вычисляемое свойство для таблицы
        [NotMapped]
        public string Coast
        {
            get
            {
                int price = 0;
                if (this.Rooms == null)
                    return "-";
                else
                    price = this.Rooms.Price;

                int countDays = (int)(DateEnd - DateStart).TotalDays + 1;

                return (price * countDays).ToString();
            }
        }
        // Вычисляемое свойство для таблицы
        [NotMapped]
        public string DateStartStr
        {
            get { return DateStart.ToString("dd/MM/yyyy"); }
        }
        // Вычисляемое свойство для таблицы
        [NotMapped]
        public string DateEndStr
        {
            get { return DateEnd.ToString("dd/MM/yyyy"); }
        }

        
        //public void customInit(DbSet<RoomTypes> dbSet)
        //{
        //    RoomTypes tmp_roomType = dbSet.Where(c => c.Id == this.RoomTypesId).ToList()[0];
        //    this.RoomTypes = tmp_roomType;
        //}


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        
        // Вручную устанавливаем зависисые сущности
        public void customInit(DbSet<Clients> dbClients, DbSet<Rooms> dbRooms)
        {
           Clients tmp_client = dbClients.Where(c => c.Id == this.ClientsId).ToList()[0];
           this.Clients = tmp_client;

           Entity.Rooms tmp_room = dbRooms.Where(r => r.Id == this.RoomsId).ToList()[0];
           this.Rooms = tmp_room;
        }
        
        
        // инициализируем таблицу Заказы (вручную т.к. EF не хочет подтягивать зависимые сущности)
        public static List<Order_entity> init_Orders(DbContext db)
        {
            // создаем репозиторий комнат, для работы с бд
            db.Set<Order_entity>().Load();
            EFGenericRepository<Order_entity> orderRepo = new EFGenericRepository<Order_entity>(db);
            List<Order_entity> tmpList = (List<Order_entity>) orderRepo.Get();
            foreach (Order_entity order in tmpList)
            {
                order.customInit(db.Set<Clients>(), db.Set<Rooms>());
            }
            return tmpList;
        }
        
    }
}
