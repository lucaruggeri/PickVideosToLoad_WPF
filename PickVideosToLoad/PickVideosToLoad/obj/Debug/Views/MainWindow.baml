   M S B A M L   `   `   `  ����K  GPickVideosToLoad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PickVideosToLoadPickVideosToLoad  5   	   G(clr-namespace:PickVideosToLoad.ViewModelPickVideosToLoad.ViewModel  5   	   !    PickVideosToLoad.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:PickVideosToLoad     5   	   >	ViewModel(clr-namespace:PickVideosToLoad.ViewModel     5   	   -   5          � Title$  Pick Videos to Load��5
   	   $��420.553��5   	   $��511.166��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��82* �6   �� 5      $
��429* �6   �� 5      $��6* �6   .+�5   
   �� -   5         ��Name$ btnSetSourceFolder��5      $��Set source folder��6-   $
��Left=�6I   $��	20,18,0,0q�6d   $	��Top=�6w   $	��114��6�   $��20��6�   $��2��6�   �� 5   
   -   5         ��Name$ lblSourceFolder��5      
�������$
��Left=�6�   $��	20,38,0,0q�6�   $	��Top=�6�   $	��457��6�   $��26��6�   $��2��6�   ��6@   �� *myConfig.sourceFolder+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5        $ btnSetDestinationFolder��5      $��Set destination folder��62   $
��Left=�6S   $��	20,80,0,0q�6n   $	��Top=�6�   $	��114��6�   $��2��6�   �� 5   
   -   5        $ lblDestinationFolder��5      
�������$
��Left=�6�   $��
20,100,0,0q�6�   $	��Top=�6�   $	��457��6�   $��2��6�   ��6E   �� *myConfig.destinationFolder+$ TwoWay=�6�   �� 5   
   -   5        $ btnTransfer��5      $��Transfer��6&   $
��Left=�69   $��
20,309,0,0q�6T   $	��Top=�6h   $��75��6�   $��2��6�   6�   �� 5   
   -   5         ��Name$ chkFirstFile��5       d� IsThreeState .  $6��0Start from the first file (more ordered picking)��6>   $
��Left=�6y   $��
20,289,0,0q�6�   $	��Top=�6�   $��2��6�    d� 	IsChecked 6�   �� * myConfig.startFromTheFirstFile+$ TwoWay=�$ PropertyChanged=�6;  �� 5   
   -   5        $ chkMove��5       .  $%��Move files (delete from source)��69   $
��Left=�6c   $��
20,269,0,0q�6~   $	��Top=�6�   $��2��6�    6�   �� *" myConfig.moveAndDeleteFromSource+$ TwoWay=�$ PropertyChanged=�6'  � 5   
   -	   5        	 �Name$	 objProgressBar��5      $
��Left=�6.   $��31��6I   $��
20,334,0,0q�6U   $	��Top=�6i   $	��457��6�   $��2��6�   �� 5   
   -
   5        
 ��Name$
 txtGigas��5      $
��Left=�6$   $��23��6?   $��
28,196,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6�   $��79��6�   $��2��6�   ��6s   �� *myConfig.gigas+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   -   5        $ lblSpace��5      $"��How much space is available?��6"   $
��Left=�6I   $��
20,165,0,0q�6d   $	��Top=�6x   $��2��6�   $	��184��6�   �� 5    
   -   5        $ lblGigas��5       $��Gigas��6"   $��1��62   $
��Left=�6B   $��
27,191,0,0q�6]   $	��Top=�6q   �� 5!   
   -   5        $
 txtMegabits��5!      $
��Left=�6'   $��23��6B   $��
28,227,0,0q�6N   $
 Wrap=�6b   $	��Top=�6�   $��79��6�   $��2��6�   ��6v   �� *myConfig.megas+$ TwoWay=�$ PropertyChanged=�6�   �� 5"   
   -   5        $ lblMegabits��5"      $��Megabits��6%   $��1��68   $
��Left=�6H   $��
27,222,0,0q�6c   $	��Top=�6w   5#      5$      