   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.Timer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   -   5          �Name$  	TimerForm��5   	    � Topmost . 
������  � AllowsTransparency .  � WindowStyle$
 None=�5   	    J� OpacityMask
 ����� � Title$ Timer��5   	   $	��120��6   $	��120��6$   .��5      � -   5        $��	gridTitle��5      $��0,0,0,0q�6    � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $	��13* �6   .+�5   
   �� -   5         ��Name$ timer1��5      $��Resources/timer.png��6!   $
��Fill=�6?    J� RenderTransformOrigin$ 	0.329,0.5&�6N   $��0,0,0,1q�6p   $��2��6�   5      �� 5   
   -   5        	 ��Name$	 timer2��5      $�� ��6    $
��Left=�6+   $��	20,49,0,0q�6F   $	��Top=�6Y   $��83��6q   $��30��6|   $��18(�6�   $��2��6�   
 � ContextMenu
 5   
   �� 5      -   5        $
��Menu��5   r    � DataContext 6   ��  �� Path$ PlacementTarget.Tag� �� RelativeSource �� *Self+.@�5      m� -   5        $��暂停��5       m� Command 6'   �� *
CutCommand+6F   m� 5      -   5        $��继续��5       6'   �� *
CutCommand+6F   m� 5      -   5        $��放弃��5       6'   �� *CopyCommand+6G   5      5"      5$      