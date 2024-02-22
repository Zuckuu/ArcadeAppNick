namespace ArcadeAppNick;

public partial class Checkers : ContentPage
{
    //class attributes 
    public List<CheckerboardSquare> CheckerBoard = new List<CheckerboardSquare>();
    public List<Checker> AIPieces = new List<Checker>();
    public List<Checker> UserPieces = new List<Checker>();
    public bool checkerIsSelected = false;
    public List<AIMove> PriorityMoves = new List<AIMove>();
    //class constructor
    public Checkers()
    {
        InitializeComponent();
        PopulateGameboard();
    }

    public void PopulateGameboard()
    {
        int rows = 8;
        int columns = 8;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Color color = new Color();
                if (i % 2 == 0)
                {
                    //even row
                    if (j % 2 == 0)
                    {
                        color = Color.FromRgb(255, 255, 255);
                    }
                    else
                    {
                        color = Color.FromRgb(0, 0, 0);
                    }
                }
                else
                {
                    //odd row
                    if (j % 2 == 0)
                    {
                        color = Color.FromRgb(0, 0, 0);
                    }
                    else
                    {
                        color = Color.FromRgb(255, 255, 255);
                    }
                }
                ImageButton sq = new ImageButton()
                {
                    BackgroundColor = color,
                };

                //create AI pieces
                if ((j == 0 || j == 1 || j == 2) && sq.BackgroundColor.Equals(new Color(0, 0, 0)))
                {
                    sq.Source = "black_piece.png";
                    Checker newPiece = new Checker(i, j);
                    AIPieces.Add(newPiece);
                }

                //create user pieces
                if ((j == 5 || j == 6 || j == 7) && sq.BackgroundColor.Equals(new Color(0, 0, 0)))
                {
                    sq.Source = "red_piece.png";
                    Checker newPiece = new Checker(i, j);
                    UserPieces.Add(newPiece);
                }

                Gameboard.Add(sq, i, j);
                CheckerBoard.Add(new CheckerboardSquare(this, sq, i, j));
            }
        }
    }

    public Checker IdentifyChecker(int[] location)
    {
        foreach (Checker piece in UserPieces)
        {
            if (piece.currentLocation[0] == location[0] && piece.currentLocation[1] == location[1])
            {
                return piece;
            }
        }
        foreach (Checker piece in AIPieces)
        {
            if (piece.currentLocation[0] == location[0] && piece.currentLocation[1] == location[1])
            {
                return piece;
            }

        }
        return null;
    }

    public CheckerboardSquare IdentifyCheckerBoardSquare(int[] location)
    {
        foreach (CheckerboardSquare square in CheckerBoard)
        {
            if (square.location[0] == location[0] && square.location[1] == location[1])
            {
                return square;
            }
        }
        return null;
    }

    public void HighlightSquares(List<int[]> locations)
    {
        if (locations.Count != 0)
        {
            foreach (int[] location in locations)
            {
                IdentifyCheckerBoardSquare(location).AddBorder();
            }
        }
        else
        {
            return;
        }
    }

    public void DeHighlightSquares(List<int[]> locations)
    {
        if (locations.Count != 0)
        {
            foreach (int[] location in locations)
            {
                IdentifyCheckerBoardSquare(location).RemoveBorder();
            }
        }
        else
        {
            return;
        }
    }

    public void EndOfUserTurn(CheckerboardSquare movedTo)
    {
        movedTo.isActive = true; //square now has a checker

        foreach (CheckerboardSquare bs in CheckerBoard)
        {
            if (bs.choosingForMove)
            {
                int[] fromLocation = bs.location;
                //check for elimination
                CheckForCheckerElimination("user", fromLocation, movedTo.location);
                foreach (Checker piece in UserPieces)
                {
                    if (piece.currentLocation[0] == fromLocation[0] && piece.currentLocation[1] == fromLocation[1])
                    {
                        piece.currentLocation[0] = movedTo.location[0];
                        piece.currentLocation[1] = movedTo.location[1];
                    }
                }
                bs.square.Source = null;
                bs.isActive = false;
                bs.choosingForMove = false;
            }
            //.NET 8 -> if(bs.square.BorderWidth == 5)
            if (Convert.ToString(bs.square.BackgroundColor) == Convert.ToString(Color.FromRgb(255, 223, 0)))
            {
                DeHighlightSquares(new List<int[]>() { bs.location });
            }
        }
        foreach (CheckerboardSquare bs in CheckerBoard)
        {
            bs.RemoveEvents();
            //bs.TestActive(); 
        }
        AITurn();
    }

    public void AITurn()
    {
        if (AIPieces.Count == 0)
        {
            UserWin();
            return;
        }
        List<List<int[]>> AIMoves = new List<List<int[]>>(); //list of lists of locations

        foreach (Checker piece in AIPieces)
        {
            List<int[]> moves = piece.GetPossibleMoves("ai", this); //calculate valid moves
            AIMoves.Add(moves);

            foreach (int[] location in moves)
            {
                if (location[1] - piece.currentLocation[1] > 1) //if move is larger than 1 square (jump)
                {
                    AIMove move = new AIMove(1, piece.currentLocation, location, piece);
                    PriorityMoves.Add(move);
                }
                else
                {
                    AIMove move = new AIMove(0, piece.currentLocation, location, piece);
                    PriorityMoves.Add(move);
                }
            }
        }

        if (PriorityMoves.Count > 0)
        {
            AIMove chosenMove = ChooseAIMove();
            CheckerboardSquare fromSquare = IdentifyCheckerBoardSquare(chosenMove.fromLocation);
            CheckerboardSquare toSquare = IdentifyCheckerBoardSquare(chosenMove.toLocation);

            chosenMove.piece.currentLocation = chosenMove.toLocation; //update checker currentLocation

            fromSquare.square.Source = null;
            toSquare.square.Source = "black_piece.png";
            toSquare.choosingForMove = false;

            //check for elimination
            CheckForCheckerElimination("ai", chosenMove.fromLocation, chosenMove.toLocation);

            foreach (CheckerboardSquare bs in CheckerBoard) //check each square for a piece
            {
                bs.TestActive();
            }
            PriorityMoves.Clear();
        }
        else
        {
            UserWin();
        }
    }

    public AIMove ChooseAIMove()
    {
        foreach (AIMove move in PriorityMoves)
        {
            if (move.priority == 1) //first choose move with priority
            {
                return move;
            }
        }
        var rand = new Random();
        int randMove = rand.Next(0, PriorityMoves.Count);
        return PriorityMoves[randMove]; //otherwise choosen random move
    }

    public void CheckForCheckerElimination(string playerID, int[] from, int[] to)
    {
        int[] checkerToEliminate = new int[2] { -1, -1 };

        if (playerID == "user")
        {
            if (from[1] - to[1] > 1) // if has moved 2 rows up
            {
                if (to[0] > from[0]) //if column has increased (right move)
                {
                    checkerToEliminate[0] = to[0] - 1;
                }
                else //column has decreased (left move)
                {
                    checkerToEliminate[0] = from[0] - 1;
                }
                checkerToEliminate[1] = to[1] + 1;
                if (AIPieces.Contains(IdentifyChecker(checkerToEliminate)))
                {
                    AIPieces.Remove(IdentifyChecker(checkerToEliminate));
                }
            }
        }
        else if (playerID == "ai")
        {
            if (to[1] - from[1] > 1) // if has moved 2 rows down
            {
                if (to[0] > from[0]) //if column has increased (right move)
                {
                    checkerToEliminate[0] = to[0] - 1;
                }
                else //column has decreased (left move)
                {
                    checkerToEliminate[0] = from[0] - 1;
                }
                checkerToEliminate[1] = to[1] - 1;
                if (UserPieces.Contains(IdentifyChecker(checkerToEliminate)))
                {
                    UserPieces.Remove(IdentifyChecker(checkerToEliminate));
                }
            }
        }
        if (checkerToEliminate[0] != -1 && checkerToEliminate[1] != -1)
        {
            IdentifyCheckerBoardSquare(checkerToEliminate).square.Source = null;
            IdentifyCheckerBoardSquare(checkerToEliminate).isActive = false;
            IdentifyCheckerBoardSquare(checkerToEliminate).choosingForMove = false;
        }
    }

    private void Forfeit_Button_Clicked(object sender, EventArgs e)
    {
        Forfeit_Button.IsVisible = false;
        GameOver.Text = "AI has won. You have lost.";
        GameOver.IsVisible = true;
        foreach (CheckerboardSquare sq in CheckerBoard)
        {
            sq.square.IsEnabled = false;
        }
    }

    public void UserWin()
    {
        Forfeit_Button.IsVisible = false;
        GameOver.Text = "You have won!";
        GameOver.IsVisible = true;
        foreach (CheckerboardSquare sq in CheckerBoard)
        {
            sq.square.IsEnabled = false;
        }
    }
}
public class Checker
{
    public int[] currentLocation = new int[2];

