   M S B A M L   `   `   `  ����L  HEventDrivenStruct, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EventDrivenStruct.ViewsEventDrivenStruct.Views  5   	   _4clr-namespace:EventDrivenStruct.ViewModels.Converter&EventDrivenStruct.ViewModels.Converter  5   	   M ICustomizedTitleBar, Version=1.0.4.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:CustomizedTitleBar;assembly=CustomizedTitleBarCustomizedTitleBar 5	   	   ,    &EventDrivenStruct.Views.MainWindow_Two   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   7local%clr-namespace:EventDrivenStruct.Views     5   	   J	converter4clr-namespace:EventDrivenStruct.ViewModels.Converter     5   	   QtitleBar<clr-namespace:CustomizedTitleBar;assembly=CustomizedTitleBar     5	   	   -   5          �Name$  
ThisWindow��5   	    � Title$ MainWindow_Two��5   	   $	��900��6    $
��1600��6-    � 
ResizeMode$ 	CanResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5      F   @EventDrivenStruct.ViewModels.Converter.BoolToVisibilityConverter   BooltoVisibilityConverterA   ;EventDrivenStruct.ViewModels.Converter.ReverseBoolConverter  ReverseBoolConverter  Study_ComboBox_Template � RowDefinitions � ColumnDefinitions �� Orientation  App_ListBox_Template �� CanHorizontallyScroll	 �� CanVerticallyScroll
 � ContextMenu m� Command��%v  &	        &	      &	 &     &	 e      5   
   6.     5   
   6)   �� 5   
   .R�5      �  5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5      � $
��Row1��6$   $��0��6    5      �� 5      $��* �6+   �� 5      $��* �6+   .+�5!      �� $��PatientNameLabel��60   $��0��6    $��5q�5"       $��Light"�6+   $
��Left=�5#      $	��Top=�68   ��5!   H   �� *PatientName+5#   8   �� 5$      $��PatientGenderLabel��61   $��1��6    $��5q�5%       $��Light"�6+   $��Right=�5&       $	��Top=�6<   ��5$   K   �� *PatientGender+5&   <   5(      � 5+      $
��Row2��6$   $��1��6   $	��220��60    5,      �� 5-      $��* �6+   �� 5.      $��56 �6+   .+�52      �� $ 
Horizontal=�6!   .+�53      �� $��PatientAgeLabel��6$   $��Light"�54   !   $��5,0,5,0q�55       $
��Left=�56       $��Center=�6;   ��53   ;   �� *
PatientAge+56   ;   �� 57      $��PatientUidLabel��6$   $��Light"�58   $   $
��Left=�59   $   $��Center=�6?   ��57   ;   �� *
StudyUid+59   ?   5:      �� 5<      $��1��6   $��3q�5=      $��Center=�5>      $��Right=�5?      $��Bottom=�69   $��50��5@      ���	#FF007FFF
���������5<   -   �� *LockingStatusStr+��5A      �� *LockSwitchCommand+{�5B      �� *
IsLockable+5C   4   5F      5H      5I      �� 5L   
   .R�5M      �� $ 
Horizontal=�6   $ False��62   $	 False��6P   .+�5N      �� $��50��6   $��0,0,5,0q�5O      $
��Wrap=�5Q      $��Center=�5R      $��Center=�5S      $��15(�5T      $��Light"�5U      ��5P      �� *	AppName+5U      �� 5V      $��1��6   $��2q�6#   $��25��67   
���   �6C   
 5W      �� 5X      .@�5Y      m� $��关闭该应用��6#    62   �� *CloseThisAppCommand+5Z      5\      5]      5_      .��5a      �  5b   
   �� 5c      $��40 �6   �� 5d      $��35 �6   �� 5e      $	��580 �6   �� 5f      $��* �6   .+�5j   
   8  2CustomizedTitleBar.CustomizedTitileBar_UserControl  $��40��5l         
HostWindow 5k      ��  �� ElementName$ 
ThisWindow��5l      �� 5p   
   $��1��6   $ 
Horizontal=�6"   .+�5r      �� -   5        $��ActionButton��5r      $��Light"�5s      $��0,3, 7,0q�5t      $��30��5u      $	��180��5v      $	��Top=�5w      $
��Left=�5x      ���	#FF007FFF
���������5z      �� *ActionButtonContent+��5{      �� *TEST_COMMAND+�� 5      -   5        $��AddPatientButton��5      $��Light"�5�      $��0,3,7,0q�5�      $��30��5�      $	��180��5�      $	��Top=�5�      $
��Left=�5�      $��Add Patient��5�      ��5�      �� *TEST_ADD_COMMAND+�� 5�      -   5        $��TriggeredActionButton��5�      $��Light"�5�      $��0,3,7,0q�5�      $��30��5�      $	��180��5�      $	��Top=�5�      $
��Left=�5�      ���	#FFBF0816
�������$��清除所有检查��5�      y�5�      �� *TriggeredActionBool+ �� 	Converter# [  ��5�      �� *TEST_CLEARALL_COMMAND+�� 5�      $��0,3,0,0q�6   $��Light"�5�      $��30��5�      $	��180��5�      $	��Top=�5�      $
��Left=�5�      $��Append to CurrentStudy��5�      ��5�      �� *TEST_APPEND_COMMAND+5�      � 5�   
   $��2��6    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      � -   5        $��LeftCol��5�   #   $��0��6    5�      �� 5�      $
��Auto �6$   �� 5�      $��* �6$    5�      �� 5�      $��* �6'   .+�5�      � -   5         �Name$ HeaderGridLeft��5�   $   $��0��6    5�      �� 5�      $
��Auto �6+   �� 5�      $
��auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��auto �6+   .+�5�      �� $��5��6   �� 5�      -   5        $��PaButtonLeft��5�      $��2q�5�      $��0��5�      $��50��5�      $��50��6%   $��PA��5�      $��20(�6&   $��Light"�64   $y�Visible=�5�      $|�False��5�       � Cursor$
 Hand��5�      $
��Left=�5�      $��Bottom=�64   ��5�      �� *GotoPaTabCommand+5�      � 5�      -   5        $��StudiesComboGridLeft��5�   +   $��1��6   $��0,0,2,2q�5�      $��50��5�      $	��240��5�      $��Bottom=�5�      y�5�      �� *!StudyContainerViewModel.HasItem+# [  .+�5�      �� -	   5        $��StudiesComboBoxLeft��5�   #    J� 	AllowDrop$ False��5�   #   $
 Hand��5�   #   $��50��5�   #   $	��240��5�   #    �� 
IsReadOnly$
 True��5�   #   #��[ 5�   #   ��5�   #   �� *)'StudyContainerViewModel.StudyViewModels+ �� SelectedItem 5�   #   �� *'%StudyContainerViewModel.SelectedStudy+5�      �� 5�      $	��222��6!   $��50��6-   $
 Hand��5�   !   $
��Left=�5�   !   $��Center=�5�   !   $��2,1,0,2q�5�   !   #��[ 5�   !   $
��Left=�5�   !   $��Center=�5�   !   $��0q�5�   !   ��5�   !   �� *'%StudyContainerViewModel.SelectedStudy+ �� Mode$ TwoWay=� � ToolTip 5�   !   �� *97StudyContainerViewModel.SelectedStudy.RepresentationStr+��5�   !   �� *0.StudyContainerViewModel.TriggerSelectedCommand+
 5�      �� 5�   "   .@�5�   &   m� $��关闭当前检查��6/    6?   �� *.,StudyContainerViewModel.CloseSelectedCommand+m� 5�   &   $��关闭所有检查��6/    6?   �� *)'StudyContainerViewModel.ClearAllCommand+5�   #   5�      5�      �� 5�      -
   5        $��AppsListBoxLeft��5�   .   $��2��6   $��0,0,0,2q�5�      $��50��5�      $
��Left=�5�      $��Bottom=�69   #��[ 5�      y�5�      �� *AppTabViewModel.IsExpanded+# [  ��5�      �� *><AppTabViewModel.SelectedAppItemContainer.VisibleAppModelList+ 5      �� *ZXAppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[0].AppItemSelected+$ TwoWay=���5     �� 5     .R�5  "   �� $ 
Horizontal=�6-   6H   5     5     �� 5     $��3��6   $��2q�6-   $��+��5     $��0��5     
�������$��0q�5     $��40��5     $��40��6(   $��30(�64   $��Light"�6B   y�5     �� *AppTabViewModel.IsExpanded+# [  ��5     �� *AppTabViewModel.AddAppCommand+5  B   5     � 5     $��1��6   .+�5     � -   5        $��PrimaryWindowPreview��5     $��Stretch=�68   .+�5     � $	��Top=�6   $��0,0,0,0q�67   .+�5     �� 
���   �$��1q�69   $	��500��6M   �� 5     $ Vertical=�6)   .+�5  "   �� $��屏幕1��6(   $��50��66   $��30(�6B   $��Light"�6P   �� 5  "   $��40(�6p   $��Center=�6~   $��Light"�6�   ��6,   �� *42ScreenManagerViewModel.ScreenCollection[0].AppText+6�   �� 5  "   $��25(�6r   $��Center=�6�   $��0,0,0,30q�6�   $��Light"�6�   ��6,   �� *64ScreenManagerViewModel.ScreenCollection[0].StudyText+6�   5     5      5!     5"     5$     � 5'     -   5        $��RightCol��5'  #   $��1��6    5(     �� 5)     $
��Auto �6$   �� 5*     $��* �6$   .+�5.     � -   5        $ HeaderGridRight��5.  $   $��0��6    5/     �� 50     $
��auto �6+   �� 51     $
��auto �6+   �� 52     $
��Auto �6+   �� 53     $
��auto �6+   .+�55     �� $��5��6   �� 58     -   5        $��PaButtonRight��5>     $��2q�58     $��1��5:     $��50��5;     $��50��6%   $��PA��5<     $��20(�6&   $��Light"�64   $y�Visible=�5=     $|�False��5?     $
 Hand��5@     $
��Left=�5A     $��Bottom=�64   ��59     �� *GotoPaTabCommand+5B     � 5E     -   5        $��StudiesComboGridRight��5E  +   $��1��6   $��0,0,2,2q�5G     $��50��5H     $	��240��5I     $��Bottom=�5J     y�5F     �� *!StudyContainerViewModel.HasItem+# [  .+�5L     �� -   5        $��StudiesComboBoxRight��5L  #   $ False��5M  #   $
 Hand��5N  #   $��50��5O  #   $	��240��5P  #   $
 True��5Q  #   #��[ 5R  #   ��5S  #   �� *)'StudyContainerViewModel.StudyViewModels+ 5T  #   �� *'%StudyContainerViewModel.SelectedStudy+5V     �� 5W     $	��222��6!   $��50��6-   $
 Hand��5X  !   $
��Left=�5Y  !   $��Center=�5Z  !   $��2,1,0,2q�5[  !   #��[ 5\  !   $
��Left=�5^  !   $��Center=�5_  !   $	 TBA��5`  !   $��0q�5a  !   ��5]  !   �� *'%StudyContainerViewModel.SelectedStudy+$ TwoWay=���5b  !   �� *0.StudyContainerViewModel.TriggerSelectedCommand+
 5d     �� 5e  "   .@�5f  &   m� $��关闭当前检查��6/    6?   �� *.,StudyContainerViewModel.CloseSelectedCommand+m� 5g  &   $��关闭所有检查��6/    6?   �� *)'StudyContainerViewModel.ClearAllCommand+5h  #   5k     5l     �� 5o     -   5        $��AppsListBoxRight��5o  .   $��2��6   $��0,0,0,2q�5p     $��50��5r     $
