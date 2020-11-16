using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Assessment_2
{
    public static class GlobalVariables
     {
        //public static string[,] DataArray; //Initial 2D array used which was fully string, would have caused a lot of problems with conversion for sorting
        public static object[,] DataArrayObject; // Newer 2D array that are contained as objects //Later found the same issue of conversion
        public static Dictionary<string, string> Months = new Dictionary<string, string>(); //Storage to help with month sorting
      }

    class DataChoice
    {
        static void Main(string[] args)
        {
            string D1;
            bool Menu = true;
            while (Menu == true)
            {
                Console.Clear();
                Console.WriteLine("Please enter the numerical value depending on what data sets you wish to use:");
                Console.WriteLine("1. Data 1");
                Console.WriteLine("2. Data 2");
                Console.WriteLine("3. Data 1 + Data 2");
                Console.WriteLine("4. Exit");
                string DataChoice = Convert.ToString(Console.ReadLine());

                if (DataChoice == "1" || DataChoice == "one" || DataChoice == "One" || DataChoice == "ONE")
                {
                    D1 = "Data_1"; //First file with data set 1
                    if (Directory.Exists(D1))
                    {
                        Data(D1);
                        //Console.ReadLine();
                    }
                    else { Console.WriteLine("File Retrieval failed"); }
                }
                else if (DataChoice == "2" || DataChoice == "two" || DataChoice == "Two" || DataChoice == "TWO")
                {
                    D1 = "Data_2"; //Second file with data set 2
                    if (Directory.Exists(D1))
                    {
                        Data(D1);
                        //Console.ReadLine();
                    }
                    else { Console.WriteLine("File Retrieval failed"); }
                }
                else if (DataChoice == "3" || DataChoice == "three" || DataChoice == "Three" || DataChoice == "THREE")
                {
                    D1 = "Data_3"; //Third file with data set 1 and 2
                    if (Directory.Exists(D1))
                    {
                        Data(D1);
                        //Console.ReadLine();
                    }
                    else { Console.WriteLine("File Retrieval failed"); }

                }
                else if (DataChoice == "4" || DataChoice == "four" || DataChoice == "Four" || DataChoice == "FOUR")
                {
                    Menu = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a correct numerical value");
                }
            }
            //foreach (string path in Directory.EnumerateFiles("Data_1"))
            //{
            //    Console.WriteLine(path);
            //    Console.ReadLine();
            //}
            //string Year = @"Data_1\Year_1.txt";
            //Data(Year);
        }
        //public static void DataFusion(string[] BothData) {
        //    for (int z = 0; z < BothData.Length; z++) {
        //        string ShrtName1 = Path.GetFileNameWithoutExtension(BothData[z]);
        //        ShrtName1 = ShrtName1.Remove(ShrtName1.Length - 2);
        //        string ShrtName2 = Path.GetFileNameWithoutExtension(BothData[z+1]);
        //        ShrtName2 = ShrtName2.Remove(ShrtName2.Length - 2);
        //        if (ShrtName1 == ShrtName2) {

        //        }
        //    }


        //}
        static string Data(string DataChoice)
        {
            // DataTable DataChoice1 = getTable(ref DataChoice);

            //height = Convert.ToInt32(height * 0.92); width = Convert.ToInt32(width * 0.92);

            //Console.SetWindowPosition(0,0);
            string[] Data1 = Directory.GetFiles(DataChoice);

            int columns = Data1.Length;
            string Arraysize2 = File.ReadAllText(Data1[0]);
            string[] ArraysizeArray = Regex.Split(Arraysize2, "\r\n");
            int rows = ArraysizeArray.Length + 1;
            if (DataChoice == "Data_3") { //shortens the num of columns and elgongates the rows to realign it for two sets of data
                columns = columns / 2;
                rows -= 1;
                rows = rows * 2;
                rows -= 1;

            }// DataFusion(Data1); }
            //Need to set array size now to accomodate future additions to the code //Helps
            //GlobalVariables.DataArray = new string[(Data1.Length), (ArraysizeArray.Length + 1)]; //Older array that was completely string
            GlobalVariables.DataArrayObject = new object[(columns), (rows)];
            int count1 = 0;
            int count2 = 1; // Set to 1 so I can add the names of the files at the top
            string FullName;
            foreach (string info in Data1)
            {
                //Choice between creating several arrays or one large array, large array easier to implement but a large amount of conversion needed,
                //several arrays need to have their variable specifically chosen
                //Could use a different data structure like list that doesn't need it's input variable decided before data is inputted
                //Need to use an arraylist or other data structure in case a future user wishes to add in more data to the lists
                //Unsure if conversion of parts of a string 2d array to int will be viable // Look up data tables - might solve the issue
                //Iris_ID TimeStamp Latt long Region Magnitude Depth Time Day Month	Year		
                FullName = Path.GetFileName(info);
                string name = Path.GetFileNameWithoutExtension(FullName);
                name = name.Remove(name.Length - 2);
                string Type = "String";
                switch (name) {
                    case "IRIS_ID":
                        count1 = 0;
                        Type = "Int";
                        break;
                    case "Timestamp":
                        count1 = 1;
                        //Type = "Int";
                        break;
                    case "Latitude":
                        count1 = 2;
                        Type = "Double";
                        break;
                    case "Longitude":
                        count1 = 3;
                        Type = "Double";
                        break;
                    case "Magnitude":
                        count1 = 4;
                        Type = "Double";
                        break;
                    case "Depth":
                        count1 = 5;
                        Type = "Double";
                        break;
                    case "Time":
                        count1 = 6;
                        break;
                    case "Day":
                        count1 = 7;
                        Type = "Int";
                        break;
                    case "Month":
                        count1 = 8;
                        break;
                    case "Year":
                        count1 = 9;
                        Type = "Int";
                        break;
                    case "Region":
                        count1 = 10;
                        break;
                    default:
                        Console.WriteLine("Error, name: {0}", name);
                        break;
                }
                if (GlobalVariables.DataArrayObject[count1, 1] != null) //For the ability to have both files in at once
                {
                    count2 = Convert.ToInt32((rows - 1) / 2);
                }
                GlobalVariables.DataArrayObject[count1, 0] = name;
                string text = File.ReadAllText(info);
                string[] DataSet = Regex.Split(text, "\r\n");
                if (Type == "Int") {
                    foreach (string Data in DataSet)
                    {
                        int Data2 = Convert.ToInt32(Data);
                        GlobalVariables.DataArrayObject[count1, count2] = Data2;
                        count2++;
                    }
                }
                else if (Type == "Double") {
                    foreach (string Data in DataSet)
                    {
                        double Data2 = Convert.ToDouble(Data);
                        GlobalVariables.DataArrayObject[count1, count2] = Data2;
                        count2++;
                    }
                }
                else
                {
                    foreach (string Data in DataSet)
                    {
                        string Data2 = Data; //Can't assign Data itself as it is part of the foreach loop
                        if (name == "Month" && Data.EndsWith(" "))
                        {
                            Data2 = Data.Remove(Data.Length - 1);
                        }
                        //Data = Data.Replace(" ", "") ; } //Solves issue of Month data having a space at the end
                        GlobalVariables.DataArrayObject[count1, count2] = Data2;
                        count2++;
                    }
                }
                count2 = 1;
                // forgot to reset count2 and went out of bounds on file 5? why not file two if the count was adding on from 600, the array size. Further testing needed.
                // Console.WriteLine("Got File"); //count1++;
            }
            //Console.ReadLine();
            Console.Clear();
            //Console.WriteLine(GlobalVariables.DataArrayObject[columns-1,rows-1]);
            //Console.ReadLine();
            DisplayAll(ref columns, ref rows);
            ArrayChoice(ref columns, ref rows);//ref Data1);
            return DataChoice;
        }

        public static void DisplayAll(ref int columns, ref int rows) {
            int height = Console.LargestWindowHeight;
            int width = Console.LargestWindowWidth;
            Console.SetWindowSize(width, height);
            Console.BufferHeight = 1500;
            for (int i = 0; i < rows; i++)
            { //foreach (string disp in GlobalVariables.DataArray) {Console.Write(disp);}
                for (int j = 0; j < columns; j++) {
                    if (j != 10)
                    {
                        Console.Write("{0, 15}" + "|", GlobalVariables.DataArrayObject[j, i]);
                    }
                    else
                    {
                        Console.Write("{0, 30}" + "|", GlobalVariables.DataArrayObject[j, i]);
                    }

                }
                Console.Write("\r\n");
                // Console.WriteLine("..............................................................................");
                //Console.WriteLine(i);
            }
            Console.ReadLine();
            Console.Clear();
            Console.SetWindowSize((width / 2), (height / 2));

        }

        public static void ArrayChoice(ref int columns, ref int rows)//ref string[] NameList)
        {
            bool Exit = false;
            while (Exit == false)
            {
                Console.WriteLine("Please choose one of the following data pieces to use:");
                for (int x = 0; x < columns; x++)
                {
                    Console.WriteLine("{0}. {1}", x, GlobalVariables.DataArrayObject[x, 0]);
                }
                Console.WriteLine("{0}. Exit", (columns)); //Means if another column of data is added Later on it will still work
                string Choice = Console.ReadLine();
                int ChoiceNum = -1;
                bool result = Int32.TryParse(Choice, out ChoiceNum);
                Console.WriteLine(ChoiceNum);
                if (result == true && ChoiceNum >= 0 && ChoiceNum <= columns)
                {
                    //Console.WriteLine("Converted '{0}' to {1}.", Choice, ChoiceNum);
                    if (ChoiceNum < columns) { SearchSortChoice(ref ChoiceNum, ref columns, ref rows); }
                    else { Exit = true; }
                }
                else
                {
                    Console.WriteLine("You have not entered a correct value, try again");
                }
                //int choice = Convert.ToInt32(Console.ReadLine());
            }
        }

        public static void SearchSortChoice(ref int ColumnChosen, ref int columns, ref int rows)
        {
            bool SeSoExit = true;
            while (SeSoExit == true)
            {
                Console.WriteLine("Please enter the numerical value depending on whether you wish to sort or Search the {0} Data Set:", GlobalVariables.DataArrayObject[ColumnChosen, 0]);
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Sort");
                Console.WriteLine("3. Exit");
                string SeSoChoice = Convert.ToString(Console.ReadLine());
                Console.WriteLine();
                if (SeSoChoice == "1" || SeSoChoice == "one" || SeSoChoice == "One" || SeSoChoice == "ONE")
                {
                    LinearSearch(ref ColumnChosen, ref columns, ref rows);
                }
                else if (SeSoChoice == "2" || SeSoChoice == "two" || SeSoChoice == "Two" || SeSoChoice == "TWO")
                {
                    if (Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, 0]) == "Month") {
                        bool intConv = false;
                        GlobalVariables.Months.Add("01", "January");
                        GlobalVariables.Months.Add("02", "February");
                        GlobalVariables.Months.Add("03", "March");
                        GlobalVariables.Months.Add("04", "April");
                        GlobalVariables.Months.Add("05", "May");
                        GlobalVariables.Months.Add("06", "June");
                        GlobalVariables.Months.Add("07", "July");
                        GlobalVariables.Months.Add("08", "August");
                        GlobalVariables.Months.Add("09", "September");
                        GlobalVariables.Months.Add("10", "October");
                        GlobalVariables.Months.Add("11", "November");
                        GlobalVariables.Months.Add("12", "December");
                        MonthSort(ref ColumnChosen, ref columns, ref rows, ref intConv);
                        QuickSort(ref ColumnChosen, ref columns, ref rows);
                        MonthSort(ref ColumnChosen, ref columns, ref rows, ref intConv);
                        DisplayAll(ref columns, ref rows);
                        MaxMin(ref columns, ref rows);

                    }
                    else
                    {
                        QuickSort(ref ColumnChosen, ref columns, ref rows);
                        Console.WriteLine();
                        DisplayAll(ref columns, ref rows);
                        MaxMin(ref columns, ref rows);
                    }
                }
                else if (SeSoChoice == "3" || SeSoChoice == "three" || SeSoChoice == "Three" || SeSoChoice == "THREE")
                {
                    SeSoExit = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a correct numerical value");
                }
            }
        }
        public static void MonthSort(ref int ColumnChosen, ref int columns, ref int rows, ref bool intConv) {
            if (intConv == false)
            {
                for (int h = 1; h < rows; h++)
                {
                    foreach (KeyValuePair<string, string> Mon in GlobalVariables.Months)
                    {
                        if (Mon.Value == Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, h]))
                        {

                            GlobalVariables.DataArrayObject[ColumnChosen, h] = Mon.Key;

                        }
                    }
                }
                intConv = true;
            }
            else if (intConv == true) {
                for (int h = 1; h < rows; h++)
                {
                    foreach (KeyValuePair<string, string> Mon in GlobalVariables.Months)
                    {
                        if (Mon.Key == Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, h]))
                        {

                            GlobalVariables.DataArrayObject[ColumnChosen, h] = Mon.Value;

                        }
                    }
                }
                intConv = false;
            }
        }


        public static int typing(ref int ColumnChosen) {
            int type = 0;
            Type ArrayType = GlobalVariables.DataArrayObject[ColumnChosen, 1].GetType(); // Check type of first proper value in the column of the array
            //Console.WriteLine(GlobalVariables.DataArrayObject[ColumnChosen, 1].GetType());
            if (ArrayType.Name == "Int32") { type = 1; }
            else if (ArrayType.Name == "Double") { type = 2; }
            else if (ArrayType.Name == "String") { type = 3; }
            else { Console.WriteLine("Error, type is {0}", ArrayType.Name); Console.ReadLine(); }
            return type;
        }
        public static void QuickSort(ref int ColumnChosen, ref int columns, ref int rows)
        {
            int VarType = typing(ref ColumnChosen);
            //Console.WriteLine(VarType);
            //Console.ReadLine();
            if (VarType == 1)
            {
                Quick_Sort(ref ColumnChosen, 1, rows - 1, columns);
            }
            else if (VarType == 2)
            {
                Quick_SortD(ref ColumnChosen, 1, rows - 1, columns);
            }
            else if (VarType == 3) {
                Quick_SortS(ref ColumnChosen, 1, rows - 1, columns);
            }
            
        }
        
        public static void Quick_Sort(ref int ColumnChosen, int Left, int Right, int columns)
        {
            int i = Left, j = Right; //i starts at 0, j at 600
            int pivot = Convert.ToInt32(GlobalVariables.DataArrayObject[ColumnChosen, (Left + Right) / 2]); //The Pivot, value at the middle of the array

            while (i <= j)
            {
                while (Convert.ToInt32(GlobalVariables.DataArrayObject[ColumnChosen, (i)]).CompareTo(pivot) < 0) //while 
                {
                    i++;
                }

                while (Convert.ToInt32(GlobalVariables.DataArrayObject[ColumnChosen, (j)]).CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    for (int t = 0; t < columns; t++)
                    {
                        var tmp = GlobalVariables.DataArrayObject[t, (i)];
                        GlobalVariables.DataArrayObject[t, (i)] = GlobalVariables.DataArrayObject[t, (j)];
                        GlobalVariables.DataArrayObject[t, (j)] = tmp;
                    }
                    i++;
                    j--;
                }
            }
            // Recursive calls
            if (Left < j)
            {
                Quick_Sort(ref ColumnChosen, Left, j, columns);
            }

            if (i < Right)
            {
                Quick_Sort(ref ColumnChosen, i, Right, columns);
            }
        }
        public static void Quick_SortD(ref int ColumnChosen, int Left, int Right, int columns)
        {
            int i = Left, j = Right;
            double pivot = Convert.ToDouble(GlobalVariables.DataArrayObject[ColumnChosen, (Left + Right) / 2]); //The Pivot, value at the middle of the array
            while (i <= j)
            {
                while (Convert.ToDouble(GlobalVariables.DataArrayObject[ColumnChosen, (i)]).CompareTo(pivot) < 0) { i++; }
                while (Convert.ToDouble(GlobalVariables.DataArrayObject[ColumnChosen, (j)]).CompareTo(pivot) > 0) { j--; }
                if (i <= j)
                {
                    for (int t = 0; t < columns; t++)
                    {
                        var tmp = GlobalVariables.DataArrayObject[t, (i)];
                        GlobalVariables.DataArrayObject[t, (i)] = GlobalVariables.DataArrayObject[t, (j)];
                        GlobalVariables.DataArrayObject[t, (j)] = tmp;
                    }
                    i++;
                    j--;
                }
            }
            if (Left < j)
            {
                Quick_SortD(ref ColumnChosen, Left, j, columns);
            }
            if (i < Right)
            {
                Quick_SortD(ref ColumnChosen, i, Right, columns);
            }
        }
        public static void Quick_SortS(ref int ColumnChosen, int Left, int Right, int columns)
        {
            int i = Left, j = Right;
            string pivot = Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, (Left + Right) / 2]); //The Pivot, value at the middle of the array
            while (i <= j)
            {
                while (Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, (i)]).CompareTo(pivot) < 0) { i++; }
                while (Convert.ToString(GlobalVariables.DataArrayObject[ColumnChosen, (j)]).CompareTo(pivot) > 0) { j--; }
                if (i <= j)
                {
                    for (int t = 0; t < columns; t++)
                    {
                        var tmp = GlobalVariables.DataArrayObject[t, (i)];
                        GlobalVariables.DataArrayObject[t, (i)] = GlobalVariables.DataArrayObject[t, (j)];
                        GlobalVariables.DataArrayObject[t, (j)] = tmp;
                    }
                    i++;
                    j--;
                }
            }
            if (Left < j)
            {
                Quick_SortS(ref ColumnChosen, Left, j, columns);
            }
            if (i < Right)
            {
                Quick_SortS(ref ColumnChosen, i, Right, columns);
            }
        }
        public static void MaxMin(ref int columns, ref int rows) {
            Console.Clear();
            bool maxminchosen = false;
            while (maxminchosen == false)
            {
                Console.WriteLine("Do you wish to see the Maximum and minimum values of this data? Y/N");
                string choice = Console.ReadLine();
                if (choice == "YES" || choice == "Y" || choice == "Yes" || choice == "yes" || choice == "y")
                {
                    int height = Console.LargestWindowHeight;
                    int width = Console.LargestWindowWidth;
                    Console.SetWindowSize(width, height);
                    for (int i = 0; i < 2; i++)
                    { 
                        for (int j = 0; j < columns; j++)
                        {
                            if (j != 10)
                            {
                                Console.Write("{0, 15}" + "|", GlobalVariables.DataArrayObject[j, i]);
                            }
                            else
                            {
                                Console.Write("{0, 30}" + "|", GlobalVariables.DataArrayObject[j, i]);
                            }

                        }
                        Console.WriteLine("\n");
                    }
                    
                        for (int j = 0; j < columns; j++)
                        {
                            if (j != 10)
                            {
                                Console.Write("{0, 15}" + "|", GlobalVariables.DataArrayObject[j, rows - 1]);
                            }
                            else
                            {
                                Console.Write("{0, 30}" + "|", GlobalVariables.DataArrayObject[j, rows - 1]);
                            }

                        }
                    Console.WriteLine("\n");
                    Console.ReadLine();
                    Console.Clear();
                    Console.SetWindowSize((width / 2), (height / 2));
                    maxminchosen = true;

                
            }
                else if (choice == "NO" || choice == "N" || choice == "No" || choice == "no" || choice == "n") { maxminchosen = true; }
                else { Console.WriteLine("Please enter Yes or No"); }
            }
        }
        public static void BubbleSort(ref int ColumnChosen, ref int columns, ref int rows) { }
        public static void InsertionSort(ref int ColumnChosen, ref int columns, ref int rows) { }
        public static void MergeSort(ref int ColumnChosen, ref int columns, ref int rows) { }
        public static void BinarySearch(ref int ColumnChosen, ref int columns, ref int rows) { }
        public static void LinearSearch(ref int ColumnChosen, ref int columns, ref int rows) {
            //Type ArrayType = GlobalVariables.DataArrayObject[ColumnChosen, 1].GetType();
            //Console.WriteLine(ArrayType);
            bool Found = false;
            int height = Console.LargestWindowHeight;
            int width = Console.LargestWindowWidth;
            int Max = rows;
            int count = 0;
            int CurrentElement = 1;
            string ArrayString;
            Console.WriteLine("What Would you like to search for?");
            string ElementSought = Console.ReadLine();
            Console.WriteLine();
            Console.SetWindowSize(width, height);
            for (int i = 0; i< columns; i++) { Console.Write("{0, 15}" + "|", GlobalVariables.DataArrayObject[i, 0]); }
            Console.Write("\r\n");
            while (CurrentElement < Max)
            {
                ArrayString = Convert.ToString( GlobalVariables.DataArrayObject[ColumnChosen, CurrentElement]);
               // Console.WriteLine(GlobalVariables.DataArrayObject[ColumnChosen, CurrentElement]);
                if (ArrayString == ElementSought)
                {
                    for (int j = 0; j < columns; j++)
                        {
                        Console.Write("{0, 15}" + "|", GlobalVariables.DataArrayObject[j, CurrentElement]);
                        }
                    Console.Write("\r\n");
                    //Console.WriteLine();

                    Found = true;
                    count++;
                    CurrentElement += 1;
                }
                else
                {
                    CurrentElement += 1;
                }
            }
            if (Found == true){}
            else
            {
                //Console.WriteLine(GlobalVariables.DataArrayObject[ColumnChosen, CurrentElement]);
                Console.WriteLine("The element could not be found in that Array");
            }
            Console.WriteLine("Counter = {0}", count);
            Console.ReadLine();
            Console.SetWindowSize((width / 2), (height / 2));
        }


        
        private static DataTable getTable(ref string DChoice) // Was unsure whether it was ok to use a datatable and opted to not use it and instead use an array
        {
            DataTable table1 = new DataTable();
            string[] Data1 = Directory.GetFiles(DChoice);
            foreach (string info in Data1)
            {
                string text = File.ReadAllText(info);
                string[] DataSet = Regex.Split(text, "\r\n");
                string FullName = Path.GetFileName(info);
                string name = Path.GetFileNameWithoutExtension(FullName);
                name = name.Remove(name.Length - 2);
                if (name == "Day" || name == "Depth" || name == "IRIS_ID" || name == "Timestamp" || name == "Year") { table1.Columns.Add(name, typeof(int)); }
                else if (name == "Latitude" || name == "Longitude" || name == "Magnitude") { table1.Columns.Add(name, typeof(double)); }
                else if (name == "Month" || name == "Region" || name == "Time") { table1.Columns.Add(name, typeof(string)); }
                else { Console.WriteLine("Error in Names, file name is {0}", name); }
            }
            //dataTable.Columns["Qty"].SetOrdinal(0);
            return table1;
        }

        //For the month sorting give each Month a value, give them a value of powers of 100. Don't even need to compare different years just those in the same year. could use all parts of the date as a number by giving 
        //diferent increments of 0. Convert it to a universal time code. Year/Month/Day as one number
        // Can you get the location of items in an array e.g. (3,4). Could help with sorting all files in line with the one being sorted
        // e.g if you sort by years all the other files follow suit.
        //How much autonomy does the code need? As in am I coding for the data I have or for future data that might be put into the code.
    }
}










