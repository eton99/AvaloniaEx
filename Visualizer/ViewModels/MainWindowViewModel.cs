using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Visualizer.ViewModels;

internal class MainWindowViewModel : INotifyPropertyChanged
{
    private bool isPaneOpen = true;
    public bool IsPaneOpen
    {
        get => isPaneOpen;
        set
        {
            isPaneOpen = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsPaneOpen)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public AvaloniaEx.Controls.RelayCommand TogglePane { get; }

    public ObservableCollection<Node> Nodes { get; }
    public ObservableCollection<Node> SelectedNodes { get; }

    public MainWindowViewModel()
    {
        TogglePane = new AvaloniaEx.Controls.RelayCommand(() => this.IsPaneOpen = !this.isPaneOpen);
        SelectedNodes = new ObservableCollection<Node>();
        Nodes = new ObservableCollection<Node>
        {
            new Node("Animals", new ObservableCollection<Node>
            {
                new Node("Mammals", new ObservableCollection<Node>
                {
                    new Node("Lion"), new Node("Cat"), new Node("Zebra")
                })
            }),
            new Node("Birds", new ObservableCollection<Node>
            {
                new Node("Robin"), new Node("Condor"),
                new Node("Parrot"), new Node("Eagle")
            }),
            new Node("Insects", new ObservableCollection<Node>
            {
                new Node("Locust"), new Node("House Fly"),
                new Node("Butterfly"), new Node("Moth")
            }),
        };

        var moth = Nodes.Last().SubNodes?.Last();
        if (moth != null) SelectedNodes.Add(moth);
    }
}


public class Node
{
    public ObservableCollection<Node>? SubNodes { get; }
    public string Title { get; }

    public Node(string title)
    {
        Title = title;
    }

    public Node(string title, ObservableCollection<Node> subNodes)
    {
        Title = title;
        SubNodes = subNodes;
    }
}
