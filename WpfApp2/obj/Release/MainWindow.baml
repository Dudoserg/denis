   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2      WpfApp2.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp2       � Title$0  *Отель «Гранд Будапешт»��$	��590��$	��900��$	��590��$	��900�� � Icon$ 	hotel.ico��.��� .+�o� -    o�Name$
 Menu��$	��Top=�$��20��.@�m� $��20��$��Center=�$��Center=����� $��Программа��$��Center=�$��Center=�.@�m� -   $��20��$��Center=�$��Center=����� $��
Выход��$
��Left=�$��Center=�m� -   $��20��$��Center=�$��Center=����� $%��Главная страница��$
��Left=�$��Center=�m� -   $��20��$��Center=�$��Center=����� $��Клиенты��$��Center=�$��Center=�m� -    m�Name$ MenuItem_rooms��$��20��$��Center=�$��Center=����� $��Номера��$��Center=�$��Center=�m� -   $ MenuItem_createOrder��$��20��$��Center=�$��Center=����� $!��Оформить заказ��$��Center=�$��Center=�m� -   $ MenuItem_info��$��20��$��Center=�$��Center=����� $��Справка��$��Center=�$��Center=�� $��0,20,0,0q� � ColumnDefinitions �� $
��100* ��� $	��270 � � RowDefinitions �� $��35 ��� $
��100* �.+��� $��0��$��2������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #F0F0F0��	 � Offset$	 0Q��� $��Заказы��$��Center=�$��Bottom=�$��2��$��35��$
��Bold"�$��18(�$
��10,0q�&   System.Windows.Controls.DataGrid  -   
  Name$
 DataGrid_orders��$��1��$��0��   AutoGenerateColumns .  $��0,0,0,70q�   
IsReadOnly . ���� *clientsList+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Id�  $	 №��$	 15*  �� $ Rooms.Number�  $ Вместимость��$	 15*  �� $ 
Rooms.Size�  $ Тип��$	 20*  �� $ Rooms.RoomTypes.Type�  $ Цена сут.��$	 20*  �� $ Rooms.Price�  $ Стоимость��$	 20*  �� $ Coast�  $ ФИО клиента��$	 20*  �� $	 FIO�  $ Дата заезда��$	 15*  �� $ DateStartStr�  $ Дата выезда��$	 15*  �� $ 
DateEndStr�4  .System.Windows.Controls.DataGridTemplateColumn  $	 10*    CellTemplate �� .R���  ��Name$ Button_editRow��-	   .��edit  $	 10*  �� .R��� $ Button_delRow��-
   .��del�� -   $ Button_createOrder��$��1��$��0��$!��Оформить заказ��$
��Left=�$��	10,0,0,10q�$��Bottom=�$	��140��$��35��$��2��� $��1��$��1�� � UseLayoutRounding .   �� $��50 ��� $��65 ��� $��65 ��� $��65 ��� $	��130 ��� $	��125 �.+��� $��0�����	#FF858585$��Bottom=��� $��1�����	#FF858585$��Bottom=��� $��2�����	#FF858585$��Bottom=��� $��3�����	#FF858585$��Bottom=��� $��4�����	#FF858585$��Bottom=�� $��0�� �� $	��10* �.+��� -    ��Name$ Label_findTitle��$��0��$#��Критерии поиска��$��Center=�$��Center=�$��14(�$
��Bold"�$��
10,8,60,10q��� -   $ Button_resetFilter��$��X��$��Right=�$��143,13,20,0q�$	��Top=�$��27��$
��Bold"�� $��1�� �� $	��10* ��� $	��20* �.+��� -   $ Label_Number��$��0��$��Комната №��$��Center=�$��25��$��Center=��� -    ��Name$ TextBox_Number��$��1��$��23��$��5,10,5,5q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�� $��2�� �� $	��10* ��� $	��20* �.+��� -   $ 
Label_Size��$��0��$��Вместимость��$��Center=�$��25��$��Center=��� -    ��Name$ ComboBox_Size��$��	10,10,5,0q�$��1��$	��Top=�� $��3�� �� $	��10* ��� $	��20* �.+��� -   $ Label_Price��$��0��$��Цена��$��Center=�$��25��$��Center=�� $��1�� �� $	��50* ��� $	��50* �.+��� -   $ TextBox_PriceFrom��$��0��$��23��$��
30,10,10,0q�$
 Wrap=�$�� ��$	��Top=��� -   $ TextBox_PriceTo��$��1��$��23��$��
30,10,10,0q�$
 Wrap=�$�� ��$	��Top=��� $
��От��$
��Left=�$��2,8,0,0q�$	��Top=��� $
��До��$��1��$
��Left=�$��0,8,0,0q�$	��Top=�� $��4�� �� $	��10* ��� $	��20* �.+��� -   $ 	Label_FIO��$��0��$��	Ф.И.О.��$��Center=�$��25��$��Center=��� -   $ TextBox_FirstName��$��23��$��
119,0,10,0q�$
 Wrap=�$�� ��$	��Top=�$��1���� -   $ TextBox_SecondName��$��23��$��119,26,10,0q�$
 Wrap=�$�� ��$	��Top=�$��1���� -   $ TextBox_Patronymic��$��23��$��119,52,10,0q�$
 Wrap=�$�� ��$	��Top=�$��1���� $��Имя��$
��Left=�$��10,0,0,0q�$	��Top=�$��1���� $��Фамилия��$
��Left=�$��	10,26,0,0q�$	��Top=�$��1���� $��Отчество��$
��Left=�$��	10,52,0,0q�$	��Top=�$��1��� $��5�� �� $��5* ��� $	��20* �.+��� -   $ Label_Dates��$��0��$��Даты��$��Center=�$��25��$��Center=�� $��1�� �� $	��50* ��� $	��15* ��� $	��50* �.+�(  "System.Windows.Controls.DatePicker  $��3��$
��Left=�$��0��$��0,27,0,0q�$	��Top=�  -     Name$ DatePicker_StartFrom��$��
10,10,10,0q�$	��Top=�  -   $ DatePicker_StartTo��$��
10,60,10,0q�$	��Top=�  -   $ DatePicker_EndFrom��$��2��$��
10,10,10,0q�$	��Top=�  -   $ DatePicker_EndTo��$��2��$��
10,60,10,0q�$	��Top=��� $
��От��$��1��$��Center=�$��0,10,0,0q�$	��Top=��� $
��До��$��1��$��Center=�$��0,60,0,0q�$	��Top=��� -   $ Button_resetDateFilter��$��X��$��Right=�$��0,5,10,0q�$��5��$	��Top=�$��29��$
��Bold"