   M S B A M L   `   `   `  ���� L  HEventDrivenStruct, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EventDrivenStruct.ViewsEventDrivenStruct.Views  _4clr-namespace:EventDrivenStruct.ViewModels.Converter&EventDrivenStruct.ViewModels.Converter  M ICustomizedTitleBar, Version=1.0.4.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:CustomizedTitleBar;assembly=CustomizedTitleBarCustomizedTitleBar ,    &EventDrivenStruct.Views.MainWindow_Two   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:EventDrivenStruct.Views     J	converter4clr-namespace:EventDrivenStruct.ViewModels.Converter     QtitleBar<clr-namespace:CustomizedTitleBar;assembly=CustomizedTitleBar     -     �Name$  
ThisWindow�� � Title$ MainWindow_Two��$	��900��$
��1600�� � 
ResizeMode$ 	CanResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources F   @EventDrivenStruct.ViewModels.Converter.BoolToVisibilityConverter   BooltoVisibilityConverterA   ;EventDrivenStruct.ViewModels.Converter.ReverseBoolConverter  ReverseBoolConverter  Study_ComboBox_Template � RowDefinitions � ColumnDefinitions �� Orientation  App_ListBox_Template �� CanHorizontallyScroll	 �� CanVerticallyScroll
 � ContextMenu m� Command��%K  &	        &	      &	 
     &	         �� .R��  �� $��* ��� $��* �.+�� $
��Row1��$��0�� �� $��* ��� $��* �.+��� $��PatientNameLabel��$��0��$��5q�$��Light"�$
��Left=�$	��Top=����� *PatientName+�� $��PatientGenderLabel��$��1��$��5q�$��Light"�$��Right=�$	��Top=����� *PatientGender+� $
��Row2��$��1��$	��220�� �� $��* ��� $��56 �.+��� $ 
Horizontal=�.+��� $��PatientAgeLabel��$��Light"�$��5,0,5,0q�$
��Left=�$��Center=����� *
PatientAge+�� $��PatientUidLabel��$��Light"�$
��Left=�$��Center=����� *
StudyUid+�� $��1��$��3q�$��Center=�$��Right=�$��Bottom=�$��50�����	#FF007FFF
����������� *LockingStatusStr+���� *LockSwitchCommand+{��� *
IsLockable+�� .R��� $ 
Horizontal=�$ False��$	 False��.+��� $��50��$��0,0,5,0q�$
��Wrap=�$��Center=�$��Center=�$��15(�$��Light"����� *	AppName+�� $��1��$��2q�$��25��
���   �
 �� .@�m� $��关闭该应用�� �� *CloseThisAppCommand+.���  �� $��40 ��� $��35 ��� $	��580 ��� $��* �.+�8  2CustomizedTitleBar.CustomizedTitileBar_UserControl  $��40��   
HostWindow ��  �� ElementName$ 
ThisWindow���� $��1��$ 
Horizontal=�.+��� -   $��ActionButton��$��Light"�$��0,3, 7,0q�$��30��$	��180��$	��Top=�$
��Left=����	#FF007FFF
����������� *ActionButtonContent+���� *TEST_COMMAND+�� -   $��AddPatientButton��$��Light"�$��0,3,7,0q�$��30��$	��180��$	��Top=�$
��Left=�$��Add Patient������ *TEST_ADD_COMMAND+�� -   $��TriggeredActionButton��$��Light"�$��0,3,7,0q�$��30��$	��180��$	��Top=�$
��Left=����	#FFBF0816
�������$��清除所有检查��y��� *TriggeredActionBool+ �� 	Converter# [  ���� *TEST_CLEARALL_COMMAND+�� $��0,3,0,0q�$��Light"�$��30��$	��180��$	��Top=�$
��Left=�$��Append to CurrentStudy������ *TEST_APPEND_COMMAND+� $��2�� �� $��* ��� $��* �.+�� -   $��LeftCol��$��0�� �� $
��Auto ��� $��* � �� $��* �.+�� -    �Name$ HeaderGridLeft��$��0�� �� $
��Auto ��� $
��auto ��� $
��Auto ��� $
��auto �.+��� $��5���� -   $��PaButtonLeft��$��2q�$��0��$��50��$��50��$��PA��$��20(�$��Light"�$y�Visible=�$|�False�� � Cursor$
 Hand��$
��Left=�$��Bottom=����� *GotoPaTabCommand+� -   $��StudiesComboGridLeft��$��1��$��0,0,2,2q�$��50��$	��240��$��Bottom=�y��� *!StudyContainerViewModel.HasItem+# [  .+��� -	   $��StudiesComboBoxLeft�� J� 	AllowDrop$ False��$
 Hand��$��50��$	��240�� �� 
IsReadOnly$
 True��#��[ ���� *)'StudyContainerViewModel.StudyViewModels+ �� SelectedItem �� *'%StudyContainerViewModel.SelectedStudy+�� $	��222��$��50��$
 Hand��$
��Left=�$��Center=�$��2,1,0,2q�#��[ $
��Left=�$��Center=�$��0q����� *'%StudyContainerViewModel.SelectedStudy+ �� Mode$ TwoWay=� � ToolTip �� *97StudyContainerViewModel.SelectedStudy.RepresentationStr+���� *0.StudyContainerViewModel.TriggerSelectedCommand+
 �� .@�m� $��关闭当前检查�� �� *.,StudyContainerViewModel.CloseSelectedCommand+m� $��关闭所有检查�� �� *)'StudyContainerViewModel.ClearAllCommand+�� -
   $��AppsListBoxLeft��$��2��$��0,0,0,2q�$��50��$
��Left=�$��Bottom=�#��[ y��� *AppTabViewModel.IsExpanded+# [  ���� *><AppTabViewModel.SelectedAppItemContainer.VisibleAppModelList+ �� *ZXAppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[0].AppItemSelected+$ TwoWay=����� .R��� $ 
Horizontal=��� $��3��$��2q�$��+��$��0��
�������$��0q�$��40��$��40��$��30(�$��Light"�y��� *AppTabViewModel.IsExpanded+# [  ���� *AppTabViewModel.AddAppCommand+� $��1��.+�� -   $��PrimaryWindowPreview��$��Stretch=�.+�� $	��Top=�$��0,0,0,0q�.+��� 
���   �$��1q�$	��500���� $ Vertical=�.+��� $��屏幕1��$��50��$��30(�$��Light"��� $��40(�$��Center=�$��Light"����� *42ScreenManagerViewModel.ScreenCollection[0].AppText+�� $��25(�$��Center=�$��0,0,0,30q�$��Light"����� *64ScreenManagerViewModel.ScreenCollection[0].StudyText+� -   $��RightCol��$��1�� �� $
��Auto ��� $��* �.+�� -   $ HeaderGridRight��$��0�� �� $
��auto ��� $
��auto ��� $
��Auto ��� $
��auto �.+��� $��5���� -   $��PaButtonRight��$��2q�$��1��$��50��$��50��$��PA��$��20(�$��Light"�$y�Visible=�$|�False��$
 Hand��$
��Left=�$��Bottom=����� *GotoPaTabCommand+� -   $��StudiesComboGridRight��$��1��$��0,0,2,2q�$��50��$	��240��$��Bottom=�y��� *!StudyContainerViewModel.HasItem+# [  .+��� -   $��StudiesComboBoxRight��$ False��$
 Hand��$��50��$	��240��$
 True��#��[ ���� *)'StudyContainerViewModel.StudyViewModels+ �� *'%StudyContainerViewModel.SelectedStudy+�� $	��222��$��50��$
 Hand��$
��Left=�$��Center=�$��2,1,0,2q�#��[ $
��Left=�$��Center=�$	 TBA��$��0q����� *'%StudyContainerViewModel.SelectedStudy+$ TwoWay=����� *0.StudyContainerViewModel.TriggerSelectedCommand+
 �� .@�m� $��关闭当前检查�� �� *.,StudyContainerViewModel.CloseSelectedCommand+m� $��关闭所有检查�� �� *)'StudyContainerViewModel.ClearAllCommand+�� -   $��AppsListBoxRight��$��2��$��0,0,0,2q�$��50��$
��Left=�$��Bottom=�#��[ y��� *AppTabViewModel.IsExpanded+# [  ���� *><AppTabViewModel.SelectedAppItemContainer.VisibleAppModelList+ �� *ZXAppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[1].AppItemSelected+$ TwoWay=����� .R��� $ 
Horizontal=��� $��3��$��2q�$��+��$��1��
�������$��0q�$��40��$��40��$��30(�$��Light"�y��� *AppTabViewModel.IsExpanded+# [  ���� *AppTabViewModel.AddAppCommand+� $��1��.+�� -   $��SecondaryWindowPreview��$��Stretch=�.+�� $	��Top=�$��0,0,0,0q�.+��� 
���   �$��1q�$	��500���� $ Vertical=�.+��� $��屏幕2��$��50��$��30(�$��Light"��� $��40(�$��Center=�$��Light"����� *42ScreenManagerViewModel.ScreenCollection[1].AppText+�� $��25(�$��Center=�$��0,0,0,30q�$��Light"����� *64ScreenManagerViewModel.ScreenCollection[1].StudyText+� $��3��$��	0,-20,0,0q� �� $��20 ��� $��* �.+��� $��0��$ 
Horizontal=�.+��� $��System Information��$��17(�$��Light"�$��0,0,5,0q��� $��Clear��$��20��$	��100��$��12(�$��Light"����	#FF007FFF
����������� *" SystemInfoCollector.ClearCommand+�� $��1��$��1q�
���   ��� -   $��1��$��Disabled=�.����  �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�$��0q�$��15(�$��Light"����� *SystemInfoCollector.Content+