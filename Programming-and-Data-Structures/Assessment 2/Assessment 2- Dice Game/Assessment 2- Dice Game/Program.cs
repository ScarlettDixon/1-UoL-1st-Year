using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2__Dice_Game
{
    public static class GlobalVar //An attempt at making Global Variables, Just to test if it works to possibly streamline the code
    {
        public static int Score1; 
        public static int Score2;
    }


    class MainMenu
    {
        static void Main(string[] args)
        {
            bool mainmenu = true;
            Player PlayerMain = new Player();
            while (mainmenu == true)
            {
                Console.Clear();
                Console.WriteLine("DICE GAME: 3 OR MORE");
                Console.WriteLine("Please enter the integer value depending on what game you'd like to play:");
                Console.WriteLine("1. Human Vs Human");
                Console.WriteLine("2. Human Vs Computer");
                Console.WriteLine("3. Game Rules");
                Console.WriteLine("4. Exit");
                string GameType = Convert.ToString(Console.ReadLine());
                //Player 1 and either Player 2 or the computer are initiialised
                if (GameType == "1" || GameType == "one" || GameType == "One" || GameType == "ONE")
                {
                    Console.Clear();
                    PlayerMain.InitialisePlayer1();
                    PlayerMain.InitialisePlayer2();
                }
                else if (GameType == "2" || GameType == "two" || GameType == "Two" || GameType == "TWO")
                {
                    Console.Clear();
                    PlayerMain.InitialisePlayer1();
                    PlayerMain.InitialiseAI();
                }
                else if (GameType == "3" || GameType == "three" || GameType == "Three" || GameType == "THREE")
                {
                    PlayerMain.Rules();
                }
                else if (GameType == "4" || GameType == "four" || GameType == "Four" || GameType == "FOUR")
                {
                    mainmenu = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a correct integer value");
                }
                Console.Clear();
                GameType = "";
            }
        }
     }
    public class Player
    {

        public string Name1;
        public string Name2;
        public static Dictionary<int, string> Players = new Dictionary<int, string>(); 
        // ^^ Needs to be static to acces from other classes. Wipes if class is left otherwise.
        //Other way to do it but this seems easiest at this point in time
        public void InitialisePlayer1()
        {
            Players.Clear();
            Console.WriteLine("Player 1 please enter your name or type no if you don't wish to give a name");
            Name1 = Console.ReadLine();
            if (Name1 == "No" || Name1 == "N" || Name1 == "NO" || Name1 == "n") { Name1 = "Player 1"; };
            Players.Add(1, Name1);
        }
        public void InitialisePlayer2()
        {
            Console.WriteLine("Player 2 please enter your name or type no if you don't wish to give a name");
            Name2 = Console.ReadLine();
            if (Name2 == "No" || Name2 == "N" || Name2 == "NO" || Name2 == "n") { Name2 = "Player 2"; };
            Players.Add(2, Name2);
            Game.Player1(); //Can go straight to player1 method as there will always be a player 1

        }
        public void InitialiseAI()
        {
            Players.Add(2, "Computer");
            Game.Player1(); //Can go straight to player1 method as there will always be a player 1
        }
        public void Rules()
        {
            Console.Clear();
            Console.WriteLine("DICE GAME RULES");
            Console.WriteLine("Players take turns rolling five dice");
            Console.WriteLine("and attempt to score three-of-a-kind or better");
            Console.WriteLine("If you score 5-of-a-kind: 12 points");
            Console.WriteLine("If you score 4-of-a-kind: 6 points");
            Console.WriteLine("If you score 3-of-a-kind: 3 points");
            Console.WriteLine("If you score 2-of-a-kind: Re Roll");
            Console.WriteLine("You may reroll the remaining 3 dice once to try to get a better score");
            Console.WriteLine("fail to get 3-of-a-kind on a second roll: 0 points");
            Console.WriteLine("If you have none matching : 0 points");
            Console.WriteLine("You may choose at the start to only roll once that turn no matter what");
            Console.WriteLine("This doubles any points you get on that roll");
            Console.WriteLine("First to 50 Points wins");
            Console.WriteLine("Hit enter to return to the main menu");
            Console.ReadLine();


        }
    }
    public class Game
    {

        public static Double AveDie;
        public static int AveTot;
        public static int DiceRolled;
        public static int AllThroTot;
        public static int AveThroTot;
        public static int TotalTurns;
        public static int NoOfTurns;
        public static int NoPointsTurns;
        public static int Playernum;
        public static List<string> DicePoints = new List<string>();
        public string TurnInfo = "";
        int[] DiceRoll = new int[5];
        public static void Player1()
        {
            int Inputscore = 0;
            int NoOfAKind = 0;
            int NoOfTwos = 0;
            int NoofRerolls = 0;
            int OneTurnPoints = 0;
            Game.Playernum = 1;
            //bool EndGame = false;
            int[] TwoMatches = new int[2];
            Game Scores = new Game();
            //Player Names = new Player();

            Console.Clear();
            if (GlobalVar.Score1 < 50 && GlobalVar.Score2 < 50)
            {
                Console.WriteLine("{0}'s turn", Player.Players[1]);
                Game.NoOfTurns++; // Only use this for Player 1
                Console.WriteLine("Do you want to throw all dice just once for double points? Y/N");
                string DoubPoints = Console.ReadLine();
                string AllTurnScores = "";
                for (int i = 0; i < 5; i++)
                {
                    Inputscore = 0;
                    Die.Dice(ref Inputscore);
                    // Scores.ThroTot1 += Inputscore;
                    // Scores.DiceRolled1++;
                    //Console.WriteLine(Inputscore);
                    Scores.DiceRoll[i] = Inputscore;
                }
                MatchesNumber(ref Scores.DiceRoll, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfAKind, ref NoOfTwos, ref Game.NoOfTurns, ref TwoMatches);


                //NoOfAKind = Array.IndexOf(Scores.DiceRoll, j);
                //if (NoOfAKind > 1 && NoOfAKind >= 3)
                //{
                //Matches[Z] = j;
                GlobalVar.Score1 += Game.Point(ref NoOfAKind, DoubPoints, ref NoofRerolls, ref OneTurnPoints, ref Scores.DiceRoll, ref TwoMatches, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfTwos);
                //break;
                //}
                //else if (NoOfAKind == 2) //put something in place to fix when two sets of two come up, just choose one of the two sets of two?
                ////Could let player choose which set of 2s to stick with? If there is time to implement it
                //{
                //    //Scores.DiceRoll[0] = j;
                //    //Scores.DiceRoll[1] = j;
                //    GlobalVar.Score1 += Game.Point(ref NoOfAKind, DoubPoints, ref NoofRerolls, ref OneTurnPoints, ref Scores.DiceRoll, ref TwoMatches);
                //    MatchesNumber(ref Scores.DiceRoll, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfAKind, ref NoOfTwos, ref Game.NoOfTurns, ref TwoMatches);
                //    GlobalVar.Score1 += Game.Point(ref NoOfAKind, DoubPoints, ref NoofRerolls, ref OneTurnPoints, ref Scores.DiceRoll, ref TwoMatches);

                //}
                //else
                //{
                //    GlobalVar.Score1 += 0;
                //}
                //*1 Look at bottom for more
                // Console.WriteLine("Your Score for this round is {0}" , GlobalVar.Score1);
                Scores.TurnInfo += " Total: " + GlobalVar.Score1;
                DicePoints.Add(Scores.TurnInfo);
                Console.WriteLine("\r");
                foreach (string Turn in DicePoints)
                {
                    Console.WriteLine(Turn);
                }
                //Console.ReadLine();
                Console.WriteLine("\r");
                Statistics();
                if (Player.Players[2] == "Computer") { Computer();}
                else { Player2(); }
           
            }
            else
            {
                if (GlobalVar.Score1 > GlobalVar.Score2) { Console.WriteLine("{0} Wins", Player.Players[1] ); }
                else if (GlobalVar.Score2 > GlobalVar.Score1) { Console.WriteLine("{0} Wins", Player.Players[2]); }
                else { Console.WriteLine("The Game is a Draw"); }
                Console.ReadLine();
            }
        }
        public static void Player2() {
            int Inputscore = 0;
            int NoOfAKind = 0;
            int NoOfTwos = 0;
            int NoofRerolls = 0;
            int OneTurnPoints = 0;
            Game.Playernum = 2;
            int[] TwoMatches = new int[2];
            Game Scores = new Game();
            Console.Clear();
            if (GlobalVar.Score1 < 50 && GlobalVar.Score2 < 50)
            {
                Console.WriteLine("{0}'s turn", Player.Players[2]);
                Console.WriteLine("Do you want to throw all dice just once for double points? Y/N");
                string DoubPoints = Console.ReadLine();
                string AllTurnScores = "";
                for (int i = 0; i < 5; i++)
                {
                    Inputscore = 0;
                    Die.Dice(ref Inputscore);
                    Scores.DiceRoll[i] = Inputscore;
                }
                MatchesNumber(ref Scores.DiceRoll, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfAKind, ref NoOfTwos, ref Game.NoOfTurns, ref TwoMatches);
                GlobalVar.Score2 += Game.Point(ref NoOfAKind, DoubPoints, ref NoofRerolls, ref OneTurnPoints, ref Scores.DiceRoll, ref TwoMatches, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfTwos);
                Scores.TurnInfo += " Total: " + GlobalVar.Score2;
                DicePoints.Add(Scores.TurnInfo);
                Console.WriteLine("\r");
                foreach (string Turn in DicePoints)
                {
                    Console.WriteLine(Turn);
                }
                Console.WriteLine("\r");
                //Console.ReadLine();
                Statistics();
                Player1();
           
            }
            else
            {
                if (GlobalVar.Score1 > GlobalVar.Score2) { Console.WriteLine("{0} Wins", Player.Players[1]); }
                else if (GlobalVar.Score2 > GlobalVar.Score1) { Console.WriteLine("{0} Wins", Player.Players[2]); }
                else { Console.WriteLine("The Game is a Draw"); }
                Console.ReadLine();
            }


        }
        public static void Computer() {
            int Inputscore = 0;
            int NoOfAKind = 0;
            int NoOfTwos = 0;
            int NoofRerolls = 0;
            int OneTurnPoints = 0;
            Game.Playernum = 2;
            int[] TwoMatches = new int[2];
            Game Scores = new Game();
            Console.Clear();
            if (GlobalVar.Score1 < 50 && GlobalVar.Score2 < 50)
            {
                Console.WriteLine("{0}'s turn", Player.Players[2]);
                //Console.WriteLine("Do you want to throw all dice just once for double points? Y/N");
                string DoubPoin = "";
                string DoubPoints = "";
                Random AIRand = new Random();
                int AIChoice = AIRand.Next(1, 11);
                if (AIChoice > 7)
                {
                    DoubPoin = " Not";
                    DoubPoints = "No";
                }
                else {
                    DoubPoin = "";
                    DoubPoints = "Yes";
                }
                Console.WriteLine("Computer chooses to{0} Double their score", DoubPoin);
                
                string AllTurnScores = "";
                for (int i = 0; i < 5; i++)
                {
                    Inputscore = 0;
                    Die.Dice(ref Inputscore);
                    Scores.DiceRoll[i] = Inputscore;
                }
                MatchesNumber(ref Scores.DiceRoll, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfAKind, ref NoOfTwos, ref Game.NoOfTurns, ref TwoMatches);
                GlobalVar.Score2 += Game.Point(ref NoOfAKind, DoubPoints, ref NoofRerolls, ref OneTurnPoints, ref Scores.DiceRoll, ref TwoMatches, ref AllTurnScores, ref Scores.TurnInfo, ref NoOfTwos);
                Scores.TurnInfo += " Total: " + GlobalVar.Score2;
                

                
                DicePoints.Add(Scores.TurnInfo);
                Console.WriteLine("\r");
                foreach (string Turn in DicePoints)
                {
                    
                    Console.WriteLine(Turn);
                }
                //int DiceCount = Game.DicePoints.Count(); // attempt at making sure the code wouldn't fill the screen and over. Count for some reason wouldn't increase
               // int DiCount = DicePoints.Count();
                //if (DicePoints.Count == 15) {
                   // DicePoints.Clear();
                //}
                Console.WriteLine("\r");
                //Console.ReadLine();
                Statistics();
                Player1();

            }
            else
            {
                if (GlobalVar.Score1 > GlobalVar.Score2) { Console.WriteLine("{0} Wins", Player.Players[1]); }
                else if (GlobalVar.Score2 > GlobalVar.Score1) { Console.WriteLine("{0} Wins", Player.Players[2]); }
                else { Console.WriteLine("The Game is a Draw"); }
                Console.ReadLine();
            }

        }
        public static void Statistics() {
            Game.AveTot = Convert.ToInt32(Game.AveDie);
            Game.AveDie = Game.AveDie / Game.DiceRolled;
            Game.AllThroTot += Game.AveTot;
            Game.TotalTurns++;
            Game.AveThroTot = Game.AllThroTot / Game.TotalTurns;
            
            bool exitstat = false;
            while (exitstat == false)
            {
                Console.WriteLine("Do you want to look at the stats for all turns so far? Y/N");
                string Choice = Console.ReadLine();
                if (Choice == "y" || Choice == "YES" || Choice == "Y" || Choice == "yes" || Choice == "yes")
                {
                    Console.WriteLine("Statistics:");
                    Console.WriteLine("Average individual dice score for that turn: {0}", Game.AveDie);
                    Console.WriteLine("Total dice number score for that turn: {0}", Game.AveTot);
                    Console.WriteLine("Total dice number score for All Turns : {0}", Game.AllThroTot);
                    Console.WriteLine("Average Total dice number score for All Turns : {0}", Game.AveThroTot);
                    Console.WriteLine("Number of turns where player scored Nothing : {0}", Game.NoPointsTurns);
                    Console.ReadLine();
                    exitstat = true;
                }
                else if ((Choice == "n" || Choice == "NO" || Choice == "N" || Choice == "no" || Choice == "No"))
                {
                    exitstat = true;
                }
                else
                {
                    Console.WriteLine("Please enter Yes or No");
                }
            }
            Game.AveDie = 0;
            Game.DiceRolled = 0;
            //AveDie1 = AveDie1 + (ThroTot1 / DiceRolled1);
            //AveDie2 = AveDie2 + (ThroTot2 / DiceRolled2);
        }
        public static int MatchesNumber(ref int[] DiceRoll, ref string AllTurnScores, ref string Turninfo, ref int NoOfAKind, ref int NoOfTwos, ref int NoOfTurns, ref int[] TwoMatches ) {
            AllTurnScores = "";
            foreach (int Dscore in DiceRoll)
            {
                AllTurnScores = AllTurnScores + Convert.ToString(Dscore) + " ";
                Game.AveDie+= Dscore;
                Game.DiceRolled++;
            }
            Turninfo = "Turn " + Convert.ToString(NoOfTurns) + " "+ Player.Players[Game.Playernum] +": " + AllTurnScores;
            Console.WriteLine(Turninfo);
            Array.Sort(DiceRoll);
            NoOfAKind = 1;
            NoOfTwos = 0;
            for (int j = 0; j <= 3; j++)
            {
                if (DiceRoll[j] == DiceRoll[j + 1]) //If they match the number of matches increases
                {
                    NoOfAKind++;
                }
                else if (DiceRoll[j] != DiceRoll[j + 1] && NoOfAKind > 2) { break; } //If the next one in the list doesn't match but they already have 3-ofakind or above then break out of the loop
                else if (DiceRoll[j] != DiceRoll[j + 1] && NoOfAKind == 2 && NoOfTwos == 0) //If they've only got two but the array hasn't been fully searched yet.
                {
                    TwoMatches[0] = DiceRoll[j];
                    NoOfTwos++;
                    NoOfAKind = 1;

                }
                //*2
                
            }
            if (NoOfAKind == 2 && TwoMatches[0] == 0) { TwoMatches[0] = DiceRoll[4]; } //Stops the error of having two matches on the end of the array
            if (NoOfAKind < 2 && NoOfTwos == 1) //Have one set of two matches but nothing else
            {
                NoOfAKind = 2;
            }
           
            //*3
            return NoOfAKind;



        }
        public static int Point(ref int Score, string DoubPoints, ref int NoOfRerolls, ref int OneTurnPoint, ref int[] DiceRoll, ref int[] Matches, ref string AllTurnScores, ref string TurnInfo, ref int NoOfTwos)
        {
            //Game ScoresSecond = new Game();

            if (DoubPoints == "y" || DoubPoints == "YES" || DoubPoints == "Y" || DoubPoints == "yes" || DoubPoints == "Yes")
            { // If they put double points it will go here first
                switch (Score)
                {
                    case 0:
                        OneTurnPoint = 0;
                        Game.NoPointsTurns++;
                        break;
                    case 1:
                        OneTurnPoint = 0;
                        Game.NoPointsTurns++;
                        break;
                    case 2:
                        OneTurnPoint = 0;
                        Game.NoPointsTurns++;
                        break;
                    case 3:
                        OneTurnPoint = 6;
                        break;
                    case 4:
                        OneTurnPoint = 12;
                        break;
                    case 5:
                        OneTurnPoint = 24;
                        break;
                }
            }
            else if (Score > 2 && (DoubPoints == "n" || DoubPoints == "NO" || DoubPoints == "N" || DoubPoints == "no" || DoubPoints == "No"))
            { //Score more than 2 matches on the diceroll but did not click double points
                switch (Score)
                {
                    case 3:
                        OneTurnPoint = 3;
                        break;
                    case 4:
                        OneTurnPoint = 6;
                        break;
                    case 5:
                        OneTurnPoint = 12;
                        break;
                }
            }
            else if (Score == 2 && NoOfRerolls == 0 && (DoubPoints == "n" || DoubPoints == "NO" || DoubPoints == "N" || DoubPoints == "no" || DoubPoints == "No"))
                //For when two of the rolls match and they chose not to double their scores, will take those two matched values and reroll
            {
                // int secondroll = 0;
                Console.WriteLine("You got two matches and didn't choose double points! REROLL!!!");
                NoOfRerolls++;
                //break;
                //if (NoofRerolls == 1)
                //{
                int secondroll = 0;
                DiceRoll[0] = Matches[0];
                DiceRoll[1] = Matches[0];
                for (int i = 2; i < 5; i++)
                {
                    secondroll = 0;
                    Die.Dice(ref secondroll);
                    DiceRoll[i] = secondroll;
                }
                //Now it goes through the sorting and point allocation again
                MatchesNumber(ref DiceRoll, ref AllTurnScores, ref TurnInfo, ref Score, ref NoOfTwos, ref Game.NoOfTurns, ref Matches);
                OneTurnPoint = Game.Point(ref Score, DoubPoints, ref NoOfRerolls, ref OneTurnPoint, ref DiceRoll, ref Matches, ref AllTurnScores, ref TurnInfo, ref NoOfTwos);
                if (OneTurnPoint == 0) { Game.NoPointsTurns++; }

                // Point(ref Score, DoubPoints, ref NoOfRerolls);
            }
            else if (Score == 2 && NoOfRerolls == 1) { OneTurnPoint = 0; } //will have to be 2 as you have the others carried over from the previous roll
            else if (Score < 2) { OneTurnPoint = 0; } //For the case where they have picked to not double points but don't get any matches
            else //Will only occur if they have not entered yes or no correctly
            {
                Console.WriteLine("You did not enter yes or no for your double points, hence you will get 0 points");
                OneTurnPoint = 0;
            }
            

            return OneTurnPoint;
        }
    }
    
    public class Die
    {
        public static Random RandomDice = new Random();
        public static int Dice(ref int RandDice)
        {
            
            RandDice = RandomDice.Next(1, 7); // Will choose between 1 and 6, the numbers achievable on a die
            return RandDice;
        }
    }
    //use a string array to keep track of the scores?
    //could try to use recursion when implementing the dice?
    //How to check the scores from the dice to see if they are the same?, could sort then search?
    //Could use a seperate dictionary and use the built in fucntion to find the number?
    //must be a less tedious way of solving the comparing
    //will use randomised choices for the AI, some times it'll choose to roll all just once for double points
    //Believe task was chosen because the number of dice linked to ability to reroll if you get a two is hard to program
    //Can keep data on rolls above 2 matches easily. But if they get two it needs to be rolled again. Also if they get two
    //there is a possibility that they can get a three also. which adds another layer of code because you have to store the two and
    //look through the array for other matches before continuing. 
    //Nearly all code is segmented if needed to be called on more than once apart from the central player 1 2 and comp, very similar code but not easily segmented
    //If I have more time I'll segement it all so Changes don't have to be made 3 times
    //A frequency array would have been better/ less bug prone than how I sorted then searched it but hindsight is 20/20

}


