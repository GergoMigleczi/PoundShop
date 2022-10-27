using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PoundShop
{
    class PoundShop
    {
        static List<List<string>> purchases = new List<List<string>>();
        static List<string> purchase = new List<string>();

        //Read and store the data
        /* 
         The txt looks like this:
                pen
                paper
                rubber
                F
                pen
                pencil
                pencil
                pen
                F
                ..  
                ..
        The "F" separates the purchases
        Everything costs 500Ft
        If they buy more of the same item the second is 450Ft the third and beyond is 400Ft

         */
        static void Task1()
        {
            string item = "";
            StreamReader sr = new StreamReader("penztar.txt");

            while (!sr.EndOfStream)
            {
                item = sr.ReadLine();
                if (item != "F")
                {
                    purchase.Add(item);
                }
                else
                {
                    purchases.Add(purchase);
                    purchase = new List<string>();
                }
            }
            sr.Close();
        }

        //Print how many purchases were made
        static void Task2()
        {
            Console.WriteLine("Task 2");
            Console.WriteLine($"Number of purchases {purchases.Count}");
        }

        //Print how many items the first buyer bought
        static void Task3()
        {
            Console.WriteLine("Task 3");

            Console.WriteLine($"The first customer bought {purchases[0].Count} items.");
        }

        //Ask the user for information, store the answer and use it later on to complete the tasks
        static int purchaseNumber = 0;
        static string nameOfAnItem = "";
        static int quantityOfItem = 0;
        static void Task4()
        {
            Console.WriteLine("Task 4");

            Console.Write("Type in the number of the purchase (1st, 2nd etc...) = ");
            purchaseNumber = int.Parse(Console.ReadLine());
            Console.Write("Type in the name of the item = ");
            nameOfAnItem = Console.ReadLine();
            Console.Write("Type in the quantity of the item = ");
            quantityOfItem = int.Parse(Console.ReadLine());
        }

        //Print when the item given by the user was bought first, and last, and how many people have bought it
        static void Task5()
        {
            Console.WriteLine("Task 5");

            int firstPurchaseOfTheItem = 0;
            int lastPurchaseOfTheItem = 0;
            int numberOfCustomerWhoBoughtTheItem = 0;

            for (int i = 0; i < purchases.Count; i++)
            {
                for (int j = 0; j < purchases[i].Count; j++)
                {
                    if (purchases[i][j] == nameOfAnItem)
                    {
                        firstPurchaseOfTheItem = i + 1;
                        goto here;
                    }
                }
            }
        here:
            for (int i = 0; i < purchases.Count; i++)
            {
                bool vettek = false;
                for (int j = 0; j < purchases[i].Count; j++)
                {
                    if (purchases[i][j] == nameOfAnItem)
                    {
                        lastPurchaseOfTheItem = i + 1;
                        vettek = true;
                    }
                }
                if (vettek)
                    numberOfCustomerWhoBoughtTheItem++;
            }
            Console.WriteLine($"The customer who bought the item for the first time: {firstPurchaseOfTheItem}");
            Console.WriteLine($"The customer who bought the item for the last time: {lastPurchaseOfTheItem}");
            Console.WriteLine($"{numberOfCustomerWhoBoughtTheItem} Customer bought the item");
        }

        //Calculate how much it would cost to buy an item as many times as the user said quantityOfItem
        //Make a method that does the calculation
        static int price(int quantityOfItem)
        {
            int price = 0;
            if (quantityOfItem == 1)
            {
                price = 500; //one itme
            }
            else if (quantityOfItem == 2)
            {
                price = 950; //two items
            }
            else
            {
                price = 950 + (quantityOfItem - 2) * 400; //more than two items
            }
            return price;
        }
        static void Task6()
        {
            Console.WriteLine("Task 6");
            Console.WriteLine($"For {quantityOfItem} items you would pay {price(quantityOfItem)}");
        }

        //The user typed in a purchise number
        //Print which items, and how many of them were bought at that purchase
        static void Task7()
        {
            Console.WriteLine("Task 7");

            List<string> Items = new List<string>();
            for (int i = 0; i < purchases[purchaseNumber - 1].Count; i++)
            {
                if (!Items.Contains(purchases[purchaseNumber - 1][i]))
                    Items.Add(purchases[purchaseNumber - 1][i]);
            }

            foreach (string item in Items)
            {
                int numberOfCustomerWhoBoughtTheItem = 0;
                for (int i = 0; i < purchases[purchaseNumber - 1].Count; i++)
                {
                    if (item == purchases[purchaseNumber - 1][i])
                        numberOfCustomerWhoBoughtTheItem++;
                }
                Console.WriteLine($"{numberOfCustomerWhoBoughtTheItem} {item}");
            }

        }

        //Print the cost of each purchase into the osszeg.txt file
        static void Task8()
        {
            StreamWriter sw = new StreamWriter("osszeg.txt");

            List<string> itemsOfAPurchase = new List<string>();
            for (int j = 0; j < purchases.Count; j++)
            {
                int cost = 0;

                for (int i = 0; i < purchases[j].Count; i++)
                {
                    if (!itemsOfAPurchase.Contains(purchases[j][i]))
                        itemsOfAPurchase.Add(purchases[j][i]);
                }

                foreach (string item in itemsOfAPurchase)
                {
                    int numberOfCustomerWhoBoughtTheItem = 0;
                    for (int i = 0; i < purchases[j].Count; i++)
                    {
                        if (item == purchases[j][i])
                            numberOfCustomerWhoBoughtTheItem++;
                    }
                    //Console.WriteLine($"{numberOfCustomerWhoBoughtTheItem} {item} {price(numberOfCustomerWhoBoughtTheItem)}");
                    cost += price(numberOfCustomerWhoBoughtTheItem);
                }
                itemsOfAPurchase = new List<string>();
                //Console.WriteLine(cost);
                sw.WriteLine($"{j + 1}: {cost}");
            }


            sw.Flush();
            sw.Close();

        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Task7();
            Task8();
            Console.ReadKey();
        }
    }
}
