   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   J FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1 5   	   "   WpfApplication1.NoviKorisnik   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   4sys&clr-namespace:System;assembly=mscorlib    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	   -   5          � Title$  NoviKorisnik��5	   	   $	��600��6   $	��800��6+    � 
ResizeMode$ NoResize=�67   .��5
      � .+�5   
   �� -   5         ��Name$ label��5      $
��Ime:��6   $
��Left=�6.   $��	17,20,0,0q�6I   $	��Top=�6\   �� 5   
   -   5        $ 
label_Copy��5      $��Prezime:��6$   $
��Left=�67   $��	17,70,0,0q�6R   $	��Top=�6e   �� 5   
   -   5        $ labelPol��5      $
��Pol:��6"   $
��Left=�61   $��
17,120,0,0q�6L   $	��Top=�6`   �� 5   
   -   5        $ 
labelDatum��5      $��Datum Rodjenja:��6$   $
��Left=�6>   $��
17,170,0,0q�6Y   $	��Top=�6m   �� 5   
   -   5        $ label_Copy3��5      $��Telefon:��6%   $
��Left=�68   $��
17,220,0,0q�6S   $	��Top=�6g   �� 5   
   -   5        $ label_Copy4��5      $��E-mail:��6%   $
��Left=�67   $��
17,270,0,0q�6R   $	��Top=�6f   �� 5   
   -   5        $ labelAlergeni��5      $��Poznati Alergeni:��6'   $
��Left=�6C   $��
17,320,0,0q�6^   $	��Top=�6r   �� 5   
   -	   5        $ labelZabeleske��5      $��
Zabeleske:��6(   $
��Left=�6=   $��
17,420,0,0q�6X   $	��Top=�6l   �� 5   
   -
   5         ��Name$ 
textBoxIme��5      $
��Left=�6&   $��23��6A   $��
150,20,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��220��6�   6�   �� 5   
   -   5        $ textBoxPrezime��5      $
��Left=�6*   $��23��6E   $��
150,70,0,0q�6Q   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��220��6�   6�   	� 5   
   -   5         	�Name$ radioZenski��5      $��Zenski��6+   $
��Left=�6<   $��150,120,0,0q�6W   $	��Top=�6l    d� 	IsChecked$
 TrueY�6�   	� 5   
   -   5        $ 
radioMuski��5      $��Muski��6*   $
��Left=�6:   $��288,120,0,0q�6U   $	��Top=�6j   �� 5   
   -   5        $ labelRadnoMesto��5      $��Administrator��6)   $
��Left=�6A   $��152 120 0 0q�6\   $	��Top=�6q   $y�	Collapsed=�6�   �� 5   
   -   5        $ 
textBoxDan��5      $
��Left=�6&   $��23��6A   $��150,170,0,0q�6M   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $��25��6�   6�   �� 5   
   -   5        $ textBoxMesec��5      $
��Left=�6(   $��23��6C   $��191,170,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $��25��6�   6�   �� 5   
   -   5        $ textBoxGodina��5      $
��Left=�6)   $��23��6D   $��232,170,0,0q�6P   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $��40��6�   6�   �� 5   
   -   5        $ label1��5      $��/��6    $��15(�6,   $
��Left=�6:   $��175,165,0,0q�6U   $	��Top=�6j   �� 5    
   -   5        $ label1_Copy��5       $��/��6%   $��15(�61   $
��Left=�6?   $��216,165,0,0q�6Z   $	��Top=�6o   �� 5"   
   -   5        $ textBoxTelefon��5"      $
��Left=�6*   $��23��6E   $��150,220,0,0q�6Q   $
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��220��6�   �� 5#   
   -   5        $ textBoxEmail��5#      $
��Left=�6(   $��23��6C   $��150,270,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��220��6�   �� 5$   
   -   5        $ textBoxAlergeni��5$      $
��Left=�6+   $��80��6F   $��150,320,0,0q�6R   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��300��6�    �� AcceptsReturn . 6�   �� 5%   
   -   5        $ textBoxZabeleske��5%      $
��Left=�6,   $��80��6G   $��150,420,0,0q�6S   $
 Wrap=�6h   $�� ��6|   $	��Top=�6�   $	��300��6�    . 6�   �� 5'   
   -   5         ��Name$ buttonReset��5'      $��Ocisti podatke��6&   $
��Left=�6?   $��
17,521,0,0q�6Z   $	��Top=�6n   $	��150��6�   $��30��6�   �� 5(   
   -   5        $ buttonUbaci��5(      $��Dodaj novog korisnika��6&   $
��Left=�6F   $��300,521,0,0q�6a   $	��Top=�6v   $	��150��6�   $��30��6�   �� 5*   
   -   5        	 ��Name$	 groupBox��5*      $��Pretraga��6%   $��14(�67   $
��Left=�6E   $��	501,0,0,0q�6`   $	��Top=�6s   .��5+      �� -   5        $ textBoxPretraga��5+      $
��Left=�6/   $��23��6J   $��5q�6V   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��150��6�   6�   5,      �� 5-   
   -   5        $ buttonNoviKorisnik��5-      $��Novi Korisnik��6-   $
��Left=�6E   $��
693,20,0,0q�6`   $	��Top=�6t   $��80��6�   $��30��6�   6�   &   System.Windows.Controls.DataGrid  5.   
   -   5        
  Name$
 dataGridPrikaz��5.      $
��Left=�6+   $��
501,70,0,0q�6F   $	��Top=�6Z   $	��430��6r      AlternatingRowBackground
 ��ح� �� AlternationCount$ 2��6�      CanUserAddRows .     AutoGenerateColumns .     SelectionMode$ Single=�6�      SelectionUnit$ FullRow=�6     
IsReadOnly .    Columns 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$	 Ime��6B      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$	 Ime�6O     51      $ Prezime��6F   $	 152 6W    6%   �� $ Prezime�6W   53      �� 54   
   -   5        $ buttonPovratak��54      $��Povratak  >��6)   $
��Left=�6B   $��673,521,0,0q�6]   $	��Top=�6r   $	��100��6�   $��30��6�   6�   �� 55   
   -   5        $ buttonOtkaz��55      $��Otkaz!��6&   $
��Left=�67   $��175,339,0,0q�6R   $	��Top=�6g   $	��100��6   $��30��6�   {�.  $y�	Collapsed=�6�   6�   �� 56   
   -    5        $ buttonIzbrisi��56      $��Izbrisi korisnika��6(   $
��Left=�6D   $��501,521,0,0q�6_   $	��Top=�6t   $	��100��6�   $��30��6�   {�.  6�   57      58      