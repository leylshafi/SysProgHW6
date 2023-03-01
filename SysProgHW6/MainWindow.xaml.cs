using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace SysProgHW6;

public partial class MainWindow : Window
{
    ObservableCollection<string> words = new ObservableCollection<string>();
    public ObservableCollection<string> fittingWords { get; set; }
    bool allow=false;
    public MainWindow()
    {
        InitializeComponent();
        DataContext= this;
        fittingWords = new();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if(!words.Contains(tb.Text))
            words.Add(tb.Text);
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        tb.Text= String.Empty;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        //if(tb.SelectedItem is not null)
        //{
        //    tb.Text= tb.SelectedItem.ToString();
        //}
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        // tb.Focus();
        // tb.Select(tb.Text.);
    }

    private void tb_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tb.Text))
        {
            fittingWords.Clear();
            return;
        }

        Task.Run(() =>
        {
            Dispatcher.Invoke(() =>
            {
                fittingWords.Clear();

                foreach (var word in words)
                {
                    if (word.ToLower().StartsWith(tb.Text.ToLower()))
                        fittingWords.Add(word);
                }
                
            });
        });
    }
}
