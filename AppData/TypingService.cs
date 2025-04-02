using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastType.AppData
{
    public class TypingService
    {
        //Создаем поля для ХРАНИЕНИ и ИСПОЛЬЗОВАНИЕ прерменных для внутренней логики класса
        private Stopwatch _stopWatch;

        //Создали поля для ХРАНЕНИЯ и ИСПОЛЬЗОВАНИЯ элементов управления в рамках класса

        private Grid _keyboardGrid;
        private TextBox _typingTextBox;
        private TextBlock _typingTextBlock;
        private TextBlock _speedTextBlock;
        private ProgressBar _progressBar;
        //Создаем свойство для ПОЛУЧЕНИЯ и ПРИСВАИВАНИЯ расчетов
        public double TypeSpeed { get; private set; }

        //Создаём конструктор класса для ПРИЁМА элементов кправления из интерфейса (TypingTutorPage.xaml)
        public TypingService(Grid keyboardGrid, TextBox typingTextBox, TextBlock typingTextBlock, TextBlock speedTextBlock, ProgressBar progressBar)
        {
            _stopWatch = new Stopwatch();
            _keyboardGrid = keyboardGrid;
            _typingTextBox = typingTextBox;
            _typingTextBlock = typingTextBlock;
            _speedTextBlock = speedTextBlock;
            _progressBar = progressBar;


            _typingTextBox.PreviewKeyDown += _typingTextBox_PreviewKeyDown;
            _typingTextBox.PreviewKeyUp += _typingTextBox_PreviewKeyUp;
            _typingTextBox.TextChanged += _typingTextBox_TextChanged;
        }

        private void _typingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_typingTextBox.Text.Length >= 1 && !_stopWatch.IsRunning)
            {
                _stopWatch.Start();
            }
            if (_typingTextBlock.Text == _typingTextBox.Text)
            {
                _stopWatch.Stop();
            }
            if (_typingTextBox.Text.Length >= 2)
            {
                _speedTextBlock.Text = CalculateSpeed();
            }
            UpdateProgress();
        }

        private void _typingTextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var button = FindButtonByKey(e.Key);
            if (button != null)
            {
                button.BorderThickness = new Thickness(0);
            }
        }

        private void _typingTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Показывал название кнопки в системе
            //    _typingTextBox.Clear();
            //    _typingTextBox.Text = e.Key.ToString();

            var button = FindButtonByKey(e.Key);
            if (button != null)
            {
                button.BorderThickness = new Thickness(2.0, 2.0, 2.0, 4.0);
            }

        }

        private Button FindButtonByKey(Key key)
        {
            foreach (var stackPanel in _keyboardGrid.Children.OfType<StackPanel>())
            {
                foreach (var button in stackPanel.Children.OfType<Button>())
                {
                    if (button.Tag.ToString() == key.ToString())
                    {
                        return button;
                    }
                }
            }
            return null;
        }
        private string CalculateSpeed()
        {
            TypeSpeed = _typingTextBox.Text.Length / _stopWatch.Elapsed.TotalMinutes;
            return $"{TypeSpeed:F0} СВМ ";
        }
        private void UpdateProgress()
        {
            double result = _typingTextBox.Text.Length * 100.0 / _typingTextBlock.Text.Length;
            _progressBar.Value = Math.Floor(result);
        }
    }
}
