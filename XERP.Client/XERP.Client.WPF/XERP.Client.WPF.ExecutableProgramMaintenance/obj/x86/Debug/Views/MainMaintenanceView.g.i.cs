﻿#pragma checksum "..\..\..\..\Views\MainMaintenanceView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4150B27C5A329567BE7C4CE2F5A8E631"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
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


namespace XERP.Client.WPF.ExecutableProgramMaintenance.Views {
    
    
    /// <summary>
    /// MainMaintenanceView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainMaintenanceView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuNewExecutableProgram;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuSave;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabctrlMain;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabDetail;
        
        #line default
        #line hidden
        
        
        #line 222 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtKey;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ghost;
        
        #line default
        #line hidden
        
        
        #line 336 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabList;
        
        #line default
        #line hidden
        
        
        #line 345 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgMain;
        
        #line default
        #line hidden
        
        
        #line 356 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgMainPasteRow;
        
        #line default
        #line hidden
        
        
        #line 414 "..\..\..\..\Views\MainMaintenanceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ghost2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/XERP.Client.WPF.ExecutableProgramMaintenance;component/views/mainmaintenanceview" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainMaintenanceView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((XERP.Client.WPF.ExecutableProgramMaintenance.Views.MainMaintenanceView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((XERP.Client.WPF.ExecutableProgramMaintenance.Views.MainMaintenanceView)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.UserControl_PreviewKeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mnuNewExecutableProgram = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.mnuSave = ((System.Windows.Controls.MenuItem)(target));
            
            #line 45 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.mnuSave.Click += new System.Windows.RoutedEventHandler(this.SaveMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 115 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 117 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tabctrlMain = ((System.Windows.Controls.TabControl)(target));
            return;
            case 7:
            this.tabDetail = ((System.Windows.Controls.TabItem)(target));
            return;
            case 8:
            this.txtKey = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ghost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 276 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTypeMaintenance_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 302 "..\..\..\..\Views\MainMaintenanceView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCodeMaintenance_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.tabList = ((System.Windows.Controls.TabItem)(target));
            return;
            case 13:
            this.dgMain = ((System.Windows.Controls.DataGrid)(target));
            
            #line 348 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.dgMain.Initialized += new System.EventHandler(this.DataGrid_Initialized);
            
            #line default
            #line hidden
            
            #line 349 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.dgMain.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.dgMain_PreviewKeyUp);
            
            #line default
            #line hidden
            
            #line 349 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.dgMain.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgMain_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 350 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.dgMain.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.dgMain_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 14:
            this.dgMainPasteRow = ((System.Windows.Controls.MenuItem)(target));
            
            #line 356 "..\..\..\..\Views\MainMaintenanceView.xaml"
            this.dgMainPasteRow.Click += new System.Windows.RoutedEventHandler(this.dgMainPasteRow_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.ghost2 = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

