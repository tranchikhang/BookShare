﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\GreetingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5C0BD955D4C9CB1CBAA82A5E49167BFB"
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
                    this.listViewBooks = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 74 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element2).Tapped += this.TitleTapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 32 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).GotFocus += this.SearchBox_GotFocus;
                    #line default
                }
                break;
            case 4:
                {
                    this.Search = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 37 "..\..\..\AppPage\GreetingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Search).Click += this.SearchClick;
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

