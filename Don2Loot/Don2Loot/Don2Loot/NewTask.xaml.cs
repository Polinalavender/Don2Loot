using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot
{
    public partial class NewTask : ContentPage
    {
        //private readonly NotificationSerializer notificationSerializer;
        public NewTask()
        {
            InitializeComponent();
        }

        //Make sure coins are updated when page is opened
        protected override async void OnAppearing() {
            base.OnAppearing();
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
            newTaskCoins.Text = user.UserCoins.ToString();
        }
        private void Cancel(object sender, EventArgs e)
        {
            txtFileText.Text = "";
            txtFileName.Text = "";

        }
        protected async void Save(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtFileText.Text) && !string.IsNullOrEmpty(txtFileName.Text))
            {
                Task task = new Task();
                task.TaskDescription = txtFileText.Text;
                task.TaskName = txtFileName.Text;
                await App.Database.saveTask(task);
                List<Task> tasks = new List<Task>();
                tasks = await App.Database.getTask();
                int taskId = tasks[tasks.Count() - 1].Id;

                var notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Description = "How did you do on " + tasks[tasks.Count() - 1].TaskName.ToLower() + "?",
                    Title = tasks[tasks.Count() - 1].TaskName.ToUpper(),
                    ReturningData = taskId.ToString(),
                    NotificationId = taskId,
                    Schedule =
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5),
                        RepeatType = NotificationRepeat.Daily
                    },
                    Android =
                    {
                        Priority = AndroidNotificationPriority.Max
                    }
                };

                await NotificationCenter.Current.Show(notification);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Warning", "Please give the task a title and description", "Ok");
            }
        }
        async void backButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}