using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameController : MonoBehaviour
{
    //I really should have made more classes - this is a mess holding almost everything in one
    //Works though!
    //TODO: Add a different class for deck related stuff
    //heh, deck - moar like dick am i rite? XD
    //      ~~THE KNIGHT WHO SAYS XD~~
    List<Card> cards = new List<Card>();
    List<GameObject> playerCards = new List<GameObject>();
    List<GameObject> enemyCards = new List<GameObject>();
    [SerializeField] GameObject cardPrefab;
    [SerializeField] TMP_Text playerScore;
    [SerializeField] TMP_Text endGameText;
    string[] colors = new string[] { "clubs", "diamonds", "hearts", "spades" };
    int[] figures = new int[13] { 2, 3, 4, 5, 6, 6, 8, 9, 10, 11, 12, 13, 1 };
    int indexOfNextCard = 0;
    int playerWorth = 0;
    int enemyWorth = 0;
    int nextPlayerCardPosition = 0;
    int nextEnemyCardPosition = 0;
    bool enemyStand = false;
    // Start is called before the first frame update
    void Start()
    {
        //so much spaghetti it could feed Mussolini's army
        GenerateCards();
        Shuffle();
        for (int i = 0; i < 2; i++)
        {
            CardPrefabMethod(playerCards, -1);
        }
        nextEnemyCardPosition = 0;
        for (int i = 0; i<2; i++)
        {
            CardPrefabMethod(enemyCards, 1);
        }
        playerWorth = CalculateWorth(playerCards);
        //below is a simple tooltip to keep track of your score
        //totally not there to teach you how to count cards, no
        playerScore.text = playerWorth.ToString(); 
        enemyWorth = CalculateWorth(enemyCards);
        //the life of a dealer is shit - he can't hit his cards if they are above 17... wait that sounds dirty
        //FBI OPEN UP!
        if (enemyWorth > 17)
            enemyStand = true;
    }

    /// <summary>
    /// Just a card generator
    /// I THINK i fucked something up - not sure what though XD
    /// </summary>
    private void GenerateCards()
    {
        foreach (string color in colors)
        {
            foreach (int figure in figures)
            {
                cards.Add(new Card(color, figure));
            }
        }
    }
    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }
    int CalculateWorth(List<GameObject> list)
    {
        int tempWorth = 0;
        for(int i = 0; i<list.Count;i++)
        {
            //"numbers" above 10 are face cards - they thus give 10 points
            //TODO: an ever fluctuating value of an Ace which makes it more or less valuable
            //handling two of them at once will be a pain
            if (list.ElementAt(i).GetComponent<Card>().number > 10)
            {
                tempWorth += 10;
            }
            else
                tempWorth += list.ElementAt(i).GetComponent<Card>().number;
        }
        return tempWorth;
    }
    void EnemyDecision()
    {
        if (enemyStand != true)
        {
            if (enemyWorth < 17)
            {
                CardPrefabMethod(enemyCards, 1);
                enemyWorth = CalculateWorth(enemyCards);
                if (enemyWorth > 21)
                {
                    endGameText.gameObject.SetActive(true);
                    endGameText.text = "You Win!";
                }
                if (enemyWorth == 21)
                {
                    endGameText.gameObject.SetActive(true);
                    endGameText.text = "You Lose!";
                }
            }
            else
                enemyStand = true;
        }
    }
    public void Stand()
    {
        if (enemyStand == true)
        {
            CompareScores();
        }
        else
            EnemyDecision();
    }
    public void Hit()
    {
        CardPrefabMethod(playerCards,-1);
        playerWorth = CalculateWorth(playerCards);
        playerScore.text = playerWorth.ToString();
        if (playerWorth > 21)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "You Lose!";
        }
        else if(playerWorth == 21)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "You Win!";
        }
        else
            EnemyDecision();
    }
    void CompareScores()
    {
        if (playerWorth>enemyWorth|| playerWorth==21)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "You Win!";
        }
        else if (playerWorth == enemyWorth)
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "DRAW!";
        }
        else
        {
            endGameText.gameObject.SetActive(true);
            endGameText.text = "You Lose!";
        }
    }
    /// <summary>
    /// Will instantiate a new card from deck - enemy or player's.
    /// Will also add all those fucked up parameters that i need
    /// Hopefully that is.
    /// The if is there to vary the method ever so slightly depending if the enemy or player needs it
    ///                     ~~THE KNIGHT WHO SAYS XD~~
    /// </summary>
    /// <param name="list">enemyCards or playerCards</param>
    /// <param name="sign">-1 or 1 - needed for y coordinates of cards</param>
    void CardPrefabMethod(List<GameObject> list, int sign)
    {
        //TODO: Add covering the first enemy card
        //Teacher did it with an additional bool and sprite in the Card class
        //I THINK it could be done with less variables, but in my retarded autistic fashion
        if (list == enemyCards)
        {
            list.Add(Instantiate
            (cardPrefab,
            new Vector3(nextEnemyCardPosition, 3.5f * sign, 0),
            Quaternion.identity));
            //you are probably wondering about those values below
            //i am too - without those two lines of list.ElementAt(...) the cards would have the wrong sprites and values
            //Why? fuck if i know, i'm an autistic retard - not a techwizard like Terry Davis xD
            list.ElementAt(list.Count - 1).GetComponent<SpriteRenderer>().sprite = cards.ElementAt(indexOfNextCard).sprite;
            list.ElementAt(list.Count - 1).GetComponent<Card>().number = cards.ElementAt(indexOfNextCard).number;
            nextEnemyCardPosition+=2;
            indexOfNextCard++;
        }
        else
        {
            list.Add(Instantiate
            (cardPrefab,
            new Vector3(nextPlayerCardPosition, 3.5f * sign, 0),
            Quaternion.identity));
            list.ElementAt(list.Count - 1).GetComponent<SpriteRenderer>().sprite = cards.ElementAt(indexOfNextCard).sprite;
            list.ElementAt(list.Count - 1).GetComponent<Card>().number = cards.ElementAt(indexOfNextCard).number;
            nextPlayerCardPosition+=2;
            indexOfNextCard++;
        }
    }
}
