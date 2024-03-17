using ArcadeAppNick.Models;

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

    public int handSize = 5;
    public int currentTurn = 0;
    public bool cardIsSelected = false;
    public static Random shuffleNum = new Random();

    public int playerHealthN = 10;
    public int aiHealthN = 10;

    public Zoo()
	{
		InitializeComponent();
        AddDatabase();

        playerHealth.Text = playerHealthN.ToString();
        aiHealth.Text = aiHealthN.ToString();

    }
    public void createBoard()
    {
        for (int i = 0; i < handSize; i++)
        {        
            ImageButton sq = new ImageButton();
            sq.Source = aiDeck[0].Name + ".png";
            Card newCard = aiDeck[0];
            newCard.Spot = i;
            newCard.Location = "aiHand";
            aiHand.Add(newCard);
            aiDeck.RemoveAt(0);
            AiHandGrid.Add(sq, i, 0);
            CardBoard.Add(new CardBoardSquare(this, sq, i, "aiHand", false));
        }
        for (int i = 0; i < handSize; i++)
        {
            ImageButton sq = new ImageButton();
            AiFieldGrid.Add(sq, i, 0);
            CardBoard.Add(new CardBoardSquare(this, sq, i, "aiField", true));
        }
        for (int i = 0; i < handSize; i++)
        {
            ImageButton sq = new ImageButton();
            PlayerFieldGrid.Add(sq, i, 0);
            CardBoard.Add(new CardBoardSquare(this, sq, i, "playerField", true));
        }
        for (int i = 0; i < handSize; i++)
        {
            ImageButton sq = new ImageButton();
            sq.Source = playerDeck[0].Name + ".png";
            Card newCard = playerDeck[0];
            newCard.Spot = i;
            newCard.Location = "playerHand";
            playerHand.Add(newCard);
            playerDeck.RemoveAt(0);
            PlayerHandGrid.Add(sq, i, 0);
            CardBoard.Add(new CardBoardSquare(this, sq, i, "playerHand", false));
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
            Card newCard = new Card(result.Name, 0, "DECK");
            deck.Add(newCard);
            deckIndexNum++;
        }

        for (int r = 0; r < rare; r++)
        {
            int randNum = rand.Next(rareList.Count);
            string name = rareList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            Card newCard = new Card(result.Name, 0, "DECK");
            deck.Add(newCard);
            deckIndexNum++;
        }

        for (int l = 0; l < legendary; l++)
        {
            int randNum = rand.Next(legendaryList.Count);
            string name = legendaryList[randNum];
            Cards result = App.UserRepo.GetCard(name);
            Card newCard = new Card(result.Name, 0, "DECK");
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
        App.UserRepo.AddCard("tiger","tiger.png", 30, 25, "rare");
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
    public Card IdentifyCard(int spot, string location )
    {
        foreach (Card card in playerHand)
        {
            if (card.Spot == spot && card.Location == location)
            {
                return card;
            }
        }
        foreach (Card card in aiHand)
        {
            if (card.Spot == spot && card.Location == location)
            {
                return card;
            }
        }
        foreach (Card card in playerField)
        {
            if (card.Spot == spot && card.Location == location)
            {
                return card;
            }
        }
        foreach (Card card in aiField)
        {
            if (card.Spot == spot && card.Location == location)
            {
                return card;
            }
        }
        return null;
    }

    public CardBoardSquare IdentifyCardBoardSquare(int spot, string location)
    {
        foreach (CardBoardSquare square in CardBoard)
        {
            if (square.spot == spot && square.location == location)
            {
                return square;
            }
        }
        return null;
    }

    public void playerMoveCard(CardBoardSquare newSpot)
    {
        int count = 0;
        int index = 0;
        foreach(CardBoardSquare boardCard in CardBoard)
        {
            if(boardCard.chosenForMove && boardCard.location == "playerHand")
            {
                count++;
                index = boardCard.spot;
                for(int x = 0; x < playerHand.Count; x++)
                {
                    if (playerHand[x].Spot == index)
                    {
                        newSpot.square.Source = playerHand[x].Name + ".png";
                        newSpot.emtpy = false;
                        playerHand[x].Location = "playerField";
                        playerHand[x].Spot = index;
                        playerField.Add(playerHand[x]);
                        playerHand.Remove(playerHand[x]);
                    }
                }
                Card newCard = playerDeck[0];
                newCard.Location = "playerHand";
                newCard.Spot = index;
                playerHand.Add(newCard);
                boardCard.square.Source = playerDeck[0].Name + ".png";
                playerDeck.RemoveAt(0);
                boardCard.square.Scale = 1;
                boardCard.chosenForMove = false;
                cardIsSelected = false;
                boardCard.currentState = 0;
                boardCard.emtpy = false;
            }
        }
        foreach(CardBoardSquare boardCard in CardBoard)
        {
            boardCard.RemoveEvents();
        }
        checkWinner();
    }

    public void playerAttack(CardBoardSquare targetSq)
    {
        Card attackerCard = null;
        foreach (CardBoardSquare attacker in CardBoard)
        {
            if (attacker.chosenForAttack)
            {
                attackerCard = IdentifyCard(attacker.spot, "playerField");
                attacker.square.Scale = 1;
            }
        }

        Card targetCard = IdentifyCard(targetSq.spot, "aiField");
        Cards targetData = App.UserRepo.GetCard(targetCard.Name);
        Cards attackerData = App.UserRepo.GetCard(attackerCard.Name);

        if(attackerData.Hitpoint <= targetData.Attack && targetData.Hitpoint <= attackerData.Attack)
        {
            deteleCard(targetCard);
            deteleCard(attackerCard);
        }
        else
        {
            if(targetData.Hitpoint <= attackerData.Attack)
            {
                deteleCard(targetCard);
            }
            else if (attackerData.Hitpoint <= targetData.Attack)
            {
                deteleCard(attackerCard);
            }
        }
        foreach (CardBoardSquare boardCard in CardBoard)
        {
            boardCard.RemoveEvents();
        }
        checkWinner();
    }

    public void aiTurn()
    {
        int cardsInField = aiField.Count;
        int playerLowestHitpointCard = 201;
        Cards playerCard = null;
        Card target = null;
        Card Temp = null;
        List<Card> playableAiCard = new List<Card>();

        foreach (Card card in playerField)
        {
            playerCard = App.UserRepo.GetCard(card.Name); //get the card data
            if (playerCard.Hitpoint < playerLowestHitpointCard)
            {
                playerLowestHitpointCard = playerCard.Hitpoint;
                target = card;
            }
        }
        foreach (Card card in aiHand)
        {
            Cards aiCardData = App.UserRepo.GetCard(card.Name);
            CardBoardSquare moveTo = IdentifyCardBoardSquare(card.Spot, "aiField");
            if(moveTo.emtpy == true)
            {
                if (aiCardData.Attack > playerLowestHitpointCard)
                {
                    playableAiCard.Add(card);
                }
                else
                {
                    if (aiCardData.Hitpoint > playerLowestHitpointCard)
                    {
                        playableAiCard.Add(card);
                    }
                    else
                    {
                        playableAiCard.Add(card);
                    }
                }
            }
        }

        if (cardsInField < 2 || playerField.Count == 0)
        {
            Temp = playableAiCard[0];
            foreach (Card card in playableAiCard)
            {
                int tempAtt = App.UserRepo.GetCard(Temp.Name).Attack;
                int cardAtt = App.UserRepo.GetCard(card.Name).Attack;
                if (tempAtt < cardAtt)
                {
                    Temp = card;
                }
            }
            aiPlayCard(Temp);
        }
        else
        {
            if(cardsInField >= 2)
            {
                Temp = aiField[0];
                foreach (Card card in aiField)
                {
                    int tempAtt = App.UserRepo.GetCard(Temp.Name).Attack;
                    int cardAtt = App.UserRepo.GetCard(card.Name).Attack;
                    if (tempAtt < cardAtt)
                    {
                        Temp = card;
                    }
                }
                aiAttack(Temp, target);
            }
        }
        List<CardBoardSquare> ignoreList = new List<CardBoardSquare>();
       foreach (CardBoardSquare bs in CardBoard)
        {
            
            if(bs.location == "playerField" && bs.emtpy == false)
            {
                CardBoardSquare ignoreCard = IdentifyCardBoardSquare(bs.spot, "playerHand");
                ignoreList.Add(ignoreCard); 
            }
            bs.toggleCard(); 
        }
        foreach (CardBoardSquare ignore in ignoreList)
        {
            ignore.RemoveEvents(); 
        }
        checkWinner();
    }

    public void aiAttack(Card attacker, Card target)
    {
        Cards ATTACKER = App.UserRepo.GetCard(attacker.Name);
        Cards TARGET = App.UserRepo.GetCard(target.Name);
        if(ATTACKER.Attack >= TARGET.Hitpoint && ATTACKER.Hitpoint <= TARGET.Attack)
        {
            deteleCard(target);
            deteleCard(attacker);
        }
        else 
        {
            if (ATTACKER.Attack >= TARGET.Hitpoint)
            {
                deteleCard(target);
            }
            else if(TARGET.Attack >= ATTACKER.Hitpoint)
            {
                deteleCard(attacker);
            }
        }
        checkWinner();
    }

    public void deteleCard(Card card)
    {
        CardBoardSquare square = null;
        if(card.Location == "aiField")
        {
            square = IdentifyCardBoardSquare(card.Spot, "aiField");
            square.square.Source = null;
            square.emtpy = true;
            aiField.Remove(card);
            aiHealthN--;
            aiHealth.Text = aiHealthN.ToString();
        }
        if(card.Location == "playerField")
        {
            square = IdentifyCardBoardSquare(card.Spot, "playerField");
            square.square.Source = null;
            square.emtpy = true;
            playerField.Remove(card);
            playerHealthN--;
            playerHealth.Text = playerHealthN.ToString();
        }


    }

    public void aiPlayCard(Card card)
    {
        CardBoardSquare fromSquare = IdentifyCardBoardSquare(card.Spot, "aiHand");
        CardBoardSquare moveSquare = IdentifyCardBoardSquare(card.Spot, "aiField");
        int index = fromSquare.spot;
        aiHand.Remove(IdentifyCard(card.Spot, "aiHand"));
        card.Location = "aiField";
        aiField.Add(card);
        moveSquare.square.Source = card.Name + ".png";
        moveSquare.emtpy = false;

        Card newCard = aiDeck[0];
        newCard.Location = "aiHand";
        newCard.Spot = index;
        fromSquare.square.Source = aiDeck[0].Name + ".png";
        aiHand.Add(newCard);
        aiDeck.RemoveAt(0);
    }

    public void checkWinner()
    {
        if(aiField.Count == 5 || playerField.Count == 5)
        {
            foreach(CardBoardSquare bs in CardBoard)
            {
                bs.RemoveEvents();
                bs.square.IsEnabled = false;
            }
            if(aiField.Count == 5)
            {
                endTurnButton.Text = "AI WINS!";
            }
            else
            {
                endTurnButton.Text = "YOU WINS!";

            }
            endTurnButton.IsEnabled = false;
        }
        if(playerHealthN == 0 || aiHealthN == 0)
        {
            foreach (CardBoardSquare bs in CardBoard)
            {
                bs.RemoveEvents();
                bs.square.IsEnabled = false;
            }
            if (playerHealthN == 0)
            {
                endTurnButton.Text = "Ai WINS!";
            }
            else
            {
                endTurnButton.Text = "YOU WINS!";

            }
            endTurnButton.IsEnabled = false;
        }
    }
    
    private void endTurn(object sender, EventArgs e)
    {
        if(playerField.Count == 0)
        {
            foreach(CardBoardSquare bs in CardBoard)
            {
                bs.RemoveEvents();
            }
        }
        aiTurn();
    }
}

public class Card
{
    public int Spot;
    public string Name;
    public string Location;
    public Card(string name,int s, string l)
    {
        Name = name;
        Spot = s;
        Location = l;
    }
}

public class CardBoardSquare
{
    Zoo p;
    public ImageButton square;
    public int spot = 0;
    public string location;
    public bool chosenForMove = false;
    public bool chosenForAttack = false;
    public bool emtpy = true;
    public int currentState = 0;
    public EventHandler DoToggle;
    public EventHandler DoMove;
    public EventHandler CanAttack;
    public CardBoardSquare(Zoo page, ImageButton sq, int s, string l, bool e)
    {
        p = page;
        square = sq;
        spot = s;
        location = l;
        emtpy = e;
        toggleCard();
    }

    public void toggleCard()
    {
        if(emtpy == false)
        {
            if (location == "playerHand" || location == "playerField")
            {
                DoToggle = (sender, args) =>
                {
                    if (currentState == 0 && p.cardIsSelected == false)
                    {
                        square.Scale = 1.1;
                        currentState = 1;
                        p.cardIsSelected = true;
                        if(location == "playerHand")
                        {
                            chosenForMove = true;
                        }
                        if(location == "playerField")
                        {
                            chosenForAttack = true;
                        }
                    }
                    else if(chosenForMove || chosenForAttack)
                    {
                        square.Scale = 1;
                        currentState = 0;
                        p.cardIsSelected = false;
                        chosenForMove = false;
                        chosenForAttack = false;
                    }
                };
                square.Clicked += DoToggle;
            } 
        }
        else
        {
            MoveCard();
        }
    }

    public void MoveCard()
    {
        if (location == "playerField")
        {
            DoMove = (sender, args) =>
            {
                if (Convert.ToString(square.Source).Length == 0)
                {
                    currentState = 0;
                    chosenForMove = false;
                    p.cardIsSelected = false;
                    p.playerMoveCard(this);
                }
            };
            square.Clicked += DoMove;
        }
        else
        {
            CanAttack = (sender, args) =>
            {
                if (Convert.ToString(square.Source).Length > 0)
                {
                    currentState = 0;
                    chosenForAttack = false;
                    p.cardIsSelected = false;
                    p.playerAttack(this);
                }
            };
            square.Clicked += CanAttack;
        }
    }
 
    public void RemoveEvents()
    {
        square.Clicked -= DoToggle; // Remove current toggle
        square.Clicked -= DoMove;
    }
}
