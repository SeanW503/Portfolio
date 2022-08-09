using System;
using System.Collections.Generic;

public static class Database
{
    public static string[,] dummyUsers = new string[5, 2];
    public static LinkedList<string[]> clients = new LinkedList<string[]>();
    public static void fillDatabase()
	{
        dummyUsers[0, 0] = "Bob";
        dummyUsers[0, 1] = "Spike123";
        dummyUsers[1, 0] = "Harold";
        dummyUsers[1, 1] = "reV456!";
        dummyUsers[2, 0] = "Cheryl";
        dummyUsers[2, 1] = "tbm93rtw";
        dummyUsers[3, 0] = "Francisco";
        dummyUsers[3, 1] = "66642069";
        dummyUsers[4, 0] = "HubertBlaineWolfeschlegelsteinhausenbergerdorff";
        dummyUsers[4, 1] = "hubert1";
    }

    public static String getUserName(int i)
    {
        return dummyUsers[i, 0];
    }

    public static String getUserPwd(int i)
    {
        return dummyUsers[i, 1];
    }
    /*public static void addUsers()
    {
        //including for the sake of it, no create account implemented
    }*/
    public static void addClient(string name, string email, string phonenumber, string EIN, string billingAddress, string shippingAddress)
    {
        string [] s = new string[6];
        s[0] = name;
        s[1] = email;
        s[2] = phonenumber;
        s[3] = EIN;
        s[4] = billingAddress;
        s[5] = shippingAddress;
        clients.AddLast(s);
    }

    public static String [] getClient(String name)
    {
        foreach(string[] s in clients)
        {
            if (s[1].Equals(name))
            {
                return s;
            }
        }
        return null;
    }
    
       
}


public class Clients
{
    public string name;
    public string email;
    public string phonenumber;
    public string EIN;
    public string billingAddress; 
    public string shippingAddress;

    public Clients(string name, string email, string phonenumber, string EIN, string billingAddress, string shippingAddress)
    {
        this.name = name;
        this.shippingAddress= shippingAddress;
        this.EIN = EIN; 
        this.billingAddress= billingAddress;
        this.phonenumber = phonenumber; 
        this.email = email;

        Database.addClient(name, email, phonenumber, EIN, billingAddress, shippingAddress);

    }
      


}

/*public class Users
{
    //unused
}*/