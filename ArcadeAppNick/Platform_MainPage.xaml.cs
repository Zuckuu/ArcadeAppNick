using CommunityToolkit.Maui.Views;

namespace ArcadeAppNick;

public partial class Platform_MainPage : ContentPage
{
    public bool gridReady;
    private Player user;
    private Platform[] platformList = new Platform[4];
    public int level = 1;
    public int score = 0;
    public Label scoreLabel;
    public Label levelLabel;
    public double difficulty = 1000;
    public CloudEnemy cloud;
    public Button startButton;
    public MediaElement gameOver; 

    public Platform_MainPage()
    {
        InitializeComponent();
        startButton = Start_Button;
        gameOver = Game_Over_Sound; 
    }

    private void Start_Button_Clicked(object sender, EventArgs e)
    {
        Fill_Grid(true);
        gridReady = true;
        Start_Button.IsEnabled = false;
        Grid_Sound.Play(); 
    }

    private void Reset_Button_Clicked(object sender, EventArgs e)
    {
        gameGrid.Clear();
        Start_Button.IsEnabled = true;
        gridReady = true;
        score = 0;
        level = 1;
        Start_Button.Text = "Play!";
    }

    async public void Fill_Grid(bool start)
    {
        var randCloudPosition = new Random();
        int cloudRow = randCloudPosition.Next(4);
        if (start)
        {
            int rows = 5;
            int columns = 5;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    StackLayout unit = new StackLayout() { ZIndex = 0 };
                    unit.BackgroundColor = Colors.LightSkyBlue;
                    gameGrid.Add(unit, j, i);

                    if (i < 4 && j == 0)
                    {
                        BoxView newRect = new BoxView()
                        {
                            HeightRequest = 20,
                            Color = Colors.Green,
                            VerticalOptions = LayoutOptions.End,
                            CornerRadius = 10,
                            ZIndex = 1
                        };
                        Platform newPlat = new Platform(newRect, i, j);
                        platformList[i] = newPlat;
                        gameGrid.Add(newRect, newPlat.col, newPlat.row);
                    }

                    await Task.Delay(100);
                }
            }

