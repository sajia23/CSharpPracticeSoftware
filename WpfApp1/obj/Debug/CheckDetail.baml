   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.CheckDetail   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  CheckDetail��5   	   $	��300��6   $	��400��6*   .��5	      � -   5        $��Detail��5	       � RowDefinitions 5
   
   �� 5      $	��15* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��55* �6   6+   �� 5      $	��15* �6   6+   .+�5   
   � -   5        $��shurushuchu��5      $��2��6"    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� $��输入��6   $��0��6#   .��5      �� .��5      ��  �� 
IsReadOnly . $��0q�60    �� TextWrapping$
 Wrap=�6`   ��6D   ��  �� Path$ Shuru�6x   5      5      �� 5      $��正确输出��6   $��1��6%   .��5      �� .��5      ��  . $��0q�60   $
 Wrap=�6h   ��6D   �� $ CorrectShuchu�6�   5      5      �� 5       $��你的输出��6   $��2��6%   .��5!      �� .��5"      ��  . $��0q�60   $
 Wrap=�6d   ��6D   �� $ 	ExeShuchu�6|   5#      5$      5%      �� 5'   
    �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   .+�5)      �� -   5        $��ProblemTitle��5)      $
��Wrap=�6-   $��16(�6e   $��Center=�6s   ��6A   �� $ problem.Title�6s   �� 5*      -   5        $��	TimeLimit��5*      $
��Wrap=�6*   $�� ��6>   $��16(�6F   $��Center=�6T   $��20,0,0,0q�6o   5,      �� 5-   
   $ 
Horizontal=�6   $��1��6.   $��Center=�6;   $��25��6X   .+�5.      �� -   5        $��	上一个��5.      $��0,0,10,0q�6=   6Q   �� 5/      -   5        $	��Num��5/      $
��Wrap=�6$   $�� ��68   $��16(�6@   $��Center=�6N   �� 50      -   5        $��	下一个��50      $��10,0,0,0q�6=   6Q   51      �� 52   
   $ 
Horizontal=�6   $��3��6.   $��Center=�6;   .+�53      �� -   5        $��Cuowu��53      $
��Wrap=�6&   $��错误原因:��6:   $��16(�6G   $��Center=�6U   �� 54      -	   5        $��	Errorcode��54      $
��Wrap=�6*   $��16(�6>   $��Center=�6L   $�� ��6g   $��20,0,0,0q�6o   6�   55      57      58      