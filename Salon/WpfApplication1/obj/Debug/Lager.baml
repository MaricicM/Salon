   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	       WpfApplication1.Lager   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	   -   5          � Title$  Lager��5   	   $	��430��6   $	��860��6$    � 
ResizeMode$ NoResize=�60   .��5	      � .+�5
   
   �� -   5         ��Name$ groupBoxPretraga��5
      $��Pretraga��6-   $
��Left=�6?   $��	20,10,0,0q�6Z   $	��Top=�6m   .��5      �� -   5         ��Name$ textBoxPretraga��5      $
��Left=�6/   $��23��6J   $��5q�6V    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��150��6�   6�   5      �� 5   
   -   5         ��Name$ buttonResetujPretragu��5      $��Resetuj pretragu��60   $
��Left=�6K   $��
213,26,0,0q�6f   $	��Top=�6z   $	��120��6�   $��30��6�   6�   �� 5   
   -   5        $ buttonNoviProizvod��5      $��Novi Proizvod��6-   $��Right=�6E   $��	0,26,20,0q�6a   $	��Top=�6t   $��30��6�   $	��120��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridProizvodi��5      $
��Left=�6.   $��	20,76,0,0q�6I   $	��Top=�6\   $	��255��6t      AutoGenerateColumns .     CanUserAddRows .  	   
IsReadOnly	 . 
   AlternatingRowBackground

 ��ح� �� AlternationCount$ 2��6!     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6%      Header$ Sifra��6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   ��  �� Path$ SifraProizvoda�6X     5      $	 120 6%   $ Naziv proizvoda��6P    61   �� $ Naziv�6P     5      $	 120 6%   $ Proizvodjac��6V    61   �� $ Proizvodjac�6V     5      $	 120 6%   $ 
Kategorija��6U    61   �� $ 
Kategorija�6U     5      $ 60 6%   $
 Cena��6^    60   �� $
 Cena� �� StringFormat$ n��6^     5      $ 50 6%   $ Stanje��6P    60   �� $ Stanje�6P   5      �� 5   
   -   5        $ groupBoxUnos��5      $��Proizvod��6)   $
��Left=�6;   $��
587,76,0,0q�6V   $	��Top=�6j   $	��255��6�   $	��245��6�   .��5      � .+�5      �� -   5         ��Name$ labelProizvod��5      $��Proizvod��6/   $��Center=�6B   $��0,20,0,0q�6_   $	��Top=�6q   �� 5      -	   5        $ labelStanje��5      $��Trenutno stanje:��6-   $
��Left=�6H   $��	20,80,0,0q�6c   $	��Top=�6v   �� 5      -
   5        $ label2��5      $��
Novi unos:��6(   $
��Left=�6=   $��
20,140,0,0q�6X   $	��Top=�6l   �� 5       -   5        $ textBoxUnos��5       $��Right=�6/   $��23��6K   $��
0,140,60,0q�6W   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $��60��6�   6�   �� 5!      -   5        $ label1��5!      $
��kom.��6(   $��Right=�67   $��
0,140,20,0q�6S   $	��Top=�6g   �� 5"      -   5        $ buttonUnesi��5"      $��Unesi��6.   $��Right=�6>   $��	0,0,45,20q�6Z   $��Bottom=�6m   $��75��6�   6�   5#      5$      �� 5&   
   -   5        $ buttonPovratak��5&      $��Povratak��6)   $
��Left=�6<   $��20q�6W   $��Bottom=�6c   $	��120��6~   $��30��6�   6�   �� 5'   
   -   5        $ buttonIzbaci��5'      $��Izbaci proizvod��6'   $
��Left=�6A   $��
230,0,0,20q�6\   $��Bottom=�6p   $	��120��6�   $��30��6�   {�.  6�   �� 5(   
   -   5        $ buttonIzmeni��5(      $��Izmeni podatke��6'   $
��Left=�6@   $��
432,0,0,20q�6[   $��Bottom=�6o   $	��120��6�   $��30��6�   {�.  6�   5)      5*      