using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaEx.Controls;

internal partial class DialogMessage : UserControl
{
    public System.Threading.Tasks.TaskCompletionSource<MessageBox.DialogResult> DialogResult { get; private set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = "Dialog message should be assigned!!!";
    internal System.Windows.Input.ICommand? CommandCancel { get; set; }
    internal System.Windows.Input.ICommand? CommandOK { get; set; }
    internal System.Windows.Input.ICommand? CommandYes { get; set; }
    internal System.Windows.Input.ICommand? CommandNo { get; set; }
    internal System.Windows.Input.ICommand? CommandAbort { get; set; }
    internal System.Windows.Input.ICommand? CommandRetry { get; set; }
    internal System.Windows.Input.ICommand? CommandIgnore { get; set; }
    internal System.Windows.Input.ICommand? CommandTryAgain { get; set; }
    internal System.Windows.Input.ICommand? CommandContinue { get; set; }
    public DialogMessage(string _message, string? _title, MessageBox.MessageBoxButtons buttons, MessageBox.MessageBoxIcon icon, int defaultButton)
    {
        InitializeComponent();
        this.Message = _message;
        if (!string.IsNullOrEmpty(_title))
            this.Title = _title;

        if (icon != MessageBox.MessageBoxIcon.None)
        {
            this.imgIcon.IsVisible = true;
            string imageResource = icon switch
            {
                MessageBox.MessageBoxIcon.Stop => "avares://AvaloniaEx/Assets/Images/stop.png",
                MessageBox.MessageBoxIcon.Exclamation => "avares://AvaloniaEx/Assets/Images/warning.png",
                MessageBox.MessageBoxIcon.Information => "avares://AvaloniaEx/Assets/Images/alert.png",
                MessageBox.MessageBoxIcon.Question => "avares://AvaloniaEx/Assets/Images/question.png",
                _ => string.Empty,
            };
            this.imgIcon.Source = new Avalonia.Media.Imaging.Bitmap(Avalonia.Platform.AssetLoader.Open(new System.Uri(imageResource)));
        }
        System.Collections.Generic.List<Button> actualButton = new System.Collections.Generic.List<Button>();
        switch (buttons)
        {
            case MessageBox.MessageBoxButtons.OK:
                this.CommandOK = new RelayCommand(() => this.Close(MessageBox.DialogResult.OK));
                actualButton.Add(this.btnOK);
                break;
            case MessageBox.MessageBoxButtons.OKCancel:
                this.CommandOK = new RelayCommand(() => this.Close(MessageBox.DialogResult.OK));
                this.CommandCancel = new RelayCommand(() => this.Close(MessageBox.DialogResult.Cancel));
                actualButton.Add(btnOK);
                actualButton.Add(btnCancel);
                break;
            case MessageBox.MessageBoxButtons.YesNo:
                this.CommandYes = new RelayCommand(() => this.Close(MessageBox.DialogResult.Yes));
                this.CommandNo = new RelayCommand(() => this.Close(MessageBox.DialogResult.No));
                actualButton.Add(this.btnYes);
                actualButton.Add(this.btnNo);
                break;
            case MessageBox.MessageBoxButtons.YesNoCancel:
                this.CommandYes = new RelayCommand(() => this.Close(MessageBox.DialogResult.Yes));
                this.CommandNo = new RelayCommand(() => this.Close(MessageBox.DialogResult.No));
                this.CommandCancel = new RelayCommand(() => this.Close(MessageBox.DialogResult.Cancel));
                actualButton.Add(this.btnYes);
                actualButton.Add(this.btnNo);
                actualButton.Add(this.btnCancel);
                break;
            case MessageBox.MessageBoxButtons.AbortRetryIgnore:
                this.CommandAbort = new RelayCommand(() => this.Close(MessageBox.DialogResult.Abort));
                this.CommandRetry = new RelayCommand(() => this.Close(MessageBox.DialogResult.Retry));
                this.CommandIgnore = new RelayCommand(() => this.Close(MessageBox.DialogResult.Ignore));
                actualButton.Add(this.btnAbort);
                actualButton.Add(this.btnRetry);
                actualButton.Add(this.btnIgnore);
                break;
            case MessageBox.MessageBoxButtons.RetryCancel:
                this.CommandRetry = new RelayCommand(() => this.Close(MessageBox.DialogResult.Retry));
                this.CommandCancel = new RelayCommand(() => this.Close(MessageBox.DialogResult.Cancel));
                actualButton.Add(this.btnRetry);
                actualButton.Add(this.btnCancel);
                break;

        }

        foreach (Button btn in actualButton)
            btn.IsVisible = true;
        if (actualButton.Count > defaultButton)
            this.Loaded += (s, eArgs) => actualButton[defaultButton].Focus();

        this.DialogResult = new System.Threading.Tasks.TaskCompletionSource<MessageBox.DialogResult>();
        //DataContext actually need to be assigned after Control show up.
        //Here, for short, assign it after all command and property assigned
        this.DataContext = this;
    }



    private void Close(MessageBox.DialogResult result)
    {
        this.DialogResult.SetResult(result);
        Panel? panel = this.Parent as Panel;
        System.Diagnostics.Debug.Assert(panel != null);
        panel.Children.Remove(this);
    }

}