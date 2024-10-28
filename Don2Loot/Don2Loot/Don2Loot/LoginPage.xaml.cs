using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Don2Loot
{
    public partial class LoginPage : ContentPage
    {
        Contact contact = new Contact
        {
            Email = "",
            Name = ""
        };
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<User> users = new List<User>();
            users = await App.Database.getUser();
            bool loggedIn = false;
            foreach (var user in users)
            {
                if (user.IsLoggedIn)
                {
                    loggedIn = true;
                    contact.Email = user.UserEmail;
                    contact.Name = user.UserName.ToUpper();
                    break;
                }
            }

            if (loggedIn)
            {
                var mainPage = new MainPage();
                mainPage.BindingContext = contact;
                Navigation.InsertPageBefore(mainPage, this);
                await Navigation.PopAsync();
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Warning!", "All fields must be filled in!", "Ok");
                return;
            }

            if (txtName.Text.Length > 20 && txtName.Text.Length < 2)
                {
                    await DisplayAlert("Warning!", "Username must contain 2-20 characters", "Ok");
                    return;
                }

            if (!IsAllLetters(txtName.Text))
            {
                await DisplayAlert("Warning!", "Username must only contain letters", "Ok");
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                await DisplayAlert("Warning!", "Email is not valid", "Ok");
                return;
            }
                var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                if(image == null) 
                {
                    await DisplayAlert("Warning", "All fields must be filled in!", "Ok");
                    return;
                }
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                User user = new User();
                user.UserName = txtName.Text;
                user.UserEmail = txtEmail.Text;
                user.UserSignature = base64Val;
                bool userExists = false;
                List<User> users = new List<User>();
            foreach (var oldUser in users)
            {
                if(oldUser.UserEmail == txtEmail.Text)
                {
                    userExists = true;
                    user = oldUser;
                    break;
                }
            }
            contact.Email = user.UserEmail;
            contact.Name = user.UserName.ToUpper();
            if (userExists)
            {
                await App.Database.updateIsLoggedIn(user.UserEmail, true);
            }
            else
            {
                await App.Database.saveUser(user);
            }
            var mainPage = new MainPage();
            mainPage.BindingContext = contact;
            Navigation.InsertPageBefore(mainPage, this);    //inserting page before current page
            await Navigation.PopAsync();                    //popping current page off the navigation stack (Mainpage is now the main page/root page
        }

        public bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    internal class Contact
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
