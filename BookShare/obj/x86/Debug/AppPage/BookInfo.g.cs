﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\BookInfo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AB654472E65E40BF22472E3501E17EC3"
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
    partial class BookInfo : 
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
                    this.progressBar = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 3:
                {
                    this.listViewLenders = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Image element4 = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 180 "..\..\..\AppPage\BookInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)element4).Tapped += this.UserAccountTapped;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element5 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 189 "..\..\..\AppPage\BookInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element5).Tapped += this.UserAccountTapped;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 197 "..\..\..\AppPage\BookInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.SendRequest;
                    #line default
                }
                break;
            case 7:
                {
                    this.mainScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 8:
                {
                    this.relativePanelInfo = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 9:
                {
                    this.BookCover = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 10:
                {
                    this.stackPanelInfo = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 11:
                {
                    this.stackPanelDes = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 12:
                {
                    this.textBlockRelatedBooks = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13:
                {
                    this.listViewRelatedBooks = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.Grid element14 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 116 "..\..\..\AppPage\BookInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element14).Tapped += this.RelatedBookTapped;
                    #line default
                }
                break;
            case 15:
                {
                    this.buttonAddBook = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 80 "..\..\..\AppPage\BookInfo.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.buttonAddBook).Click += this.AddToYourBook;
                    #line default
                }
                break;
            case 16:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 17:
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

