/*
*	NAME	      : EditorWindow.xaml.cs
*	PROJECT		  : Basic Notepad
*	PROGRAMMER	  : lhori
*	FIRST VERSION : 2020-09-20
*	PURPOSE       : This file includes the functions for the events
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace lhori_Editor
{
    /*
    *	NAME	      : EditorWindow
    *	PROPOSE		  : EditorWindow class has been created for the interaction logic of EditorWindow.xaml.
    *	                It has functions that handles the event.         
    */
    public partial class EditorWindow : Window
    {

        string initialContent = "";

        /* -------------------------------------------------------------------------------------
        *	Name	: AboutWindow
        *	Purpose : This function initializes the editor window component
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        public EditorWindow()
        {
            InitializeComponent();
        }

        /* -------------------------------------------------------------------------------------
        *	Name	: OpenFile
        *	Purpose : This function is used to open the content from the directory files.
        *	          It will be called when the "Open" command is activated.
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "txt files (*.txt)|*.txt|html files(*.html)|*.html|all files (*.*)|*.*";
            DialogResult dlgRes = ofd.ShowDialog();
            string fname = ofd.FileName;
            if (dlgRes == System.Windows.Forms.DialogResult.OK)
            {
                textInput.Text = System.IO.File.ReadAllText(fname);
                //copy the current content to the update the initialContent
                initialContent = textInput.Text;
            }
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: SaveFile
        *	Purpose : This function is used to save the content in the editor. It will be called
        *             when the "Save As" command is activated.
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.Filter = "txt files (*.txt)|*.txt|html files(*.html)|*.html|all files (*.*)|*.*";
            DialogResult dlgRes = sfd.ShowDialog();
            if (dlgRes == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, textInput.Text);
                //copy the current content to the update the initialContent
                initialContent = textInput.Text;
            }
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: NewFile
        *	Purpose : This function is used to empty the content in the editor. It will be called
        *             when the "New" command is activated.
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void NewFile()
        {            
            textInput.Text = string.Empty;
            //copy the current content to the update the initialContent
            initialContent = textInput.Text;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: CharacterCountStatus
        *	Purpose : This function is used for counting the character in the editor.
        *	          It will not count the new line (= \n or \r).
        *	          Will show the line count and character count in the status bar.
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void CharacterCountStatus()
        {
            int line = 1;   //starts from 1
            int charCount = 0;

            line += textInput.GetLineIndexFromCharacterIndex(textInput.CaretIndex);

            //regex for counting \n or \r
            MatchCollection res = Regex.Matches(textInput.Text, "\n|\r");
            //subtracting the count for res from the total count
            charCount = textInput.Text.Length - res.Count;

            //showing the count on the status bar
            StatusShow.Text = "Char Count: " + charCount + " Line Count: " + line;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: NewCommandBinding_CanExecute
        *	Purpose : This function occurs the new command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: NewCommandBinding_Executed
        *	Purpose : This function occurs the new command activation, depending on the 
        *	          textInput content it will behave differently
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (textInput.Text != initialContent)
            {
                MessageBoxResult msgResult = System.Windows.MessageBox.Show("Do you want to save changes?",
                "A-02 LHori Text Editor", MessageBoxButton.YesNoCancel);

                if (msgResult == MessageBoxResult.Cancel)
                {
                    return;
                }
                else if (msgResult == MessageBoxResult.Yes)
                {
                    //save the file and then execute NewFile();
                    SaveFile();
                }
            }
            NewFile();
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: OpenCommandBinding_CanExecute
        *	Purpose : This function occurs the open command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: OpenCommandBinding_Executed
        *	Purpose : This function occurs the open command activation, depending on the 
        *	          textInput content it will behave differently
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (textInput.Text != initialContent)
            {
                MessageBoxResult msgResult = System.Windows.MessageBox.Show("Do you want to save changes?",
                "A-02 LHori Text Editor", MessageBoxButton.YesNoCancel);

                if (msgResult == MessageBoxResult.No)
                {
                    //open other file directly with out saving
                    OpenFile();
                }
                else if (msgResult == MessageBoxResult.Yes)
                {
                    //save the file and open other file
                    SaveFile();
                    OpenFile();
                }
                else if (msgResult == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                OpenFile();
            }
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: SaveAsCommandBinding_Executed
        *	Purpose : This function occurs the save command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void SaveAsCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: SaveAsCommandBinding_Executed
        *	Purpose : This function occurs the save command activation, calls the SaveFile 
        *	          function
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: CloseCommandBinding_CanExecute
        *	Purpose : This function occurs the close command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: CloseCommandBinding_Executed
        *	Purpose : This function occurs the close command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //move same as TextWindow_Closing();
            this.Close();
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: TextWrapCommandBinding_CanExecute
        *	Purpose : This function occurs the text wrap command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void TextWrapCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: TextWrapCommandBinding_Executed
        *	Purpose : This function occurs the text wrap command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void TextWrapCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (textInput.TextWrapping == TextWrapping.NoWrap)
            {
                textInput.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                //if the text wrap is off it will show the horizontal scroll bar
                textInput.TextWrapping = TextWrapping.NoWrap;
                textInput.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }

        }


        /* -------------------------------------------------------------------------------------
        *	Name	: AboutCommandBinding_CanExecute
        *	Purpose : This function occurs the about command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void AboutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: AboutCommandBinding_Executed
        *	Purpose : This function occurs the about command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void AboutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AboutWindow aboutBox = new AboutWindow();
            aboutBox.Owner = this;
            aboutBox.ShowDialog();
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: TextWindow_Closing
        *	Purpose : This function occurs the close command activation
        *	Inputs	: object sender : Object of which rasied the event
        *	        : CancelEventArgs e : Execution of the close
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void TextWindow_Closing(object sender, CancelEventArgs e)
        {
            if (textInput.Text != initialContent)
            {
                MessageBoxResult msgResult = System.Windows.MessageBox.Show("Do you want to save changes?",
                "A-02 LHori Text Editor", MessageBoxButton.YesNoCancel);

                if (msgResult == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (msgResult == MessageBoxResult.Yes)
                {
                    SaveFile();
                }
            }
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: StatusShow_KeyUp
        *	Purpose : This function occurs when the key is pressed, it will show the word count
        *	          in the status bar
        *	Inputs	: object sender : Object of which rasied the event
        *	        : System.Windows.Input.KeyEventArgs e : Execute of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void StatusShow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CharacterCountStatus();
        }

    }
}
