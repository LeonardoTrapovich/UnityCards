No fucking idea how git works so i'm adding this to remind myself what the fuck was inside the unity project
GameController - empty object with a script that holds way too much shit
Canvas with two buttons(Hit, Stand) and two TMP texts - one with the player score, another hidden until it needs to display a message
Card(prefab) - the most important prefab ever. Something is fucky with it - objects of card class hold values but the cards themselves don't get these values and i have to assign them manually. It holds the script of the card class.
DeckOfCards - unused class. I took many methods from it and expanded them after planting them into the GameController. Should be remade to unfuck the problems with the Card class and prefab. Originally used in a console application that generated MANY decks of cards.

The "game" uses Pixel Fantasy Playing Cards asset pack from itch.io - it's free real estate. https://cazwolf.itch.io/pixel-fantasy-cards Credits to Cazwolf for it.

Still a better game than Dreamworld, Earth2 and many other scams since i actually coded it and i'm not taking your money.
