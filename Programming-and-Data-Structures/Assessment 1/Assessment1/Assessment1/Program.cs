using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Assessment1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            bool counter = true;
            while (counter == true)
            {
                Console.WriteLine("Please enter the numerical value depending on which way you wish to enter:");
                Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                Console.WriteLine("2. Do you want to read in the text from a file?");
                Console.WriteLine("3. Exit");
                string Keybofile = Convert.ToString(Console.ReadLine());

                if (Keybofile == "1" || Keybofile == "one" || Keybofile == "One" || Keybofile == "ONE")
                {
                    //Testers for keyboard input to know if code is working, counter used to be int, then string, now bool
                    //Console.WriteLine("works");
                    //counter = 3;
                    Console.Clear();
                    //Will now transfer over to the keyboard input function
                    keyinp();
                }
                else if (Keybofile == "2" || Keybofile == "two" || Keybofile == "Two" || Keybofile == "TWO")
                {
                    //Testers for text input to know if code is working
                    //Console.WriteLine("works2");
                    //counter = 4;
                    Console.Clear();
                    //Will now transfer over to the text file input function
                    textinp(/*counter*/);
                }
                else if (Keybofile == "3" || Keybofile == "three" || Keybofile == "Three" || Keybofile == "THREE")
                {
                    //Will now exit the code
                    counter = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a correct numerical value");
                }
            }
        }            //Console.WriteLine("{0}", counter);
                     //Console.ReadLine();



        public static void keyinp()
        {
           
            Boolean finentry = false, specificlet = false;
            int sent = 0, vowels = 0, conson = 0, upper = 0, lower = 0, totallett = 0, indivlett = 0;
            string totalinput = "";
            while (finentry == false)
            {

                Console.WriteLine("Please enter the string via Keyboard one sentence at a time. \nHit enter to start a new sentence then type * to finish your entry. \nIt will then be Analysed:");
                string input = Console.ReadLine();
                char[] array = input.ToCharArray();

                for (int i = 0; i < array.Length; i++)
                {
                    char letter = array[i];

                    switch (array[i])
                    {
                        case 'a': case 'e': case 'i': case 'o': case 'u':
                        case 'A': case 'E': case 'I': case 'O': case 'U':
                            vowels++;
                            totallett++;
                            break;

                        case 'b': case 'c': case 'd': case 'f': case 'g': case 'h': case 'j': case 'k': case 'l': case 'm': case 'n': case 'p': case 'q': case 'r': case 's': case 't': case 'v': case 'w': case 'x': case 'y': case 'z':
                        case 'B': case 'C': case 'D': case 'F': case 'G': case 'H': case 'J': case 'K': case 'L': case 'M': case 'N': case 'P': case 'Q': case 'R': case 'S': case 'T': case 'V': case 'W': case 'X': case 'Y': case 'Z':
                            conson++;
                            totallett++;
                            break;
                    }
                    //used isupper and islower to reduce the use of cases in the switch statement and try to make it more streamlined
                    if (char.IsUpper(array[i])) { upper++; }
                    if (char.IsLower(array[i])) { lower++; }
                    
                    if (array[i] == '*') {
                        finentry = true;
                    }
                    if (array.Length == 1 && (array[0] == '*' || array[0] == ' ' ) ) { sent--; }
                    
                }

                totalinput = totalinput + " " + input;
                sent++;
                Console.Clear();
            }
            Program Sentiment = new Program();
            string keyboardmood = Sentiment.mood(totalinput);

            Console.WriteLine("Your total sentence was :{0}", totalinput);
            Console.WriteLine("Number of sentences entered = {0}", sent);
            Console.WriteLine("Number of vowels entered = {0}", vowels);
            Console.WriteLine("Number of consonants entered = {0}", conson);
            Console.WriteLine("Number of upper case letters entered = {0}", upper);
            Console.WriteLine("Number of lower case letters entered = {0}", lower);
            Console.WriteLine("Number of letters in total entered = {0}", totallett);

            while (specificlet == false) { 
            Console.WriteLine("Would you like to find the number of a specific letter in your string? Y/N");
            string speclett = Console.ReadLine();
            if (speclett == "Y" || speclett == "y")
            { 
                Console.WriteLine("Which letter do you want to know the number of in your sentence?");
                    char lett = Convert.ToChar(Console.ReadLine());
                if (char.IsLetter(lett)) {
                        char[] array = totalinput.ToCharArray();
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] == lett || char.ToUpper(array[i]) == lett || char.ToLower(array[i]) == lett) { indivlett++; }
                        }
                        // code below in case user tries to find how many spaces are in the text
                        if (lett == ' ') { indivlett--; }
                        Console.WriteLine("There are {0} of the letter {1}", indivlett, lett);
                        Console.ReadLine();
                        indivlett = 0;
                    }
                else { Console.WriteLine("Please enter a specific letter to find out how many are in your text"); }
            }
            else if (speclett == "N" || speclett == "n") { specificlet = true; }
            else { Console.WriteLine("Please enter either Y for yes or N for no"); }
        }
            specificlet = false;
            while (specificlet == false)
            {
                Console.WriteLine("Would you like to see the number of every specific letter in your string? Y/N");
                string alllett = Console.ReadLine();
                if (alllett == "Y" || alllett == "y")
                {
                    allunicdoce(totalinput);

                }
                else if (alllett == "N" || alllett == "n") { specificlet = true; }
                else { Console.WriteLine("Please enter either Y for yes or N for no"); }
            }


            Console.Clear();
            
        }


        
        public static void textinp()
        {
            Boolean specificlet = false;
            int sent = 0, vowels = 0, conson = 0, upper = 0, lower = 0, totallett = 0, indivlett = 0 , longwords = 0, longcounter = 0, filelocation = 0;
            string file = "", sevenword = "", address="";
            char[] arrayfile;

            while (filelocation != 3)
            {
                Console.WriteLine("Please enter the location of the text file you wish to be analysed, every sentence should be finished with a full stop");
                Console.WriteLine(@"e.g C:\documents\source\text.txt");
                file = @Console.ReadLine();
                arrayfile = file.ToCharArray();
                //Error exception handling below by checking the file is a txt file
                string extention = Path.GetExtension(file);


                if (File.Exists(file) && extention == ".txt") {
                    filelocation = 3;

                }
                else { Console.WriteLine("You have entered an incorrect file location or extension, please try again"); }
                
                
                for (int k = 0; k < arrayfile.Length; k++)
                {
                    //first attempt at checking if the file exists, found file.exists after this and implemented it instead as there is much less chance of errors
                    //    if (k == 0) {

                    //        switch (arrayfile[k])
                    //        {
                    //            case 'A': case 'E': case 'I': case 'O': case 'U':case 'B': case 'C': case 'D': case 'F': case 'G': case 'H': case 'J': case 'K': case 'L': case 'M': case 'N': case 'P': case 'Q': case 'R': case 'S': case 'T': case 'V': case 'W': case 'X': case 'Y': case 'Z':
                    //                filelocation++;
                    //                break;
                    //        }}
                    //    if ( arrayfile[1] == ':') { filelocation++; }
                    //    if (arrayfile[2] == '\\') { filelocation++; }
                    //    if (filelocation != 3) {
                    //        Console.WriteLine("You have entered an incorrect file location, please try again");
                    //        k = arrayfile.Length - 1;
                    //    }
                    if (filelocation == 3) {
                        int arrleng = arrayfile.Length;
                        int w = arrayfile.Length -1;
                        while (arrayfile[w] != '\\')
                        {
                            w--;
                            // for (int w = arrayfile.Length; char.IsLetter(arrayfile[w])== true; w--) {  For loop caused exceptions involving overlapping the end of index
                            arrleng--;
                        }
                        arrleng--;              

                        for (int p = 0; p <= arrleng; p++){
                            address = address + arrayfile[p];
                        }
                        address = address + "longwordoutput.txt";
                        k = arrayfile.Length - 1;
                    }
                       

                }
            }
            
        
            string text = File.ReadAllText(file);
            Console.WriteLine("Your text file goes as follows: " + text);
            char[] array = text.ToCharArray();
            for (int j = 0; j < array.Length; j++)
            {
                char letter = array[j];

                switch (array[j])
                    {
                    case '.':
                        sent++;
                        longcounter = 0;
                        break;
                    case '\\': case ' ':case ',': case '-': case '/': 
                        longcounter = 0;
                        break;

                        case 'a': case 'e': case 'i': case 'o': case 'u':
                        case 'A': case 'E': case 'I': case 'O': case 'U':
                            vowels++;
                            totallett++;
                            longcounter++;
                            break;

                        case 'b': case 'c': case 'd': case 'f': case 'g': case 'h': case 'j': case 'k': case 'l': case 'm': case 'n': case 'p': case 'q': case 'r': case 's': case 't': case 'v': case 'w': case 'x': case 'y': case 'z':
                        case 'B': case 'C': case 'D': case 'F': case 'G': case 'H': case 'J': case 'K': case 'L': case 'M': case 'N': case 'P': case 'Q': case 'R': case 'S': case 'T': case 'V': case 'W': case 'X': case 'Y': case 'Z':
                            conson++;
                            totallett++;
                            longcounter++;
                            break;
                    }
                    if (char.IsUpper(array[j])) { upper++; }
                    if (char.IsLower(array[j])) { lower++; }
                    if (longcounter == 7) {
                    longwords++;
                    int s = j;
                    while (char.IsLetter (array[s])) {
                        s--;
                    }
                    s++;
                    while (char.IsLetter(array[s]))
                    {
                        sevenword += array[s];
                        s++;
                    }
                    sevenword += " ";
                    s = 0;
                    File.WriteAllText(address, sevenword);
                    }
                
            }
            Program Sentimentfile = new Program();
            string filemood = Sentimentfile.mood(text);
            Console.WriteLine("Number of sentences entered = {0}", sent);
            Console.WriteLine("Number of vowels entered = {0}", vowels);
            Console.WriteLine("Number of consonants entered = {0}", conson);
            Console.WriteLine("Number of upper case letters entered = {0}", upper);
            Console.WriteLine("Number of lower case letters entered = {0}", lower);
            Console.WriteLine("Number of letters in total entered = {0}", totallett);
            Console.WriteLine("Number of words with more than seven letters entered = {0}", longwords);
            specificlet = false;
            while (specificlet == false)
            {
                Console.WriteLine("Would you like to find the number of a specific letter in your string? Y/N");
                string speclett = Console.ReadLine();
                if (speclett == "Y" || speclett == "y")
                {
                    Console.WriteLine("Which letter do you want to know the number of in your text?");
                    char lett = Convert.ToChar(Console.ReadLine());
                    char[] array2 = text.ToCharArray();
                    for (int i = 0; i < array2.Length; i++)
                    {
                       if (char.IsLetter(lett))
                        {
                            if (array2[i] == lett || char.ToUpper(array2[i]) == lett || char.ToLower(array2[i]) == lett) { indivlett++; }
                            //Console.WriteLine("There are {0} of the letter {1}", indivlett, lett);
                        }
                        else
                        {
                            Console.WriteLine("Please enter a specific letter to find out how many are in your text");
                            i = array2.Length - 1;
                        }
                    }
                    if (indivlett > 0) { Console.WriteLine("There are {0} of the letter {1}", indivlett, lett); }

                   indivlett = 0;
                }
                else if (speclett == "N" || speclett == "n") { specificlet = true; }
                else { Console.WriteLine("Please enter either Y for yes or N for no"); }
            }
            specificlet = false;
            while (specificlet == false)
            {
                Console.WriteLine("How about the number of every letter in your string? Y/N");
                string alllett = Console.ReadLine();
                if (alllett == "Y" || alllett == "y")
                {
                    allunicdoce(text);

                }
                else if (alllett == "N" || alllett == "n") { specificlet = true; }
                else { Console.WriteLine("Please enter either Y for yes or N for no"); }
            }
            Console.Clear();
            
        }

            public string mood (string allwords)
            {
            //doesn't matter if the user uses keyboard or text file input, they should come here
            //allwords = "working";
            //ArrayList moodarray = new ArrayList();
            //Dictionary<int, char> SentimDict = new Dictionary<int, char>();
            string lowerallwords = allwords.ToLower(); 
            List<string> sentimlistpos = new List<string>();
            List<string> sentimlistneg = new List<string>();
            List<string> sentiminput = new List<string>();
            List<char> sentimword = new List<char>();
            string pos = File.ReadAllText("Positive.txt");
            string neg = File.ReadAllText("Negative.txt");
            //Each word has their own line and so hnce stored with a \r\n inbetween them when called, split was used to counteract this.
            string[] poswords = Regex.Split(pos, "\r\n");
            string[] negwords = Regex.Split(neg, "\r\n");
            foreach (string p in poswords) {
                //older less efficient code, replaced when a better solution was found. Still used later on for user input
                //if (p != ' ') {sentimword.Add(p);}
                //else if (p == ' ') {
                //string temp1 = string.Join(" ",sentimword.ToArray());
                sentimlistpos.Add(p);
                //sentimword.RemoveAll();
                //sentimword.Clear();
            //    }
            }
            //sentimword.Clear();
            foreach (string n in negwords)
            {
                sentimlistneg.Add(n);  
            }
            foreach (char inp in lowerallwords)
            {
                if (inp != ' ') { sentimword.Add(inp); }
                else if (inp == ' ')
                {
                    string temp3 = string.Join("", sentimword.ToArray());
                    sentiminput.Add(temp3);
                    sentimword.Clear();
                }
            }
            sentimword.Clear();
            int posit = 0;
            int negat = 0;
            int total = 0;
            string Amount = " ";
            string Mood = "neutral";
            foreach (string s in sentiminput) {
                if (sentimlistpos.Contains(s)) { posit++; }
                if (sentimlistneg.Contains(s)) { negat++; }
            }
            total = posit - negat;
            if (total > 0) { Mood = "Positive"; }
            else if (total < 0) { Mood = "Negative"; }
            if (total > 5 || total < -5) { Amount = " very "; }
            //string[] sentstriarray;
            
            
            Console.WriteLine("Your input has a{0}{1} outlook to it", Amount, Mood);
            Console.ReadLine();
            return allwords;
        }


        public static string allunicdoce(string text)
        {
            Console.Clear();
             Dictionary<char, int> x = new Dictionary<char, int>();
            int num = 0;
            //The list filled in below contains all possible letters and is stored in a dictionary
            for (int i = 0 ; i <= 160; i++)
            {//creates dictionary with all alphabetic letters stored inside
                char c = Convert.ToChar(i);
                if (char.IsLetter(c)) {
                    x.Add(c, num);    
                }
            }
            foreach ( char ch in text) {
                //when text input is given, all values are checked and if they match the dictionary the number of that number is increased
                if (x.ContainsKey(ch))
                {
                    x[ch]++;
                }
            }
            for (int g = 0; g <= 160; g++)
            {
                char q = Convert.ToChar(g);
                //any letters in the dictionary with more than 0 will be output
                if (char.IsLetter(q) && x[q] > 0)
                {
                    Console.WriteLine("Letter: {0} \t Number of that Letter: {1} ", q , x[q]);
                } 
            }
                     Console.ReadLine();
                     return text;
        }
}  
    }
