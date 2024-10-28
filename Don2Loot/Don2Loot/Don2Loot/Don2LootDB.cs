using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Don2Loot
{
    public class Database
    {
        char[] specialCharacters = {',', '<', '.', '>', ';', ':', '\'', '"', '{', '{', ']', '}', '\\',
            '|', '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+'};
        char[] specialCharactersForEmail = {',', '<', '>', ';', ':', '\'', '"', '{', '{', ']', '}', '\\',
            '|', '`', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '='};
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            //checks if file exists beforehand to make sure that it doesnt get
            //created before checking to add db entries
            bool dbNotCreated = true;
            if(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Don2Loot.db3")))
            {
                dbNotCreated = false;
            }

            //makes sure that it doesnt run infinitely when creating default database entries
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Task>().Wait();
            _database.CreateTableAsync<Chest>().Wait();
            _database.CreateTableAsync<Reward>().Wait();

            //if db actually didnt exist add data to db
            if (dbNotCreated)
            {
                generateDBRewards(false, "MUI_Goku", "Mastered Ultra Instinct Goku", 1, "Anime Crate");

                generateDBRewards(false, "Naruto", "Sage of Six Paths Naruto", 1, "Anime Crate");

                generateDBRewards(false, "Saitama", "Saitama", 2, "Anime Crate");

                generateDBRewards(false, "Luffy", "Luffy", 2, "Anime Crate");

                generateDBRewards(false, "Ichigo", "Ichigo", 2, "Anime Crate");

                generateDBRewards(false, "Hisoka", "Hisoka", 3, "Anime Crate");

                generateDBRewards(false, "Yuujiro", "Yuujiro", 3, "Anime Crate");

                generateDBRewards(false, "Attack_Titan", "Attack Titan", 3, "Anime Crate");

                generateDBRewards(false, "Dio", "Dio", 3, "Anime Crate");

                generateDBRewards(false, "Meliodas", "Meliodas", 4, "Anime Crate");

                generateDBRewards(false, "Tanjiro", "Tanjiro", 4, "Anime Crate");

                generateDBRewards(false, "Gojo", "Gojo", 4, "Anime Crate");

                generateDBRewards(false, "Kaneki", "Kaneki", 4, "Anime Crate");

                generateDBRewards(false, "Spongebob_Squarepants", "Spongebob", 1, "Cartoon Crate");

                generateDBRewards(false, "Peter_Griffin", "Peter Griffin", 1, "Cartoon Crate");

                generateDBRewards(false, "Homer_Simpson", "Homer Simpson", 2, "Cartoon Crate");

                generateDBRewards(false, "Bender", "Bender", 2, "Cartoon Crate");

                generateDBRewards(false, "Eric_Cartman", "Eric Cartman", 2, "Cartoon Crate");

                generateDBRewards(false, "Roger", "Roger", 3, "Cartoon Crate");

                generateDBRewards(false, "Rick_Sanchez", "Rick Sanchez", 3, "Cartoon Crate");

                generateDBRewards(false, "Balloony", "Balloony", 3, "Cartoon Crate");

                generateDBRewards(false, "Bill_Cipher", "Bill Cipher", 3, "Cartoon Crate");

                generateDBRewards(false, "Shaun_The_Sheep", "Shaun The Sheep", 3, "Cartoon Crate");

                generateDBRewards(false, "Nijntje", "Nijntje", 4, "Cartoon Crate");

                generateDBRewards(false, "Randall", "Randall", 4, "Cartoon Crate");

                generateDBRewards(false, "Stitch", "Stitch", 4, "Cartoon Crate");

                generateDBRewards(false, "Mike_Wazowski", "Mike Wazowski", 4, "Cartoon Crate");

                generateDBRewards(false, "Winnie_The_Pooh", "Winnie The Pooh", 4, "Cartoon Crate");

                generateDBRewards(false, "Amogus_drip", "Amogus Drip", 1, "Meme Crate");

                generateDBRewards(false, "Gigachad", "Giga Chad", 1, "Meme Crate");

                generateDBRewards(false, "Spiderman_pointing", "Spiderman Pointing", 2, "Meme Crate");

                generateDBRewards(false, "Goku_Drip", "Goku Drip", 2, "Meme Crate");

                generateDBRewards(false, "Big_Chungus", "Big Chungus", 2, "Meme Crate");

                generateDBRewards(false, "Ricardo_Milos", "Ricardo Milos", 3, "Meme Crate");

                generateDBRewards(false, "American_Psycho_Sigma_Male", "Sigma Male", 3, "Meme Crate");

                generateDBRewards(false, "Yes_Chad", "Yes Chad", 3, "Meme Crate");

                generateDBRewards(false, "Are_Ya_Winning_Son", "Are Ya Winning Son?", 3, "Meme Crate");

                generateDBRewards(false, "Giorno_Polpo", "Giorno Polpo", 4, "Meme Crate");

                generateDBRewards(false, "Bongo_Cat", "Bongo Cat", 4, "Meme Crate");

                generateDBRewards(false, "Brainlet_Wojak", "Brainlet Wojak", 4, "Meme Crate");

                generateDBRewards(false, "Yes_Chad", "Yes Chad", 4, "Meme Crate");

                generateDBRewards(false, "Butter_Cat", "Butter Cat", 4, "Meme Crate");

                generateDBChests("Anime Crate", 150, "Anime_Crate");

                generateDBChests("Cartoon Crate", 100, "Cartoon_Crate");

                generateDBChests("Meme Crate", 125, "Meme_Crate");
            }
        }

        //Gets a list of all database entries from all tables
        public Task<List<User>> getUser()
        {
            return _database.Table<User>().ToListAsync();
        }
        public Task<List<Task>> getTask()
        {
            return _database.Table<Task>().ToListAsync();
        }
        public Task<List<Chest>> getChest()
        {
            return _database.Table<Chest>().ToListAsync();
        }
        public Task<List<Reward>> getReward()
        {
            return _database.Table<Reward>().ToListAsync();
        }


        //Adding fields to database
        public Task<int> saveUser(User user)
        {
            User user2 = new User();
            user2.UserName = user.UserName.Trim(specialCharacters);
            user2.UserEmail = user.UserEmail.Trim(specialCharactersForEmail);
            user2.UserSignature = user.UserSignature.Trim(specialCharacters);
            user2.UserCoins = 100;
            user2.UserStreak = user.UserStreak;
            user2.IsLoggedIn = true;
            return _database.InsertAsync(user2);
        }
        public Task<int> saveTask(Task task)
        {
            return _database.InsertAsync(task);
        }
        public Task<int> saveChest(Chest chest)
        {
            return _database.InsertAsync(chest);
        }
        public Task<int> saveReward(Reward reward)
        {
            return _database.InsertAsync(reward);
        }

        //Update specific fields of the user table from a specific user based on the PK
        public Task<int> updateUserName(string PK, string newUserName)
        {
            newUserName = newUserName.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE User SET username = ? WHERE useremail = ?", newUserName, PK);
        }
        public Task<int> updateUserSignature(string PK, string newUserSignature)
        {
            newUserSignature = newUserSignature.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE User SET usersignature = ? WHERE useremail = ?", newUserSignature, PK);
        }
        public Task<int> updateUserStreak(string PK, int newUserStreak)
        {
            return _database.ExecuteAsync("UPDATE User SET userstreak = ? WHERE useremail = ?", newUserStreak, PK);
        }
        public Task<int> updateUserCoins(string PK, int newUserCoins)
        {
            return _database.ExecuteAsync("UPDATE User SET usercoins = ? WHERE useremail = ?", newUserCoins, PK);
        }
        public Task<int> updateIsLoggedIn(string PK, bool isLoggedIn)
        {
            return _database.ExecuteAsync("UPDATE User SET isloggedin = ? WHERE useremail = ?", isLoggedIn, PK);
        }

        //Update specific fields of the tasks table from a specific task based on the PK
        public Task<int> updateTaskName(int PK, string newTaskName)
        {
            newTaskName = newTaskName.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE Task SET taskname = ? WHERE id = ?", newTaskName, PK);
        }
        public Task<int> updateTaskDescription(int PK, string newTaskDescription)
        {
            newTaskDescription = newTaskDescription.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE Task SET taskdescription = ? WHERE id = ?", newTaskDescription, PK);
        }
        public Task<int> updateTaskDeadline(int PK, DateTime newTaskDeadline)
        {
            return _database.ExecuteAsync("UPDATE Task SET taskdeadline = ? WHERE id = ?", newTaskDeadline, PK);
        }
        public Task<int> updateTaskIsFinished(int PK, bool newTaskIsFinished)
        {
            return _database.ExecuteAsync("UPDATE Task SET isfinished = ? WHERE id = ?", newTaskIsFinished, PK);
        }

        //Update specific fields of the Chest table from a specific chest based on the PK
        public Task<int> updateChestPrice(string PK, int newChestPrice)
        {
            return _database.ExecuteAsync("UPDATE Chest SET chestPrice = ? WHERE chestname = ?", newChestPrice, PK);
        }

        //Update specific fields of the Reward table from a specific chest based on the PK
        public Task<int> updateRewardIsUnlocked(int PK, bool newRewardIsUnlocked)
        {
            return _database.ExecuteAsync("UPDATE Reward SET isunlocked = ? WHERE rewardid = ?", newRewardIsUnlocked, PK);
        }
        public Task<int> updateRewardImage(int PK, string newRewardImage)
        {
            newRewardImage = newRewardImage.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE Reward SET rewardimage = ? WHERE rewardid = ?", newRewardImage, PK);
        }
        public Task<int> updateRewardRarity(int PK, string newRewardRarity)
        {
            return _database.ExecuteAsync("UPDATE Reward SET rewardrarity = ? WHERE rewardid = ?", newRewardRarity, PK);
        }
        public Task<int> updateRewardName(int PK, string newRewardName)
        {
            newRewardName = newRewardName.Trim(specialCharacters);
            return _database.ExecuteAsync("UPDATE Reward SET rewardname = ? WHERE rewardid = ?", newRewardName, PK);
        }

        //Delete Tables from database
        public Task<int> deleteUser(User user)
        {
            return _database.ExecuteAsync("DELETE FROM User WHERE useremail = ?", user.UserEmail);
        }
        public Task<int> deleteTask(Task task)
        {
            return _database.ExecuteAsync("DELETE FROM Task WHERE id = ?", task.Id);
        }
        public Task<int> deleteReward(Reward reward)
        {
            return _database.ExecuteAsync("DELETE FROM Reward WHERE rewardid = ?", reward.RewardId);
        }
        public Task<int> deleteChest(Chest chest)
        {
            return _database.ExecuteAsync("DELETE FROM Chest WHERE chestName = ?", chest.ChestName);
        }

        public void generateDBRewards(bool isUnlocked, string rewardImage, string rewardName, int rewardRarity
            , string chestName) {
            Reward reward = new Reward();
            reward.isUnlocked = isUnlocked;
            reward.RewardImage = rewardImage;
            reward.RewardName = rewardName;
            reward.RewardRarity = rewardRarity;
            reward.ChestName = chestName;
            saveReward(reward).Wait();
        }

        public void generateDBChests(string chestName, int chestPrice, string chestImage)
        {
            Chest chest = new Chest();
            chest.ChestPrice = chestPrice;
            chest.ChestImage = chestImage;
            chest.ChestName = chestName;
            saveChest(chest).Wait();
        }

        /// <summary>
        /// Takes the name of the chest as input and returns a random item from the chest based on drop chances
        /// </summary>
        /// <param name="chestName">String. Specifies name of the chest in DB</param>
        public async Task<Reward> getCrateDrop(String chestName) {
            chestName = chestName.Trim(specialCharacters);
            Random rnd = new Random();
            int rarityType = 0;

            //Get rewards into a local variable
            List<Reward> rewards = new List<Reward>();
            rewards = await App.Database.getReward();
            //Leave only the rewards from the desired Chest
            rewards.RemoveAll(r => r.ChestName != chestName);

            //Create a list storing all unique droprates (rates of different categories)
            List<int> dropRates = new List<int>();
            int tempDropRate = 0;
            foreach (Reward reward in rewards) {
                switch (reward.RewardRarity) {
                    case 1:
                        tempDropRate = 1;
                        break;
                    case 2:
                        tempDropRate = 4;
                        break;
                    case 3:
                        tempDropRate = 20;
                        break;
                    case 4:
                        tempDropRate = 75;
                        break;
                }
                if (!dropRates.Contains(tempDropRate)) {
                    dropRates.Add(tempDropRate);
                }
            }

            //Generate list to select the rarirty type randomly
            List<int> dropSelection = new List<int>();
            for (int i = 0; i < dropRates.Count; i++) {
                for (int a = 0; a <= dropRates[i]; a++) {
                    //Add +1 since list indexes start from 0, but rarity types start from 1
                    dropSelection.Add(i+1);
                }
            }

            //Get random rarity type
            int randomNumber = rnd.Next(dropSelection.Count);
            //Save the rarity type
            rarityType = dropSelection[randomNumber];

            //Leave only the rewards with suitable rarityTypes
            rewards.RemoveAll(r => r.RewardRarity != rarityType);

            //Get final random Reward 
            randomNumber = rnd.Next(rewards.Count);
            Reward outputReward = rewards[randomNumber];

            //Update the reward to be unlocked
            _ = updateRewardIsUnlocked(outputReward.RewardId, true);
            return outputReward;
        }
    }
}



    //Creation of the database
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [Column("useremail")]
        [NotNull]
        public string UserEmail { get; set; }

        [Column("username")]
        [NotNull]
        public string UserName { get; set; }

        [Column("usersignature")]
        [NotNull]
        public string UserSignature { get; set; }

        [Column("userstreak")]
        public int UserStreak { get; set; }

        [Column("usercoins")]
        public int UserCoins { get; set; }

        [Column("isloggedin")]
        public bool IsLoggedIn { get; set; }
    }

    [Table("Task")]
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("taskname")]
        [NotNull]
        public string TaskName { get; set; }

        [Column("taskdescription")]
        [NotNull]
        public string TaskDescription { get; set; }

        [Column("taskdeadline")]
        public DateTime TaskDeadline { get; set; }

        [Column("isfinished")]
        public bool IsFinished { get; set; }

        [Column("useremail")]
        [ForeignKey(typeof(User))]
        public string UserEmail { get; set; }
    }

    [Table("Chest")]
    public class Chest
    {
        [PrimaryKey]
        [Column("chestname")]
        [NotNull]
        public string ChestName { get; set; }

        [Column("chestprice")]
        [NotNull]
        public int ChestPrice { get; set; }

        [Column("chestimage")]
        [NotNull]
        public string ChestImage { get; set; }
    }

    [Table("Reward")]
    public class Reward
    {
        [PrimaryKey, AutoIncrement]
        [Column("rewardid")]
        public int RewardId { get; set; }

        [Column("isunlocked")]
        [NotNull]
        public bool isUnlocked { get; set; }

        [Column("rewardimage")]
        [NotNull]
        public string RewardImage { get; set; }

        [Column("rewardrarity")]
        [NotNull]
        public int RewardRarity { get; set; }

        [Column("chestname")]
        [ForeignKey(typeof(Chest))]
        [NotNull]
        public string ChestName { get; set; }

        [Column("rewardname")]
        [NotNull]
        public string RewardName { get; set; }
    }
