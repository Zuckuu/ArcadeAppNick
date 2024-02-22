namespace ArcadeAppNick;

public partial class TicTacToe : ContentPage
{
    public BoardSquare sq0;
    public BoardSquare sq1; 
    public BoardSquare sq2;
    public BoardSquare sq3;
    public BoardSquare sq4;
    public BoardSquare sq5;
    public BoardSquare sq6;
    public BoardSquare sq7;
    public BoardSquare sq8;
    public List<BoardSquare> squares = new List<BoardSquare>();
    public int currentTurn = 0; 

	public TicTacToe()
	{
		InitializeComponent();
        InitializeGame(); 
    }

    private void Square0_Clicked(object sender, EventArgs e)
    {
        sq0.PlayerTurn();
        currentTurn++; 
        squares.Remove(sq0);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square1_Clicked(object sender, EventArgs e)
    {
        sq1.PlayerTurn();
        currentTurn++; 
        squares.Remove(sq1);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square2_Clicked(object sender, EventArgs e)
    {
        sq2.PlayerTurn();
        currentTurn++; 
        squares.Remove(sq2);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square3_Clicked(object sender, EventArgs e)
    {
        sq3.PlayerTurn();
        currentTurn++;
        squares.Remove(sq3);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square4_Clicked(object sender, EventArgs e)
    {
        sq4.PlayerTurn();
        currentTurn++;
        squares.Remove(sq4);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square5_Clicked(object sender, EventArgs e)
    {
        sq5.PlayerTurn();
        currentTurn++;
        squares.Remove(sq5);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square6_Clicked(object sender, EventArgs e)
    {
        sq6.PlayerTurn();
        currentTurn++;
        squares.Remove(sq6);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square7_Clicked(object sender, EventArgs e)
    {
        sq7.PlayerTurn();
        currentTurn++;
        squares.Remove(sq7);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    private void Square8_Clicked(object sender, EventArgs e)
    {
        sq8.PlayerTurn();
        currentTurn++;
        squares.Remove(sq8);
        CheckForWin(); //checking 
        RandomLocation();
        CheckForWin(); //checking
    }

    public void RandomLocation()
    {
        if(squares.Count > 0)
        {
            var rand = new Random();
            int loc = rand.Next(squares.Count);
            
            squares[loc].AITurn();
            squares.Remove(squares[loc]);
        }
         
    }

    public void CheckForWin()
    {       
        if(sq0.isX && sq1.isX && sq2.isX ||     //Checking Xs Horizontal
           sq3.isX && sq4.isX && sq5.isX ||
           sq6.isX && sq7.isX && sq8.isX ||
           
           sq0.isX && sq3.isX && sq6.isX ||     //Checking Xs Vertical
           sq1.isX && sq4.isX && sq7.isX ||
           sq2.isX && sq5.isX && sq8.isX ||
           
           sq0.isX && sq4.isX && sq8.isX ||     //Checking Xs Diagonal
           sq2.isX && sq4.isX && sq6.isX)
        {
            //X (user) has won
            GameOver("user"); 
        }
        if (sq0.isO && sq1.isO && sq2.isO ||     //Checking Os Horizontal
           sq3.isO && sq4.isO && sq5.isO ||
           sq6.isO && sq7.isO && sq8.isO ||
                                       
           sq0.isO && sq3.isO && sq6.isO ||     //Checking Os Vertical
           sq1.isO && sq4.isO && sq7.isO ||
           sq2.isO && sq5.isO && sq8.isO ||
                                       
           sq0.isO && sq4.isO && sq8.isO ||     //Checking Os Diagonal
           sq2.isO && sq4.isO && sq6.isO)
        {
            //O (AI) has won
            GameOver("ai"); 
        }
    }

    public void GameOver(string id)
    {
        if(squares.Count > 0 ) {
            foreach (var boardsquare in squares)
            {
                boardsquare.square.IsEnabled = false; 
            }
        }
        if(id == "user")
        {
            Winner_Label.Text = "You have won!";
        }
        else if(id == "ai")
        {
            Winner_Label.Text = "AI has won!"; 
        }
        New_Game_Button.IsVisible = true; 
    }

    public void InitializeGame()
    {
        squares.Clear(); //empty list
        currentTurn = 0; //reset turns
        New_Game_Button.IsVisible = false; 

        sq0 = new BoardSquare(Square0, 0);
        squares.Add(sq0);
        sq1 = new BoardSquare(Square1, 1);
        squares.Add(sq1);
        sq2 = new BoardSquare(Square2, 2);
        squares.Add(sq2);
        sq3 = new BoardSquare(Square3, 3);
        squares.Add(sq3);
        sq4 = new BoardSquare(Square4, 4);
        squares.Add(sq4);
        sq5 = new BoardSquare(Square5, 5);
        squares.Add(sq5);
        sq6 = new BoardSquare(Square6, 6);
        squares.Add(sq6);
        sq7 = new BoardSquare(Square7, 7);
        squares.Add(sq7);
        sq8 = new BoardSquare(Square8, 8);
        squares.Add(sq8);
    }

    private void New_Game_Button_Clicked(object sender, EventArgs e)
    {
        Winner_Label.Text = ""; 
        InitializeGame();
        foreach (var boardsquare in squares)
        {
            boardsquare.square.IsEnabled = true;
            boardsquare.square.Source = ""; 
        }
    }
}

public class BoardSquare
{
    public ImageButton square; 
    public int squareNumber;
    public bool isX = false;
    public bool isO = false;
    public string imageSource; 

    public BoardSquare(ImageButton ib, int number)
    {
        square = ib;
        squareNumber = number;
    }

    public void PlayerTurn()
    {
        isX = true;
        imageSource = "tictactoe_x.png"; 
        square.IsEnabled = false;
        square.Source = imageSource; 
    }

    public void AITurn()
    {
        isO = true;
        imageSource = "tictactoe_o.png";
        square.IsEnabled =false;
        square.Source = imageSource; 
    }
}