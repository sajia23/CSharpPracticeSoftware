using System;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.Windows.Input;
using System.Drawing;

namespace WpfApp1
{
    /// <summary>
    /// Syntax.xaml 的交互逻辑
    /// </summary>
    public partial class Syntax : UserControl
    {
        private bool hasPendingParseData;
        private IProjectAssembly projectAssembly;
        private Classfile cf;
        public Syntax(Object ob)
        {
            cf = (Classfile)ob; 
            InitializeComponent();
            projectAssembly = new CSharpProjectAssembly("SampleBrowser");
            //projectAssembly.AssemblyReferences.ItemAdded += OnAssemblyReferencesChanged;
            //projectAssembly.AssemblyReferences.ItemRemoved += OnAssemblyReferencesChanged;
            var assemblyLoader = new BackgroundWorker();
            assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
            assemblyLoader.RunWorkerAsync();

            // Load the .NET Languages Add-on C# language and register the project assembly on it
            var language = new CSharpSyntaxLanguage();
            language.RegisterProjectAssembly(projectAssembly);
            codeEditor.Document.Language = language;
            
        }
        
        private void DotNetProjectAssemblyReferenceLoader(object sender, DoWorkEventArgs e)
        {
            // Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
            projectAssembly.AssemblyReferences.AddMsCorLib();
            projectAssembly.AssemblyReferences.Add("System");
            projectAssembly.AssemblyReferences.Add("System.Core");
            projectAssembly.AssemblyReferences.Add("System.Xml");
            if(cf.Name != "")
            {
                for (int i = 0; i < cf.Reference[0].References.Count; i++)
                {
                    projectAssembly.AssemblyReferences.Add(cf.Reference[0].References[i].Path);
                }
            }
            
        }
        private void OnCodeEditorDocumentParseDataChanged(object sender, EventArgs e)
        {
            //
            // NOTE: The parse data here is generated in a worker thread... this event handler is called 
            //         back in the UI thread immediately when the worker thread completes... it is best
            //         practice to delay UI updates until the end user stops typing... we will flag that
            //         there is a pending parse data change, which will be handled in the 
            //         UserInterfaceUpdate event
            //

            hasPendingParseData = true;
        }
        private void OnCodeEditorUserInterfaceUpdate(object sender, RoutedEventArgs e)
        {
            // If there is a pending parse data change...
            if (hasPendingParseData)
            {
                // Clear flag
                hasPendingParseData = false;

                ILLParseData parseData = codeEditor.Document.ParseData as ILLParseData;
                if (parseData != null)
                {
                    //if (codeEditor.Document.CurrentSnapshot.Length < 10000)
                    //{
                    //    // Show the AST
                    //    if (parseData.Ast != null)
                    //        astOutputEditor.Document.SetText(parseData.Ast.ToTreeString(0));
                    //    else
                    //        astOutputEditor.Document.SetText(null);
                    //}
                    //else
                    //    astOutputEditor.Document.SetText("(Not displaying large AST for performance reasons)");

                    //// Output errors
                    //errorListView.ItemsSource = parseData.Errors;
                }
                else
                {
                    // Clear UI
                    //astOutputEditor.Document.SetText(null);
                    //errorListView.ItemsSource = null;
                    //messagePanel.Content = null;
                }
            }
        }

        private void OnCodeEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e)
        {
            // Quit if this event is not for the active view
            if (!e.View.IsActive)
                return;

            // Update line, col, and character display
            //linePanel.Text = String.Format("Ln {0}", e.CaretPosition.DisplayLine);
            //columnPanel.Text = String.Format("Col {0}", e.CaretDisplayCharacterColumn);
            //characterPanel.Text = String.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
        }
        
    }
}
