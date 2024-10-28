using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        public ICommand openCrateCommand => new Command(openCrate);
        public StorePage()
        {
            InitializeComponent();
            FlowListView.Init();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Chest> chests = await App.Database.getChest();
            storePageView.FlowItemsSource = chests;
            //Update coins
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
            storePageCoins.Text = user.UserCoins.ToString();
        }

            private void backButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void openCrate(object sender)
        {
            List<User> user = await App.Database.getUser();
            Chest chest = (Chest)sender;
            if (user != null)
            {
                if (user[0].UserCoins < chest.ChestPrice)
                {
                    await DisplayAlert("not enough money", "you lack the required funds", "ok");
                } else
                {
                    await Navigation.PushAsync(new ItemWon((Chest)sender));
                }
            }
            
        }
    }
}