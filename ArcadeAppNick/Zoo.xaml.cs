using ArcadeAppNick.Models;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using SQLite;
using System.Collections.Generic;


namespace ArcadeAppNick;

public partial class Zoo : ContentPage
{
    List<String> commonList = new List<String>()
    {
        "baby_goat",
        "hyena",
        "moose",
    };

    List<String> rareList = new List<String>() 
    {
        "buffalo",
        "polar_bear",
        "rat",
        "seal",
        "tiger",
        "shark",
        "vampire_bat"
    };
    List<String> legendaryList = new List<String>() 
    {
        "blue_whale",
        "centipede",
        "king_kong",
        "king_cobra",
        "sea_serpent",
        "pterodactyl"
    };


    public List<CardBoardSquare> CardBoard = new List<CardBoardSquare>();

    public List<String> deck1 = new List<String>();// aiDeck
    public List<String> deck2 = new List<String>();// playerDeck
    public List<Card> hand1 = new List<Card>();// aiHand
    public List<Card> hand2 = new List<Card>();// playerHand
    public List<String> field1 = new List<String>();//aiField
    public List<String> field2 = new List<String>();//playerField 
    public int currentTurn = 0;
    public bool cardIsSelected = false;
    public static Random shuffleNum = new Random();
    public Zoo()
	{
		InitializeComponent();
        AddDatabase();

    }


