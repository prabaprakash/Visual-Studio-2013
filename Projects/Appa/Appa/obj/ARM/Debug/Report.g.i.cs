﻿#pragma checksum "C:\Users\PrabaKarthi\Documents\Visual Studio 2013\Projects\Appa\Appa\Report.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F0932BB1114EFAC198D3B5137D3350C7"
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
    
    
    public partial class Report : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot ReportPage;
        
        internal Microsoft.Phone.Controls.LongListSelector OfferList;
        
        internal Microsoft.Phone.Controls.LongListSelector RedemptionList;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Appa;component/Report.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ReportPage = ((Microsoft.Phone.Controls.Pivot)(this.FindName("ReportPage")));
            this.OfferList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("OfferList")));
            this.RedemptionList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("RedemptionList")));
        }
    }
}
