﻿#pragma checksum "..\..\AddItemView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "278D017D631BAEA72ADF0704F00F4FD36CD1D31115CE42E0937371560A1B18CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using Project;
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


namespace Project {
    
    
    /// <summary>
    /// AddItemView
    /// </summary>
    public partial class AddItemView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox barcode;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox initialPrice;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sellingPrice;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox companyName;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox privateName;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox qunatity;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBttn;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listOfCams;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\AddItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FontAwesome.WPF.ImageAwesome spinner;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project;component/additemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddItemView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.barcode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.initialPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.sellingPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.companyName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.privateName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.qunatity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.saveBttn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\AddItemView.xaml"
            this.saveBttn.Click += new System.Windows.RoutedEventHandler(this.saveItem);
            
            #line default
            #line hidden
            return;
            case 8:
            this.listOfCams = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            
            #line 40 "..\..\AddItemView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.startScanner);
            
            #line default
            #line hidden
            return;
            case 10:
            this.spinner = ((FontAwesome.WPF.ImageAwesome)(target));
            return;
            case 11:
            
            #line 43 "..\..\AddItemView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.backAction);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

