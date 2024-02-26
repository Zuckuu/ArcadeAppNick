using ArcadeAppNick.Models;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using SQLite;
using System.Collections.Generic;
using static SQLite.SQLite3;


namespace ArcadeAppNick;

public partial class Zoo : ContentPage
{
    List<String> commonList = new List<String>()
    {
        "baby_goat",
        "hyena",
        "moose",
        "zebra",
        "fox",
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

    public List<CardBoardSquare> CardBoard = new List<CardBoardSquare>(); //board for square

    public List<Card> aiDeck = new List<Card>();
    public List<Card> playerDeck = new List<Card>();

    public List<Card> aiHand = new List<Card>();
    public List<Card> playerHand = new List<Card>();

    public List<Card> aiField = new List<Card>();
    public List<Card> playerField = new List<Card>();

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
        int columns = 5;

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
                    sq.Source = aiDeck[i].Name + ".png";
                    Card newCard = new Card(aiDeck[i].Name,i, j);
                    aiHand.Add(newCard); //add card to hand
                    aiDeck.RemoveAt(i); //remove card from deck
                }
                if(j == 3)//adding to player
                {
                    sq.Source = playerDeck[i].Name + ".png";
                    Card newCard = new Card(playerDeck[i].Name, i, j);
                    playerHand.Add(newCard);
                    playerDeck.RemoveAt(i);
                }
                Gameboard.Add(sq, i, j);
                CardBoard.Add(new CardBoardSquare(this, sq, i, j));
            }
        }

    }
    public void populateDeck(List<Card> deck)
    {
        int common = 15 , rare = 10, legendary = 5;
        int deckIndexNum = 0;
        Random rand = new Random();


        deck.Clear();

        for(int c = 0; c < common; c++)
        {
            int randNum = rand.Next(commonList.Count);
            string name = commonList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            Card newCard = new Card(result.Name, 0, 0);
            deck.Add(newCard);
            deckIndexNum++;
        }

        for (int r = 0; r < rare; r++)
        {
            int randNum = rand.Next(rareList.Count);
            string name = rareList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            Card newCard = new Card(result.Name, 0, 0);
            deck.Add(newCard);
            deckIndexNum++;
        }

        for (int l = 0; l < legendary; l++)
        {
            int randNum = rand.Next(legendaryList.Count);
            string name = legendaryList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            Card newCard = new Card(result.Name, 0, 0);
            deck.Add(newCard);
            deckIndexNum++;
        }

        Shuffle(deck);//shuffle the deck
    }
    public List<Card>Shuffle(List<Card> deck) 
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
        App.UserRepo.AddCard("fox", "fox.png", 20, 25, "common");
        App.UserRepo.AddCard("zebra", "zebra.png", 10, 20, "common");

        //add horse and wolf

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

        populateDeck(aiDeck); //populate the decks
        populateDeck(playerDeck);
        createBoard(); //create the board with cards in hand
    }
    public Card IdentifyCard(int[] location)
    {
        foreach (Card card in playerHand)
        {
            if (card.currentLocation[0] == location[0] && card.currentLocation[1] == location[1])
            {
                return card;
            }
        }
        foreach (Card piece in aiHand)
        {
            if (piece.currentLocation[0] == location[0] && piece.currentLocation[1] == location[1])
            {
                return piece;
            }

        }
        return null;
    }

    public void playerMoveCard(CardBoardSquare newSquare) 
    {
        newSquare.isActive = true; //this square will now be active
        int[] fromLocation = new int[2];
        foreach(CardBoardSquare boardCard in CardBoard)
        {
            if (boardCard.chosenForMove)
            {
                fromLocation = boardCard.location; //set the past location to an int array

                foreach(Card card in playerHand)
                {
                    //find the card with the same location
                    if (card.currentLocation[0] == fromLocation[0] && card.currentLocation[1] == fromLocation[1])
                    {
                        card.currentLocation[0] = newSquare.location[0];//move the card
                        card.currentLocation[1] = newSquare.location[1];

                        newSquare.square.Source = card.Name + ".png"; //set the image to new square

                        playerField.Add(card);//add the card to field
                    }
                }


                boardCard.square.Source = playerDeck[0].Name + ".png"; //add a new card img to the old spot
                boardCard.square.Scale = 1; //scale back
                boardCard.chosenForMove = false;
            }

        }
        foreach(CardBoardSquare boardCard in CardBoard) //remove all event in on the board
        {
            boardCard.RemoveEvents();
        }
        drawCard(playerDeck, fromLocation, playerHand); //draw a new card
    }

    public void aiTurn()
    {
        int cardsInField = aiField.Count;
        int playerHighestHitpointCard = 0;
        Card playerCard = null;
        Card aiCard = null;
        bool endTurn = false;
        List<Card> playableAiCard = new List<Card>(); //store all ai card that can defeat player's card

        //find the highest hp in player field
        foreach (Card card in playerField)
        {
            Cards cardData = App.UserRepo.GetCard(card.Name); //get the card data

            if (cardData.Hitpoint > playerHighestHitpointCard)
            {
                playerHighestHitpointCard = cardData.Hitpoint;
                playerCard = card;
            }
        }

        //add any ai card that can defeat player card
        foreach (Card card in aiHand)
        {
            Cards aiCardData = App.UserRepo.GetCard(card.Name);
            if (aiCardData.Attack > playerHighestHitpointCard) //if attack is bigger
            {
                playableAiCard.Add(card);
            }
            else
            {
                //if hp is bigger
                if (aiCardData.Hitpoint > playerHighestHitpointCard)
                {
                    playableAiCard.Add(card);
                }
            }
        }

        foreach (Card card in playableAiCard)
        {
            aiCard = card;
            int currentCardAtt = App.UserRepo.GetCard(aiCard.Name).Attack;
            int cardInListAtt = App.UserRepo.GetCard(card.Name).Attack;
            if (currentCardAtt >= cardInListAtt)
            {
                aiCard = card;
            }
        }



        if (cardsInField < 2)
        {
            aiPlayCard(aiCard);//ai play the card
        }else
        {
            //aiAttack();
        }

        
        


        foreach (CardBoardSquare bs in CardBoard) //check each square for a card
        {
            bs.toggleCard();
        }

    }

    public void aiPlayCard(Card card) //we need to check if the location already have a card, if so then either play it to the left or right
    {
        int[] moveUp = new int[2] { card.currentLocation[0], card.currentLocation[1] + 1 };
        int[] moveUpRight = new int[2] { card.currentLocation[0] + 1, card.currentLocation[1] + 1 };
        int[] moveUpLeft = new int[2] { card.currentLocation[0] - 1, card.currentLocation[1] + 1 };
        int[] currentlocation = new int[2] { card.currentLocation[0], card.currentLocation[1] };
        CardBoardSquare fromSquare = IdentifyCardBoardSquare(currentlocation);
        CardBoardSquare toSquare = IdentifyCardBoardSquare(moveUp);

        if (Convert.ToString(toSquare.square.Source).Length > 2)// check if the square have an image
        {
            CardBoardSquare toSquareLeft = IdentifyCardBoardSquare(moveUpLeft);

            if (Convert.ToString(toSquareLeft.square.Source).Length > 2)
            {
                CardBoardSquare toSquareRight = IdentifyCardBoardSquare(moveUpRight);
                card.currentLocation = toSquareRight.location;
                fromSquare.square.Source = aiDeck[0].Name + ".png";
                toSquareRight.square.Source = card.Name + ".png";
                Card newCard = new Card(card.Name, toSquareRight.location[0], toSquareRight.location[1]);
                aiField.Add(newCard);
            }
            else
            {
                card.currentLocation = toSquareLeft.location;
                fromSquare.square.Source = aiDeck[0].Name + ".png";
                toSquareLeft.square.Source = card.Name + ".png";
                Card newCard = new Card(card.Name, toSquareLeft.location[0], toSquareLeft.location[1]);
                aiField.Add(newCard);
            }
        }
        else
        {
            card.currentLocation = toSquare.location; //move the card
            fromSquare.square.Source = aiDeck[0].Name + ".png";
            toSquare.square.Source = card.Name + ".png";
            Card newCard = new Card(card.Name, toSquare.location[0], toSquare.location[1]);
            aiField.Add(newCard);
        }


        drawCard(aiDeck, currentlocation, aiHand);

        foreach(CardBoardSquare square in CardBoard)
        {
            square.enemyCard();
        }   
    }

    public void attackThis(CardBoardSquare targetBoard)
    {
        int[] targetLocation = new int[2] { targetBoard.location[0], targetBoard.location[1]};

        Card target = IdentifyCard(targetLocation);

        Cards targetData = App.UserRepo.GetCard(target.Name);

        foreach(CardBoardSquare playerCard in CardBoard)
        {
            if (playerCard.chosenForAttack)
            {
                Card attacker = IdentifyCard(playerCard.location);

                Cards attackerData = App.UserRepo.GetCard(attacker.Name);

                if(attackerData.Attack > targetData.Hitpoint)
                {
                    deleteCard(target);
                }
                if(attackerData.Hitpoint < targetData.Attack)
                {
                    deleteCard(attacker);
                }

                playerCard.square.Scale = 1;
               
            }
        }

        foreach (CardBoardSquare boardCard in CardBoard) //remove all event in on the board
        {
            boardCard.toggleCard();
        }

    }

    public void aiAttack(Card attack, Card target)
    {
        Cards attacker = App.UserRepo.GetCard(attack.Name);
        Cards defender = App.UserRepo.GetCard(target.Name);

        if(attacker.Attack > defender.Hitpoint)
        {
            deleteCard(target);
        }
        else 
        {
            if (attacker.Attack == defender.Hitpoint)
            {
                deleteCard(attack);
                deleteCard(target);
            }
            else
            {
                deleteCard(attack);
            }
        }
    }

    public void deleteCard(Card card)
    {
        CardBoardSquare cardSquare = IdentifyCardBoardSquare(card.currentLocation);

        //reset the card
        cardSquare.square.Source = null; 
        cardSquare.square.BackgroundColor = Color.FromRgb(255, 255, 255);

        if (card.currentLocation[1] == 1)
        {
            aiField.Remove(card);
        }
        if (card.currentLocation[1] == 2)
        {
            playerField.Remove(card);
        }

    }

    public void drawCard(List<Card> deck, int[] fromLocation, List<Card> hand) //draws a new card 
    {
        //create new card
        Card newCard = new Card(deck[0].Name, fromLocation[0], fromLocation[1]);

        hand.Add(newCard);//add the new one to the same spot

        deck.RemoveAt(0);//remove the first card from deck
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
    } //finds the boardSquare
    private void endUserTurn(object sender, EventArgs e)
    {
        aiTurn();
    } // ends the player turn
}

