﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\MessagePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "92C00DF982E80A1A4DB24509884150B7"
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
    partial class MessagePage : 
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
                    this.listViewConversation = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4:
                {
                    this.gridMessages = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5:
                {
                    this.listViewMessage = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 6:
                {
                    this.gridSendBox = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 7:
                {
                    this.textBoxContent = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 125 "..\..\..\AppPage\MessagePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Tapped += this.SendMessageTap;
                    #line default
                }
                break;
            case 9:
                {
                    global::Windows.UI.Xaml.Controls.Grid element9 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 52 "..\..\..\AppPage\MessagePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element9).Tapped += this.CovnersationGridTapped;
                    #line default
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.Button element10 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\AppPage\MessagePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element10).Click += this.NotificationDismiss;
                    #line default
                }
                break;
            case 11:
                {
                    this.textBlockContent = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
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

