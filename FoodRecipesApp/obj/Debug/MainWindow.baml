   M S B A M L   `   `   `  ����I  EFoodRecipesApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FoodRecipesAppFoodRecipesApp  5   	       FoodRecipesApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:FoodRecipesApp     5   	   -   5          � Title$  Food Recipes��5   	   $	��800��6   $
��1400��6+    � UseLayoutRounding .  �� BitmapScalingMode$ HighQuality=�6Q    � WindowStartupLocation$ CenterScreen=�6   .��5
      ]� -   5        $
��Main��5
      ��. .+�5   
   �� -   5        $��TopBar��5   F    �� Orientation$ 
Horizontal=�6   $	��Top=�6.   $��40��6T   $	��Top=�6`   ���#ffd634.+�5      �� $��Food Recipes��6   $��20(�6+   $
��Bold"�69   $��Center=�6K   $��10,0,0,0q�6m   ���	#FF89754C6   5      � 5   
   -   5        $��TagMenu��5      $��60��6   ���	#FFF3EEDB$
��Left=�6@   .+�5      �� -   5         ��Name$ TagItem��5   $   $	��600��6   $��Disabled=�65   $	��Top=�6k   $��Center=�6�   ��6�   Z� ��6�   Z� .@�5      �� -   5        $��60��5      .��5      ��  K� ZIndex$ 1��64   $��44��6E   $��44��6P   $��Center=�6\   $��Center=�6y   ��6   Z� ._�5      �� $��Assets/menu.png��6    $��40��69   $��40��6D   5      5      �� 5      $��60��6   .��5      �� $ 
Horizontal=�6!   .+�5      �� $ 1��68   $��44��6I   $��44��6T   $��Center=�6`   $��Center=�6}   ��6!   Z� ._�5      �� $��Assets/home.png��6$   $��35��6=   $��35��6H   6V   5      �� 5      $��Recipes��6$   $��10,0,0,0q�63   $��Center=�6E   $��16(�6`   $
��Bold"�6n   ���	#FFCD42426�   5      5       �� 5"      $��60��6   .��5#      �� $ 
Horizontal=�6!   .+�5$      �� $ 1��68   $��44��6I   $��44��6T   $��Center=�6`   $��Center=�6}   ��6!   Z� ._�5%      �� $��Assets/favourite.png��6$   $��35��6B   $��35��6M   6[   5&      �� 5'      $��Favorite Recipe��6$   $��10,0,0,0q�6;   $��Center=�6M   $��16(�6h   $
��Bold"�6v   ���	#FFCD42426�   5(      5)      �� 5+      $��60��6   .��5,      �� $ 
Horizontal=�6!   .+�5-      �� $ 1��68   $��44��6I   $��44��6T   $��Center=�6`   $��Center=�6}   ��6!   Z� ._�5.      �� $��Assets/edit.png��6$   $��40��6=   $��40��6H   5/      �� 50      $��
New Recipe��6$   $��10,0,0,0q�66   $��Center=�6H   $��16(�6c   $
��Bold"�6q   ���	#FFCD42426�   51      52      53      54      � 56   
   -   5         �Name$ Recipes��56      57      58      59      