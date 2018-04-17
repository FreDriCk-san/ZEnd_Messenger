﻿#pragma checksum "..\..\Messenger.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "91247C73147E6D60F5752A37E20C264CA57B54E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using ZEnd;


namespace ZEnd {
    
    
    /// <summary>
    /// Messenger
    /// </summary>
    public partial class Messenger : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox messageBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox1;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createChat;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox2;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox3;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox chatName;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button userButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userLogin;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label welcome;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Messenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox4;
        
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
            System.Uri resourceLocater = new System.Uri("/ZEnd;component/messenger.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Messenger.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.messageBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 13 "..\..\Messenger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.listBox1 = ((System.Windows.Controls.ListBox)(target));
            
            #line 15 "..\..\Messenger.xaml"
            this.listBox1.Loaded += new System.Windows.RoutedEventHandler(this.listBox1_Loaded);
            
            #line default
            #line hidden
            
            #line 15 "..\..\Messenger.xaml"
            this.listBox1.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.listBox1_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.createChat = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\Messenger.xaml"
            this.createChat.Click += new System.Windows.RoutedEventHandler(this.createChat_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.listBox2 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.listBox3 = ((System.Windows.Controls.ListBox)(target));
            
            #line 18 "..\..\Messenger.xaml"
            this.listBox3.Loaded += new System.Windows.RoutedEventHandler(this.listBox3_Loaded);
            
            #line default
            #line hidden
            return;
            case 9:
            this.chatName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.userButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Messenger.xaml"
            this.userButton.Click += new System.Windows.RoutedEventHandler(this.userButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.userLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.welcome = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.listBox4 = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