public class Card
{
    public int[] currentLocation = new int[2];
    public string Name;
    public Card(string name,int i, int j)
    {
        Name = name;
        currentLocation[0] = i;//col
        currentLocation[1] = j;//row
    }
}

public class CardBoardSquare
{
    Zoo p;
    public ImageButton square;
    public int[] location = new int[2];
    public bool isActive = false;
    public bool chosenForMove = false;
    public bool chosenForAttack = false;
    public int currentState = 0;
    public EventHandler DoToggle;
    public EventHandler DoMove;
    public EventHandler CanAttack;

    public CardBoardSquare(Zoo page, ImageButton sq, int i, int j)
    {
        p = page;
        location[0] = i;
        location[1] = j;
        square = sq;
        toggleCard();
    }

    public void toggleCard()
    {
        if(Convert.ToString(square.Source).Length > 3) //if card have img
        {
            isActive = true;
            if(checkCardLocation()) //check if card is in field or hand
            {
                DoToggle = (sender, args) =>
                {
                    Card currentCard = p.IdentifyCard(location); //get the clicked card
                    if (currentState == 0 && (p.cardIsSelected == false))
                    {
                        square.Scale = 1.1;
                        currentState = 1;
                        p.cardIsSelected = true;
                        checkMoveOrAttack(currentCard); //check if you can attack or move

                    }
                    else if (chosenForAttack || chosenForMove) //detoggle
                    {
                        chosenForMove = false;
                        chosenForAttack = false;
                        currentState = 0;
                        p.cardIsSelected = false;
                        square.Scale = 1;
                    }
                };
                square.Clicked += DoToggle;
            }
        }
        else
        {
            if (location[1] == 2)
            {
                emptySquare(); //if card doesn't have img, its empty
            }
            if (location[1] == 1)
            {
                enemyCard();
            }
        }
    }

