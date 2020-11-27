using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestImageScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> listAllChest;
    List<GameObject> listUnlockChest;
    GameObject actualChest;
    Player player;
    Image imageChest;   
    [SerializeField]
    TextMeshProUGUI textTimer;
    [SerializeField]
    Sprite chestLock;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject buttonUnlock;
    [SerializeField]
    GameObject arrowBack, arrowNext, ItemImage,shopSlotPanel;
    private bool canNextChest = true;
    void Start()
    {   
        player = GameObject.Find("Player").GetComponent<Player>(); 
        listUnlockChest = new List<GameObject>();
        //Add all the chest already unlock
        FillListUnlockChest();
        imageChest = GetComponent<Image>();
        setChestSprite();
        setArrowActive();        
        
       
        
        StartCoroutine(timerCount());
    }

    private void FillListUnlockChest()
    {
        for (int i = 0; i <= player.actualChestNumber; i++)
        {
            listUnlockChest.Add(listAllChest[i]);
        }
        actualChest = listAllChest[player.actualChestNumber];
    }
    // Update is called once per frame
    void Update()
    {
        ConvertTimer();
        
    }

    private void ConvertTimer()
    {
        if (canNextChest)
        {
            buttonUnlock.SetActive(false);
            //To transform in days and hours
            if (player.listTimer[player.actualChestNumber] >= 86400)
            {
                textTimer.text = "" + player.listTimer[player.actualChestNumber] / 86400 + "d" + (player.listTimer[player.actualChestNumber] % 86400) / 3600 + "h";
            }
            //To transform in hours and minutes
            else if (player.listTimer[player.actualChestNumber] >= 3600)
            {
                textTimer.text = "" + player.listTimer[player.actualChestNumber] / 3600 + "h" + (player.listTimer[player.actualChestNumber] % 3600) / 60 + "m";
            }
            //To transform in minutes and secondes
            else if (player.listTimer[player.actualChestNumber] >= 60)
            {
                textTimer.text = "" + player.listTimer[player.actualChestNumber] / 60 + "m " + player.listTimer[player.actualChestNumber] % 60 + "s";
            }
            //To transform in secondes
            else if (player.listTimer[player.actualChestNumber] < 60)
            {
                textTimer.text = "" + player.listTimer[player.actualChestNumber] + "s";
            }

        }
        else
        {
            buttonUnlock.SetActive(true);
            textTimer.text = "Lock";
        }
    }



    public void OpenChest()
    {
        //If the chest is unlock
        if(canNextChest)
        {
            ChestScript actualChestScript = actualChest.GetComponent<ChestScript>();
            //If the timer is up or the chest is open
            if (player.listTimer[player.actualChestNumber] == 0 || imageChest.sprite == actualChestScript.open)
            {
                //Open the chest, get a loot and reset the timer
                if (imageChest.sprite == actualChestScript.close)
                {
                    imageChest.sprite = actualChestScript.open;
                    actualChestScript.getLoot();
                    //Reset and save the timer
                    player.listTimer[player.actualChestNumber] = actualChestScript.calculateTimer();
                    //To fill the inventory
                    bool isInInventory = false;
                    int numberSlot = 0;
                    if(player.listIDItem.Contains(actualChest.GetComponent<ChestScript>().getDropLoot().GetComponent<ItemScript>().id))
                    {
                        isInInventory = true;
                        numberSlot = player.listIDItem.IndexOf(actualChest.GetComponent<ChestScript>().getDropLoot().GetComponent<ItemScript>().id);
                    }


                        ////If the item is already in the inventory
                        //if (inventory.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite == GameObject.Find("Loot").GetComponent<Image>().sprite)
                        //{
                        //    //Set the amount of the item
                        //    inventory.transform.GetChild(i).GetComponent<slotScript>().setTextNumber();
                        //    //Save the amount of the item
                        //    player.listNumberItem[i] = inventory.transform.GetChild(i).GetComponent<slotScript>().getNumber();
                        //    isInInventory = true;
                        //}
                        ////Else put in a new slot
                        //else if (inventory.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite == null)
                        //{
                        //   //Save the slot position
                        //    player.listSlotItem.Add(i);  
                        //    //Put the loot sprite in the inventory
                        //    inventory.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Loot").GetComponent<Image>().sprite;
                        //    slotScript slotScript = inventory.transform.GetChild(i).GetComponent<slotScript>();
                        //    //Set the amount of the item
                        //    slotScript.setTextNumber();
                        //    //Save the new numberItem and the new idItem
                        //    player.listNumberItem.Add(1);
                        //    player.listIDItem.Add(actualChestScript.getIDLoot());
                        //    isInInventory = true;
                        //}

                    
                    if(!isInInventory)
                    {
                        bool isFill = false;
                        for(int i =0;i<inventory.transform.childCount && !isFill;i++)
                        {
                            if(!player.listSlotItem.Contains(i))
                            {
                                player.listSlotItem.Add(i);
                                player.listIDItem.Add(actualChest.GetComponent<ChestScript>().getDropLoot().GetComponent<ItemScript>().id);
                                player.listNumberItem.Add(1);
                                Debug.Log("ça bug ? : " + actualChest.GetComponent<ChestScript>().getDropLoot().GetComponent<ItemScript>().id);
                                inventory.transform.GetChild(i).GetComponent<slotScript>().setNewItemSlot(actualChest.GetComponent<ChestScript>().getDropLoot().GetComponent<ItemScript>().id);
                                inventory.transform.GetChild(i).GetComponent<slotScript>().setText(1);
                                

                                isFill = true;
                            }
                        }
                        if(!isFill)
                        {
                            Debug.Log("Inventory full !");
                        }
                    }
                    else if(isInInventory)
                    {
                        player.listNumberItem[numberSlot]++;
                        inventory.transform.GetChild(numberSlot).GetComponent<slotScript>().setText(player.listNumberItem[numberSlot]);
                    }
               
                }
                //Close the chest and destroy the loot image 
                else if (imageChest.sprite == actualChestScript.open)
                {
                    GameObject.Find("Loot").GetComponent<Image>().sprite = null;
                    imageChest.sprite = actualChestScript.close;
                }
            }
        }
        




    }
    

    private void setArrowActive()
    {
        //Set the loot image to null
        ItemImage.GetComponent<Image>().sprite = null;
        if (player.actualChestNumber == 0)
        {
            arrowNext.SetActive(true);
            arrowBack.SetActive(false);
        }
        else if (player.actualChestNumber > 0 && player.actualChestNumber < listAllChest.Count - 1 )
        {
            arrowNext.SetActive(true);
            arrowBack.SetActive(true);
        }
        else if (player.actualChestNumber == listAllChest.Count - 1)
        {
            arrowNext.SetActive(false);
            arrowBack.SetActive(true);
        }
        //if the chest is lock
        if(!canNextChest)
        {
            arrowNext.SetActive(false);
        }
    }

    public void changeChestNext()
    {
        //Set up the next chest
        player.actualChestNumber++;
        actualChest = listAllChest[player.actualChestNumber];
        //Set up the sprite
        setChestSprite();
        //Set up the arrows
        setArrowActive();
    }

    public void changeChestBack()
    {
        //Set up the next chest
        player.actualChestNumber--;
        actualChest = listAllChest[player.actualChestNumber];
        //Set up the sprite
        setChestSprite();
        //Set up the arrows
        setArrowActive();
    }

    private void setChestSprite()
    {
        //If the chest is lock, set the black sprite
        if(player.actualChestNumber >= listUnlockChest.Count)
        {
            imageChest.sprite = chestLock;
            canNextChest = false;
        }
        //Show the close sprite
        else
        {
            imageChest.sprite = actualChest.GetComponent<ChestScript>().close;
            canNextChest = true;
        }
    }

    //To reduce the timer time
    private IEnumerator timerCount()
    {
        
        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < player.listTimer.Count; i++)
        {
            if (player.listTimer[i] > 0)
            {
                player.listTimer[i]--;               
            }
            player.timerCustomer--;
            if (player.timerCustomer <= 0)
            {

                SellItemShop();
                player.timerCustomer = player.timerCustomerMax;
            }
        }
        StartCoroutine(timerCount());
    }

    private void SellItemShop()
    {
        for (int slot = 0; slot < player.listAmountInShop.Count; slot++)
        {
            player.money += player.listAmountInShop[slot];
            ResetSlotShop(slot);
            EnableSlotShop(shopSlotPanel.transform.GetChild(slot).gameObject);
        }
    }
    private void EnableSlotShop(GameObject actualSlotShop)
    {
        actualSlotShop.GetComponent<Image>().color = Color.white;
        actualSlotShop.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        actualSlotShop.transform.GetChild(0).GetComponent<Image>().sprite = null;
        actualSlotShop.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + 0;
        actualSlotShop.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "x" + 0;
        actualSlotShop.GetComponent<Button>().interactable = true;
    }

    private void ResetSlotShop(int slot)
    {
        player.listAmountInShop.RemoveAt(slot);
        player.listIDItemInShop.RemoveAt(slot);
        player.listSlotItemInShop.RemoveAt(slot);
        player.listNumberItemInShop.RemoveAt(slot);
    }

    public void unlockNewChest()
    {
        ChestScript chestScript = actualChest.GetComponent<ChestScript>();
        //If there is otherChest to unlock
        if (listAllChest.Count != listUnlockChest.Count )
        {
            //deduct the money of the player
            player.money -= chestScript.getGoldToUnlock();
            //For all the item that we need to unlock the chest
            for(int i=0;i<chestScript.getItemToUnlock().Count;i++)
            {
                //Check in inventory
                for(int j =0;j<player.listIDItem.Count;j++)
                {
                    //If this is the right item
                    if (player.listIDItem[j] == chestScript.getItemToUnlock()[i].GetComponent<ItemScript>().id)
                    {
                        //deduct the number
                        player.listNumberItem[j] -= chestScript.getNumberItemToUnlock()[i];
                        //Refresh the number
                        TextMeshProUGUI textNumberItem = inventory.transform.GetChild(player.listSlotItem[j]).GetChild(1).GetComponent<TextMeshProUGUI>();
                        textNumberItem.text = "" + player.listNumberItem[j];
                        //If we use all the item
                        if (player.listNumberItem[j] == 0)
                        {
                            //remove from the inventory and reset the number to null
                            player.listNumberItem.RemoveAt(j);
                            player.listIDItem.RemoveAt(j);
                            inventory.transform.GetChild(player.listSlotItem[j]).GetChild(0).GetComponent<Image>().sprite = null;
                            textNumberItem.text = null;
                            player.listSlotItem.RemoveAt(j);
                        }
                    }
                }
            }
            //add to the unlockChest
            listUnlockChest.Add(listAllChest[player.actualChestNumber]);
            //Save the new timer and set it to the default timer
            player.listTimer.Add(listAllChest[player.actualChestNumber].GetComponent<ChestScript>().calculateTimer());

        }
        setChestSprite();
        setArrowActive();
    }



    public GameObject getActualChest()
    {
        return actualChest;
    }

    
    


}
