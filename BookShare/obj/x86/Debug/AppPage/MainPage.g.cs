﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5A46E7F0E10D27B052D8666A34A7DBC7"
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
    partial class MainPage : 
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
                    this.MainSplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element2 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 56 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element2).Click += this.HomeClick;
                    #line default
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element3 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 60 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element3).Checked += this.BookShelfClick;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element4 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 64 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element4).Checked += this.RequestListClick;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element5 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 68 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element5).Checked += this.NotificationClick;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element6 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 72 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element6).Checked += this.MessageListClick;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element7 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 76 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element7).Checked += this.SettingClick;
                    #line default
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element8 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 80 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element8).Checked += this.InfoClick;
                    #line default
                }
                break;
            case 9:
                {
                    this.mainFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 10:
                {
                    this.textBlockTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element11 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 35 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element11).Tapped += this.HamburgerClick;
                    #line default
                }
                break;
            case 12:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 13:
                {
                    this.NarrowState = (global::Windows.UI.Xaml.VisualState)(target);
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

