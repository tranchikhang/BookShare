﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\Register.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB8933574E18E280648D9E4B70EEF01F"
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
    partial class Register : 
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
                    this.LeftBlank = (global::Windows.UI.Xaml.Controls.ColumnDefinition)(target);
                }
                break;
            case 2:
                {
                    this.RightBlank = (global::Windows.UI.Xaml.Controls.ColumnDefinition)(target);
                }
                break;
            case 3:
                {
                    this.textBoxUser = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.textBoxEmail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.textBoxFullName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.pwBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 7:
                {
                    this.pwBoxRe = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 8:
                {
                    this.chkBoxPolicy = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 9:
                {
                    global::Windows.UI.Xaml.Controls.Button element9 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 72 "..\..\..\AppPage\Register.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element9).Click += this.RegisterClick;
                    #line default
                }
                break;
            case 10:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 11:
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

