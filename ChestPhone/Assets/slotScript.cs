using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slotScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int number = 0;
    Player player;
    int actualItemID =999;
    listItemScript listItemScript;


    void Start()
    {
        listItemScript = GameObject.Find("ListItem").GetComponent<listItemScript>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        
    }



    public int getNumber()
    {
        return number;
    }

    public void setNumber(int number)
    {
        this.number = number;
    }

    public void setText(int number)
    {
        setNumber(number);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + number;
    }

    public void chooseItemShop()
    {
        listItemScript = GameObject.Find("ListItem").GetComponent<listItemScript>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if (GameObject.Find("Canvas").GetComponent<ControlMenu>().getShopOpen())
        {
            //If the slot who we click have an item
            if (player.listSlotItem.Contains(transform.GetSiblingIndex()))
            {
                GameObject.Find("Canvas").GetComponent<ControlMenu>().getActualSlotShop().GetComponent<slotShopScript>().setNewSlotShop(actualItemID);
                //Active the amount menu
                transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("AmountText").GetComponent<amountShopScript>().amoutNumber = 1;
                GameObject.Find("AmountText").GetComponent<amountShopScript>().setTextAmount();
            }
            else
            {
                transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(false);              
            }
            //transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(false);
            //transform.parent.parent.parent.gameObject.SetActive(false);
        }
    }

    public void setNewItemSlot(int actualItemID)
    {
        listItemScript = GameObject.Find("ListItem").GetComponent<listItemScript>();
        player = GameObject.Find("Player").GetComponent<Player>();
        this.actualItemID = actualItemID;       
        transform.GetChild(0).GetComponent<Image>().sprite = listItemScript.getListSortedItem()[actualItemID].GetComponent<SpriteRenderer>().sprite;
    }
}
