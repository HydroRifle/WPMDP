   M S B A M L   `   `   `  ����h  dWPPushNotification.ServerSideWeatherSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:WPPushNotification.ServerSideWeatherSimulator-WPPushNotification.ServerSideWeatherSimulator  5      N    HWPPushNotification.ServerSideWeatherSimulator.PushNotificationsLogViewer   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Kapp;clr-namespace:WPPushNotification.ServerSideWeatherSimulator     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � 	Resources  5	      J   DWPPushNotification.ServerSideWeatherSimulator.StatusToBrushConverter   statusConverter  GridHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value �� TextAlignment  DefaultGridElementStyle  StatusHeaderElementStyle �� BasedOn �� 	Converter �� RelativeSource �� 
ResourceId	 �� TypeInTargetAssembly&   System.Windows.Controls.DataGrid
 �� 
TargetType ��Name J� SnapsToDevicePixels �Name J� Opacity � Points �� Stretch �� Triggers P� Property P� Value �� 
TargetName  SimpleDataGrid ��Name � ColumnDefinitions � RowDefinitions   SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!   HeadersVisibilityConverter �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name - )SimpleDataGridColumnHeadersPresenterStyle   Column ��Name  �� CanContentScroll! ��Name" �� ScrollableHeight# � Maximum$ �� Orientation*% �� #ComputedVerticalScrollBarVisibility& �� ViewportHeight' �� ViewportSize( � Value) �� Mode* �� ScrollableWidth,+ �� %ComputedHorizontalScrollBarVisibility, �� ViewportWidth- �� Triggers # SimpleDataGridColumnHeaderStyle. �� Padding  DataGridRowStyle)  #System.Windows.Controls.DataGridRow/ ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter0   
ItemsPanelA	  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter1   DetailsVisibility$2   SelectiveScrollingOrientation;
  5System.Windows.Controls.SelectiveScrollingOrientation3 
  Vertical#4   RowDetailsScrollingConverter:  4System.Windows.Controls.Primitives.DataGridRowHeader
5   Row6  Name7 J� IsHitTestVisible8 �� Color  SimpleDataGridCellStyle*  $System.Windows.Controls.DataGridCell��%-  &	        &	      &	     &	 \    7  7   (�� �    5       $" DataGridSelectAllButtonStyle��"	  )&	 j    &	 �    &	 4    &	 �    (�� �    5�      *DataGrid.FocusBorderBrushKey+)&	 �      5   
   6%   �� 5   
   "  6(   .�5      ��  � ��$ Stretch=�6;   �� 5       � �� 5      �� 5      .R�5      �� $
��Wrap=�6$   $ Center=�68   $��Center=�6O   $��Bottom=�6l   #��z��6�   5      5      5      �� 5   
   " ��60   .�5      ��  �@��Padding$ 15,3,0,3q�6(   5      �� 5   
   " ��61   8   .�5      ��  � �� 6+   �� *Text+8   �� *Self+5      �� 5    
   " ��6   .�5!      ��  � �� 5"      �� 5#      "
 ��6&   .R�5$      � .+�5%      �� $ Border��6(   #��� ��68    . 6y   � 5&      $ Arrow��6&   
���   �$��Right=�6B   $��8,8,3,3q�6^   $
 0.15Q�6o    �   ��� ��� ��� ��� $ Uniform=�6�   $��Bottom=�6�   5'       5(      P� 5)       �@J�IsMouseOver . . �5*   "   �� $ Border��6;   # � ��6O    � ��5+      P� 5,       � �� . . �5-   "   �� $ Border��69   # � ��6M    � ��5.      P� 5/       � {� .  . �50   "   �� $ Arrow��6?    � y�$ 	Collapsed=�6R   51      53      55      56      �� 57   
   "  6'   .�58      ��  � ��# � ��6+   �� 59       � ��# � ��6+   �� 5:       � ��
 ���� 6,   �� 5;       � ��$ 0q�60   �� 5<       � ��$ 14(�6)   �� 5=       � ��$ Normal"�6+   �� 5>       � �� �#0921336+   �� 5?        �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�69   �� 5@       � �� . 6>   �� 5A       �@��PanningMode$
 Both=�69   �� 5B       �@��IsFlicksEnabled .  67   �� 5C       � �� 5D      �� 5E      "
  6&   .R�5F      �� $ DG_ScrollViewer��6'   |�.  ��5G      �� 5H   "   "
 ��62   .R�5I   &   �  5J   *   �� 5K   .   $
��Auto �6?   �� 5L   .   $��* �6?   �� 5M   .   $
��Auto �6?    5O   *   �� 5P   .   $
��Auto �6<   �� 5Q   .   $��* �6<   �� 5R   .   $
��Auto �6<   .+�5T   *   �� #��Z 61   |�.  ��6r   C� *�� $" DataGridSelectAllButtonStyle��"	  +y�6�   �� *HeadersVisibility+# Z # Z  �� "  ��6�  �� *CellsPanelHorizontalOffset+ �� "    5U   *   $! PART_ColumnHeadersPresenter��6I   $��1��6n   #���  6a  y�6~   �� *HeadersVisibility+# Z # Z  �� "  6a  �� 5V   *   $! PART_ScrollContentPresenter��6A   #  z��6f   $��2��6�   $��1��6�   �� 5W   *   $! PART_VerticalScrollBar��64   $��2��6T   ## z" 6d   $$ Vertical=�6�   $��1��6�   #y�z% 6�   #' z& 6W  ( 6�   �� *VerticalOffset+$) OneWay=� �� *TemplatedParent+6W  � 5X   *   $��1��6/   $��2��6?    5Y   .   �� 5Z   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� 5[   2   $��* �6C   .+�5]   .   �� $! PART_HorizontalScrollBar��68   $��1��6Z   ## z* 6j   $$ 
Horizontal=�6�   #y�z+ 6�   #' z, 6U  ( 6�   �� *HorizontalOffset+$) OneWay=� �� *TemplatedParent+6U  5^   +   5_   '   5`   #   .��5b      �� # z 6-   5c      5d      5f      - 5g      P� 5h       �@��
IsGrouping . . �5i      ��  � �� .  6F   5j      5l      �� 5n   
   "  68   .�5o      ��  � ��$ Stretch=�6;   �� 5p       � ��
 ���� 6+   �� 5q       � �� 5r      �� 5s      .R�5t      �� $
