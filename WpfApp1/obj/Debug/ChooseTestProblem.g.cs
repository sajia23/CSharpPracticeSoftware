﻿#pragma checksum "..\..\ChooseTestProblem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "146CE98C6F28E28BBF76D37EC25C1A9A781E7EAA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// ChooseTestProblem
    /// </summary>
    public partial class ChooseTestProblem : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton current;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton other;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox pr;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sl;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button solve1;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\ChooseTestProblem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button solve2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/choosetestproblem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChooseTestProblem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\ChooseTestProblem.xaml"
            ((WpfApp1.ChooseTestProblem)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.current = ((System.Windows.Controls.RadioButton)(target));
            
            #line 10 "..\..\ChooseTestProblem.xaml"
            this.current.Checked += new System.Windows.RoutedEventHandler(this.Current_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.other = ((System.Windows.Controls.RadioButton)(target));
            
            #line 11 "..\..\ChooseTestProblem.xaml"
            this.other.Checked += new System.Windows.RoutedEventHandler(this.Other_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.pr = ((System.Windows.Controls.ComboBox)(target));
            
            #line 12 "..\..\ChooseTestProblem.xaml"
            this.pr.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Pr_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.sl = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\ChooseTestProblem.xaml"
            this.sl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Sl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.solve1 = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\ChooseTestProblem.xaml"
            this.solve1.Click += new System.Windows.RoutedEventHandler(this.Solve1_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.solve2 = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\ChooseTestProblem.xaml"
            this.solve2.Click += new System.Windows.RoutedEventHandler(this.Solve2_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

