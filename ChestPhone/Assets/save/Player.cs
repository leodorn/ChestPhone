using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool music;
    public int actualChestNumber;
    public List<int> listTimer;
    public List<int> listIDItem;
    public List<int> listSlotItem;
    public List<int> listNumberItem;
    public int money;
    private bool leave;
    public int timerCustomer;
    public int timerCustomerMax;
    public DateTime timerLeave, timerLaunch;
    private TimeSpan difference;
    private int totalDifference;
    public List<int> listIDItemInShop;
    public List<int> listSlotItemInShop;
    public List<int> listNumberItemInShop;
    public List<int> listAmountInShop;
    
    //Destroy this code when the game is finish
    [SerializeField]
    bool load; 
    private void Awake()
    {
        if(load)
        {
            LoadPlayer();
            //To calculate the timer even when the player leave the game
            timerLaunch = DateTime.Now;            
            difference = timerLaunch.Subtract(timerLeave);
            totalDifference = (int)difference.TotalSeconds;
            for(int i=0;i<listTimer.Count;i++)
            {
                listTimer[i] -= totalDifference;
                if(listTimer[i]<0)
                {
                    listTimer[i] = 0;
                }
            }
            timerCustomer -= totalDifference;
            if(timerCustomer < 0)
            {
                timerCustomer = 0;
            }
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SavePlayer();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            LoadPlayer();
        }

    }

    private void SavePlayer()
    {
        SaveSystem.savePlayer(this);
    }

    private void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        this.music = data.music;
        this.actualChestNumber = data.actualChestNumber;
        this.listTimer = data.listTimer;
        this.listIDItem = data.listIDItem;
        this.listNumberItem = data.listNumberItem;
        this.listSlotItem = data.listSlotItem;
        this.money = data.money;
        this.timerLaunch = data.timerLaunch;
        this.timerLeave = data.timerLeave;
        this.timerCustomer = data.timerCustomer;
        this.timerCustomerMax = data.timerCustomerMax;
        this.listIDItemInShop  = data.listIDItemInShop;
        this.listSlotItemInShop = data.listSlotItemInShop;
        this.listNumberItemInShop = data.listNumberItemInShop;
        this.listAmountInShop = data.listAmountInShop;
}
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            timerLeave = DateTime.Now;
            leave = true;
            SavePlayer();
        }
    }

    private void OnApplicationQuit()
    {
        timerLeave = DateTime.Now;
        leave = true;
        SavePlayer();
    }

    private void OnApplicationFocus(bool focus)
    {
        if(leave)
        {
            timerLaunch = DateTime.Now;
            Debug.Log(DateTime.Compare(timerLeave, timerLaunch));
            leave = false;
        }
    }

}
