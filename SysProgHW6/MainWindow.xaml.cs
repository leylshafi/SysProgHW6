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
    int i = 0;
    int startCount = 0;
    public MainWindow()
    {
        InitializeComponent();
        DataContext= this;
        fittingWords = new();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!words.Contains(tb.Text))
        {
            words.Add(tb.Text);
            tb.Text= string.Empty;
        }
           
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        tb.Text= String.Empty;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        tb.Text = tb.Text;
        fittingWords.Clear();
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        tb.Text = String.Empty;
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
                    if (startCount > 0 && word.ToLower().StartsWith(tb.Text.Substring(0, startCount).ToLower()))
                    {
                        fittingWords.Add(word);
                    }

                    if (word.ToLower().StartsWith(tb.Text.ToLower()) && startCount==0)
                        fittingWords.Add(word);
                }
            });
        });
    }
    int tempStartIndex;
    private void Button_Click_4(object sender, RoutedEventArgs e)
    {
        var selectedWord = fittingWords[i];
        var startIndex= tempStartIndex;
        if (i==0)
        {
            startIndex = tb.Text.Length;
            tempStartIndex = startIndex;
            startCount = startIndex;
        }
        
        var length = selectedWord.Length - startIndex;
        lb.SelectedIndex = i;
        if (i < fittingWords.Count - 1)
            i++;
        tb.Text=lb.SelectedItem.ToString(); 
        tb.SelectionBrush = Brushes.Gray;

        tb.Select(startIndex, length);
        tb.Focus();
    }

    private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //tb.Text = lb.SelectedItem.ToString();
    }
}
