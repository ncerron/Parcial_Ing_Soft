   M S B A M L   `   `   `  ����A  =Pacial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PacialPacial  5   	       Pacial.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Pacial     5   	     � WindowStyle$
  None=�5   	    � Title$ Busqueda de productos��5	   	   $	��407��6'   $��764.503��64    � Topmost .  � 
ResizeMode$ NoResize=�6S   ���	#FFF7F8F9$��	5,10,5,10q�6�   ��5
      �� 5   
    �� ImageSource$ iconos/fondo22.jpg��6   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5      $��Center=�6%   $	��220��6B   $��10,100,11,67q�6O   $��Center=�6e   $	��734��6�      SelectionMode$ Single=�6�   $��15(�6�    J� Opacity$	 0.8Q�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   
���   ����	#FF1F2225	   HorizontalGridLinesBrush	 �	#FFB1E3FF
   VerticalGridLinesBrush
 �	#FFB1E2FFz�5      V� 5      .~�5      ��  �� ScaleX$ 0.999Q�6%   �� 5       �� AngleX$ 0Q�6$    �� AngleY$ 0Q�6/   5         	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5      ��  �� Property � �� �� Value
 ��=H�63   5      5      �� 5   
   -   5         ��Name$ buscar��5      $��218,35,418,315q�64   $ 0.38,-0.379&�6L   $��14(�6p   $	 0.7Q�6�   ��6~   Z� ��6�   Z� .��5      �� $��iconos/buscar2.png��6   $
 0.79Q�60   6A   5       �� 5"   
   -   5         ��Name$ txtbox��5"      $
��Left=�6"   $��23��6=   $��	40,43,0,0q�6I    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $	��164��6�   $��14(�6�   $	 0.7Q�6�   �� 5#   
   -   5        $��btnCancelar��5#      $��626,339,10,10q�6%   $��14(�6Q   $	 0.7Q�6�   $ 0.5,0.5&�6�   ��6_   Z� ��6u   Z� .��5$      �� $��iconos/cancelar2.png��6   $	 0.8Q�62   6B   5%      5&      5'      