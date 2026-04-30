using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Controls;

/// <summary> Show modern "MessageBox", block entire control so user must take the choice. </summary>
/// <remarks> Both the <see cref="MessageBox"/> and it control must be placed in a <see cref="Panel"/></remarks>
public static class MessageBox
{
    public static void Show(this Panel panel, string message)
        => panel.Children.Add(new AvaloniaEx.Controls.DialogMessage(message, null, MessageBoxButtons.OK, MessageBoxIcon.None, 0));

    public static Task<DialogResult> Affirmative(this Panel panel, string message)
    {
        AvaloniaEx.Controls.DialogMessage dlg = new AvaloniaEx.Controls.DialogMessage(message, "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, 1);
        panel.Children.Add(dlg);
        return dlg.DialogResult.Task;
    }

    public static Task<DialogResult> Ask(this Panel panel, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        AvaloniaEx.Controls.DialogMessage dlg = new AvaloniaEx.Controls.DialogMessage(message, title, buttons, icon, int.MaxValue);
        panel.Children.Add(dlg);
        return dlg.DialogResult.Task;
    }

    /// <summary> Specifies identifiers to indicate the return value of a dialog box. </summary>
    public enum DialogResult
    {
        /// <summary>Nothing is returned from the dialog box. This means that the modal dialog continues running.</summary>
        None = 0,
        /// <summary>The dialog box return value is OK (usually sent from a button labeled OK).</summary>
        OK = 1,
        /// <summary>The dialog box return value is Cancel (usually sent from a button labeled Cancel).</summary>
        Cancel = 2,
        /// <summary>The dialog box return value is Abort (usually sent from a button labeled Abort).</summary>
        Abort = 3,
        /// <summary>The dialog box return value is Retry (usually sent from a button labeled Retry).</summary>
        Retry = 4,
        /// <summary>The dialog box return value is Ignore (usually sent from a button labeled Ignore).</summary>
        Ignore = 5,
        /// <summary>The dialog box return value is Yes (usually sent from a button labeled Yes).</summary>
        Yes = 6,
        /// <summary>The dialog box return value is No (usually sent from a button labeled No).</summary>
        No = 7,
        TryAgain = 10,
        Continue = 11
    }
    ///    <summary> Specifies constants defining which information to display.</summary>
    public enum MessageBoxIcon
    {
        /// <summary>The message box contains no symbols.</summary>
        None = 0,
        /// <summary>The message box contains a symbol consisting of a question mark in a circle. 
        /// The question mark message icon is no longer recommended because it does not clearly represent 
        /// a specific type of message and because the phrasing of a message as a question could apply to any message type. 
        /// In addition, users can confuse the question mark symbol with a help information symbol. 
        /// Therefore, do not use this question mark symbol in your message boxes. 
        /// The system continues to support its inclusion only for backward compatibility.</summary>
        Question = 32,
        /// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
        Exclamation = 48,
        /// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
        Asterisk = 64,
        /// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
        Stop = 16,
        /// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
        Error = 16,
        /// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
        Warning = 48,
        /// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
        Information = 64
    }
    ///    <summary> Specifies constants defining which buttons to display on a System.Windows.Forms.MessageBox.</summary>
    public enum MessageBoxButtons
    {
        /// <summary>The message box contains an OK button.</summary>
        OK,
        /// <summary>The message box contains OK and Cancel buttons.</summary>
        OKCancel,
        /// <summary>The message box contains Abort, Retry, and Ignore buttons.</summary>
        AbortRetryIgnore,
        /// <summary>The message box contains Yes, No, and Cancel buttons.</summary>
        YesNoCancel,
        /// <summary>The message box contains Yes and No buttons.</summary>
        YesNo,
        /// <summary>The message box contains Retry and Cancel buttons.</summary>
        RetryCancel,
    }
}