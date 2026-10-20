using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Blackjack
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Card> Deck { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> PlayerHand { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> DealerHand { get; set; } = new ObservableCollection<Card>();

        private int _playerScore;
        public int PlayerScore
        {
            get => _playerScore;
            set
            {
                _playerScore = value;
                OnPropertyChanged(nameof(PlayerScore));
                OnPropertyChanged(nameof(PlayerScoreHeader));
            }
        }

        private int _dealerScore;
        public int DealerScore
        {
            get => _dealerScore;
            set
            {
                _dealerScore = value;
                OnPropertyChanged(nameof(DealerScore));
                OnPropertyChanged(nameof(DealerScoreHeader));
            }
        }

        public string PlayerScoreHeader => $"Player — Total: {PlayerScore}";
        public string DealerScoreHeader => $"Dealer — Total: {DealerScore}";

        private DeckWindow? _deckWindow; // Modeless window reference

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            StartNewGame();
        }

        private void StartNewGame()
        {
            Deck.Clear();
            PlayerHand.Clear();
            DealerHand.Clear();

            string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            foreach (var suit in suits)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    string rank = ranks[i];
                    int value = i + 1;
                    if (value > 10) value = 10;
                    if (rank == "Ace") value = 11;

                    Deck.Add(new Card(suit, rank, value));
                }
            }

            ShuffleDeck();

            // Deal 2 cards to dealer and 2 cards to player
            DealCardTo(DealerHand);
            DealCardTo(DealerHand);
            DealCardTo(PlayerHand);
            DealCardTo(PlayerHand);

            CalculateScores();
        }

        private void ShuffleDeck()
        {
            Random rng = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Deck.Move(k, n);
            }
        }

        private void DealCardTo(ObservableCollection<Card> hand)
        {
            if (Deck.Count > 0)
            {
                Card topCard = Deck[0];
                Deck.RemoveAt(0);
                hand.Add(topCard);
            }
        }

        private int CalculateHandScore(ObservableCollection<Card> hand)
        {
            int score = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                score += card.Value;
                if (card.Rank == "Ace")
                {
                    aceCount++;
                }
            }

            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        private void CalculateScores()
        {
            PlayerScore = CalculateHandScore(PlayerHand);
            DealerScore = CalculateHandScore(DealerHand);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
            if (_deckWindow != null)
            {
                _deckWindow.DataContext = Deck;
            }
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            DealCardTo(PlayerHand);
            CalculateScores();
        }

        private void OpenDeckWindow_Click(object sender, RoutedEventArgs e)
        {
            if (_deckWindow == null)
            {
                _deckWindow = new DeckWindow(Deck);
                _deckWindow.Owner = this;
                _deckWindow.Closed += (closedSender, args) => _deckWindow = null;
                _deckWindow.Show();
            }
            else
            {
                _deckWindow.Activate();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}