��Left=�5s     $��Bottom=�69   #��[ 5t     y�5q     �� *AppTabViewModel.IsExpanded+# [  ��5u     �� *><AppTabViewModel.SelectedAppItemContainer.VisibleAppModelList+ 5v     �� *ZXAppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[1].AppItemSelected+$ TwoWay=���5y     �� 5z     .R�5{  "   �� $ 
Horizontal=�6-   6H   5|     5~     �� 5�     $��3��6   $��2q�6-   $��+��5�     $��1��5�     
�������$��0q�5�     $��40��5�     $��40��6(   $��30(�64   $��Light"�6B   y�5�     �� *AppTabViewModel.IsExpanded+# [  ��5�     �� *AppTabViewModel.AddAppCommand+5�  B   5�     � 5�     $��1��6   .+�5�     � -   5        $��SecondaryWindowPreview��5�     $��Stretch=�6:   .+�5�     � $	��Top=�6   $��0,0,0,0q�67   .+�5�     �� 
���   �$��1q�69   $	��500��6M   �� 5�     $ Vertical=�6)   .+�5�  "   �� $��屏幕2��6(   $��50��66   $��30(�6B   $��Light"�6P   �� 5�  "   $��40(�6p   $��Center=�6~   $��Light"�6�   ��6,   �� *42ScreenManagerViewModel.ScreenCollection[1].AppText+6�   �� 5�  "   $��25(�6r   $��Center=�6�   $��0,0,0,30q�6�   $��Light"�6�   ��6,   �� *64ScreenManagerViewModel.ScreenCollection[1].StudyText+6�   5�     5�     5�     5�     5�     5�     � 5�  
   $��3��6   $��	0,-20,0,0q�6    5�     �� 5�     $��20 �6    �� 5�     $��* �6    .+�5�     �� $��0��6   $ 
Horizontal=�6&   .+�5�     �� $��System Information��6   $��17(�5�     $��Light"�6*   $��0,0,5,0q�5�     �� 5�     $��Clear��6   $��20��6)   $	��100��65   $��12(�6A   $��Light"�6O   ���	#FF007FFF
���������5�     �� *" SystemInfoCollector.ClearCommand+5�     �� 5�     $��1��6   $��1q�6"   
���   �66   �� 5�     -   5        $��1��5�     $��Disabled=�5�     .��5�     ��  �� 
IsReadOnly$
 True��6    �� TextWrapping$
 Wrap=�6,   $��0q�5�     $��15(�5�     $��Light"�6(   ��5�     �� *SystemInfoCollector.Content+5�     5�     5�     5�     