//Below is code that was removed but shows iterations of previous code:
//They were moved down here because they got in the way of future iterations
//They are numbered with a star and number e.g *5 to show where in the code they came from


//*1
//int Z = 0;
// int NoOfAKind = 0;
//for (int j = 0; j < DiceRoll.Length; j++) {
//for (int k = j + 1; k < DiceRoll.Length; k++)
//{
//    if ((k != j) && (DiceRoll[k] == DiceRoll[j]) && j > 0)
//    {
//        //NoOfAKind++;
//        Matches[Z] = DiceRoll[j]; //need to store there location and be able to retrieve it
//        //Console.WriteLine("There is more than one of {0} at array location {1} and {2}", Matches[Z], j, k);
//        //Z++;
//        //Matches[Z] = DiceRoll[k];
//        //Z++;
//    }
//    else if ((k != j) && (DiceRoll[k] == DiceRoll[j]) && k < DiceRoll.Length)
//    {
//    }
//}
//    
//}
//foreach (int test in Matches)
//{
//    if (test != 0)
//    {
//        NoOfAKind++;
//    }
//}

// Console.WriteLine("Number of duplicates = {0}" , NoOfAKind);
//Console.WriteLine(AllTurnScores);
//Console.ReadLine();

//Game.Player2();
//if (Scores.Turns > 1)
//{
//    Scores.Statistics();
//}


