   M S B A M L   `   `   `  ����A  =MEMORY, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MEMORYMEMORY  5          MEMORY.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:MEMORY     5      $%��/MEMORY;component/Fonts/#Baymax)�5      $	��800��5	      $
��1200��6   .��5
      ��   �� CornerRadius$  20��6
      PagesBackground#���   6   ._�5      � $��50q�6	    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      .+�5      �  5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5      �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      ��   TottalTbStyle#���  6   $��40(�68   $��Center=�6F   $��Center=�6c   $&�� Громкость звуков:��6~   $��10q�6�   �� 5      -   5         ��Name$ VolumeSlider��5      $��1��6#   $
��Left=�63   $��0��6N    � Minimum$ 0Q�6[    � Maximum$	 100Q�6g    � Value$ 50Q�6u   $	��500��6�   $��10q�6�   �� 5"      #���  6   $��40(�68   $��Center=�6F   $��Center=�6c   $��1��6~   $&�� Громкость музыки:��6�   $��10q�6�   �� 5#      -   5        $ VolumeMusikSlider��5#      $��1��6(   $
��Left=�68   $��1��6S   $ 0Q�6`   $	 100Q�6l   $ 50Q�6z   $	��500��6�   $��10q�6�   �� 5&      #���  6   $��Center=�68   $��Center=�6U   $��40(�6p   $��	Тема:��6~   $��2��6�   $��0��6�   $��20q�6�   �� 5'      -   5         ��Name$ ThemeComboBox��5'      $��30(�6&   $	��400��64   $
��Left=�6@   $��1��6[   $��2��6k   $��20q�6x   $	��Top=�6�   6�   5(      �� 5+      -   5          ButtonStyle#���  5+   %   $��
Назад��6K   $��1��6[   $��10q�6h   $��Bottom=�6u   $
��Left=�6�   �� 5,      -   5        #���  5,   /   $��Сохранить��6U   $��1��6i   $��
0,403,10,0q�6v   $	��Top=�6�   $��Right=�6�   5.      50      51      