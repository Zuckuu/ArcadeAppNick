namespace ArcadeAppNick;

public partial class Clicker_MainPage : ContentPage
{
    public bool game_ready = false;
    public int tier = 1;
    public Bank bank;
    public Image coinImg;
    public Clicker clicker = new Clicker(1, 5, 1);
    public Passive passive = new Passive(0, 100, 0);
    public Interest interest = new Interest(1, 500, 1);
    public Clicker_MainPage()
	{
		InitializeComponent();
	}

    private void start_Button_Clicked(object sender, EventArgs e)
    {
        if (username_Entry.Text != null)
        {
            game_ready = true;
            username_Label.Text = "Hello " + username_Entry.Text.ToString();
            bank = new Bank(username_Entry.Text, 0, 1, 1, 200000);
            start_Game(game_ready);
        }
        else
        {
            username_Entry.Placeholder = "Please enter your Name!";
        }
    }

    private void image_Clicker_Clicked(object sender, EventArgs e)
    {
        bank.addBalance(clicker.clicker_Value * interest.interest_Value);
        changeMoney_Label();
    }

    private void clicker_upgrade_Clicked(object sender, EventArgs e)
    {
        if (checkBalance(clicker.clicker_Cost))
        {
            clicker.upgrade();
            clicker_upgrade.Text = "Clicker Upgrade: Lv." + clicker.clicker_Level + " Cost: $" + clicker.clicker_Cost;
            changeMoney_Label();
        }
    }

    private void passive_upgrade_Clicked(object sender, EventArgs e)
    {
        if (checkBalance(passive.passive_Cost))
        {
            passiveIncome(interest.interest_Value);
            passive.upgrade();
            passive_upgrade.Text = "Passive Income: Lv. " + passive.passive_Level + " Cost: $" + passive.passive_Cost;
            changeMoney_Label();
        }

    }

    private void interest_upgrade_Clicked(object sender, EventArgs e)
    {
        if (checkBalance(interest.interest_Cost))
        {
            interest.upgrade();
            interest_upgrade.Text = "Interest Upgrade: Lv." + interest.interest_Level + " Cost: $" + interest.interest_Cost;
            changeMoney_Label();
        }
    }

    private void buy_coin_Clicked(object sender, EventArgs e)
    {
        if (checkBalance(bank.coin_Cost))
        {
            bank.addCoinImg(coinImg, coin_pouch);
            buy_coin.Text = "Buy Coin! Cost: " + bank.coin_Cost;
            changeMoney_Label();
        }

    }

    private void tier_upgrade_Clicked(object sender, EventArgs e)
    {
        if (bank.buyLevel(bank.level_Cost, coin_pouch, coinImg))
        {
            tier++;
            changeTier_Label();
        }
    }


    public void start_Game(bool ready)
    {
        if (ready)
        {
            animation();
            game_board.IsEnabled = true;
            game_board.IsVisible = true;
            username_Entry.IsEnabled = false;
            username_Entry.IsVisible = false;
            start_Button.IsVisible = false;
            start_Button.IsEnabled = false;
            game_board.TranslationY = (game_board.Y - 60);
        }

    }

    async public void animation()
    {
        uint duration = 120 * 60 * 1000; // 2 hours
        int timer = 0;

        while (duration > timer)
        {
            timer++;
            await image_Clicker.ScaleTo(.98, 500);
            await image_Clicker.ScaleTo(1, 500);
            await image_Clicker.ScaleTo(1.02, 500);
        }
    }

    public bool checkBalance(int cost)
    {
        if (bank.balance >= cost)
        {
            bank.balance -= cost;
            return true;
        }
        return false;
    }

    async public void passiveIncome(int interestValue)
    {
        bank.balance += passive.passive_Value * interestValue * bank.multi;
        changeMoney_Label();
        await Task.Delay(1000);
        passiveIncome(interest.interest_Value);
    }

    public void changeMoney_Label()
    {
        money_Label.Text = "$ " + bank.balance.ToString();
    }

    public void changeTier_Label()
    {
        if (tier == 2)
        {
            tier_upgrade.Text = "Bank Tier Upgrade: Lv." + bank.level + " Cost: " + bank.level_Cost + " Coins";
            tier2_box.BackgroundColor = Colors.Green;
        }
        if (tier == 3)
        {
            tier_upgrade.Text = "Bank Tier Upgrade: Lv." + bank.level + " Cost: " + bank.level_Cost + " Coins";
            tier3_box.BackgroundColor = Colors.Green;
        }
        if (tier == 4)
        {
            tier_upgrade.Text = "Bank Tier Upgrade: Lv." + bank.level + " MAX!";
            tier4_box.BackgroundColor = Colors.Green;
            tier_upgrade.IsEnabled = false;
            buy_coin.IsEnabled = false;
        }
    }
}

public class Bank
{
    public string name;
    public int balance;
    public int level;
    public int level_Cost;
    public int coin_Cost;
    List<Image> pouch = new List<Image>();
    public int multi = 1;

    public Bank(string n, int b, int lv, int lvc, int cc)
    {
        name = n;
        balance = b;
        level = lv;
        level_Cost = lvc;
        coin_Cost = cc;
    }

    public void addBalance(int value)
    {
        balance += value * multi;
    }

    public bool buyLevel(int cost, VerticalStackLayout pouchStack, Image coinImg)
    {
        if (pouch.Count >= cost)
        {
            if (cost == 1)
            {
                pouch.RemoveAt(0);
                pouchStack.Children.RemoveAt(0);
                level++;
                multi++;
                level_Cost *= 2;
                return true;
            }
            else
            {
                for (int i = 0; i < cost; i++)
                {
                    pouch.RemoveAt(0);
                    pouchStack.Children.RemoveAt(0);
                }
                level++;
                multi++;
                level_Cost *= 2;
                return true;
            }
        }
        return false;
    }

    public void addCoinImg(Image coinImg, VerticalStackLayout pouchStack)
    {
        coinImg = new Image() { Source = "coin.png", HeightRequest = 40, };
        pouch.Add(coinImg);
        pouchStack.Add(coinImg);
        coin_Cost *= 2;
    }

}

public class Clicker
{
    public int clicker_Value;
    public int clicker_Cost;
    public int clicker_Level;

    public Clicker(int v, int c, int lv)
    {
        clicker_Value = v;
        clicker_Cost = c;
        clicker_Level = lv;
    }

    public void upgrade()
    {
        clicker_Value += 3;
        clicker_Cost *= 2;
        clicker_Level++;
    }
}

public class Passive
{
    public int passive_Value;
    public int passive_Cost;
    public int passive_Level;

    public Passive(int v, int c, int lv)
    {
        passive_Value = v;
        passive_Cost = c;
        passive_Level = lv;
    }

    public void upgrade()
    {
        passive_Value += 2;
        passive_Cost *= 2;
        passive_Level++;
    }
}

public class Interest
{
    public int interest_Value;
    public int interest_Cost;
    public int interest_Level;

    public Interest(int v, int c, int lv)
    {
        interest_Value = v;
        interest_Cost = c;
        interest_Level = lv;
    }

    public void upgrade()
    {
        interest_Value++;
        interest_Cost *= 2;
        interest_Level++;
    }
}
