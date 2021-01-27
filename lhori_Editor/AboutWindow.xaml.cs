/*
*	NAME	      : AboutWindow.xaml.cs
*	PROJECT		  : Basic Notepad
*	PROGRAMMER	  : lhori
*	FIRST VERSION : 2020-09-20
*	PURPOSE       : This file includes the class and functions for the events occuring in the about window
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
using System.Windows.Shapes;

namespace lhori_Editor
{
    /*
    *	NAME	      : AboutWindow
    *	PROPOSE		  : AboutWindow class has been created for the interaction logic of AboutWindow.xaml.
    *	                It has function that handles the event.         
    */
    public partial class AboutWindow : Window
    {
        /* -------------------------------------------------------------------------------------
        *	Name	: AboutWindow
        *	Purpose : This function initializes the about window component
        *	Inputs	: None
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        public AboutWindow()
        {
            InitializeComponent();
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: CloseCommandBinding_CanExecute
        *	Purpose : This function occurs the OK button command execution
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        /* -------------------------------------------------------------------------------------
        *	Name	: CloseCommandBinding_CanExecute
        *	Purpose : This function occurs the OK command execution which will close the about
        *	          modal window
        *	Inputs	: object sender : Object of which rasied the event
        *	        : ExecutedRoutedEventArgs e : Execution of the event
        *	Returns	: None
        *------------------------------------------------------------------------------------ */
        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