    public Checker(int i, int j)
    {
        currentLocation[0] = i;
        currentLocation[1] = j;
    }

    public List<int[]> GetPossibleMoves(string player, Checkers p)
    {
        List<int[]> moves = new List<int[]>();
        if (player == "user")
        {
            int[] move1 = new int[2] { currentLocation[0] - 1, currentLocation[1] - 1 }; //up-left move
            int[] move2 = new int[2] { currentLocation[0] + 1, currentLocation[1] - 1 }; //up-right move

            CheckerboardSquare sq1 = p.IdentifyCheckerBoardSquare(move1);
            CheckerboardSquare sq2 = p.IdentifyCheckerBoardSquare(move2);

            move1 = GetActualUserMoves(move1, sq1, "move1", p);
            move2 = GetActualUserMoves(move2, sq2, "move2", p);

            if (move1 != null)
            {
                moves.Add(move1);
            }
            if (move2 != null)
            {
                moves.Add(move2);
            }
        }
        else if (player == "ai")
        {
            int[] move1 = new int[2] { currentLocation[0] - 1, currentLocation[1] + 1 }; //down-left move
            int[] move2 = new int[2] { currentLocation[0] + 1, currentLocation[1] + 1 }; //down-right move

            CheckerboardSquare sq1 = p.IdentifyCheckerBoardSquare(move1);
            CheckerboardSquare sq2 = p.IdentifyCheckerBoardSquare(move2);

            move1 = GetActualAIMove(move1, sq1, "move1", p);
            move2 = GetActualAIMove(move2, sq2, "move2", p);

            if (move1 != null)
            {
                moves.Add(move1);
            }
            if (move2 != null)
            {
                moves.Add(move2);
            }
        }
        return moves;
    }

