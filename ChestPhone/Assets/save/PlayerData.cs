using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public bool music;
    public int actualChestNumber;
    public List<int> listTimer;
    public List<int> listIDItem;
    public List<int> listSlotItem;
    public List<int> listNumberItem;
    public int timerCustomer;
    public int timerCustomerMax;
    public int money;
    public DateTime timerLeave, timerLaunch;
    public List<int> listIDItemInShop;
    public List<int> listSlotItemInShop;
    public List<int> listNumberItemInShop;
    public List<int> listAmountInShop;

    public PlayerData(Player player)
    {
        this.music = player.music;
        this.actualChestNumber = player.actualChestNumber;
        this.listTimer = player.listTimer;
        this.listIDItem = player.listIDItem;
        this.listSlotItem = player.listSlotItem;
        this.listNumberItem = player.listNumberItem;
        this.money = player.money;
        this.timerLaunch = player.timerLaunch;
        this.timerLeave = player.timerLeave;
        this.timerCustomer = player.timerCustomer;
        this.timerCustomerMax = player.timerCustomerMax;
        this.listIDItemInShop = player.listIDItemInShop;
        this.listSlotItemInShop = player.listSlotItemInShop;
        this.listNumberItemInShop = player.listNumberItemInShop;
        this.listAmountInShop = player.listAmountInShop;
    }
    
}
