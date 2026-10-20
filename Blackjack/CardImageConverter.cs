using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    public class CardImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Card card)
            {
                // Mapeo del palo (Suit) según los nombres de tus archivos
                string suitPrefix = card.Suit switch
                {
                    "Hearts" => "C",
                    "Spades" => "D",
                    "Diamonds" => "P",
                    "Clubs" => "T",
                    _ => ""
                };

                // Mapeo del rango (Rank) según los nombres de tus archivos
                string rankSuffix = card.Rank switch
                {
                    "Ace" => "As",
                    "Jack" => "Jota",
                    "Queen" => "Reina",
                    "King" => "Rey",
                    "10" => "10",
                    _ => card.Rank.Length == 1 ? $"0{card.Rank}" : card.Rank
                };

                // Nombre del archivo (ej. C_02.jpg o C_02.png)
                // Asegúrate de cambiar .jpg por .png si tus archivos tienen esa extensión
                string imageFilename = $"{suitPrefix}_{rankSuffix}.png";

                // Ruta absoluta en tu computadora
                string imagePath = $@"C:\Users\andym\source\repos\Blackjack\Blackjack\Deck of cards\{imageFilename}";

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // Evita que la imagen se quede bloqueada en disco
                    bitmap.EndInit();
                    return bitmap;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}