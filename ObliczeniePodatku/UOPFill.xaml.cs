using System;
using System.Windows;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Logika interakcji dla klasy UOPFill.xaml
    /// </summary>
    public partial class UOPFill : Window
    {
        public UOPFill()
        {
            InitializeComponent();
        }


        private double salary;
        private double ubezpieczenieEmerytalne;
        private double ubezpieczenieRentowe;
        private double ubezpieczenieChorobowe;
        private double wynagrodzenieZasadnicze;
        private double kosztyUzyskaniaPrzychodu;
        private double miesiecznaUlga;
        private double skladka9Procent;
        private double skladka775Procent;
        private void SetData()
        {
            try
            {
                salary = Convert.ToDouble(Salary.Text);
                if ((bool)PracaWMiejscuZamieszkania.IsChecked)
                {
                    kosztyUzyskaniaPrzychodu = 250.0;
                }
                else
                {
                    kosztyUzyskaniaPrzychodu = 300.0;
                }
            }
            catch(Exception ex)
            {
                test.Content = $"{ex.Message}";
            }
        }

        private double Calculate()
        {
            ubezpieczenieEmerytalne = salary * 0.0976;
            ubezpieczenieRentowe = salary * 0.015;
            ubezpieczenieChorobowe = salary * 0.0245;
            wynagrodzenieZasadnicze = salary - (ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe);
            skladka9Procent = wynagrodzenieZasadnicze * 0.09;
            skladka775Procent = wynagrodzenieZasadnicze * 0.0775;
            wynagrodzenieZasadnicze -= kosztyUzyskaniaPrzychodu;
            wynagrodzenieZasadnicze = Math.Round(wynagrodzenieZasadnicze, MidpointRounding.AwayFromZero);
            miesiecznaUlga = wynagrodzenieZasadnicze * 0.17;
            miesiecznaUlga -= 43.76;
            salary = salary - (ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe) - 
                skladka9Procent - Math.Round(miesiecznaUlga - skladka775Procent, MidpointRounding.AwayFromZero);
            return Math.Round(salary, 2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetData();
            if ((bool)WiekPowyzej.IsChecked)
            {
                test.Content =
                    $"Pensja netto: {Calculate().ToString()}\n" +
                    $"ZUS : {Math.Round(ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe, 2)}\n" +
                    $"Składka zdrowotna : {Math.Round(skladka775Procent + skladka9Procent, 2)}";
            }
            else
            {
                test.Content = 
                    $"Pensja netto: {Math.Round(Calculate() * 1.07, MidpointRounding.AwayFromZero)}\n" +
                    $"ZUS : {Math.Round(ubezpieczenieChorobowe + ubezpieczenieEmerytalne + ubezpieczenieRentowe, 2)}\n" +
                    $"Składka zdrowotna : {Math.Round(skladka775Procent + skladka9Procent, 2)}";
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var chooseContract = new ChooseContract();
            this.Close();
            chooseContract.Show();
        }
    }
}
