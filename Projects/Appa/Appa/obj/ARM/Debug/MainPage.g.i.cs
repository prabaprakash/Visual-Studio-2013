﻿#pragma checksum "C:\Users\PrabaKarthi\Documents\Visual Studio 2013\Projects\Appa\Appa\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C8E349360F9C88AF0EFD342048770C52"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Appa {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot MainPivot;
        
        internal System.Windows.Controls.TextBox Mobile_No;
        
        internal System.Windows.Controls.TextBox Name;
        
        internal Microsoft.Phone.Controls.ListPicker Coupon_List;
        
        internal Microsoft.Phone.Controls.DatePicker Date_Picker;
        
        internal System.Windows.Controls.TextBox CopuonCode;
        
        internal System.Windows.Controls.TextBlock CouponCheck;
        
        internal System.Windows.Controls.RadioButton SelectReports;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Appa;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("MainPivot")));
            this.Mobile_No = ((System.Windows.Controls.TextBox)(this.FindName("Mobile_No")));
            this.Name = ((System.Windows.Controls.TextBox)(this.FindName("Name")));
            this.Coupon_List = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("Coupon_List")));
            this.Date_Picker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("Date_Picker")));
            this.CopuonCode = ((System.Windows.Controls.TextBox)(this.FindName("CopuonCode")));
            this.CouponCheck = ((System.Windows.Controls.TextBlock)(this.FindName("CouponCheck")));
            this.SelectReports = ((System.Windows.Controls.RadioButton)(this.FindName("SelectReports")));
        }
    }
}

