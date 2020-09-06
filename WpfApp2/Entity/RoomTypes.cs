using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Repos;

namespace WpfApp2.Entity
{
    public class RoomTypes : INotifyPropertyChanged
    {

        public int Id { get; set; }

        private string type;

        public List<Rooms> Rooms { get; set; }


        public RoomTypes(string type)
        {
            this.type = type;
        }

        public RoomTypes()
        {
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }


        public override string ToString()
        {
            string result = this.type.ToLower();

            if (String.IsNullOrEmpty(result))    
                return result;
            else   
                return result.First().ToString().ToUpper() + result.Substring(1);
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        public static List<RoomTypes> init_RoomTypes(DbContext db)
        {
            // создаем репозиторий типов комнат, для работы с бд
            db.Set<RoomTypes>().Load();
            EFGenericRepository<Entity.RoomTypes> roomTypesRepo = new EFGenericRepository<Entity.RoomTypes>(db);
            return (List<Entity.RoomTypes>) roomTypesRepo.Get();
        }
    }
}