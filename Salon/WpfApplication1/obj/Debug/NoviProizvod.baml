   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	   "    WpfApplication1.NoviProizvod   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	   -   5          � Title$  NoviProizvod��5   	   $	��430��6   $	��350��6+   .��5	      � .+�5
   
   �� -   5         ��Name$ label��5
      $��Naziv proizvoda:��6   $
��Left=�6:   $��	21,60,0,0q�6U   $	��Top=�6h   �� 5   
   -   5        $ label1��5      $��Proizvodjac:��6    $
��Left=�67   $��
21,110,0,0q�6R   $	��Top=�6f   �� 5   
   -   5        $ label2��5      $��Kategorija:��6    $
��Left=�66   $��
21,160,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ labelx��5      $��Sifra Proizvoda:��6    $
��Left=�6;   $��
21,210,0,0q�6V   $	��Top=�6j   �� 5   
   -   5        $ label3��5      $��Prodajna cena:��6    $
��Left=�69   $��
21,260,0,0q�6T   $	��Top=�6h   �� 5   
   -   5        $ labelStanje��5      $��Pocetno stanje:��6%   $
��Left=�6?   $��
21,310,0,0q�6Z   $	��Top=�6n   �� 5   
   -   5         ��Name$ textBoxNaziv��5      $
��Left=�6(   $��23��6C   $��
141,60,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��170��6�   �� 5   
   -	   5        $ textBoxProizvodjac��5      $
��Left=�6.   $��23��6I   $��141,110,0,0q�6U   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��170��6�   �� 5   
   -
   5        $ textBoxKategorija��5      $
��Left=�6-   $��23��6H   $��141,160,0,0q�6T   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��170��6�   �� 5   
   -   5        $ textBoxSifraProizvoda��5      $
��Left=�61   $��23��6L   $��141,210,0,0q�6X   $
 Wrap=�6m   $�� ��6�   $	��Top=�6�   $��80��6�   6�   �� 5   
   -   5        $ textBoxCena��5      $
��Left=�6'   $��23��6B   $��141,260,0,0q�6N   $
 Wrap=�6c   $�� ��6w   $	��Top=�6   $��80��6�   6�   �� 5   
   -   5        $ textBoxStanje��5      $
��Left=�6)   $��23��6D   $��141,310,0,0q�6P   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $��80��6�   6�   �� 5   
   -   5         ��Name$ buttonResetuj��5      $��Resetuj��6(   $��Center=�6:   $��0,10,0,0q�6W   $	��Top=�6i   $	��100��6�   $��30��6�   6�   �� 5   
   -   5        $ buttonPovratak��5      $��Povratak��6)   $
��Left=�6<   $��	20,0,0,10q�6W   $��Bottom=�6j   $	��120��6�   $��30��6�   6�   �� 5   
   -   5        $ buttonUbaci��5      $��Ubaci novi proizvod��6&   $��Right=�6D   $��	0,0,20,10q�6`   $��Bottom=�6s   $	��120��6�   $��30��6�   6�   5      5      