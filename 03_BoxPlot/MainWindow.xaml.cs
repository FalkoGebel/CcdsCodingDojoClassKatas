using System.Windows;

namespace _03_BoxPlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> _values = [];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetValues()
        {
            _values.Clear();
            UpdateCurrentValues();
        }

        private void AddValue()
        {
            if (NewValueTextBox.Text == string.Empty)
                return;

            if (int.TryParse(NewValueTextBox.Text, out int newValue))
            {
                _values.Add(newValue);
                _values = _values.OrderBy(x => x).ToList();
                UpdateCurrentValues();
            }
            else
            {
                MessageBox.Show($"\"{NewValueTextBox.Text}\" is not a valid integer. Input a valid integer and try again",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            NewValueTextBox.Text = string.Empty;
        }

        private void UpdateBoxPlot()
        {
            CurrentValuesBoxPlot.UpdateWithValues(_values);
        }

        private void AddValueButton_Click(object sender, RoutedEventArgs e)
        {
            AddValue();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }

        private void UpdateCurrentValues()
        {
            CurrentValuesTextBox.Text = string.Join(", ", _values);
            UpdateBoxPlot();
        }
    }
}