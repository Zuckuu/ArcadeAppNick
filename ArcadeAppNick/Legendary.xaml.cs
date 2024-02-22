using ArcadeAppNick.Models;

namespace ArcadeAppNick;

public partial class Legendary : ContentPage
{

    List<String> cards = new List<String>()
    {
        "blue_whale.png",
        "virus.png",
        "sea_serpent.png",
        "pterodactyl.png",
        "vampire_bat.png",
        "king_cobra.png",
        "king_kong.png",
        "centipede.png"   
    };

    List<String> columns = new List<String>()
    {
        "BlueWhale",
        "Virus",
        "SeaSerpent",
        "Pterodactyl",
        "VampireBat",
        "KingCobra",
        "KingKong",
        "Centipede"
    };

    public Legendary()
	{
		InitializeComponent();
	}

    async private void OpenPackButton_Clicked(object sender, EventArgs e)
    {
        var rand = new Random();
        int rand1 = rand.Next(8);

        rand = new Random();
        int rand2 = rand.Next(8);

        string imageSource1 = cards[rand1];
        string column1 = columns[rand1];
        string imageSource2 = cards[rand2];
        string column2 = columns[rand2];

        Users loggedInUser = App.UserRepo.GetUser(App.LoggedInUser);

        var prop1 = typeof(Users).GetProperty(column1);
        int val1 = (int)prop1.GetValue(loggedInUser);

        var prop2 = typeof(Users).GetProperty(column2);
        int val2 = (int)prop2.GetValue(loggedInUser);

        App.UserRepo.UpdateUserAnimalCard(App.LoggedInUser, column1, val1 + 1);
        App.UserRepo.UpdateUserAnimalCard(App.LoggedInUser, column2, val2 + 1);

        Card1.Source = imageSource1;
        Card2.Source = imageSource2;

        await TopPack.TranslateTo(TopPack.X - 340, 0, 2000, Easing.Linear);
        await TopPack.FadeTo(0);

        await BottomPack.TranslateTo(0, BottomPack.Y + 300, 2000, Easing.Linear);
        await BottomPack.FadeTo(0);

        await Card1.TranslateTo(Card1.X - 60, 0);
        await Card2.TranslateTo(Card2.X + 215, 0);
    }

    async private void ViewInventoryButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("inventory");
    }
}