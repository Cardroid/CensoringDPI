using System;
using System.Windows.Input;

using GBDPIGUI.Core;

using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;

namespace GBDPIGUI.View.Helper
{
    public partial class ArgumentListViewItem : ListViewItem
    {
        public ArgumentListViewItem(string arg, string info, bool isNeedValue)
        {
            InitializeComponent();

            this.Arg.Text = arg;

            IsNeedValue = isNeedValue;

            if (IsNeedValue)
            {
                IsGotValueText = false;

                // 텍스트 박스가 빌경우 Value로 채우기
                this.ArgValue.LostFocus += (s, e) =>
                {
                    if (this.ArgValue.Text.Length == 0)
                        IsGotValueText = false;
                };
                this.ArgValue.GotFocus += (s, e) =>
                {
                    if (!IsGotValueText)
                    {
                        IsGotValueText = true;
                        this.ArgValue.Clear();
                    }
                };

                this.ArgValue.LostKeyboardFocus += (s, e) => ArgumentApply();
                this.ArgValue.PreviewKeyDown += (s, e) => { if (e.Key == Key.Enter) Keyboard.ClearFocus(); };
            }
            else
                this.ArgValue.Text = "Null";

            this.ArgValue.IsEnabled = false;

            this.IsUse.Toggled += (s, e) =>
            {
                ArgumentApply();
                if (IsNeedValue)
                {
                    if (this.IsUse.IsOn)
                        this.ArgValue.IsEnabled = true;
                    else
                        this.ArgValue.IsEnabled = false;
                }
            };
            this.InfoIcon.ToolTip = info;
        }

        public string Argument => this.Arg.Text;

        private bool IsNeedValue { get; }

        private bool _IsGotValueText;
        private bool IsGotValueText
        {
            get => _IsGotValueText;
            set
            {
                if (IsNeedValue)
                {
                    _IsGotValueText = value;
                    if (!_IsGotValueText)
                        this.ArgValue.Text = "Value";
                }
            }
        }

        private void ArgumentApply()
        {
            var arg = this.Argument.Split(' ')[0];

            if (this.IsUse.IsOn)
            {
                if (IsNeedValue && IsGotValueText/* || !string.IsNullOrEmpty(this.ArgValue.Text)*/)
                    GlobalProperty.GetInstence().GoodByeDPIOptions.AddArgument(arg, this.ArgValue.Text);
                else
                    GlobalProperty.GetInstence().GoodByeDPIOptions.AddArgument(arg);
            }
            else
            {
                GlobalProperty.GetInstence().GoodByeDPIOptions.RemoveArgument(arg);
            }
        }
    }
}
