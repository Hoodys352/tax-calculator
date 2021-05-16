using System.Windows;
using System.Threading.Tasks;
using ObliczeniePodatku.Models;
using Microsoft.EntityFrameworkCore;

namespace ObliczeniePodatku
{
    /// <summary>
    /// Logika interakcji dla klasy SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private string Password { get; set; }
        private bool doesExist { get; set; } = false;

        private async void SignUpWindowsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(usernameSignUp.Text == "" || passwordSignUp.Password == "" || passwordConfirmedSignUp.Password == "")
            {
                MessageBox.Show("At least one of the textboxes is empty");
            }
            else if(passwordSignUp.Password != passwordConfirmedSignUp.Password)
            {
                MessageBox.Show("Passwords dont match");
            }
            else if(usernameSignUp.Text.Length < 3 || usernameSignUp.Text.Length > 10)
            {
                MessageBox.Show("Username must have at least 3 letters, max 10");
            }
            else
            {
                Password = PasswordHasher.Encrypt(passwordConfirmedSignUp.Password);
                UserModel User = new UserModel();
                User.Login = usernameSignUp.Text.Trim();
                User.Password = Password.Trim();
                using (AppDbContext context = new AppDbContext())
                {
                    var Users = await context.Users.ToListAsync();
                    foreach(var user in Users)
                    {
                        if(user.Login == User.Login)
                        {
                            doesExist = true;
                        }
                    }
                    if(doesExist)
                    {
                        MessageBox.Show("User already exists");
                    }
                    else
                    {
                        await context.Users.AddAsync(User);
                        await context.SaveChangesAsync();
                        AccountCreated.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var oPMain = new OPMain();
            this.Close();
            oPMain.Show();
        }
    }

    


}
