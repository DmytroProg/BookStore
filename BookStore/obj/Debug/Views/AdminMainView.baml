   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BookStore.ViewModelsBookStore.ViewModels  5   	   #    BookStore.Views.AdminMainView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:BookStore.ViewModels     5   	   $	��450��5	      $	��800��6     � 	Resources  5
      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value��%d   '        �� 5   
   "  6   .�5      ��  �@��TextAlignment$ Center=�68   5      5      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $
��0.2* �6   �� 5      .+�5   
   � $��0��6   
�������5      � 5   
   $��1��6   $��60��6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5       $
��1.5* �6#   .+�5"      �� -   5         ��Name$ 
addBookBtn��5"      $��20, 15q�6)   ��5#      �� *ShowAddBookView+.��69   
Add book5%      �� 5&      -   5        $ addAuthorBtn��5&      $��20, 15q�6+   $��1��5'      .��6%   
Add author5)      �� 5*      -   5        $ addDiscountBtn��5*      $��20, 15q�6-   $��2��5+      .��6%   Add discount5-      �� 5.      $��3��6    �� CornerRadius$ 10��6%   $��20, 15q�67   $��1q�5/      
���   � �� Padding$ 3q�6=   ._�50      �  51      �� 52      �� 53      $��30 �6+   .+�55      �� -   5        	 ��Name$	 searchTextBox��55      $��0q�65   $��20(�56      6.   �� 57      $��20��6   $��20��6'   $��/Images/search.png��63   $��1��58      6.   59      5:      5;      &   System.Windows.Controls.DataGrid  5<   
   -   5        
  Name$
 booksDataGrid��5<      $��2��6*   $��20q�67      AutoGenerateColumns .     CanUserAddRows .  ��5>      �� *BookDetails+   Columns 5?      4  .System.Windows.Controls.DataGridTemplateColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$ Cover��6)      CellTemplate 5A      �� 5B      .R�5C      �� $	��100��6$   $��5q�61   ��6<   ��  �� Path$ 
Book.Image�6a   5D      5F      0  *System.Windows.Controls.DataGridTextColumn  5G         FontSize$ 20Q�6%   $ Title��63      Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 6B   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6M   �� $ 	Book.Name�6r     5H      $ 20Q�6%   $ Count��63   $ 3* 6B    6M   �� $ Count�6n   4  .System.Windows.Controls.DataGridCheckBoxColumn  5I      $ In sale��6)    6:   �� $ IsAvailable�6a     5J      $	 10* 6)    5K      �� 5L      .R�5M      ��  �� Orientation$ 
Horizontal=�6)   $��Right=�6B   $��10, 5q�5N   )   .+�5O   "   �� $��10, 30q�6)   .��69   Update6A   �� 5P   "   $��10, 30q�6)   .��69   Delete6A   5Q      5R      5T      5V      5W      5X      