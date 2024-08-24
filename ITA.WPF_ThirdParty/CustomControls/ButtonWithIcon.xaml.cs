﻿using MaterialDesignThemes.Wpf;
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

namespace WPF_ThirdParty.CustomControls
{
    /// <summary>
    /// Interaction logic for ButtonWithIcon.xaml
    /// </summary>
    public partial class ButtonWithIcon : Button
    {
        public ButtonWithIcon()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PackIconKind), typeof(ButtonWithIcon));

        public PackIconKind Icon
        {
            get { return (PackIconKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }



        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ButtonWithIcon));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
