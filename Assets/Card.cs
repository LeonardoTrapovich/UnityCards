using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

    public class Card : MonoBehaviour
{
        //I think something is wrong - but what?
        public string color;
        public int number;
        public Sprite sprite;
        public Card(string color, int number)
        {
            this.color = color;
            this.number = number;
            sprite = Resources.Load<Sprite>($"Images/card-{color}-{number}");
        }
}
