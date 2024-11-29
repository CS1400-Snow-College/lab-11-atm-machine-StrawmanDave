// David Barlow 11/25/24 Lab-11 ATM machine

string[] DataBase = File.ReadAllLines("bank.txt");

Console.WriteLine("Please enter your username.");
string getUsername = Console.ReadLine();
string name = "";

string SaveData = "";
string subData = "";

string welcomeMenu = @$"
Welcome {name} what would you like to do?
1)Check Balance
2)Withdraw
3)Deposit
4)Display last transactions
5)Quick Withdraw $40
6)Quick Widthraw $100
7)End current session
Please pick the number corresponding to the option.
";

for(int i = 0; i<DataBase.Length; i++)
{
    string[] splitLines = DataBase[i].Split(",");
    for(int j = 0; j<splitLines.Length; j++)
    {
        if(getUsername == splitLines[j])
        {
            Console.WriteLine("What is your Pin?");
            string getPin = Console.ReadLine();
            if(getPin == splitLines[j+1])
            {
                Console.Clear();
                name = splitLines[j];
                welcomeMenu = @$"
Welcome {name} what would you like to do?
1)Check Balance
2)Withdraw
3)Deposit
4)Display last transactions
5)Quick Withdraw $40
6)Quick Widthraw $100
7)End current session
Please pick the number corresponding to the option.
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
                                Console.WriteLine($"Your current balance stands at {splitLines[j + 2]}");
                                break;
                            case 2:
                                Console.WriteLine("How much would you like to withdraw?");
                                string dollarSign = splitLines[j+2].Substring(0,1);
                                string stringBalance = splitLines[j+2].Substring(1);
                                decimal getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                splitLines[j+2] = dollarSign + decimalToString(Withdraw(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {splitLines[j+2]}");
                                Console.WriteLine("Account updated what next?");
                                break;
                            case 3:
                                Console.WriteLine("How much would you like to deposit?");
                                stringBalance = splitLines[j+2].Substring(1);
                                dollarSign = splitLines[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Deposit(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                splitLines[j+2] = dollarSign + decimalToString(Deposit(ReadDecimal(Console.ReadLine()), getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {splitLines[j+2]}");
                                Console.WriteLine("Account updated what next?");
                                break;
                            case 4:
                                foreach(string transaction in lastTransactions)
                                {
                                    Console.WriteLine(transaction);
                                }
                                break;
                            case 5:
                                stringBalance = splitLines[j+2].Substring(1);
                                dollarSign = splitLines[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(40,getCurrentBalance));
                                splitLines[j+2] = dollarSign + decimalToString(Withdraw(40, getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {splitLines[j+2]}");
                                Console.WriteLine("Account updated what next?");
                                break;
                            case 6:
                                stringBalance = splitLines[j+2].Substring(1);
                                dollarSign = splitLines[j+2].Substring(0,1);
                                getCurrentBalance = ReadDecimal(stringBalance);
                                //Console.WriteLine(Withdraw(100,getCurrentBalance));
                                splitLines[j+2] = dollarSign + decimalToString(Withdraw(100, getCurrentBalance));
                                lastTransactions.Enqueue($"{dollarSign}{stringBalance} to {splitLines[j+2]}");
                                Console.WriteLine("Account updated what next?");
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
                        Console.WriteLine("Sorry that was not an option or your input did not register");
                        Console.WriteLine(welcomeMenu);
                    }
                }while(exit == false);
            }else
            {
                Console.WriteLine("Incorrect pin please try again");
            }
        }
        subData = subData + "," + splitLines[j];
    }
    subData = subData.Substring(1);
    SaveData = SaveData + subData + '\n';
    subData = "";
}


//Console.WriteLine();
//Console.WriteLine(SaveData);
File.WriteAllText("bank.txt", SaveData);


decimal Withdraw (decimal amountWithdraw, decimal currentBalance)
{
    try
    {
        if(currentBalance < amountWithdraw)
        {
            throw new ArgumentException("You do not have enough funds to do that.");
        }

        if(amountWithdraw < 0)
        {
            throw new FormatException();
        }
        decimal newBalance = currentBalance - amountWithdraw;
        return newBalance;
    }catch(ArgumentException e)
    {

        Console.WriteLine(e.Message);
        Console.WriteLine(welcomeMenu);
        return currentBalance;
    }
}

decimal Deposit(decimal amountDeposit, decimal currentBalance)
{
    try
    {
        if(amountDeposit < 0)
        {
            throw new ArgumentException("That is a withdraw not a depoist");
        }

        decimal newBalance = currentBalance + amountDeposit;
        return newBalance;
    }catch(ArgumentException e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine(welcomeMenu);
        return currentBalance;
    }
}

decimal ReadDecimal(string getInput)
{
    decimal number = -1;
    do
    {
        number = Convert.ToDecimal(getInput);
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