��Wrap=�6$   $ Center=�68   $
��Left=�6O   $	��Top=�6j   #��z��6�   $. 15,0,5,0q�6�   5u      5w      �� 5x       � �� �#0921336+   �� 5y       � ��$
 Bold"�6+   �� 5z       � ��$ 14(�6)   5{      �� 5|   
   "  6)   .�5}      ��  �@J�SnapsToDevicePixels . 64   �� 5~       �@=�ErrorTemplate 69   Z� �� 5       �@ ValidationErrorTemplate 5�      �� 5�      .R�5�      �� 
���  ��$��2,0,0,0q�65   $��!��6F   $��Center=�6O   5�      5�      �� 5�       � �� 5�      �� 5�      "
  6&   .R�5�      �� $/ 
DGR_Border��6!    . ._�5�         5�   "   �� 5�   &   $
��Auto �67   �� 5�   &   $��* �67    5�   "   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "     $��1��69   #��z0 6I   # z 6s   	  5�   "   $��1��6;   $��1��6K   #y�z1 6o  2 6X   �� *AreRowDetailsFrozen+# Z3 # Z4  �� "  6o    5�   "   $��2��64   $2 Vertical=�6E   y�6�   �� *HeadersVisibility+# Z5 # Z  �� "  5�      5�      5�      5�      5�      �� 5�   
   "  6B   .�5�      ��  � �� 5�      �� 5�      "
  6&   .R�5�      � .+�5�        $6 PART_FillerColumnHeader��63   7 .  6T   �� 5�      5�      5�      5�      5�      �� 5�   
   $8 	#FF000000��6J   �� 5�   
   "  60   .�5�      ��  � ��
 ���� 6+   �� 5�       � ��
 ���� 6,   �� 5�       � ��$ 0q�60   �� 5�       � �� 5�      �� 5�      "
  6&   .R�5�      �� #��z��6!   #��z��6M   #��z��6�    . ._�5�      �� # z 6/   5�      5�      5�      - 5�      P� 5�       �@ 
