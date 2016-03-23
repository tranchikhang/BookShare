﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\RequestList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A56F9E374903A2516D5045D36B3D6B37"
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
    partial class RequestList : 
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
                    this.listViewNotification = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 269 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.ButtonUserTapped;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 274 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.DeactiveRequest;
                    #line default
                }
                break;
            case 6:
                {
                    this.listViewRequestedBook = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 7:
                {
                    this.listViewSentRequest = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element8 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 211 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element8).Tapped += this.TextBlockUserTapped;
                    #line default
                }
                break;
            case 9:
                {
                    global::Windows.UI.Xaml.Controls.Button element9 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 217 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element9).Click += this.DeleteRequest;
                    #line default
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.Grid element10 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 164 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element10).Tapped += this.RequestedBookTapped;
                    #line default
                }
                break;
            case 11:
                {
                    this.listViewPostedBook = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 12:
                {
                    this.listViewReceivedRequest = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 13:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element13 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 117 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element13).Tapped += this.TextBlockUserTapped;
                    #line default
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.Button element14 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 124 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element14).Click += this.AcceptRequest;
                    #line default
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.Button element15 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 128 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element15).Click += this.DenyRequest;
                    #line default
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.Grid element16 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 70 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element16).Tapped += this.PostedBookTapped;
                    #line default
                }
                break;
            case 17:
                {
                    global::Windows.UI.Xaml.Controls.Button element17 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\AppPage\RequestList.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element17).Click += this.NotificationDismiss;
                    #line default
                }
                break;
            case 18:
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

