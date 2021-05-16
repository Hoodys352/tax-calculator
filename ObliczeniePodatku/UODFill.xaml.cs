using System;
using System.Windows;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Logika interakcji dla klasy UODFill.xaml
    /// </summary>
    public partial class UODFill : Window
    {
        public UODFill()
        {
            InitializeComponent();
        }

        private double uODSalary;
        private double deductibleCosts;
        private void SetData()
        {
            if(Salary.Text == "")
            {
                MessageBox.Show("Salary field is not filled");
            }
            else
            {
                try
                {
                    uODSalary = Convert.ToDouble(Salary.Text);
                    if ((bool)FifyPercent.IsChecked)
                    {
                        deductibleCosts = 0.5;
                    }
                    else
                    {
                        deductibleCosts = 0.2;
                    }
                } catch( Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private double Calculate()
        {
            double temp1 = uODSalary, temp2 = uODSalary;
            temp1 *= deductibleCosts;
            temp2 -= temp1;
            temp2 *= 0.17;
            temp2 = Math.Round(temp2, MidpointRounding.AwayFromZero);
            return uODSalary - temp2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetData();
            KwotaNetto.Content = $" Twoja pensja netto : {Calculate()} PLN";
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {

            var chooseContract = new ChooseContract();
            this.Close();
            chooseContract.Show();
        }
    }
}