IsSelected . . �5�      ��  � �� �#400063   5�      5�      5�      .��5�      � -   5        9 �Name$9 
LayoutRoot��5�      .+�5�   
     -   5        :  Name$: tracelog��5�      ;   
IsReadOnly; . $��0q�67   
������ <   AutoGenerateColumns< .  $��Stretch=�6�   $��Stretch=�6�   =   GridLinesVisibility$
= None=�6�   >   HorizontalGridLinesBrush
> ���� ?   VerticalGridLinesBrush
? ���� @   AlternatingRowBackground
@ �����A   RowDetailsVisibilityMode$A VisibleWhenSelected=�6G  B   ColumnHeaderStyle#B �  6v  #���  6�  C   RowStyle#C �  6�  D   	CellStyle#D �  6  E   ColumnsE 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumnF   Header$F 	Timestamp��6%   G   Width5  /System.Windows.Controls.DataGridLengthConverter$
G Auto 6s   1  +System.Windows.Controls.DataGridBoundColumnH   ElementStyle#H [ 6�   I   BindingI 68   �� *	Timestamp+$) OneWay=�J �� StringFormat$J G��6�     5�      $F 
Message ID��6%   $G * 6d   K   MinWidth$	K 100Q�6n   #H [ 6}   I 69   �� *	MessageId+$) OneWay=�6}     5�      $F Notification Type��6%   $
G Auto 6r   #H [ 6   I 6@   �� *NotificationType+$) OneWay=�6     5�      $F Response Code��6%   $
G Auto 6h   #H [ 6u   I 6<   �� *
StatusCode+$) OneWay=�6u   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $F Connection Status��6)   $
G Auto 6D   L   CellTemplateL 5�      �� 5�      .R�5�      �� $��15,0,0,0q�6)   $��Center=�6;   M �� Orientation$M 
Horizontal=�6V   .+�5�   "   A� $��Center=�6*   $��0,0,6,0q�6E   $��6��6V   $��6��6`   ��6k   �� *DeviceConnectionStatus+# [  �� 5�   "   $��Center=�6,   ��6G   �� *DeviceConnectionStatus+$) OneWay=�5�      5�      5�        5�      $F Subscription Status��6)   $
G Auto 6F   L 5�      �� 5�      .R�5�      �� $��15,0,0,0q�6)   $��Center=�6;   $M 
Horizontal=�6V   .+�5�   "   A� $��Center=�6*   $��0,0,6,0q�6E   $��6��6V   $��6��6`   ��6k   �� *SubscriptionStatus+# [  �� 5�   "   $��Center=�6,   ��6G   �� *SubscriptionStatus+$) OneWay=�5�      5�      5�        5�      $F Notification Status��6)   $
G Auto 6F   L 5�      �� 5�      .R�5�      �� $��15,0,0,0q�6)   $��Center=�6;   $M 
Horizontal=�6V   .+�5�   "   A� $��Center=�6*   $��0,0,6,0q�6E   $��6��6V   $��6��6`   ��6k   �� *NotificationStatus+# [  �� 5�   "   $��Center=�6,   ��6G   �� *NotificationStatus+$) OneWay=�5�      5�      5�      N   RowDetailsTemplateN 5�      �� 5�      .R�5�      �� $M 
Horizontal=�6!   $��0q�6:   
������ .+�5�      �� $��Center=�6    $
��Bold"�6;   $��15,0,0,0q�6M   $��Channel URI:��6_   �� 5�      $��Center=�6$   $��5,0,5,0q�6?   ��6P   �� *
ChannelUri+5�      5�      O   RowHeaderStyleO 5�      �� 5�      " �6   .�5�      ��  � ��$ 0��6.   5�      5�      5�      5�      