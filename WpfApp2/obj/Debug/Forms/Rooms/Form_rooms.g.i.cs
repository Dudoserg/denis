﻿#pragma checksum "..\..\..\..\Forms\Rooms\Form_rooms.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "773C372056CBC85EA31CF7F5F425649766698A4E5493F73110A85262621F331A"
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
using WpfApp2.Forms.Rooms;


namespace WpfApp2.Forms.Rooms {
    
    
    /// <summary>
    /// Form_rooms
    /// </summary>
    public partial class Form_rooms : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 33 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_findTitle;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_resetFilter;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_roomType;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_roomType;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_roomNumber;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox_roomNumber;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_size;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_size;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_price;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox_priceFrom;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox_priceTo;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_priceFrom;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_priceTo;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_createRoom;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid_rooms;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/forms/rooms/form_rooms.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
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
            this.Label_findTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Button_resetFilter = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.Button_resetFilter.Click += new System.Windows.RoutedEventHandler(this.Button_resetFilter_click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Label_roomType = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ComboBox_roomType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.ComboBox_roomType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_roomType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Label_roomNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TextBox_roomNumber = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.TextBox_roomNumber.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_roomNumber_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Label_size = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ComboBox_size = ((System.Windows.Controls.ComboBox)(target));
            
            #line 59 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.ComboBox_size.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_size_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Label_price = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.TextBox_priceFrom = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.TextBox_priceFrom.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_priceFrom_TextChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.TextBox_priceTo = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.TextBox_priceTo.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_priceTo_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Label_priceFrom = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.Label_priceTo = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.Button_createRoom = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            this.Button_createRoom.Click += new System.Windows.RoutedEventHandler(this.Button_createRoom_click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.DataGrid_rooms = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 16:
            
            #line 99 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_editRow_click);
            
            #line default
            #line hidden
            break;
            case 17:
            
            #line 106 "..\..\..\..\Forms\Rooms\Form_rooms.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_delRow_click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

