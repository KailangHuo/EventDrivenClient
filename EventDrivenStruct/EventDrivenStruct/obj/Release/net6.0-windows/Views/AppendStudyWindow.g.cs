﻿#pragma checksum "..\..\..\..\Views\AppendStudyWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CEEBD9096BB2308A39A0DE29FA68718386DD8AC3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EventDrivenStruct.ViewModels.Converter;
using EventDrivenStruct.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace EventDrivenStruct.Views {
    
    
    /// <summary>
    /// AppendStudyWindow
    /// </summary>
    public partial class AppendStudyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 57 "..\..\..\..\Views\AppendStudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\Views\AppendStudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid button_grid;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Views\AppendStudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button confirm_button;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\Views\AppendStudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancle_button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.22.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EventDrivenStruct;component/views/appendstudywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AppendStudyWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.22.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\Views\AppendStudyWindow.xaml"
            ((EventDrivenStruct.Views.AppendStudyWindow)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.AddExamWindow_OnSizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.button_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.confirm_button = ((System.Windows.Controls.Button)(target));
            
            #line 114 "..\..\..\..\Views\AppendStudyWindow.xaml"
            this.confirm_button.IsEnabledChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Button_OnIsEnabledChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cancle_button = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\..\..\Views\AppendStudyWindow.xaml"
            this.cancle_button.IsEnabledChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Button_OnIsEnabledChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

