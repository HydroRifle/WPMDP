   M S B A M L   `   `   `  ����E  APushServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:WindowsPhone.Recipes.Push.Server.ViewModels+WindowsPhone.Recipes.Push.Server.ViewModels  5      g8clr-namespace:WindowsPhone.Recipes.Push.Server.Behaviors*WindowsPhone.Recipes.Push.Server.Behaviors  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35  �� DataTypeU   OWindowsPhone.Recipes.Push.Server.ViewModels.CustomTileImagePushPatternViewModel   ViewBorderStyle.  (System.Windows.Interactivity.Interaction   	BehaviorsE   ?WindowsPhone.Recipes.Push.Server.Behaviors.VisualBinderBehavior  NullTileImageConverter �� 	Converter J� InputBindings d� MouseAction �� Command �Name �� TextAlignment ��Name	 �� TextWrapping
 �� UpdateSourceTrigger � Minimum � Maximum �� IsSnapToTickEnabled �� TickFrequency �� AutoToolTipPlacement � Value �� SelectedIndex$ �� IsSynchronizedWithCurrentItem �� FallbackValue J� RenderTransformOrigin �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      @vm9clr-namespace:WindowsPhone.Recipes.Push.Server.ViewModels  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      F	behaviors8clr-namespace:WindowsPhone.Recipes.Push.Server.Behaviors  5      %�  (��       5
      *,  +)7  �� 6   "    6   .R�5   
   �� #���   6   ._�5      � .+�5      �� $
��Auto��6)   $	��Top=�66   ��5      �� 5      $��Tile Message��6$   $
��Bold"�68   .��5      � .+�5      �� $��1��6!   $��1��61   $��5q�6>   ���	#FFDEDEDE$
��Auto��6j   $
��Auto��6w   ���	#FF333333$��Right=�6�   $	��Top=�6�   ._�5      � $
��Auto��6#   $
��Auto��60   .+�5   "   � $	��173��6'   $	��173��63    5   &     5   *   .+�5   &   �� $��UniformToFill=�6�   ��6,   �� *TileBackground+8   6�   �� 5   &   $��Center=�60   $��4,48,4,0q�6M   $
��Wrap=�6_   $	��Top=�6�   ��6s   �� *
TextSize+��6�   �� *TextColors/+��6�   �� *
FreeText+6�   �� 5   &   
������ 
���   � 5   *   d� 5   .   $ 	LeftClicke�6;    6S   �� *PickImageCommand+5   '   5    #   � 5!   "   $ counter��6'   $��Right=�68   $��30.733��6T   $��	0,12,12,0q�6d   $	��Top=�6w   $��30.733��6�   .+�5"   &   A� 
���   �$��Stretch=�6;   $
��Auto��6Y   $��0q�6g   
���   �$��Stretch=�6�   $
��Auto��6�   $��0��6�   �� 5#   &   $��Center=�60   $
��Auto��6M   $
��Wrap=�6[   $��Stretch=�6�   $
��Auto��6�   $ Center=�6�   $��21.333(�6�   $��0,-0.321,0,2.679q�6�   ��6o   �� *Count+6�   5$   #   �� 5%   "   $��22��6,   $��NoWrap=�68   $��Bottom=�6e   $��6,0,6,6q�6�   $��18.667(�6�   $
��Bold"�6�   $	��158��6�   $��Center=�6�   ��6N   �� *Title+6�   �� 5&   "   $ ImageComment��6,   $��Center=�6B   $
��Wrap=�6_   $��Center=�6s   5'      5(      �� 5)      $��0��6%   $��Stretch=�62   $��	0,0,187,0q�6P   .+�5+      �� $
��Wrap=�6(   $��	Free Text��6<   $��4q�6M   �� 5,      $
	 Wrap=�6&   $��4q�6:   ��6E   �� *
FreeText+$
 PropertyChanged=��� 5.      $
��Wrap=�6(   $��	Text Size��6<   $��4q�6M   �� 5/      $ 18Q�6%   $ 32Q�62    . $ 2Q�6Z   $ BottomRight=�6l    6�   �� *
TextSize+$
 PropertyChanged=��� 51      $
��Wrap=�6(   $��
Text Color��6<   $��4q�6N   �� 52      $ 0��6J   $
 TrueY�6\   ��6'   �� *
TextColors+��53   "   �� 54   &   .R�55   *   ]� $��36��64   .+�56   .   �� 
���   �$	��0.5��6G   $	��120��6�   $��28��6�   $��4q�6�   ��6]   �� $ Transparent��6�   57   +   58   '   5:      �� 5<      $
��Wrap=�6(   $��Count��6<   $��4q�6I   �� 5=      $ 0Q�6%   $ 99Q�61    . $ 1Q�6Y   $ BottomRight=�6k    6�   �� *Count+$
 PropertyChanged=��� 5?      $
��Wrap=�6(   $��Title��6<   $��4q�6I   �� 5@      $
	 Wrap=�6&   $��4q�6:   ��6E   �� *Title+$
 PropertyChanged=�5B      5C      5D      �� 5F      $
��Send��6   $
��Auto��6(   $��Bottom=�6V   $��64��6�   $ 2.643,1.088&�6�   $��Right=�6�   $��
0,0,0,2.68q�6�   ��66   �� *SendCommand+6�   5H      5I       5J   
   �� 5K      $ 0��64    6   �� *Count+.a�5L      �� $ counter��6    � y�$ Hidden=�6D   5M      �� 5N       6   �� *TileBackground+ 6=   Z� .a�5O      �� $ ImageComment��6    � ��$! Click here to pick an image��6C   5P      5R      5T      