    public void createBoard()
    {
        int rows = 4;
        int columns = 7;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Color color = new Color();
                

                if (j == 1 || j == 2)
                {
                    color = Color.FromRgb(255, 255, 255);
                }

                ImageButton sq = new ImageButton()
                {
                    BackgroundColor = color,
                };  

                if (j == 0)//adding to aihand
                {
                    sq.Source = deck1[i] + ".png";
                    Card newCard = new Card(deck1[i],i, j, i);
                    hand1.Add(newCard); //add card to hand
                    deck1.RemoveAt(i); //remove card from deck
                }
                if(j == 3)//adding to player
                {
                    sq.Source = deck2[i] + ".png";
                    Card newCard = new Card(deck2[i], i, j, i);
                    hand2.Add(newCard);
                    deck2.RemoveAt(i);
                }
                Gameboard.Add(sq, i, j);
                CardBoard.Add(new CardBoardSquare(this, sq, i, j));
            }
        }

    }

    public void populateDeck()
    {
        int common = 15;
        int rare = 10;
        int legendary = 5;
        Random rand = new Random();

        deck2.Clear();

        for(int c = 0; c < common; c++)
        {
            int randNum = rand.Next(commonList.Count);
            string name = commonList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck2.Add(result.Name);
        }

        for (int r = 0; r < rare; r++)
        {
            int randNum = rand.Next(rareList.Count);
            string name = rareList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck2.Add(result.Name);
        }

        for (int l = 0; l < legendary; l++)
        {
            int randNum = rand.Next(legendaryList.Count);
            string name = legendaryList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck2.Add(result.Name);
        }

        Shuffle(deck2);//shuffle the deck
    }

    public void populateAiDeck()
    {
        int common = 15;
        int rare = 10;
        int legendary = 5;
        Random rand = new Random();

        deck1.Clear();

        for (int c = 0; c < common; c++)
        {
            int randNum = rand.Next(commonList.Count);
            string name = commonList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck1.Add(result.Name);
        }

        for (int r = 0; r < rare; r++)
        {
            int randNum = rand.Next(rareList.Count);
            string name = rareList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck1.Add(result.Name);
        }

        for (int l = 0; l < legendary; l++)
        {
            int randNum = rand.Next(legendaryList.Count);
            string name = legendaryList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            deck1.Add(result.Name);
        }

        Shuffle(deck1);//shuffle the deck
    }

    public List<string>Shuffle(List<string> deck) 
    {
        for(int x = deck.Count-1; x > 0; x--)
        {
            int k = shuffleNum.Next(x + 1);
            var value = deck[k];
            deck[k] = deck[x];
            deck[x] = value;
        }
        return deck;
    }

    public void AddDatabase()
    {

        App.UserRepo.RemoveCard();

        //commons
        App.UserRepo.AddCard("baby_goat", "baby_goat.png", 5, 10, "common");
        App.UserRepo.AddCard("hyena","hyena.png", 35, 15, "common");
        App.UserRepo.AddCard("moose", "moose.png", 15, 25, "common");

        //rare
        App.UserRepo.AddCard("buffalo","buffalo.png", 10, 30, "rare");
        App.UserRepo.AddCard("polar_bear","polar_bear.png", 15, 35, "rare");
        App.UserRepo.AddCard("rat","rat.png", 50, 1, "rare");
        App.UserRepo.AddCard("seal","seal.png", 20, 20, "rare");
        App.UserRepo.AddCard("shark","shark.png", 35, 20, "rare");
        App.UserRepo.AddCard("tiger","tiger.png", 25, 30, "rare");
        App.UserRepo.AddCard("vampire_bat","vampire_bat.png", 50, 25, "rare");

        //legendary
        App.UserRepo.AddCard("blue_whale","blue_whale.png", 5, 200, "legendary");
        App.UserRepo.AddCard("centipede","centipede.png", 40, 60, "legendary");
        App.UserRepo.AddCard("king_cobra","king_cobra.png", 65, 40, "legendary");
        App.UserRepo.AddCard("king_kong","king_kong.png", 125, 150, "legendary");
        App.UserRepo.AddCard("pterodactyl","pterodactyl.png", 75, 60, "legendary");
        App.UserRepo.AddCard("sea_serpent","seap_serpent", 80, 120, "legendary");

        populateDeck();
        populateAiDeck();
        createBoard();
    }

    public Card IdentifyCard(int[] location)
    {
        foreach (Card card in hand2)
        {
            if (card.currentLocation[0] == location[0] && card.currentLocation[1] == location[1])
            {
                return card;
            }
        }
        foreach (Card piece in hand1)
        {
            if (piece.currentLocation[0] == location[0] && piece.currentLocation[1] == location[1])
            {
                return piece;
            }

        }
        return null;
    }

    public void userTurn(CardBoardSquare movedTo)
    {
        movedTo.isActive = true;
        int IndexOfCard = 0;
       
        foreach(CardBoardSquare boardSquare in CardBoard){

            if (boardSquare.choosingForMove)
            {
                if (boardSquare.location[1] == 2)
                {
                    //attack or something
                }
                else if (boardSquare.location[1] == 3)

                    {
                        int[] fromLocation = boardSquare.location;

                        foreach (Card card in hand2)
                        {
                            if (card.currentLocation[0] == fromLocation[0] && card.currentLocation[1] == fromLocation[1])
                            {
                                card.currentLocation[0] = movedTo.location[0]; // move the card
                                card.currentLocation[1] = movedTo.location[1];
                                movedTo.square.Source = card.Name + ".png"; // change the image
                                field2.Add(card.Name); // add to field
                                IndexOfCard = card.CardIndex;
                            }

                        }

                        drawCard(deck2, fromLocation, IndexOfCard, hand2);
                        boardSquare.square.Source = deck2[0] + ".png";
                        boardSquare.square.BackgroundColor = null;
                        boardSquare.isActive = false;
                        boardSquare.choosingForMove = false;
                        boardSquare.square.Scale = 1;
                    }
            }

        }

        foreach (CardBoardSquare boardSquare in CardBoard)
        {
            boardSquare.RemoveEvents();
        }

        aiTurn();
    }

    public void aiTurn()
    {
        int playerHighestHitpointCard = 0;
        foreach(string card in field2) // player field
        {
            Cards currentCard = App.UserRepo.GetCard(card); //get the card data
            if(currentCard.Hitpoint > playerHighestHitpointCard) //check if the attack is bigger than the player's card
            {
                playerHighestHitpointCard = currentCard.Hitpoint; //set it to the variable
            }
        }

        List<Card> PlayableCardList = new List<Card>();

        foreach(Card card in hand1)//ai hand 
        {
            Cards aiPlayableCard = App.UserRepo.GetCard(card.Name);// get the card data
            if(aiPlayableCard.Attack > playerHighestHitpointCard) // check if aiCard's attack is higher than hp
            {
                PlayableCardList.Add(card); //add card to a list
            }else if(aiPlayableCard.Hitpoint > playerHighestHitpointCard) //check if aiCard's hp is higher
            {
                PlayableCardList.Add(card); //add card to a list
            }
        }

        Card CardtoPlay = null; // empty card

        foreach (Card card in PlayableCardList) 
        {
            CardtoPlay = card; // set a card to it
            if ((App.UserRepo.GetCard(CardtoPlay.Name).Attack) >= (App.UserRepo.GetCard(card.Name).Attack)) //compare the 2 card attack
            {
                CardtoPlay = card; //set it to CardtoPlay if card attack is lower, we dont want to play the strongest card
            }

        }
        aiPlayCard(CardtoPlay);
    }

    public void aiPlayCard(Card card)
    {
        int[] moveUp = new int[2] { card.currentLocation[0], card.currentLocation[1] + 1 };
        int[] currentlocation = new int[2] { card.currentLocation[0], card.currentLocation[1]};
        CardBoardSquare fromSquare = IdentifyCardBoardSquare(currentlocation);
        CardBoardSquare toSquare = IdentifyCardBoardSquare(moveUp);

        card.currentLocation = toSquare.location; //move the card

        drawCard(deck1, currentlocation, card.CardIndex, hand1);//add a new card and remove the old one
        fromSquare.square.Source = deck1[0] + ".png";
        toSquare.square.Source = card.Name + ".png";
        field1.Add(card.Name);
    }

    public void drawCard(List<String>deck, int[] location, int num, List<Card>hand)
    {
        Card newCard = new Card(deck[0],location[0], location[1], num);
        hand.RemoveAt(num);
        hand.Add(newCard);
    }

    public CardBoardSquare IdentifyCardBoardSquare(int[] location)
    {
        foreach (CardBoardSquare square in CardBoard)
        {
            if (square.location[0] == location[0] && square.location[1] == location[1])
            {
                return square;
            }
        }
        return null;
    }
}

