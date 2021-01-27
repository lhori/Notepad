/*
*	NAME	      : AboutWindow.xaml.cs
*	PROJECT		  : Basic Notepad
*	PROGRAMMER	  : lhori
*	FIRST VERSION : 2020-09-20
*	PURPOSE       : This file includes the class for the customized command.
*	                It is used to created customized command binding for the text wrap and about menu.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lhori_Editor
{
    /*
    *	NAME	      : CustomizedCommands
    *	PROPOSE		  : The CustomizedCommand class has been created to make customized command binding.            
    */
    public static class CustomizedCommands
    {
        //creating the command binding used for the about menu
        public static readonly RoutedUICommand About = new RoutedUICommand
        ("About", "About", typeof(CustomizedCommands));
        //creating the command binding used for the text wrap menu
        public static readonly RoutedUICommand TextWrap = new RoutedUICommand
        ("Text Wrap", "TextWrap", typeof(CustomizedCommands));

    }
}
