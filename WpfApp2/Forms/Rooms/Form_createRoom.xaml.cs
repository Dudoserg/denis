﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WpfApp2.Entity;
using WpfApp2.Repos;

namespace WpfApp2.Forms.Rooms
{
    /// <summary>
    /// Логика взаимодействия для Form_createRoom.xaml
    /// </summary>
    public partial class Form_createRoom : Window
    {
        ApplicationContext db;
        List<Entity.Rooms> roomsList;
        List<Entity.RoomTypes> roomTypesList;

        Entity.Rooms editRoom;

        public Form_createRoom(Entity.Rooms r)
        {
            this.editRoom = r;

            InitializeComponent();

            db = new ApplicationContext();


            // создаем репозиторий типов комнат, для работы с бд
            db.RoomTypes.Load();
            EFGenericRepository<Entity.RoomTypes> roomTypesRepo = new EFGenericRepository<Entity.RoomTypes>(db);
            roomTypesList = (List<Entity.RoomTypes>)roomTypesRepo.Get();

            // создаем репозиторий комнат, для работы с бд
            db.Rooms.Load();
            EFGenericRepository<Entity.Rooms> roomsRepo = new EFGenericRepository<Entity.Rooms>(db);
            roomsList = (List<Entity.Rooms>)roomsRepo.Get();

            // инициализируем комбобокс с выбором типа комнаты
            foreach (RoomTypes type in roomTypesList)
            {
                Combobox_roomType.Items.Add(type);
            }

            // Инициализируем комбобокс выбора размера комнаты значениями
            Entity.Rooms.initComboboxSize(Combobox_size);

            if (editRoom != null)
            {
                // change mod
                TextBox_number.Text = editRoom.Number.ToString();
                TextBox_price.Text = editRoom.Price.ToString();
                Combobox_roomType.SelectedItem = roomTypesList.Find(x => x.Id == editRoom.TypeId); ;
                Combobox_size.SelectedItem = editRoom.Size;
                Button_createRoom.Content = "Изменить номер";
                this.Title = "Изменение информации о номере";

            }
            else
            {
                // create mod
                Button_createRoom.Content = "Добавить номер";
            }
        }

        private void Button_createRoom_click(object sender, RoutedEventArgs e)
        {
            // верификация данных
            // Price
            int price = TextBox_price.Text != "" ? Int32.Parse(TextBox_price.Text) : -1;
            if (price == -1)
            {
                MessageBox.Show("Вы не ввели Стоимость номера");
                return;
            }
            // Size
            int size = Combobox_size.SelectedIndex > -1 ? (int)Combobox_size.SelectedItem : -1;
            if (size == -1)
            {
                MessageBox.Show("Вы не ввели вместимость номера");
                return;
            }
            // Number
            int number = TextBox_number.Text != "" ? Int32.Parse(TextBox_number.Text) : -1;
            if (number == -1)
            {
                MessageBox.Show("Вы не ввели индекс номера");
                return;
            }
            
            // Type
            RoomTypes roomType = Combobox_roomType.SelectedIndex > -1 ? (RoomTypes)Combobox_roomType.SelectedItem : null;
            if (roomType == null)
            {
                MessageBox.Show("Вы не выбрали тип номера");
                return;
            }

            

            if (editRoom != null)
            {
                // change mod

                if(editRoom.Number != number)
                {
                    // проверяем номер, только если мы его изменили
                    using (var db = new ApplicationContext())
                    {
                        db.Rooms.Load();
                        Entity.Rooms r = db.Rooms.Where(rm => rm.Number == number).FirstOr(null);
                        if (r != null)
                        {
                            MessageBox.Show("Номер с таким индексом(" + number + ") уже существует");
                            return;
                        }
                    }
                }

                // createEntity
                editRoom.Price = price;
                editRoom.Size = size;
                editRoom.TypeId = roomType.Id;
                editRoom.Type = roomType;
                editRoom.Number = number;

                // обновление записи в бд
                using (var db = new ApplicationContext())
                {
                    db.Rooms.Attach(editRoom);

                    db.Entry(editRoom).Property(x => x.TypeId).IsModified = true;
                    db.Entry(editRoom).Property(x => x.Size).IsModified = true;
                    db.Entry(editRoom).Property(x => x.Price).IsModified = true;
                    db.Entry(editRoom).Property(x => x.Number).IsModified = true;

                    db.SaveChanges();
                }
            }
            else
            {
                // create mod
                // createEntity
                Entity.Rooms room = new Entity.Rooms();
                room.Price = price;
                room.Size = size;
                room.TypeId = roomType.Id;
                room.Number = number;

                db.Rooms.Add(room);
                db.SaveChanges();
            }

            ((Form_rooms)this.Owner).updateDataGrid();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}