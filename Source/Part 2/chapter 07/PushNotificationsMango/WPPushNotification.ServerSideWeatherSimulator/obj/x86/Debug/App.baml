   M S B A M L   `   `   `  ����1  -WPPushNotification.ServerSideWeatherSimulator7    1WPPushNotification.ServerSideWeatherSimulator.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        �� 	Resources  5       �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions   HeaderedOpacityRadioButton F�Name J� Opacity �� Stretch	 ��Name

 � Tag ��Name J� IsHitTestVisible �� Triggers P� Property P� Value �� 
TargetName  SliderButtonStyle �� RelativeSource �� Path �� StringFormat  SliderThumbStyle  HorizontalSlider ]� DecreaseRepeatButton ]� IncreaseRepeatButton ]� Thumb � Cursor �� Triggers  ComboBoxToggleButton � ColumnDefinitions ��Name  ComboBoxTextBox �� 	ClickMode d� 	IsChecked �� Mode �� SelectionBoxItem �� SelectionBoxItemTemplate  � AllowsTransparency! �� IsDropDownOpen" �� MaxDropDownHeight# � ActualWidth$ J� SnapsToDevicePixels% K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation&   DirectionalNavigation  InvisibleTextBox' ��Name��%p  '��       '�� N     &	  [    &	 �    &	 �
    &	 �    7  7  7  '�� �    7  &	 �    &	 �    '��     7  '�� 3    &	 q    �� 5   
   " ��6   .�5      ��  � ��$ Segoe UI)�6+   5      �� 5	   
   " ��6   .�5
      ��  � ��$ Segoe UI)�6+   �� 5       � ��$ 18(�6)   �� 5       � ��
 �����6+   �� 5       � ��$ 0,0,0,0q�6(   �� 5       �@�OverridesDefaultStyle . 66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �  5      �� 5   "   $
��Auto �60   �� 5   "   $��* �60   .+�5      �� $
��Left=�6(   #��z��6C   #��z��6c   �� 5      $��1��6/   $��0,4,0,0q�6<   5      5      5      5      �� 5   
   " 	�63   .�5      ��  �@�Cursor$
 Hand��6'   �� 5        � ��$ Segoe UI)�6+   �� 5!       � ��$ 18(�6)   �� 5"       � ��
 �����6+   �� 5#       � ��$ 0,4,0,0q�6(   �� 5$       �@�OverridesDefaultStyle . 66   �� 5%       �@�FocusVisualStyle 61   Z� �� 5&       � �� 5'      �� 5(      " 	�6&   .R�5)      � 
������  5*      �� 5+   "   $
��Auto �60   �� 5,   "   $��* �60   .+�5.      F� $ 	Checkmark��6#   $��16��66   $��12��6A   $
��Left=�6M   $��	-20,2,0,0q�6h   "����    �'	 ��'	 �O � j     	$ 0Q�6�   $ Uniform=�6�   
�������$	��2.5��6�   �� 5/      $	 Header��6(   $
��Left=�68   $	 1.0Q�6S   #��z
 6a   �� 50      $ Content��6/   $��1��6@   #��z��6M   $
 0.25Q�6p    .  6   51       52      P� 53       �@J�IsMouseOver . . �54   "   �� $ 	Checkmark��6)    �@J�Opacity$
 0.25Q�6S   �� 55   "   $ Content��6)    �@J�Opacity$	 0.5Q�6Q   56      P� 57       �@d�	IsChecked$
 TrueY�6;   . �58   "   �� $ 	Checkmark��6)    �@J�Opacity$ 1Q�6S   �� 59   "   $ Header��6)    �@J�Opacity$ 1Q�6P   �� 5:   "   $ Content��6)    �@J�Opacity$ 1Q�6Q   �� 5;   "   $ Content��6)    �@J�IsHitTestVisible . 6Z   5<      5>      5@      5A      �� 5B   
   " ��6*   .�5C      ��  �@J�SnapsToDevicePixels . 64   �� 5D       �@�OverridesDefaultStyle . 66   �� 5E       � �� .  6*   �� 5F       � ��
 ���� 6+   �� 5G       � |� .  6*   �� 5H       � ��
 �����6+   �� 5I       �@�Cursor$
 Hand��6'   �� 5J       � ��$ 18(�6)   �� 5K       � �� 5L      �� 5M      " ��6&   .R�5N      �� #��z��6!   ._�5O      �� $	 t��6(   $	��6,0q�63   $��Center=�6@   ��6[   ��  �� *TemplatedParent+$ Content�$ {0:0}°��5P       5Q      P� 5R       69   Z�  � ��. �5S   "   �� $ t��6)    �@J�Opacity$ 0Q�6K   5T      5V      5X      5Y      �� 5Z   
   " n�6)   .�5[      ��  �@J�SnapsToDevicePixels . 64   �� 5\       �@�OverridesDefaultStyle . 66   �� 5]       � �� 5^      �� 5_      " n�6&   .R�5`      �� $��4��6!   
