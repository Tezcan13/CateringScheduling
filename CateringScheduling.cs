using System;
using System.Threading;
using System.Collections.Generic;

public class CateringScheduling
{
	public static int borek = 30;	//Total borek   
	public static int drink = 30;   //Total drink                   
	public static int cake = 15;    //Total cake                 
	public static int borekTray = 5;	//Borek on tray                  
	public static int drinkTray = 5;    //Drink on tray               
	public static int cakeTray = 5;     //Cake on tray        
	static List<Guest> guests = new List<Guest>(); //List of guests
	
	public static void Main()
	{
		Thread waiterThread = new Thread(WaiterAct); //Waiter Thread (WaiterAct)
		waiterThread.Start();
		
		for (int i = 0; i < 10; i++)
			guests.Add(new Guest(i)); //Creating guests and adding them to the list.  We will use this list to check whether guests take food or not
		
		for (int i = 0; i < 10; i++)  //Creating threads. 1 thread for every guest and all of them executing on the function (GuestsAct) simultaneously
		{
			Thread guestThread = new Thread(GuestsAct);
			guestThread.Start(i);
		}
		Console.ReadKey();  //Press any key to continue...
	}

	static void WaiterAct() //Waiter act function
	{
		bool x = true;  
		while (x) //Return untill the all foods consumed
		{
			if (borekTray < 2 && borek > 0) //If there are less than 2 boreks in borek tray, and if there are some boreks still can be added, add.
			{
				int amount = 5 - borekTray; // The amount of borek to be filled
				amount = amount > borek ? borek : amount; // If the amount of boreks to be filled is more than the borek we have, amount = all boreks that we have, if less, continue as amount
				borekTray += amount; // Increase the boreks on tray
				borek -= amount; // Decrease the boreks that we have
				Console.WriteLine("Waiter brings " + amount + " borek.");
			}
			 else if (cakeTray < 2 && cake > 0)	//If there are less than 2 cakes in cake tray, and if there are some cakes still can be added, add.                
			{
				int amount = 5 - cakeTray;	//The amount of cake to be filled
				amount = amount > cake ? cake : amount;	//If the amount of cakes to be filled is more than the cake we have, amount = all cakes that we have, if less, continue as amount
				cakeTray += amount;	//Increase the cakes on tray
				cake -= amount;	//Decrease the cakes that we have
				Console.WriteLine("Waiter brings " + amount + " cake.");
			}
			 else if (drinkTray < 2 && drink >0)	//If there are less than 2 drinks in drink tray, and if there are some drinks still can be added, add.
			{
				int amount = 5 - drinkTray;	//The amount of cake to be filled
				amount = amount > drink ? drink : amount;	//If the amount of drinks to be filled is more than the drink we have, amount = all drinks that we have, if less, continue as amount
				drinkTray += amount;	//Increase the drinks on tray
				drink -= amount;	//Decrease the drinks that we have
				Console.WriteLine("Waiter brings " + amount + " drink.");
			}

			if (borek <= 0 && cake <= 0 && drink <= 0 && borekTray<=0 && cakeTray<=0 && drinkTray<=0) //If our all foods and drinks consumed, do x=false and finish the loop
				x = false;
			
			Thread.Sleep(getSleepRandomNumber());  //Sleep command, it generates random number for sleep

		}
	}

