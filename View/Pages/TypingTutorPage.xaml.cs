using FastType.AppData;
using System.Windows;
using System.Windows.Controls;

namespace FastType.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TypingTutorPage.xaml
    /// </summary>
    public partial class TypingTutorPage : Page
    {
        private TypingService _typingService;
        public TypingTutorPage()
        {
            InitializeComponent();

            _typingService = new TypingService(KeyboardGrid, TypingTutorTb, TypingTutorTbl, SpeedTbl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
