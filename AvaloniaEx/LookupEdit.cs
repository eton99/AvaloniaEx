using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AvaloniaEx.Controls;

public class LookupEdit : ComboBox
{
    protected override System.Type StyleKeyOverride => typeof(ComboBox);
    private readonly TextBox searchBox = new TextBox() { Watermark = "Search..." };
    private Popup? _popup;
    public string? FilterValue => this.searchBox.Text;
    public bool FilterVisible
    {
        get => this.searchBox.IsVisible;
        set => this.searchBox.IsVisible = value;
    }

    public bool ClearButtonVisible { get; set; } = true;

    public LookupEdit()
    {
        this.searchBox.TextChanged += this.OnSearchBoxTextChanged;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _popup = e.NameScope.Get<Popup>("PART_Popup");
        _popup.Closed += (sender, eArgs) => this.searchBox.Clear();
        Border border = e.NameScope.Get<Border>("PopupBorder");
        Control? ctrl = border.Child;
        System.Diagnostics.Debug.Assert(ctrl != null);
        StackPanel mainPopupPanel = new StackPanel();
        border.Child = mainPopupPanel;
        mainPopupPanel.Children.Add(searchBox);
        mainPopupPanel.Children.Add(ctrl);
        if(ClearButtonVisible)
        {
            Button clearButton = new Button()
            {
                Content = "X",
                Command = new RelayCommand(() => this.SelectedIndex = -1),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right
            };

            mainPopupPanel.Children.Add(clearButton);
        }
    }

    private void OnSearchBoxTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (this.ItemCount == 0)
            return;
        System.Diagnostics.Debug.Assert(this._popup != null);
        if (!_popup.IsOpen)
            return;

        for (int index = 0; index < this.ItemCount; index++)
        {
            ComboBoxItem? item = this.ContainerFromIndex(index) as ComboBoxItem;
            System.Diagnostics.Debug.Assert(item != null);
            string? content = item.Content?.ToString();
            System.Diagnostics.Debug.Assert(content != null);
            if (string.IsNullOrEmpty(this.searchBox.Text))
                item.IsVisible = true;
            else
                item.IsVisible = content.Contains(this.searchBox.Text, StringComparison.OrdinalIgnoreCase);
        }
    }





}
