   M S B A M L   `   `   `  ���� B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp2.FormsWpfApp2.Forms       WpfApp2.Forms.Form_clients   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:WpfApp2.Forms       � Title$  Клиенты��$	��500��$	��900��$	��500��$	��900�� � Icon$" /WpfApp2;component/hotel.ico��.���  � ColumnDefinitions �� $	��83* ��� $
��511* ��� $	��200 �.+�&   System.Windows.Controls.DataGrid  -     Name$ DataGrid_clients��   AutoGenerateColumns .  $��0,0,0,50q�$��2��   
IsReadOnly . ���� *clientsList+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$
 Id�  $ Паспорт��$	 15* 	 �� $
 Passport�  $ Имя��$	 20* 	 �� $
 	FirstName�  $ Фамилия��$	 20* 	 �� $
 
SecondName�  $ Отчество��$	 20* 	 �� $
 
Patronymic�  $ 
Номер��$	 20* 	 �� $
 Phone�4  .System.Windows.Controls.DataGridTemplateColumn  $	 10*    CellTemplate �� .R���  ��Name$ Button_editRow��-   .��edit  $	 10*  �� .R��� $ Button_delRow��-   .��del� $��2�� � UseLayoutRounding .   � RowDefinitions �� $��50 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 �.+�� $��0�� �� $	��10* �.+��� -    ��Name$ Label_findTitle��$��0��$#��Критерии поиска��$��Center=�$��Center=�$��14(�$
��Bold"�$��
10,8,60,10q��� -   $ Button_resetFilter��$��X��$��Right=�$��143,13,20,0q�$	��Top=�$��27��$
��Bold"��� $��0�����	#FF858585$��Bottom=��� $��1�����	#FF858585$��Bottom=��� $��2�����	#FF858585$��Bottom=��� $��3�����	#FF858585$��Bottom=��� $��4�����	#FF858585$��Bottom=�� $��1�� �� $	��10* ��� $	��20* �.+��� -   $ Label_passport��$��0��$��Паспорт��$��Center=�$��25��$��Center=��� -    ��Name$ TextBox_findPassport��$��1��$��23��$��5,10,5,5q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�� $��2�� �� $	��10* ��� $	��20* �.+��� -   $ Label_secondName��$��0��$��Фамилия��$��Center=�$��25��$��Center=��� -	   $ TextBox_findSecondName��$��1��$��23��$��5,10,5,5q�$
 Wrap=�$�� ��$	��Top=�� $��3�� �� $	��10* ��� $	��20* �.+��� -
   $ Label_firstName��$��0��$��Имя��$��Center=�$��25��$��Center=��� -   $ TextBox_findFirstName��$��1��$��23��$��5,10,5,5q�$
 Wrap=�$�� ��$	��Top=�� $��4�� �� $	��10* ��� $	��20* �.+��� -   $ Label_patronymic��$��0��$��Отчество��$��Center=�$��25��$��Center=��� -   $ TextBox_findPatronymic��$��1��$��23��$��5,10,5,5q�$
 Wrap=�$�� ��$	��Top=�� $��5�� �� $	��10* ��� $	��20* �.+��� -   $ Label_phone��$��0��$��Телефон��$��Center=�$��25��$��Center=��� -   $ TextBox_findPhone��$��1��$��23��$��5,10,5,5q�$
 Wrap=�$�� ��$	��Top=��� -   $ Button_createClient��$%��Добавить клиента��$
��Left=�$��	10,0,0,10q�$��Bottom=�$	��140��$��35��$��2��