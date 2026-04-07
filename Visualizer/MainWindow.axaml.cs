using System.Collections.Generic;
using Avalonia.Controls;

namespace Visualizer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new Data();
        }

        public class Data
        {
            public List<DataItem> Items { get; set; } = new List<DataItem>() { new DataItem("One", 1), new DataItem("Two", 2), new DataItem("Three", 3) };
            public DataItem? Current { get; set; }

            public void AddItemCommand()
            {

            }

            public class DataItem
            {
                public string Name { get; set; } = string.Empty;
                public int Value { get; set; }
                public DataItem() { }
                public DataItem(string name, int value)
                {
                    this.Name = name;
                    this.Value = value;
                }

                public  override string ToString() => this.Name;
            }
        }

        private void OnAddNewClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}