//1. Select which individual Array or String Array is to be analysed.

//2. Sort in ascending or descending order and display the selected Array or String Array.

//3. Search according to the content of the Year_1, Month_1, Day_1, Time_1, Magnitude_1,
//Latitude_1, Longitude_1, Depth_1, Region_1, IRIS_ID_1, and Timestamp_1. If the value of the
//Date does not exist then provide an error message.

//4. Search according to the content of the Month, and display ALL the corresponding values of
//Year_1, Month_1, Day_1, Time_1, Magnitude_1, Latitude_1, Longitude_1, Depth_1,
//Region_1, IRIS_ID_1, and Timestamp_1. If the value of the Date does not exist then provide an
//error message.

//5. When sorting in ascending or descending order the Month file you should display the months as
//they appear during the calendar year (i.e. in ascending order they should be: January, February,
//March, April, May, June, July, August, September, October, November, December), not as they
//appear in Month file.

//6. Find the minimum and maximum values of Year_1, Month_1, Day_1, Time_1, Magnitude_1,
//Latitude_1, Longitude_1, Depth_1, Region_1, IRIS_ID_1, and Timestamp_1. You should then
//display ALL the corresponding values of Year_1, Month_1, Day_1, Time_1, Magnitude_1,
//Latitude_1, Longitude_1, Depth_1, Region_1, IRIS_ID_1, and Timestamp_1.

//7. Your Console Application should be in position to rearrange and display the content of all the
//other Arrays and String Arrays in respect to the one that is sorted.

//8. For additional marks, your Console Application should be in position to input the files
//Year_2.txt, Month_2.txt, Day_2.txt, Time_2.txt, Magnitude_2.txt, Latitude_2.txt,
//Longitude_2.txt, Depth_2.txt, Region_2.txt, IRIS_ID_2.txt, and Timestamp_2.txt.Then Repeat
//Tasks 1 to 7 and display the corresponding values for both regions.

//9. For additional marks, Merge the Year_1.txt, Month_1.txt, Day_1.txt, Time_1.txt,
//Magnitude_1.txt, Latitude_1.txt, Longitude_1.txt, Depth_1.txt, Region_1.txt, IRIS_ID_1.txt,
//Timestamp_1.txt, Year_2.txt, Month_2.txt, Day_2.txt, Time_2.txt, Magnitude_2.txt,
//Latitude_2.txt, Longitude_2.txt, Depth_2.txt, Region_2.txt, IRIS_ID_2.txt, and
//Timestamp_2.txt, according to the Year, Month, Day, and Time files.Then Repeat Tasks 1 to
//7 and display the corresponding values.





