﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\UserInfo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B40C6A33C49B969006EB2101278F1F6"
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
    partial class UserInfo : 
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
                    this.gridNotification = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.progressBar = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 3:
                {
                    this.scrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 4:
                {
                    this.gridSendMessage = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5:
                {
                    this.textBoxContent = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 103 "..\..\..\AppPage\UserInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Tapped += this.SendMessageTap;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element7 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 77 "..\..\..\AppPage\UserInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element7).Tapped += this.SendTap;
                    #line default
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\AppPage\UserInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.NotificationDismiss;
                    #line default
                }
                break;
            case 9:
                {
                    this.textBlockContent = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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

