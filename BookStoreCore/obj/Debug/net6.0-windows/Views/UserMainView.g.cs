﻿#pragma checksum "..\..\..\..\Views\UserMainView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7D69A2F365BBEEBA3F4035FA0AD2285505B34F62"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BookStoreCore.ViewModels;
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


namespace BookStoreCore.Views {
    
    
    /// <summary>
    /// UserMainView
    /// </summary>
    public partial class UserMainView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 68 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup savedBooksPopup;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock myBooksTextBlock;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup boughtBooksPopup;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterTopCb;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterGenreCb;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterAuthorCb;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\Views\UserMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BookStoreCore;component/views/usermainview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\UserMainView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 65 "..\..\..\..\Views\UserMainView.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.savedBooksPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 3:
            this.myBooksTextBlock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 102 "..\..\..\..\Views\UserMainView.xaml"
            this.myBooksTextBlock.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.myBooksTextBlock_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.boughtBooksPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 5:
            this.filterTopCb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.filterGenreCb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.filterAuthorCb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.searchTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

