using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GBDPIGUI.Core;

namespace GBDPIGUI.View
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

            this.IsUse.Click += (s, e) => IsUseChecked = this.IsUse.IsChecked.GetValueOrDefault();
            this.InfoIcon.ToolTip = info;

            GlobalProperty.GetInstence().GoodByeDPI.PropertyChanged += GoodByeDPI_PropertyChanged;
            GlobalProperty.GetInstence().GoodByeDPIOptions.ArgumentChangedEvent += GoodByeDPIOptions_ArgumentChangedEvent;
        }

        ~ArgumentListViewItem()
        {
            GlobalProperty.GetInstence().GoodByeDPI.PropertyChanged -= GoodByeDPI_PropertyChanged;
            GlobalProperty.GetInstence().GoodByeDPIOptions.ArgumentChangedEvent -= GoodByeDPIOptions_ArgumentChangedEvent;
        }

        private void GoodByeDPI_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsEventRequest = true;
            if (e.PropertyName == "IsRun")
                IsActive = !GlobalProperty.GetInstence().GoodByeDPI.IsRun;
            IsEventRequest = false;
        }

        private void GoodByeDPIOptions_ArgumentChangedEvent(object sender, GoodByeDPIDotNet.Core.ArgumentChangedEventArgs e)
        {
            IsEventRequest = true;
            if (e.IsCleared)
            {
                IsUseChecked = false;
                IsGotValueText = false;
            }
            else
            {
                IsActive = !e.IsPreset;
                if (e.Argument == Argument)
                {
                    IsUseChecked = e.IsAdded;
                    if (IsNeedValue)
                    {
                        if (!string.IsNullOrEmpty(e.Value))
                        {
                            IsGotValueText = true;
                            this.ArgValue.Text = e.Value.Trim('\"');
                        }
                        else
                            IsGotValueText = false;
                    }
                }
            }
            IsEventRequest = false;
        }

        public string Argument => this.Arg.Text;

        private bool IsEventRequest = false;
        private bool _IsUseChecked = false;
        public bool IsUseChecked
        {
            get => _IsUseChecked;
            set
            {
                if (_IsUseChecked != value)
                {
                    _IsUseChecked = value;

                    if (this.IsUse.IsChecked.GetValueOrDefault() != _IsUseChecked)
                        this.IsUse.IsChecked = _IsUseChecked;

                    if (IsNeedValue)
                    {
                        if (_IsUseChecked)
                            this.ArgValue.IsEnabled = true;
                        else
                            this.ArgValue.IsEnabled = false;
                    }

                    IsUseChanged?.Invoke(this, _IsUseChecked);
                    ArgumentApply();
                }
            }
        }

        private bool _IsActive = !GlobalProperty.GetInstence().GoodByeDPI.IsRun;
        public bool IsActive
        {
            get => _IsActive;
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    this.IsUse.IsEnabled = _IsActive;
                    this.ArgValue.IsEnabled = IsNeedValue && _IsActive && IsUseChecked;
                    ArgumentApply();
                }
            }
        }

        public event EventHandler<bool> IsUseChanged;

        private bool IsNeedValue { get; }
        private bool _IsGotValueText;
        private bool IsGotValueText
        {
            get => _IsGotValueText && IsNeedValue;
            set
            {
                if (IsNeedValue)
                    _IsGotValueText = value;
            }
        }

        private void ArgumentApply()
        {
            // 이벤트에 의한 요청이 아닐때
            if (!IsEventRequest)
            {
                var arg = this.Argument.Split(' ')[0];
                if (IsActive && IsUseChecked)
                {
                    if (IsNeedValue && IsNeedValue/* || !string.IsNullOrEmpty(this.ArgValue.Text)*/)
                    {
                        if (IsGotValueText)
                            GlobalProperty.GetInstence().GoodByeDPIOptions.AddArgument(arg, this.ArgValue.Text);
                    }
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
}