//below is all testing code or reminders of future tasks/ features

//Number of sentences entered = 1
//Number of vowels = 6
//Number of consonants = 11
//Number of upper case letters = 1
//Number of lower case letters = 16
//The frequency of individual letters = ?

//start using char.IsLetter/IsUpper/Islower for words instead of massive switch case statements
//need to create file to save seven letter words, how should they be found?
//Add in checking for text file to make surte user can't enter in the wrong code, can only do it for so far until an error occurs?

//use dictionary to find a words spelling mistakes
//public void Add(TKey key,TValue value){} can be used to create an array??
// also keep finding out Nhunspell when looking for spelling identifiers, uses dictionary

//fix error with user using a space or * to break sentences data
//fix error where the number of specific letters for the input is added to the previous every time a person says yes

//could start using Stacks and Queues to organise data, but would only be needed to streamline as the code is practically finished already
//Use more Data structures??

// Implement the class program to create a new method that can be called in either keyboard or text input 

// should try to add some stacks or linked lists maybe? What is the payoff of using them as opposed to array, have to find out

//Failed Attempt at creating an array for all output of every letter:
//char[] userinp = text.ToCharArray();
//List<char> charact = new List<char>();
//string[][][] x = new string[160][][];
//below all values of the letters are read out, f<= 116 was found via trial and error
//for (int x = 0; x <= userinp.Length; x++)
//{

//    for (int f = 0; f <= 116; f++)
//    {
//        if (userinp[x] == charact[f])
//        {
//            char value = charact[f];
//            Console.WriteLine(value);
//        }
//    }
//}
//Console.ReadLine();

// Want to create an array 
//completed mood, need to change location of long letter file to debug so it can work wherever
//Could look at doing a spell checker?