﻿#pragma checksum "D:\Dropbox\GitHub\BookShare\BookShare\AppPage\SettingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5A06993504D6FEFB7E91DDBA8BC918DB"
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
    partial class SettingPage : 
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
                    this.mainScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 2:
                {
                    this.WideState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 3:
                {
                    this.NarrowState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4:
                {
                    this.progressBar = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 5:
                {
                    this.gridNotification = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6:
                {
                    this.relativePanel = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 7:
                {
                    this.gridChangePass = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 8:
                {
                    this.pwBoxCurrent = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 9:
                {
                    this.textBoxNewPass = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10:
                {
                    this.pwBoxNew = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 11:
                {
                    this.pwBoxNewRe = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 12:
                {
                    global::Windows.UI.Xaml.Controls.Button element12 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 248 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element12).Click += this.ChangePass;
                    #line default
                }
                break;
            case 13:
                {
                    this.textBlockTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14:
                {
                    this.userAva = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 15:
                {
                    this.buttonAddFile = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 172 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.buttonAddFile).Click += this.AddFileClick;
                    #line default
                }
                break;
            case 16:
                {
                    this.textBlockAccount = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 17:
                {
                    this.textBoxAccount = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18:
                {
                    this.textBlockFullName = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 19:
                {
                    this.textBoxFullName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 20:
                {
                    this.textBlockEmail = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 21:
                {
                    this.textBoxEmail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 22:
                {
                    this.textBlockAddress = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 23:
                {
                    this.comboCity = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 206 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.comboCity).SelectionChanged += this.comboCity_SelectionChanged;
                    #line default
                }
                break;
            case 24:
                {
                    this.comboDistrict = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 25:
                {
                    this.textBoxAddress = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 26:
                {
                    this.buttonChangePass = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 222 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.buttonChangePass).Click += this.ChangePassClick;
                    #line default
                }
                break;
            case 27:
                {
                    this.buttonSave = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 224 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.buttonSave).Click += this.SaveClick;
                    #line default
                }
                break;
            case 28:
                {
                    global::Windows.UI.Xaml.Controls.Button element28 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 151 "..\..\..\AppPage\SettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element28).Click += this.NotificationDismiss;
                    #line default
                }
                break;
            case 29:
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