public class Card
{
    public int[] currentLocation = new int[2];
    public string Name;
    public int CardIndex;
    public Card(string name,int i, int j, int num)
    {
        Name = name;
        currentLocation[0] = i;//col
        currentLocation[1] = j;//row
        CardIndex = num;
    }
}

public class CardBoardSquare
{
    Zoo p;
    public ImageButton square;
    public int[] location = new int[2];
    public bool isActive = false;
    public bool choosingForMove = false;
    public int currentState = 0;
    public EventHandler DoToggle;
    public EventHandler DoMove;

    public CardBoardSquare(Zoo page, ImageButton sq, int i, int j)
    {
        p = page;
        location[0] = i;
        location[1] = j;
        square = sq;
        TestActive();
    }
    public void TestActive()
    {
        if (Convert.ToString(square.Source).Length > 5)
        {
            isActive = true;
            if (location[1]==3)
            {
                DefineCardClick();
            }
        }
        else
        {
            isActive = false;
            DefineCardMove();
        }
    }

    public void DefineCardClick()
    {
        DoToggle = (sender, args) =>    //arrow function ~ lambda expression
        {
            Card currentCard = p.IdentifyCard(location); //get current card
            if (currentState == 0 && (p.cardIsSelected == false))
            {
                square.Scale = 1.1;
                choosingForMove = true;
                currentState = 1;   //toggle
                p.cardIsSelected = true;               
            }
            else if (choosingForMove)
            {
                square.Scale = 1;
                currentState = 0;
                choosingForMove = false;
                p.cardIsSelected = false;
            }
        };
        square.Clicked += DoToggle;
    }

    public void DefineCardMove()
    {
        DoMove = (sender, args) =>
        {
            if (Convert.ToString(square.BackgroundColor) == Convert.ToString(Color.FromRgb(255, 255, 255)))
            {
                currentState = 0;
                choosingForMove = false;
                p.cardIsSelected = false;
                p.userTurn(this);
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




