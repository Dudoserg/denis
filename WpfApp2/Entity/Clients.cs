﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Repos;

namespace WpfApp2.Entity
{
    public class Clients : INotifyPropertyChanged
    {
        
        public int Id { get; set; }

        private string passport;

        private string firstName;

        private string secondName;

        private string patronymic;

        private string phone;

        public Clients(string passport, string firstName, string secondName, string patronymic, string phone)
        {
            this.passport = passport;
            this.firstName = firstName;
            this.secondName = secondName;
            this.patronymic = patronymic;
            this.phone = phone;
        }
        public Clients()
        {

        }

        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        } 
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        
        public static List<Entity.Clients> init_Clients(DbContext db)
        {
            // создаем репозиторий комнат, для работы с бд
            db.Set<Clients>().Load();
            EFGenericRepository<Entity.Clients> clientsRepo = new EFGenericRepository<Entity.Clients>(db);
            List<Entity.Clients> tmpList = (List<Entity.Clients>) clientsRepo.Get();
            return tmpList;
        }



        public static bool verificate_Passport( string passport)
        {
            if (passport.Length != 10)
            {
                MessageBox.Show("длина номера паспорта должна быть 10 символов!");
                return false;
            }
            return true;
        } 
        public static bool verificate_SecondName( string secondName)
        {
            if (secondName.Length < 2)
            {
                MessageBox.Show("Укажите Фамилию");
                return false;
            }
            return true;
        }
        public static bool verificate_FirstName( string firstName)
        {
            if (firstName.Length < 2)
            {
                MessageBox.Show("Укажите Имя");
                return false;
            }
            return true;
        } 
        public static bool verificate_Phone( string phone)
        {
            if (phone.Length < 3)
            {
                MessageBox.Show("Укажите номер телефона( не менее 3 символов )");
                return false;
            }
            return true;
        }
    }
}