//*2

//{
//    //if (NoOfAKind == 2)
//    //{
//    DiceRoll[0] = TwoMatches[0]; //Change later to allow player choice
//    DiceRoll[1] = TwoMatches[0];
//    //Array.Clear(DiceRoll, 2, DiceRoll.Length - 1);
//    NoOfAKind = 2;
//    //}
//    //else if (NoOfAKind == 3) //Can only equal 2 or 3 because there is already a two in the list
//    //{
//    //    //Dont need to break as it would have finished
//    //}
//}
//else if (DiceRoll[j] != DiceRoll[j + 1] && NoOfAKind <= 2 && NoOfTwos == 1)
//{
//    NoOfAKind = 2;
//    
//}
//if (((j + 1) == 4) && NoOfTwos == 1 && NoOfAKind < 3) {
//    NoOfAKind = 2;
//}
//if (((j + 1) == 4) && NoOfAKind == 2 && DiceRoll[j] == DiceRoll[j + 1]) {
//    TwoMatches[0] = DiceRoll[j];
//}


//*3

//if (NoOfTwos == 1 && NoOfAKind < 2)
//{
//    DiceRoll[0] = TwoMatches[0];
//    DiceRoll[1] = TwoMatches[0];
//    NoOfAKind = 2;
//}

//Stretch Exercises:
//Implementing the game described above well, will enable you to achieve good marks in this
//assignment (up to 2:1). However, in order to challenge yourself and potentially achieve higher marks,
//attempting further tasks will be needed:
// Keep and display a ‘history’ of the dice throws in the game, eg:
//o Turn 1 Player 1: 3 3 5 6 1
//o Turn 2 Player 1: 2 2 2 4 6
//o Show statistics on these throws (eg: average of each die, total for each throw, average
//total for all throws, etc)
// Implement a ‘throw all dice once’ option where the points totals are doubled
// Implement a primitive AI which allows you to play against the computer.