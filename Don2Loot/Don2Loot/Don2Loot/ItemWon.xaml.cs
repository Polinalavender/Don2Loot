using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemWon : ContentPage
    {
        Chest usedChest;
        public ItemWon(Chest usedChest)
        {
            InitializeComponent();
            this.BindingContext = this;
            this.usedChest = usedChest;
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
            itemWonCoins.Text = user.UserCoins.ToString();
        }

            private void backButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        async void collectionPageButton(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new CollectionPage(), this);
            await Navigation.PopAsync();
        }

        async void ItemWonButton(object sender, EventArgs e) 
        {
            //Get an Item from the chest
            Reward receivedReward = await ItemDisplayAsync();
            await App.Database.updateRewardIsUnlocked(receivedReward.RewardId, true);
            openCrateButton.IsVisible = false;

            //Subtract the chest price from user coin balance
            List<User> users = new List<User>();
            users = await App.Database.getUser();
            await App.Database.updateUserCoins(users[0].UserEmail, users[0].UserCoins - usedChest.ChestPrice);

            //ANIMATION
            //Make loop go for 3 seconds to run the animation
            Stopwatch timerAnim = new Stopwatch();
            timerAnim.Start();
            while (timerAnim.Elapsed < TimeSpan.FromSeconds(3)) {
                //Make orange background disapperar
                await openCrateBackground.FadeTo(0, 100, Easing.SinInOut);
                openCrateBackground.IsVisible = false;
                //Make the frame go white
                animationOpeningBackground.Opacity = 0;
                animationOpeningBackground.IsVisible = true;
                await animationOpeningBackground.FadeTo(100, 150, Easing.SinIn);
                //Make white frame fade away
                await animationOpeningBackground.FadeTo(0, 100, Easing.SinInOut);
                animationOpeningBackground.IsVisible = false;
                //Make orange frame appear again
                openCrateBackground.Opacity = 0;
                openCrateBackground.IsVisible = true;
                await openCrateBackground.FadeTo(100, 250, Easing.SinIn);
            }
            timerAnim.Stop();
            //Make orange background disapperar
            await openCrateBackground.FadeTo(0, 100, Easing.SinInOut);
            openCrateBackground.IsVisible = false;
            //Make the frame go white
            animationOpeningBackground.Opacity = 0;
            animationOpeningBackground.IsVisible = true;
            await animationOpeningBackground.FadeTo(100, 150, Easing.SinIn);
            //Make white frame fade away
            await animationOpeningBackground.FadeTo(0, 100, Easing.SinInOut);
            animationOpeningBackground.IsVisible = false;
            //Make orange frame appear again
            afterOpeningBackground.Opacity = 0;
            afterOpeningBackground.IsVisible = true;
            await afterOpeningBackground.FadeTo(100, 250, Easing.SinIn);
            openCrateBackground.IsVisible = false;

            timerAnim.Reset();
            rewardImageBind.IsVisible = true;

            //Display item rarity
            rarityTypeLabel.IsVisible = true;
            switch (receivedReward.RewardRarity) {
                case 1:
                    rarityTypeLabel.Text = "Legendary item";
                    break;
                case 2:
                    rarityTypeLabel.Text = "Rare item";
                    break;
                case 3:
                    rarityTypeLabel.Text = "Popular item";
                    break;
                case 4:
                    rarityTypeLabel.Text = "Common item";
                    break;
            }
            //Display Item name
            itemWonName.Text = receivedReward.RewardName;
        }

        private async Task<Reward> ItemDisplayAsync()
        {
            String chestName = usedChest.ChestName;
            Reward receivedReward = await App.Database.getCrateDrop(chestName);
            rewardImageBind.Source = receivedReward.RewardImage;
            return receivedReward;
        }

    }
}