	static void GuestsAct(object guestID)  //It's parameter is guest's ID. Do acts according to the ID of each guests
	{
		Thread.Sleep(getSleepRandomNumber());	//Sleep command
		int id = (int)guestID;	//Convert the object to int type.
		Guest guest = guests[id];	//Taking the guest if id = guestID
		bool x = true;
		while (x)
		{
			int whichTray = getRandomNumber(3);	//It shows which tray the guest will go to randomly. getRandomNumber(3) => Generates random number max 3. 0=>borek, 1=>cake, 2=>drink  
			if (whichTray == 0 && guest.borek < 5 && borekTray > 0)  //If generated number is 0, and the guest has less than 5 boreks and if waiter has boreks can add
			{
				bool check = true;
				if (!isTotalBorek() && (borek+borekTray) < 10)  //If everyone has at least 1 borek returns true. If it is false, and total boreks less than 10
				{  //If everyone didn't get borek and there are less or equal than 10 boreks, and if this guest has borek, this guest shouldn't get one more borek
					if (guest.borek > 0) //Check whether the guest has borek, if has, returns false
						check = false;
				}

				if (check) //Check if true
				{                                                                  
					guest.borek++; //Increase this guest's borek
					borekTray--; //Decrease the amount of borek in tray
					Console.WriteLine("Guest " + guest.id + " took a borek. Guest ate " + guest.borek + " borek, " + guest.cake + " cake, " + guest.drink + " drink.       " + borekTray + " Borek, " + cakeTray + " cake, " + drinkTray + " drink on the trays.");
				}
			}
			else if (whichTray == 1 && guest.cake < 2 && cakeTray > 0) //If generated number is 1, and the guest has less than 2 cakes and if waiter has cakes can add
			{
				bool check = true;
				if (!isTotalCake() && (cake+cakeTray) < 10)	//If everyone has at least 1 cake returns true. If it is false, and total cakes less than 10
				{	//If everyone didn't get cake and there are less or equal than 10 cakes, and if this guest has cake, this guest shouldn't get one more cake
					if (guest.cake > 0)	//Check whether the guest has cake, if has, returns false
						check = false;
				}

				if (check)
				{
					guest.cake++;	//Increase this guest's cake
					cakeTray--;	 //Decrease the amount of cake in tray
					Console.WriteLine("Guest " + guest.id + " took cake. Guest ate " + guest.borek + " borek, " + guest.cake + " cake, " + guest.drink + " drink.       " + borekTray + " Borek, " + cakeTray + " cake, " + drinkTray + " drink on the trays.");
				}
			}
			else if (whichTray == 2 && guest.drink < 5 && drinkTray > 0)	//If generated number is 2, and the guest has less than 5 drink and if waiter has drinks can add
			{
				bool check = true;
				if (!isTotalDrink() && (drink+drinkTray) < 10)	//If everyone has at least 1 drink returns true. If it is false, and total drinks less than 10
				{
					if (guest.drink > 0) //Check whether the guest has drink, if has, returns false
						check = false;
				}

				if (check)
				{
					guest.drink++;	//Increase this guest's drink
					drinkTray--;	//Decrease the amount of drink in tray
					Console.WriteLine("Guest " + guest.id + " took drink. Guest ate " + guest.borek + " borek, " + guest.cake + " cake, " + guest.drink + " drink.       " + borekTray + " Borek, " + cakeTray + " cake, " + drinkTray + " drink on the trays.");
				}
			}

			if (borek <= 0 && cake <= 0 && drink <= 0 && borekTray<=0 && cakeTray<=0 && drinkTray<=0)	//If there is not any food or dirnk do x=false and finish the while loop
				x = false;

			Thread.Sleep(getSleepRandomNumber());	//Sleep command
		}
	}

	static int getRandomNumber(int max) //Generates random number depends on max
	{
		Random random = new Random();
		return random.Next(1000) % max;
	}

	static int getSleepRandomNumber()	//The function generates random number for sleep.
	{
		Random random = new Random();
		return random.Next(100) + 100;
	}

	static bool isTotalBorek()	//Check everybody has a borek, if has, x=true, if not x=false and returns x
	{
		bool x = true;
		for (int i = 0; i < 10; i++)
			if (guests[i].borek == 0)
				x = false;
		return x;                               
	}

	static bool isTotalCake()	//Check everybody has a cake, if has, x=true, if not x=false and returns x
	{
		bool x = true;
		for (int i = 0; i < 10; i++)
			if (guests[i].cake == 0)
				x = false;
		return x;
	}

	static bool isTotalDrink()	//Check everybody has a drink, if has, x=true, if not x=false and returns x
	{
		bool x = true;
		for (int i = 0; i < 10; i++)
			if (guests[i].drink == 0)
				x = false;
		return x;
	}
}

public class Guest	//Guest object
{
	public int id;                                         
	public int borek;                          
	public int drink;
	public int cake;
	
	public Guest(int id)
	{
		this.id = id;
	}
}