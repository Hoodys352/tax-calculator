using System.Windows;
using ObliczeniePodatku.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OPMain : Window
    {
        public OPMain()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == "" || UserPwd.Password == "")
            {
                MessageBox.Show("Login or/and Password are empty. Please, try again");
            }
            else
            {
                UserModel User = new UserModel();
                User.Login = UserName.Text;
                User.Password = PasswordHasher.Encrypt(UserPwd.Password);
                using (AppDbContext context = new AppDbContext())
                {
                    var Users = await context.Users.ToListAsync();
                    foreach(var user in Users)
                    {
                        if(user.Login == User.Login)
                        {
                            if(user.Password == User.Password)
                            {
                                var chooseContract = new ChooseContract();
                                this.Close();
                                chooseContract.Show();
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            var signUp = new SignUp();
            this.Close();
            signUp.Show();
        }

    }
}
