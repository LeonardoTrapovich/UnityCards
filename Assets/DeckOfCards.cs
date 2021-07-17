using System;
using System.Collections.Generic;

     class DeckOfCards
    {
        List<Card> cards = new List<Card>();
        string[] colors = new string[] { "clubs", "diamonds", "hearts", "spades" };
        int[] figures = new int[13] { 2, 3, 4, 5, 6, 6, 8, 9, 10, 11, 12, 13, 1 };

        public DeckOfCards()
        {
            GenerateCards();
        }

        /*public DeckOfCards(string[] colors)
        {
            this.colors = colors;
            GenerateCards();
        }*/
        
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

        /*public void PrintCards()
        {
            foreach (Card card in cards)
            {
                card.PrintCard();
            }
        }*/

        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        /*public Card Draw()
        {

        }*/
    }
