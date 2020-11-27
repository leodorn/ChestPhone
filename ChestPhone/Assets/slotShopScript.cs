using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slotShopScript : MonoBehaviour
{
    [SerializeField]
    GameObject inventory,panelAmount;
    listItemScript listItemScript;
    Player player;
    public int itemID;
    public int numberItem;
    public int totalAmount;

    public void openInventory()
    {
        GameObject.Find("Inventory").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Canvas").GetComponent<ControlMenu>().setActualSlotShop(gameObject);
        GameObject.Find("Inventory").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void setNewSlotShop(int itemID)
    {
        this.itemID = itemID;
        player = GameObject.Find("Player").GetComponent<Player>();
        listItemScript = GameObject.Find("ListItem").GetComponent<listItemScript>();
        transform.GetChild(0).GetComponent<Image>().sprite = listItemScript.getListSortedItem()[itemID].GetComponent<SpriteRenderer>().sprite;

    }

    public void setNumberItem(int numberItem)
    {
        this.numberItem = numberItem;
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "x" + numberItem;
    }

    public void setTotalAmount()
    {
        totalAmount = listItemScript.getListSortedItem()[itemID].GetComponent<ItemScript>().price * numberItem;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + totalAmount;
    }

    public void makeEmpty()
    {
        this.numberItem = 999;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        totalAmount = 0;
        numberItem = 0;
        setNumberItem(numberItem);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + totalAmount;
    }
}
