   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfApp2.Forms.RoomsWpfApp2.Forms.Rooms  $    WpfApp2.Forms.Rooms.Form_rooms   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:WpfApp2.Forms.Rooms       � Title$  Апартаменты��$	��470��$	��800��$	��470��$	��800�� � Icon$" /WpfApp2;component/hotel.ico��.���  � ColumnDefinitions �� $	��83* ��� $
��511* ��� $	��200 �.+�� $��2�� � UseLayoutRounding .   � RowDefinitions �� $��50 ��� $��90 ��� $��90 ��� $��90 ��� $	��110 �.+��� $��0�����	#FF858585$��Bottom=��� $��1�����	#FF858585$��Bottom=��� $��2�����	#FF858585$��Bottom=��� $��3�����	#FF858585$��Bottom=�� $��0�� �� $	��10* �.+��� -    ��Name$ Label_findTitle��$��0��$#��Критерии поиска��$��Center=�$��Center=�$��14(�$
��Bold"�$��
10,8,60,10q��� -    ��Name$ Button_resetFilter��$��X��$��Right=�$��143,13,20,0q�$	��Top=�$��27��$
��Bold"�� $��1�� �� $	��10* ��� $	��20* �.+��� -   $ Label_roomType��$��0��$��Тип Номера��$��Center=�$��25��$��Center=��� -    ��Name$ ComboBox_roomType��$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�$	��178��� $��2�� �� $	��10* ��� $	��20* �.+��� -   $ Label_roomNumber��$��0��$��Номер комнаты��$��Center=�$��25��$��Center=��� -    ��Name$ TextBox_roomNumber��$��1��$��23��$��
10,10,10,0q�	 �� TextWrapping$
	 Wrap=�$�� ��$	��Top=�� $��3�� �� $	��10* ��� $	��20* �.+��� -   $ 
Label_size��$��0��$��Вместимость��$��Center=�$��25��$��Center=��� -   $ ComboBox_size��$
��Left=�$��	10,12,0,0q�$��1��$	��Top=�$	��178��� $��4�� �� $	��10* ��� $	��20* �.+��� -	   $ Label_price��$��0��$��Цена��$��Center=�$��25��$��Center=��� -
   $ TextBox_priceFrom��$��1��$��23��$��	40,7,10,0q�$
	 Wrap=�$�� ��$	��Top=��� -   $ TextBox_priceTo��$��1��$��23��$��
40,38,10,0q�$
	 Wrap=�$�� ��$	��Top=��� -   $ Label_priceFrom��$��1��$
��От��$��Center=�$��25��$
��Left=�$��5,5,0,44q�
 J� RenderTransformOrigin$
 0.847,0.413&��� -   $ Label_priceTo��$��1��$
��До��$��Center=�$��25��$
��Left=�$��	5,36,0,13q�$
 
4.042,0.48&�$��29���� -   $ Button_createRoom��$%��Добавить комнату��$
��Left=�$��	10,0,0,10q�$��Bottom=�$	��140��$��35��$��2��&   System.Windows.Controls.DataGrid  -     Name$ DataGrid_rooms��   AutoGenerateColumns .  $��0,0,0,60q�$��2��   
IsReadOnly . ���� *clientsList+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Id�  $ 
Номер��$	 15*  �� $ Number�  $ Тип номера��$	 20*  �� $ RoomTypes.Type�  $ Вместимость��$	 20*  �� $
 Size�  $ Цена��$	 20*  �� $ Price�4  .System.Windows.Controls.DataGridTemplateColumn  $	 10*    CellTemplate �� .R��� $ Button_editRow��-   .��edit  $	 10*  �� .R��� $ Button_delRow��-   .��del