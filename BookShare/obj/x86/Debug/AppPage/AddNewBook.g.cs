﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\AddNewBook.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "67CE591AD0097149E9BC62502D5E84E6"
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
    partial class AddNewBook : 
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
                    this.mainScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 3:
                {
                    this.textBoxTitle = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.suggestAuthor = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 38 "..\..\..\AppPage\AddNewBook.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.suggestAuthor).TextChanged += this.SuggestTextChanged;
                    #line default
                }
                break;
            case 5:
                {
                    this.comboYear = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 6:
                {
                    this.comboBoxGenre = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 7:
                {
                    this.textBoxDes = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 78 "..\..\..\AppPage\AddNewBook.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.AddFileClick;
                    #line default
                }
                break;
            case 9:
                {
                    global::Windows.UI.Xaml.Controls.Button element9 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 83 "..\..\..\AppPage\AddNewBook.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element9).Click += this.AddBook;
                    #line default
                }
                break;
            case 10:
                {
                    this.textBlockFile = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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

