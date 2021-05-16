using System;
using System.Windows;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Logika interakcji dla klasy UZFill.xaml
    /// </summary>
    public partial class UZFill : Window
    {

        private double uzSalary;
        private double skladkaZdrowotna;
        private double ubezpieczenieEmerytalne;
        private double ubezpieczenieRentowe;
        private double ubezpieczenieChorobowe;
        private double wynagrodzenieZasadnicze;
        private double miesiecznaUlga;
        private double skladka9Procent;
        private double skladka775Procent;
        private double kosztyUzyskaniaPrzychodu;

        public UZFill()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var chooseContract = new ChooseContract();
            this.Close();
            chooseContract.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetData();
        }

        private void SetData()
        {
            if (Salary.Text == "")
            {
                MessageBox.Show("Salary field is not filled");
            }
            else
            {
                try
                {
                    uzSalary = Convert.ToDouble(Salary.Text);
                    if ((bool)Student.IsChecked)
                    {
                        KwotaNetto.Content = $"Jesteś studentem - kwota brutto = netto. {uzSalary}";
                    }
                    else if ((bool)ZusUTegoSamegoPracodawcy.IsChecked)
                    {
                        KwotaNetto.Content = $"Twoja pensja netto wynosi : {Calculate()}\n" +
                            $"Składki na ubezpieczenia wynoszą {Math.Round(ubezpieczenieEmerytalne + ubezpieczenieRentowe + ubezpieczenieChorobowe, 2)}";
                    }
                    else if ((bool)ZusUInnegoPracodawcy.IsChecked)
                    {
                        skladkaZdrowotna = uzSalary * 0.09;
                        KwotaNetto.Content = $"Twoja pensja netto wynosi : {uzSalary -= skladkaZdrowotna}\n" +
                            $"Składki na ubezpieczenia wynoszą {Math.Round(skladkaZdrowotna, 2)}";
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private double Calculate()
        {
            ubezpieczenieEmerytalne = uzSalary * 0.0976;
            ubezpieczenieRentowe = uzSalary * 0.015;
            ubezpieczenieChorobowe = uzSalary * 0.0245;
            wynagrodzenieZasadnicze = uzSalary - (ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe);
            skladka9Procent = wynagrodzenieZasadnicze * 0.09;
            skladka775Procent = wynagrodzenieZasadnicze * 0.0775;
            wynagrodzenieZasadnicze -= kosztyUzyskaniaPrzychodu;
            wynagrodzenieZasadnicze = Math.Round(wynagrodzenieZasadnicze, MidpointRounding.AwayFromZero);
            miesiecznaUlga = wynagrodzenieZasadnicze * 0.17;
            miesiecznaUlga -= 43.76;
            uzSalary = uzSalary - (ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe) -
                skladka9Procent - Math.Round(miesiecznaUlga - skladka775Procent, MidpointRounding.AwayFromZero);
            return Math.Round(uzSalary, 2);
        }
    }
}
