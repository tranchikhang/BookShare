﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\GreetingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BBF99053A2B0D6FBBEB174B3B5C954D3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookShare.AppPage
{
    partial class GreetingPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.gridNotification = (global::BookShare.Model.Control.MyNotification)(target);
                }
                break;
            case 2:
                {
                    this.listViewBooks = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Grid element3 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 62 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element3).Tapped += this.TitleTapped;
                    #line default
                }
                break;
            case 4:
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.Search = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 37 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Search).Click += this.SearchClick;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 41 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.AdvancedSearchClick;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