�������6+   5a      5c      5d      �� 5e   
   " ��63   .R�5f      � ���	#B0000000.+�5g      ]� $��
PART_Track��6   $��1��6*   $��0,0,44,0q�67    5h      �� 5i      ���#3399ff$��Slider.DecreaseLarge��6<   8��  6[    5k      �� 5l      $��	0,0,-44,0q�6'   $��Slider.IncreaseLarge��6:   8�� ��6Y   ��  �� *TemplatedParent+$ Value�6�    5n      n� 5o      $��16��6    $
��-6,0q�6+   $ SizeWE��69   8�� 6I   5q      5r      5s      �� 5t   
   " ��6   .�5u      ��  �@J�SnapsToDevicePixels . 64   �� 5v       �@�OverridesDefaultStyle . 66    5w      P� 5x       �@��Orientation$ 
Horizontal=�61   . �5y      ��  � ��$	 104��61   �� 5z       � ��$ 21��62   �� 5{       � ��8   61   5|      5~      �� 5   
   " d�67   .R�5�      �  5�      �� 5�      �� 5�      $��20 �6'   .+�5�      �� $ Border��6   $��2��6)   ���	#B00000006=   F� 5�      $ Arrow��6   $��1��6&   $��Center=�66   $��Center=�6S   "����        � 	=  	= � z     	
�������6�   5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� $ Border��6    � �� �#2020206G   5�      5�      �� 5�   
   " ��62   .R�5�      �� $ PART_ContentHost��6   #��z��6/   |�.  6Y   5�      �� 5�   
   " ��6*   .�5�      ��  �@J�SnapsToDevicePixels . 64   �� 5�       �@�OverridesDefaultStyle . 66   �� 5�       � ��$
 Auto=�6K   �� 5�       � ��$
 Auto=�6I   �� 5�       � �� . 6>   �� 5�       � ��$ 18(�6)   �� 5�       � ��
 �����6+   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � .+�5�      d� $��ToggleButton��6+   $��2��6?   $ Press=�6O   |�.  8��   6s   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5�      �� 5�      $��ContentSite��6N   
�������$
��Left=�6a   $��
10,-1,23,1q�6|   $��Center=�6�   #��z 6�   #��z 6�   #��z��6   .  6V  � 5�      $��Popup��6$     . |�.  #��z! 6]   $��Bottom=�6�   $��Slide=�6�   .��5�   "   � $��DropDown��6'   #��z" 67   #��z# 6g   $ . .+�5�   &   �� $ DropDownBorder��6-   ���#FFFFFF6E   �� 5�   &   $ . .��5�   *   �� % . $& 	Contained=�6H   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�   
   " ��6.   .�5�      ��  �@J�SnapsToDevicePixels . 64   �� 5�       �@�OverridesDefaultStyle . 66   �� 5�       � ��
 �   �6+   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �� $��Border��6!   $ . ._�5�      �� $
��10,4q�6/   $��Center=�6=   5�       5�      P� 5�       �@��IsHighlighted . . �5�   "   �� $ Border��6)    � �� �#DDDDDD6S   5�      P� 5�       � {� .  . �5�   "   ��  � �� �#8888886?   5�      5�      5�      5�      �� 5�   
   " ��6)   .�5�      ��  �@J�SnapsToDevicePixels . 64   �� 5�       �@�FocusVisualStyle 61   Z� �� 5�       �@�OverridesDefaultStyle . 66   �� 5�       �@ TabNavigation$
 None=�6A   �� 5�       �@J�	AllowDrop . 6*   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � 
������ .+�5�      �� $	 	Watermark��6(   #��z
 6;   $��Italic$�6X    .  $ 0Q�6�   �� 5�      $' PART_ContentHost��66   $��0q�6+   66   5�       5�      P� 5�       � {� .  . �5�   "   ��  �@J�Opacity$	 0.5Q�6<   5�      P� 5�       � ��$  ��66   . �5�   "   �� $ 	Watermark��6)    �@J�Opacity$	 0.5Q�6S   5�      P� 5�       �@J�	IsFocused . . �5�   "   �� $ 	Watermark��6)    �@J�Opacity$ 0Q�6S   5�      5�      5�      5�      5�      5�      