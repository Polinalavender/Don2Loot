using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Don2Loot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionPage : ContentPage
    {
        
        
        public CollectionPage()
        {
            InitializeComponent();
            FlowListView.Init();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //grabbing rewards from database and placing it in local variable
            List<Reward> rewards = new List<Reward>();
            rewards = await App.Database.getReward();

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
            collectionCoins.Text = user.UserCoins.ToString();

        //creating groups to organize rewards based on what chest they were in
        var groupByLastNamesQuery =
            from reward in rewards
            group reward by reward.ChestName into newGroup
            orderby newGroup.Key
            select newGroup;

            var images2 = groupByLastNamesQuery.ToList();

            List<imageGroup> images = new List<imageGroup>();
            //checks if database isnt empty
            if(images2 != null)
            {
                //create all groups first
                for(int i = 0; i < images2.Count; i++)
                {
                    images.Add(new imageGroup(images2[i].Key));
                }
                //add items into groups
                for(int i = 0; i < images2.Count; i++)
                {
                    foreach(var img in images2[i])
                    {
                        if (img.isUnlocked)
                        {
                            images[i].Add(new image() { Image = img.RewardImage });
                        }
                    }
                }
            }
            //making the lists source the sorted and unlocked reward images
            myListView.FlowItemsSource = images;
        }

        //returns to mainpage
        async void backButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

    //Listview requires lists of objects
    //Image is the stored image
    public class image
    {

        public string Image { get; set; }
    }

    //imagegroup is a list of images including the group name or title from all the images
    public class imageGroup : List<image>
    {
        public string Title { get; set; }
        public imageGroup(string title)
        {
            Title = title;
        }

        public IList<image> All { private set; get; }

    }
}