    public void emptySquare() 
    {
            DoMove = (sender, args) =>
            {
                if (Convert.ToString(square.BackgroundColor) == Convert.ToString(Color.FromRgb(255,255,255)) 
                    || Convert.ToString(square.Source).Length == 0)
                {
                    currentState = 0;
                    chosenForMove = false;
                    p.cardIsSelected = false;
                    square.BackgroundColor = Color.FromRgb(0, 0, 0);//black
                    p.playerMoveCard(this); //this is the spot you clicked on
                }
            };
            square.Clicked += DoMove;
    }

    public void enemyCard()
    {
        CanAttack = (sender, args) =>
        {
            if(Convert.ToString(square.Source).Length > 1)
            {
                currentState = 0;
                chosenForAttack = false;
                p.cardIsSelected = false;
                p.attackThis(this);
            }
        };
        square.Clicked += CanAttack;
    }

    public bool checkCardLocation()
    {
        if (location[1] == 2 || location[1] == 3)
        {
            return true;
        }
        return false;
    }
    public void checkMoveOrAttack(Card card)
    {
        if (card.currentLocation[1] == 3) //if card is in hand
        {
            chosenForMove = true;
        }
        else //if card is in field
        {
            chosenForAttack = true;
        }
    }

    public void RemoveEvents()
    {
        square.Clicked -= DoToggle; // Remove current toggle
        square.Clicked -= DoMove;
    }
}