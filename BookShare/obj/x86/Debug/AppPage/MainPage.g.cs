﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7BFF3208C9817C28ED16D0B4D6D3E3FC"
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
                    #line 54 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element2).Click += this.HomeClick;
                    #line default
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element3 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 58 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element3).Checked += this.SettingClick;
                    #line default
                }
                break;
            case 4:
                {
                    this.HamburgerButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 33 "..\..\..\AppPage\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.HamburgerButton).Click += this.HamburgerClick;
                    #line default
                }
                break;
            case 5:
                {
                    this.BackButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 6:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 7:
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

