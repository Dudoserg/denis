   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfApp2.Forms.OrderWpfApp2.Forms.Order  *    $WpfApp2.Forms.Order.Form_createOrder   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:WpfApp2.Forms.Order       � Title$'  !Оформление заказа��$	��750��$	��800��$	��750��$	��800�� � Icon$ 	hotel.ico��.��� .+��� .���  � ColumnDefinitions �� $	��65* �.+��� -    ��Name$ Label_passport��$��Паспорт��$
��Left=�$��	17,80,0,0q�$	��Top=� J� RenderTransformOrigin$ 
4.75,2.462&��� -    ��Name$ TextBox_passport��$
��Left=�$��23��$��	97,80,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��170���� -   $ Label_patronymic��$��Отчество��$
��Left=�$��
17,200,0,0q�$	��Top=��� -   $ TextBox_patronymic��$
��Left=�$��23��$��
97,200,0,0q�$
 Wrap=�$	��Top=�$	��170���� -   $ Label_firstName��$��Имя��$
��Left=�$��
17,120,0,0q�$	��Top=��� -   $ TextBox_firstName��$
��Left=�$��23��$��
97,120,0,0q�$
 Wrap=�$	��Top=�$	��170���� -   $ Label_secondName��$��Фамилия��$
��Left=�$��
17,160,0,0q�$	��Top=��� -   $ TextBox_secondName��$
��Left=�$��23��$��
97,160,0,0q�$
 Wrap=�$	��Top=�$	��170���� -	   $ Label_roomType��$��Тип комнаты��$
��Left=�$��
10,376,0,0q�$	��Top=��� -
    ��Name$ ComboBox_roomType��$
��Left=�$��105,376,0,0q�$	��Top=�$	��162��$��2��&   System.Windows.Controls.Calendar  -     Name$ 	Calendar_��$
��Left=�$��
28,516,0,0q�$	��Top=��� -   $ Label_phone��$��Телефон��$
��Left=�$��
17,240,0,0q�$	��Top=��� -   $ TextBox_phone��$
��Left=�$��23��$��
97,240,0,0q�$
 Wrap=�$	��Top=�$	��170���� -   $ 
Label_size��$��Вместимость��$
��Left=�$��
10,420,0,0q�$	��Top=��� -   $ Combobox_size��$
��Left=�$��103,420,0,0q�$	��Top=�$	��164��$��2���� -   $ Label_priceFrom��$��Цена от��$
��Left=�$��
10,468,0,0q�$	��Top=��� -   $ TextBox_priceFrom��$
��Left=�$��24��$��184,468,0,0q�$
 Wrap=�$	��Top=�$��55���� -   $ Label_priceFrom1��$
��до��$
��Left=�$��155,466,0,0q�$	��Top=��� -   $ TextBox_priceTo��$
��Left=�$��24��$��
72,468,0,0q�$
 Wrap=�$	��Top=�$��66���� -   	 ��Name$	 Button_findFreeNumber��$0��*Найти свободные номера��$
��Left=�$	��Top=�$	��257��$��
10,706,0,0q�$��2��� $
��Left=�$	��100��$��365,614,0,0q�$	��Top=�$	��100��$��1��&   System.Windows.Controls.DataGrid  -   
  Name$
 DataGrid_rooms��$	��390��$��294,336,10,0q�$	��Top=�   AutoGenerateColumns .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 8* 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Id�  $ 
Номер��$	 15*  �� $ Number�  $ Тип номера��$	 15*  �� $ RoomTypes.Type�  $ Вместимость��$	 15*  �� $
 Size�  $ Цена��$	 15*  �� $ Price��� -   $	 Button_calendarReset��$��X��$
��Left=�$��244,519,0,0q�$	��Top=�$��23��$��26���� -   $	 Button_resetRoomsFilter��$#��Сбросить фильтр��$
��Left=�$��
10,336,0,0q�$	��Top=�$	��257��$��2���� -   $	 Button_Price��$��X��$
��Left=�$��244,469,0,0q�$	��Top=�$��23��$��24���� -   $ Label_reserved��$"��Резервируется :��$
��Left=�$��
10,738,0,0q�$	��Top=��� -   $ Label_statusRoom��$	��...��$
��Left=�$��120,738,0,0q�$	��Top=�$��2���� -   $	 Button_resetClientFilter��$#��Сбросить фильтр��$
��Left=�$��	97,46,0,0q�$	��Top=�$	��170��  -   $
 DataGrid_clients��$	��220��$��294,46,10,0q�$	��Top=� .   .    $ Id��$ 7*  �� $ Id�  $ Паспорт��$	 20*  �� $ Passport�  $ Имя��$	 20*  �� $ 	FirstName�  $ Фамилия��$	 20*  �� $ 
SecondName�  $ Отчество��$	 20*  �� $ 
Patronymic�  $ 
Номер��$	 20*  �� $ Phone��� -   $ Label_statusClients��$	��...��$
��Left=�$��
28,271,0,0q�$	��Top=��� -   $	 Button_confirmOrder��$'��!Подтвердить заказ��$��Right=�$��0,759,10,20q�$	��Top=�$	��140��$��40���� -   $ Label_statusMain��$	��...��$
��Left=�$��294,773,0,0q�$	��Top=�$ 8.889,0.615&