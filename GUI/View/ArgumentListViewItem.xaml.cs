using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using GBDPIGUI.Core;

using GoodByeDPIDotNet.Manual;

using HandyControl.Controls;

namespace GBDPIGUI.View
{
    public partial class ArgumentListViewItem : System.Windows.Controls.UserControl
    {
        static ArgumentListViewItem()
        {
            ArgumentProperty = DependencyProperty.Register("Argument", typeof(string), typeof(ArgumentListViewItem), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ArgumentPropertyChanged)));
            IsUsedProperty = DependencyProperty.Register("IsUsed", typeof(bool?), typeof(ArgumentListViewItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsUsedPropertyChanged)));
            IsNeedArgumentValueProperty = DependencyProperty.Register("IsNeedArgumentValue", typeof(bool?), typeof(ArgumentListViewItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(IsNeedArgumentValuePropertyChanged)));
            ArgumentValueProperty = DependencyProperty.Register("ArgumentValue", typeof(string), typeof(ArgumentListViewItem), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ArgumentValuePropertyChanged)));
            InformaionProperty = DependencyProperty.Register("Informaion", typeof(string), typeof(ArgumentListViewItem), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(InformaionPropertyChanged)));
        }

        public ArgumentListViewItem()
        {
            InitializeComponent();

            this.UseToggleButton.Click += (s, e) =>
            {
                if (s is ToggleButton toggleButton)
                    IsUsed = toggleButton.IsChecked;
            };
            this.ArgValue.LostKeyboardFocus += (s, e) =>
            {
                if (s is TextBox textBox)
                    ArgumentValue = textBox.Text;
            };

            this.IsEnabled = false;
        }

        public static readonly DependencyProperty ArgumentProperty;
        public static readonly DependencyProperty IsUsedProperty;
        public static readonly DependencyProperty IsNeedArgumentValueProperty;
        public static readonly DependencyProperty ArgumentValueProperty;
        public static readonly DependencyProperty InformaionProperty;

        private static void ArgumentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is ArgumentListViewItem item)
            {
                item.IsEnabled = !string.IsNullOrWhiteSpace(args.NewValue as string);

                //var arg = args.NewValue as string;
                //item.IsEnabled = !string.IsNullOrWhiteSpace(arg);
                //foreach (var manual in ArgumentManual.GetArgumentManual())
                //{
                //    if(manual.Key == arg)
                //    {
                //        item.IsNeedArgumentValue = manual.Value.Item1;
                //        item.Informaion = manual.Value.Item2;
                //        break;
                //    }
                //}
            }
        }

        public string Argument
        {
            get => GetValue(ArgumentProperty) as string;
            set => SetValue(ArgumentProperty, value);
        }

        private static void IsUsedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is ArgumentListViewItem item)
                item.UseToggleButton.IsChecked = args.NewValue as bool?;
        }

        public bool? IsUsed
        {
            get => GetValue(IsUsedProperty) as bool?;
            set => SetValue(IsUsedProperty, value);
        }

        private static void IsNeedArgumentValuePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is ArgumentListViewItem item)
                item.ArgValue.IsEnabled = (args.NewValue as bool?).GetValueOrDefault();
        }

        public bool? IsNeedArgumentValue
        {
            get => GetValue(IsNeedArgumentValueProperty) as bool?;
            set => SetValue(IsNeedArgumentValueProperty, value);
        }

        private static void ArgumentValuePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is ArgumentListViewItem item)
                item.ArgValue.Text = args.NewValue as string;
        }

        public string ArgumentValue
        {
            get => GetValue(ArgumentValueProperty) as string;
            set => SetValue(ArgumentValueProperty, value);
        }

        private static void InformaionPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is ArgumentListViewItem item)
                item.InfoIcon.ToolTip = args.NewValue as string;
        }

        public string Informaion
        {
            get => GetValue(InformaionProperty) as string;
            set => SetValue(InformaionProperty, value);
        }
    }
}