            scoreLabel = new Label()
            {
                Text = "Score: " + score,
                FontSize = 30,
                ZIndex = 1,
                TextColor = Colors.White,
                Margin = new Thickness(10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            levelLabel = new Label()
            {
                Text = "Level: " + level,
                FontSize = 30,
                ZIndex = 1,
                TextColor = Colors.White,
                Margin = new Thickness(10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            await Task.Delay(100);
            gameGrid.Add(scoreLabel, 0, 4);
            await Task.Delay(100);
            user = Create_User();
            gameGrid.Add(user.image, user.col, user.row);
            await Task.Delay(100);
            gameGrid.Add(levelLabel, 4, 4);

            Image cloudIMG = new Image() { Source = "cloud_enemy.png" };
            cloud = new CloudEnemy(cloudIMG, cloudRow, 4, user);
            gameGrid.Add(cloudIMG, 4, cloudRow);
            cloud.Oscillate(gameGrid, this);
        }
        else //reset user and platforms --> Level Reset State
        {
            user.row = 4;
            gameGrid.SetRow(user.image, user.row);
            cloud.row = cloudRow;
            gameGrid.SetRow(cloud.cloudImage, cloudRow);
            foreach (Platform plat in platformList)
            {
                plat.col = 0;
                gameGrid.SetColumn(plat.rect, plat.col);
            }
        }

        foreach (Platform plat in platformList)
        {
            plat.isMovingRight = true;
            plat.Oscillate(gameGrid, this);
        }


    }

    private Player Create_User()
    {
        Image userIcon = new Image { Source = "dotnet_bot.png", ZIndex = 1 };
        user = new Player(userIcon, 4, 2);
        return user;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (gridReady)
        {
            gridReady = user.Jump(gameGrid, platformList, gridReady, this);
            if (gridReady == false)
            {
                Start_Button.Text = "Game Over!";
            }
        }
    }
}

public class Player
{
    public Image image;
    public int row;
    public int col;

    public Player(Image i, int r, int c)
    {
        image = i;
        row = r;
        col = c;
    }

    public bool Jump(Grid g, Platform[] list, bool con, Platform_MainPage page)
    {
        if (this.row == 0)
        {
            page.difficulty = page.difficulty * 0.9;
            page.level++;
            page.levelLabel.Text = "Level: " + page.level;
            page.Fill_Grid(false);
        }
        else
        {
            g.SetRow(this.image, this.row -= 1);
            Platform plat = list[this.row];
            if (this.row == plat.row && this.col == plat.col)
            {
                page.score++;
                page.scoreLabel.Text = "Score: " + page.score;
                plat.StopMovement();
            }
            else
            {
                con = false; //gameover
                page.gameOver.Play();  
            }
        }
        return con;
    }
    public void Destroy(Grid g, Platform_MainPage page)
    {
        page.gridReady = false;
        g.Remove(image);
        page.startButton.Text = "Game Over!";
        page.gameOver.Play();
    }
}

public class Platform
{
    public BoxView rect;
    public int row;
    public int col;
    public bool isMovingLeft;
    public bool isMovingRight;

    public Platform(BoxView rectangle, int r, int c)
    {
        rect = rectangle;
        row = r;
        col = c;
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void StopMovement()
    {
        isMovingLeft = false;
        isMovingRight = false;
    }

    async public void Oscillate(Grid g, Platform_MainPage page)
    {
        int boundaryLeft = 0;
        int boundaryRight = 4;
        var rand = new Random();
        await Task.Delay(rand.Next(1000));
        MoveRight(g, boundaryLeft, boundaryRight, page); //start movement
    }

    async public void MoveRight(Grid g, int limitLeft, int limitRight, Platform_MainPage page)
    {
        while (col < limitRight && isMovingRight)// while no collision
        {
            col++;
            g.SetColumn(rect, col);
            await Task.Delay((int)page.difficulty);
        }
        if (isMovingRight)// if there has not been a collision
        {
            isMovingLeft = true;
            isMovingRight = false;
            MoveLeft(g, limitLeft, limitRight, page);
        }
    }

    async public void MoveLeft(Grid g, int limitLeft, int limitRight, Platform_MainPage page)
    {
        while (col > limitLeft && isMovingLeft) // while no collision
        {
            col--;
            g.SetColumn(rect, col);
            await Task.Delay((int)page.difficulty);
        }
        if (isMovingLeft) // if there has not been a collision
        {
            isMovingRight = true;
            isMovingLeft = false;
            MoveRight(g, limitLeft, limitRight, page);
        }
    }
}

public class CloudEnemy
{
    public Image cloudImage;
    public int row;
    public int col;
    public Player user;

    public CloudEnemy(Image i, int r, int c, Player u)
    {
        cloudImage = i;
        row = r;
        col = c;
        user = u;
    }
    public void Oscillate(Grid g, Platform_MainPage page)
    {
        int boundaryLeft = 0;
        int boundaryRight = 4;
        MoveLeft(g, boundaryLeft, boundaryRight, page);
    }
    async public void MoveLeft(Grid g, int limitLeft, int limitRight, Platform_MainPage page)
    {
        while (col > limitLeft)
        {
            col--;
            g.SetColumn(cloudImage, col);
            if (col == user.col && row == user.row)
            {
                user.Destroy(g, page);
                return;
            }
            await Task.Delay(500);
        }

        MoveRight(g, limitLeft, limitRight, page);

    }
    async public void MoveRight(Grid g, int limitLeft, int limitRight, Platform_MainPage page)
    {
        while (col < limitRight)
        {
            col++;
            g.SetColumn(cloudImage, col);
            if (col == user.col && row == user.row)
            {
                user.Destroy(g, page);
                return;
            }
            await Task.Delay(500);
        }

        MoveLeft(g, limitLeft, limitRight, page);

    }
}