    public int[] GetActualUserMoves(int[] move, CheckerboardSquare sq, string moveID, Checkers p, int moveIndex = 0)
    {
        if (moveID == "move1")
        {
            if (move[0] >= 0 && move[1] >= 0 && sq != null) //check if move is in board
            {
                if (sq.isActive == true) //check if square is ocupied
                {
                    if (Convert.ToString(sq.square.Source).Substring(6) != "red_piece.png" && moveIndex == 0)
                    {
                        move[0] = (move[0] - 1);
                        move[1] = (move[1] - 1);
                        sq = p.IdentifyCheckerBoardSquare(move); //identify new square
                        move = GetActualUserMoves(move, sq, moveID, p, moveIndex + 1); //recursively recheck possible jump move
                    }
                    else
                    {
                        return null; //cannot move over red piece
                    }
                }
            }
            else
            {
                return null;
            }
        }
        else if (moveID == "move2")
        {
            if (move[0] <= 7 && move[1] >= 0 && sq != null) //check for up-right move validity
            {
                if (sq.isActive == true) //check if square is ocupied
                {
                    if (Convert.ToString(sq.square.Source).Substring(6) != "red_piece.png" && moveIndex == 0)
                    {
                        move[0] = (move[0] + 1);
                        move[1] = (move[1] - 1);
                        sq = p.IdentifyCheckerBoardSquare(move);
                        move = GetActualUserMoves(move, sq, moveID, p, moveIndex + 1); //recursively recheck possible hop move
                    }
                    else
                    {
                        move = null; //cannot move over red piece
                    }

                }
            }
            else
            {
                move = null; //cannot move outside of board
            }
        }
        return move;
    }

