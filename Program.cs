// David Barlow 11/25/24 Lab-11 ATM machine

string[] DataBase = File.ReadAllLines("bank.txt");

Console.WriteLine("Please enter your username.");
string getUsername = Console.ReadLine();


for(int i = 0; i<DataBase.Length; i++)
{
    string[] splitLines = DataBase[i].Split(",");
    List<string> Split = splitLines.ToList();
    for(int j = 0; j<Split.Count; j++)
    {
        if(getUsername == Split[j])
        {
            Console.WriteLine("What is your Pin?");
            string getPin = Console.ReadLine();
            if(getPin == Split[j+1])
            {
                Console.Clear();
                string welcomeMenu = @$"
                Welcome {Split[j]} what would you like to do?

                1)Check Balance
                2)Withdraw
                3)Deposit
                4)Display last transactions
                5)Quick Withdraw $40
                6)Quick Widthraw $100
                7)End current session

                Please pick the number correspoinding to the option.
                ";

                Console.WriteLine(welcomeMenu);
                Queue<string> lastTransactions = new Queue<string>();
                bool exit = false;
                do
                {
                    
                    if(lastTransactions.Count > 5)
                    {
                        lastTransactions.Dequeue();
                    }
                    try
                    {
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case 1:
                                Console.WriteLine($"Your current balance stands at {Split[j + 2]}");
                                break;
                            case 2:
                                Console.WriteLine("How much would you like to withdraw?");
                                string dollarSign = Split[j+2].Substring(0,1);
                                string stringBalance = Split[j+2].Substring(1);
                                decimal getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                Split[j+2] = dollarSign + decimalToString(Withdraw(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {Split[j+2]}");
                                break;
                            case 3:
                                Console.WriteLine("How much would you like to deposit?");
                                stringBalance = Split[j+2].Substring(1);
                                dollarSign = Split[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Deposit(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                Split[j+2] = decimalToString(Deposit(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {Split[j+2]}");
                                break;
                            case 4:
                                foreach(string transaction in lastTransactions)
                                {
                                    Console.WriteLine(transaction);
                                }
                                break;
                            case 5:
                                stringBalance = Split[j+2].Substring(1);
                                dollarSign = Split[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(40,getCurrentBalance));
                                Split[j+2] = dollarSign + decimalToString(Withdraw(40, getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {Split[j+2]}");
                                break;
                            case 6:
                                stringBalance = Split[j+2].Substring(1);
                                dollarSign = Split[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(100,getCurrentBalance));
                                Split[j+2] = dollarSign + decimalToString(Withdraw(100, getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {Split[j+2]}");
                                break;
                            case 7:
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Sorry that was not an option");
                                break;
                        }
                    }catch(FormatException)
                    {
                        Console.WriteLine("Sorry that was not an option");
                    }
                }while(exit == false);
            }else
            {
                Console.WriteLine("Incorrect pin please try again");
            }
        }
    }
}


decimal Withdraw (decimal amountWithdraw, decimal currentBalance)
{
    decimal newBalance = currentBalance - amountWithdraw;
    return newBalance;
}

decimal Deposit (decimal amountDeposit, decimal currentBalance)
{
    decimal newBalance = currentBalance + amountDeposit;
    return newBalance;
}

decimal ReadDecimal(string getInput)
{
    decimal number = -1;
    do
    {
        try
        {
            number = Convert.ToDecimal(getInput);
        }
        catch(ArgumentOutOfRangeException)
        {
            number = -1;
            Console.WriteLine("That does not register");
        }
    }while(number == -1);
    return number;
}

string decimalToString(decimal Input)
{
    string Output = "";
    try
    {
       Output = Input.ToString();
       return Output;
    }
    catch(Exception)
    {  
        Console.WriteLine("That can not be turned into a string.");
    }
    return Output;
}