   M S B A M L   `   `   `  ���� K  GPickVideosToLoad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PickVideosToLoadPickVideosToLoad  G(clr-namespace:PickVideosToLoad.ViewModelPickVideosToLoad.ViewModel  !    PickVideosToLoad.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:PickVideosToLoad     >	ViewModel(clr-namespace:PickVideosToLoad.ViewModel     -     � Title$  Pick Videos to Load��$��420.553��$��511.166��.���  � ColumnDefinitions �� $	��82* ��� $
��429* ��� $��6* �.+��� -    ��Name$ btnSetSourceFolder��$��Set source folder��$
��Left=�$��	20,18,0,0q�$	��Top=�$	��114��$��20��$��2���� -    ��Name$ lblSourceFolder��
�������$
��Left=�$��	20,38,0,0q�$	��Top=�$	��457��$��26��$��2������ *myConfig.sourceFolder+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� -   $ btnSetDestinationFolder��$��Set destination folder��$
��Left=�$��	20,80,0,0q�$	��Top=�$	��114��$��2���� -   $ lblDestinationFolder��
�������$
��Left=�$��
20,100,0,0q�$	��Top=�$	��457��$��2������ *myConfig.destinationFolder+$ TwoWay=��� -   $ btnTransfer��$��Transfer��$
��Left=�$��
20,309,0,0q�$	��Top=�$��75��$��2���� -    ��Name$ chkFirstFile�� d� IsThreeState .  $6��0Start from the first file (more ordered picking)��$
��Left=�$��
20,289,0,0q�$	��Top=�$��2�� d� 	IsChecked �� * myConfig.startFromTheFirstFile+$ TwoWay=�$ PropertyChanged=��� -   $ chkMove�� .  $%��Move files (delete from source)��$
��Left=�$��
20,269,0,0q�$	��Top=�$��2�� �� *" myConfig.moveAndDeleteFromSource+$ TwoWay=�$ PropertyChanged=�� -	   	 �Name$	 objProgressBar��$
��Left=�$��31��$��
20,334,0,0q�$	��Top=�$	��457��$��2���� -
   
 ��Name$
 txtGigas��$
��Left=�$��23��$��
28,196,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��79��$��2������ *myConfig.gigas+$ TwoWay=�$ PropertyChanged=��� -   $ lblSpace��$"��How much space is available?��$
��Left=�$��
20,165,0,0q�$	��Top=�$��2��$	��184���� -   $ lblGigas��$��Gigas��$��1��$
��Left=�$��
27,191,0,0q�$	��Top=��� -   $
 txtMegabits��$
��Left=�$��23��$��
28,227,0,0q�$
 Wrap=�$	��Top=�$��79��$��2������ *myConfig.megas+$ TwoWay=�$ PropertyChanged=��� -   $ lblMegabits��$��Megabits��$��1��$
��Left=�$��
27,222,0,0q�$	��Top=