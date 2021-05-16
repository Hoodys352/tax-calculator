using System.Windows;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseContract.xaml
    /// </summary>
    public partial class ChooseContract : Window
    {
        public ChooseContract()
        {
            InitializeComponent();
        }

        private void UOPBtn_Click(object sender, RoutedEventArgs e)
        {
            var uopFill = new UOPFill();
            this.Close();
            uopFill.Show();
        }

        private void UZBtn_Click(object sender, RoutedEventArgs e)
        {
            var uzFill = new UZFill();
            this.Close();
            uzFill.Show();
        }

        private void UODBtn_Click(object sender, RoutedEventArgs e)
        {
            var uodFill = new UODFill();
            this.Close();
            uodFill.Show();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var oPMain = new OPMain();
            this.Close();
            oPMain.Show();
        }
    }
}
