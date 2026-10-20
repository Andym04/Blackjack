using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace Blackjack
{
    public partial class DeckWindow : Window
    {
        private ObservableCollection<Card> _deck;

        public DeckWindow(ObservableCollection<Card> deck)
        {
            InitializeComponent();
            _deck = deck;
            DeckListBox.DataContext = _deck;
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DeckListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                _deck.Move(selectedIndex, selectedIndex - 1);
                DeckListBox.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DeckListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _deck.Count - 1)
            {
                _deck.Move(selectedIndex, selectedIndex + 1);
                DeckListBox.SelectedIndex = selectedIndex + 1;
            }
        }
    }
}