    public int[] GetActualAIMove(int[] move, CheckerboardSquare sq, string moveID, Checkers p, int moveIndex = 0)
    {
        if (moveID == "move1")
        {
            if (move[0] >= 0 && move[1] <= 7 && sq != null)
            {
                if (sq.isActive == true) //check if square is ocupied
                {
                    if (Convert.ToString(sq.square.Source).Substring(6) != "black_piece.png" && moveIndex == 0)
                    {
                        move = new int[2] { move[0] - 1, move[1] + 1 };
                        sq = p.IdentifyCheckerBoardSquare(move);
                        move = GetActualAIMove(move, sq, moveID, p, moveIndex + 1); //recursively recheck possible hop move
                    }
                    else
                    {
                        move = null; //cannot move over black piece
                    }

                }
            }
            else
            {
                move = null; //cannot move outside of board
            }
        }
        else if (moveID == "move2")
        {
            if (move[0] <= 7 && move[1] <= 7 && sq != null)
            {
                if (sq.isActive == true) //check if square is ocupied
                {
                    if (Convert.ToString(sq.square.Source).Substring(6) != "black_piece.png" && moveIndex == 0)
                    {
                        move = new int[2] { move[0] + 1, move[1] + 1 };
                        sq = p.IdentifyCheckerBoardSquare(move);
                        move = GetActualAIMove(move, sq, moveID, p, moveIndex + 1); //recursively recheck possible hop move
                    }
                    else
                    {
                        move = null; //cannot move over black piece
                    }

                }
            }
            else
            {
                move = null; //cannot move outside of board
            }
        }
        return move;
    }
}

public class CheckerboardSquare
{
    Checkers p;
    public ImageButton square;
    public int[] location = new int[2];
    public bool isActive = false;
    public bool choosingForMove = false;
    public int currentState = 0;
    public EventHandler DoToggle;
    public EventHandler DoMove;

    public CheckerboardSquare(Checkers page, ImageButton sq, int i, int j)
    {
        p = page;
        location[0] = i;
        location[1] = j;
        square = sq;
        TestActive();
    }

    public void AddBorder()
    {
        //.NET 8
        //square.BorderColor = Color.FromRgb(255, 223, 0);
        //square.BorderWidth = 5;

        square.BackgroundColor = Color.FromRgb(255, 223, 0);
    }

    public void RemoveBorder()
    {
        //.NET 8
        //square.BorderColor = Color.FromRgb(0,0,0);
        //square.BorderWidth = 0;

        square.BackgroundColor = Color.FromRgb(0, 0, 0);
    }

    public void TestActive()
    {
        if (Convert.ToString(square.Source).Length > 7)
        {
            isActive = true;
            if (Convert.ToString(square.Source).Substring(6) == "red_piece.png")
            {
                DefineRedClick();
            }
        }
        else
        {
            isActive = false;
            DefineRedMove();
        }
    }

    public void DefineRedClick()
    {
        DoToggle = (sender, args) =>    //arrow function ~ lambda expression
        {
            Checker currentChecker = p.IdentifyChecker(location); //get current checker
            List<int[]> availableMoves = currentChecker.GetPossibleMoves("user", p); //list of valid moves
            if (currentState == 0 && (p.checkerIsSelected == false))
            {
                square.Source = "red_piece_active.png";
                choosingForMove = true;
                currentState = 1;   //toggle
                p.checkerIsSelected = true;
                p.HighlightSquares(availableMoves);
            }
            else if (choosingForMove)
            {
                square.Source = "red_piece.png";
                currentState = 0;
                choosingForMove = false;
                p.checkerIsSelected = false;
                p.DeHighlightSquares(availableMoves);
            }
        };
        square.Clicked += DoToggle;
    }

    public void DefineRedMove()
    {
        DoMove = (sender, args) =>
        {
            //.NET 8 -> if(square.BorderWidth == 5)
            if (Convert.ToString(square.BackgroundColor) == Convert.ToString(Color.FromRgb(255, 223, 0)))
            {
                square.Source = "red_piece.png";
                currentState = 0;
                choosingForMove = false;
                p.checkerIsSelected = false;
                p.EndOfUserTurn(this);
            }
        };
        square.Clicked += DoMove;
    }

    public void RemoveEvents()
    {
        square.Clicked -= DoToggle; // Remove current toggle
        square.Clicked -= DoMove;
    }
}

public class AIMove
{
    public int[] fromLocation = new int[2];
    public int[] toLocation = new int[2];
    public int priority;
    public Checker piece;

    public AIMove(int p, int[] f, int[] t, Checker pe)
    {
        priority = p;
        fromLocation = f;
        toLocation = t;
        piece = pe;
    }
}