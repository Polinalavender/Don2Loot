using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDescPage : ContentPage {
        Task chosenTask;
        public TaskDescPage(Task chosenTask) {
            InitializeComponent();
            this.chosenTask = chosenTask;
        }

        
        protected override async void OnAppearing() {
            base.OnAppearing();
            //Make sure coins are updated when page is opened
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
            taskCoins.Text = user.UserCoins.ToString();

            //Display correct Task information
            taskName.Text = chosenTask.TaskName;
            taskDescription.Text = chosenTask.TaskDescription;
        }
        private void backButton(object sender, EventArgs e) {
            Navigation.PopAsync();
        }
    }
}