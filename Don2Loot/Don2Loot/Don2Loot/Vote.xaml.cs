using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vote : ContentPage
    {
        Task currentTask;
        public Vote(Task currentTask)
        {
            InitializeComponent();
            this.currentTask = currentTask;
        }

        //Update coins
        protected override async void OnAppearing() {
            base.OnAppearing();
            List<User> users = new List<User>();
            users = await App.Database.getUser();
            User user = new User();
            foreach(User tempUser in users)
            {
                if(tempUser.IsLoggedIn)
                {
                    user = tempUser;
                }
            }
            voteCoins.Text = user.UserCoins.ToString();

            taskQuestion.Text = "How did you do on " + currentTask.TaskName;
            
        }
        async void backButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void setBackButton(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "YOU LOST YOUR STREAK", "Ok");
            List<User> users = new List<User>();
            users = await App.Database.getUser();
            User user = new User();
            foreach (User tempUser in users)
            {
                if (tempUser.IsLoggedIn)
                {
                    user = tempUser;
                }
            }
            await App.Database.updateUserStreak(user.UserEmail, 0);
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopToRootAsync();
        }
        async void victoryButton(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
            users = await App.Database.getUser();
            User user = new User();
            foreach (User tempUser in users)
            {
                if (tempUser.IsLoggedIn)
                {
                    user = tempUser;
                }
            }
            int newStreak = user.UserStreak + 1;
            int amountOfCoins = user.UserCoins + 100 + (10 * user.UserStreak);
            await App.Database.updateUserCoins(user.UserEmail, amountOfCoins);
            await App.Database.updateUserStreak(user.UserEmail, newStreak);
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopToRootAsync();
        }
    }
}