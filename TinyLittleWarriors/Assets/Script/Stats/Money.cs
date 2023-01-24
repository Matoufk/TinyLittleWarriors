using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{

    public int startMoney;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
    }

    // Getters and Setters for the Money
    public int getMoney()
    {
        return money;
    }
    public void setMoney(int val)
    {
        money = val;
    }
    public void addMoney(int val)
    {
        money += val;
    }
    public void subMoney(int val)
    {
        money -= val;
    }

    // Try to buy something
    // This function will try to buy the object, will return True if the player can buy, or False, and if True, will substract the price from the money
    /*
    public bool buy(GameObject amelioration)
    {
        if(amelioration.getPrice() > money)
        {
            // So we don't have enough money to buy this object
            return false;
        }
        else
        {
            // We do, we return True and we substract price from money
            // WARNING : DO NOT SUBSTRACT MONEY TWICE
            subMoney(amelioration.getPrice());
            return true;
        }
    }
    */

    // Same function but with only the price, will Sub if True, and return True if possible, or False if money is not enough
    public bool buy(int price)
    {
        if (price > money)
        {
            // So we don't have enough money to buy this object
            return false;
        }
        else
        {
            // We do, we return True and we substract price from money
            // WARNING : DO NOT SUBSTRACT MONEY TWICE
            subMoney(price);
            return true;
        }
    }


}
