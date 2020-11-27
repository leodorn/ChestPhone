using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    listItemScript listItem;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        listItem = GameObject.Find("ListItem").GetComponent<listItemScript>();
        SetSlot();

    }

    private void SetSlot()
    {
        for (int slot = 0; slot < player.listSlotItemInShop.Count; slot++)
        {
            transform.GetChild(1).GetChild(slot).GetChild(0).GetComponent<Image>().sprite = listItem.getListSortedItem()[player.listIDItemInShop[slot]].GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(1).GetChild(slot).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + player.listAmountInShop[slot];
            transform.GetChild(1).GetChild(slot).GetChild(3).GetComponent<TextMeshProUGUI>().text = "x" + player.listNumberItemInShop[slot];
            GameObject.Find("Canvas").GetComponent<ControlMenu>().DisableSlotShop(transform.GetChild(1).GetChild(slot).gameObject);
        }
    }

   
}
