   M S B A M L   `   `   `  ����E  APushServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:WindowsPhone.Recipes.Push.Server.ViewModels+WindowsPhone.Recipes.Push.Server.ViewModels  5      g8clr-namespace:WindowsPhone.Recipes.Push.Server.Behaviors*WindowsPhone.Recipes.Push.Server.Behaviors  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35  �� DataTypeH   BWindowsPhone.Recipes.Push.Server.ViewModels.MessageStatusViewModel � 	Resources   Key �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid   AutoGenerateColumns   
IsReadOnly   SelectionMode   SelectionUnit	   CanUserResizeRows
   HeadersVisibility  ViewBackgroundBrush   GridLinesVisibility   AlternatingRowBackground   RowDetailsVisibilityMode  ViewTextBrush   RowBackground  DataGridColumnHeaderStyle   ColumnHeaderStyle  DataGridCellStyle   	CellStyle   Columns0  *System.Windows.Controls.DataGridTextColumn,  &System.Windows.Controls.DataGridColumn   Header  DefaultGridElementStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle   Binding �� Mode4  .System.Windows.Controls.DataGridTemplateColumn   CellTemplate �� Orientation A�Name �� Triggers �� Value �� Binding �� 
TargetName   RowDetailsTemplate �� StringFormat�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      @vm9clr-namespace:WindowsPhone.Recipes.Push.Server.ViewModels  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      F	behaviors8clr-namespace:WindowsPhone.Recipes.Push.Server.Behaviors  5      %@  (��       5
      *,  +)7  7  7  �� 6   "    6    5   
   ���� 5      DefaultGridElementStyle  " ��64   .�5      ��  �@��Padding$ 15,3,0,3q�6,   5      5      .R�5   
   � .+�5         .   . $ Single=�5      $ FullRow=�5      	 .  $
 Column=�5      $��2q�5      8��  $
 None=�5       �	#FF535353$ VisibleWhenSelected=�5      #���  5      #���  5       �	#FF424242# �  5    !   # �  6a   ��5      �� *Status+ 5"        5$      $ Pattern��6)   8   6:   �� *	Pattern+$ OneTime=�6d     5&      $ Message��6)   8   6:   �� *MessageType+$ OneTime=�6h     5(      $ Status��6-    5)      �� 5*      .R�5+   "   �� $��15,0,0,0q�6-   $��Center=�6?   $ 
Horizontal=�6Z   .+�5,   &   A� $ 	indicator��6.   $��Center=�6A   $��0,0,6,0q�6\   $��6��6m   $��6��6w   
���  ��6�   �� 5-   &   $��Center=�60   ��6K   �� *
StatusCode+$ OneTime=�5.   #    5/   "   �� 50   &   $ OK��6Q    62   �� *
StatusCode+.a�51   *   �� $ 	indicator��61    � ��
 ����6X   52   '   54      56        58      $ 
Connection��6-    59      �� 5:      .R�5;   "   �� $��15,0,0,0q�6-   $��Center=�6?   $ 
Horizontal=�6Z   .+�5<   &   A� $ 	indicator��6.   $��Center=�6A   $��0,0,6,0q�6\   $��6��6m   $��6��6w   �� 5=   &   $��Center=�60   ��6K   �� *DeviceConnectionStatus+$ OneTime=�5>   #    5?   "   �� 5@   &   $ 	Connected��6]    62   �� *DeviceConnectionStatus+.a�5A   *   �� $ 	indicator��61    � ��
 ����6X   5B   '   �� 5C   &   $ TempDisconnected��6]    62   �� *DeviceConnectionStatus+.a�5D   *   �� $ 	indicator��61    � ��
 �  ��6X   5E   '   �� 5F   &   $ Inactive��6]    62   �� *DeviceConnectionStatus+.a�5G   *   �� $ 	indicator��61    � ��
 �����6X   5H   '   �� 5I   &   $ NotApplicable��6]    62   �� *DeviceConnectionStatus+.a�5J   *   �� $ 	indicator��61    � ��
 �   �6X   5K   '   5M      5O        5Q      $ Subscription��6-    5R      �� 5S      .R�5T   "   �� $��15,0,0,0q�6-   $��Center=�6?   $ 
Horizontal=�6Z   .+�5U   &   A� $ 	indicator��6.   $��Center=�6A   $��0,0,6,0q�6\   $��6��6m   $��6��6w   �� 5V   &   $��Center=�60   ��6K   �� *SubscriptionStatus+$ OneTime=�5W   #    5X   "   �� 5Y   &   $ Active��6Y    62   �� *SubscriptionStatus+.a�5Z   *   �� $ 	indicator��61    � ��
 ����6X   5[   '   �� 5\   &   $ Expired��6Y    62   �� *SubscriptionStatus+.a�5]   *   �� $ 	indicator��61    � ��
 �  ��6X   5^   '   �� 5_   &   $ NotApplicable��6Y    62   �� *SubscriptionStatus+.a�5`   *   �� $ 	indicator��61    � ��
 �   �6X   5a   '   5c      5e        5g      $ Notification��6-    5h      �� 5i      .R�5j   "   �� $��15,0,0,0q�6-   $��Center=�6?   $ 
Horizontal=�6Z   .+�5k   &   A� $ 	indicator��6.   $��Center=�6A   $��0,0,6,0q�6\   $��6��6m   $��6��6w   �� 5l   &   $��Center=�60   ��6K   �� *NotificationStatus+$ OneTime=�5m   #    5n   "   �� 5o   &   $ Received��6Y    62   �� *NotificationStatus+.a�5p   *   �� $ 	indicator��61    � ��
 ����6X   5q   '   �� 5r   &   $ 	QueueFull��6Y    62   �� *NotificationStatus+.a�5s   *   �� $ 	indicator��61    � ��
 �  ��6X   5t   '   �� 5u   &   $ 
Suppressed��6Y    62   �� *NotificationStatus+.a�5v   *   �� $ 	indicator��61    � ��
 �����6X   5w   '   �� 5x   &   $ Dropped��6Y    62   �� *NotificationStatus+.a�5y   *   �� $ 	indicator��61    � ��
 �����6X   5z   '   �� 5{   &   $ NotApplicable��6Y    62   �� *NotificationStatus+.a�5|   *   �� $ 	indicator��61    � ��
 �   �6X   5}   '   5      5�       5�      �� 5�      .R�5�      �� $��2q�6%   ���	#FF2A2A2A.+�5�      �� $��1q�6)   .+�5�   "   �� $
��Bold"�6,   $��0,0,0,1q�6>   $��
Time Stamp��6O   �� 5�   "   ��6,   �� *	Timestamp+$ OneTime=�$ G��5�      �� 5�      $��1q�6)   .+�5�   "   �� $
��Bold"�6,   $��0,0,0,1q�6>   $��
Message Id��6O   �� 5�   "   ��6,   �� *	MessageId+$ OneTime=�5�      �� 5�      $��1q�6)   .+�5�   "   �� $
��Bold"�6,   $��0,0,0,1q�6>   $��Channel URI��6O   �� 5�   "   ��6,   �� *
ChannelUri+$ OneTime=�5�      5�      5�      5�      5�      5�      5�      