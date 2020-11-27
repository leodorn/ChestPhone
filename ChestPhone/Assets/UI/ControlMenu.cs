using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite soundOn, soundOff, musicOn, musicOff, optionImage,slotWrong,slotRight;
    [SerializeField]
    GameObject sound, music, option,Inventory;
    Image soundImageUI, musicImageUI, optionImageUI;
    bool musicPlay,soundPlay;
    bool shopOpen = false;
    Player player;
    AudioSource audio;
    ChestImageScript chestImageScript;
    [SerializeField]
    GameObject unlockPanel1, unlockPanel2, unlockPanel3, unlockPanel4,blackTransparent,moneyTxt,shopPanel,customerTimeTxt,amounText;
    private GameObject actualSlotShop;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        chestImageScript = transform.Find("ChestImage").GetComponent<ChestImageScript>();
        player = GameObject.Find("Player").GetComponent<Player>();
        soundImageUI = sound.GetComponent<Image>();
        soundImageUI.sprite = soundOn;
        musicImageUI = music.GetComponent<Image>();
        musicImageUI.sprite = musicOn;
        optionImageUI = option.GetComponent<Image>();
        optionImageUI.sprite = optionImage;
        if (player.music)
        {
            audio.mute = false;
            musicImageUI.sprite = musicOn;
        }
        else
        {
            audio.mute = true;
            musicImageUI.sprite = musicOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setMoneyText();
    }

    private void setMoneyText()
    {
        moneyTxt.GetComponent<TextMeshProUGUI>().text = "" + player.money;
        if (shopOpen)
        {
            if (player.timerCustomer >= 60)
            {
                customerTimeTxt.GetComponent<TextMeshProUGUI>().text = "" + player.timerCustomer / 60 + "m " + player.timerCustomer % 60 + "s";
            }
            //To transform in secondes
            if (player.timerCustomer < 60)
            {
                customerTimeTxt.GetComponent<TextMeshProUGUI>().text = "" + player.timerCustomer + "s";
            }
        }
    }
    
    public void setSound()
    {
        if(soundPlay)
        {
            soundImageUI.sprite = soundOff;
            soundPlay = false;
        }
        else
        {
            soundImageUI.sprite = soundOn;
            soundPlay = true;
        }
    }
    public void setMusic()
    {
        if (player.music)
        {
            audio.mute = true;
            player.music = false;
            musicImageUI.sprite = musicOff;
        }
        else 
        {
            audio.mute = false;
            player.music = true;
            musicImageUI.sprite = musicOn;
        }
    }

    public void showInventory()
    {
        if(Inventory.active == false)
        {
            Inventory.SetActive(true);
        }
        else if(!shopOpen)
        {
            CloseInventory();          
        }
    }

    private void CloseInventory()
    {
        Inventory.transform.parent.GetChild(1).gameObject.SetActive(false);
        Inventory.transform.parent.GetChild(2).gameObject.SetActive(false);
        Inventory.SetActive(false);
    }

    

    public void setMusic(int music)
    {
        if(music ==1)
        {
            musicPlay = true;
        }
        else
        {
            musicPlay = false;
        }
    }

    public bool getMusic()
    {
        return musicPlay;
    }

    public void showUnlock()
    {
        blackTransparent.SetActive(true);
        //get the number of item need to unlock the chest
        //It will be needed to show the right unlockPanel
        int nbItem = chestImageScript.getActualChest().GetComponent<ChestScript>().getItemToUnlock().Count;
        if(nbItem == 1)
        {
            unlockPanel1.SetActive(true);
            setSlotToUnlock(unlockPanel1);
        }
        else if (nbItem == 2)
        {
            unlockPanel2.SetActive(true);
            setSlotToUnlock(unlockPanel2);
        }
        else if (nbItem == 3)
        {
            unlockPanel3.SetActive(true);
            setSlotToUnlock(unlockPanel3);
        }
        else if (nbItem == 4)
        {
            unlockPanel4.SetActive(true);
            setSlotToUnlock(unlockPanel4);
        }
    }
    public void closeUnlock()
    {
        //Close the unlockPanel
        blackTransparent.SetActive(false);
        int nbItem = chestImageScript.getActualChest().GetComponent<ChestScript>().getItemToUnlock().Count;
        if (nbItem == 1)
        {
            unlockPanel1.SetActive(false);
        }
        if (nbItem == 2)
        {
            unlockPanel2.SetActive(false);
        }
        if (nbItem == 3)
        {
            unlockPanel3.SetActive(false);
        }
        if (nbItem == 4)
        {
            unlockPanel4.SetActive(false);
        }
    }

    private void setSlotToUnlock(GameObject panel)
    {
        bool canUnlock = true;
        //for each slot in unlockPanel
        ChestScript chestScript = chestImageScript.getActualChest().GetComponent<ChestScript>();
        for (int i =0; i<chestScript.getItemToUnlock().Count;i++)
        {
            Transform transformSlot = panel.transform.GetChild(0).GetChild(i);
            //set the sprite with the item in the chestScript           
            transformSlot.GetChild(1).GetComponent<Image>().sprite = chestScript.getItemToUnlock()[i].GetComponent<SpriteRenderer>().sprite;
            //set the number with the number in the chestScript 
            transformSlot.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" +chestScript.getNumberItemToUnlock()[i];
            //set the gold with the gold in the chestScript 
            panel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + chestScript.getGoldToUnlock();
            //(Put this because have some probleme with the next unlock)
            transformSlot.GetComponent<Image>().sprite = slotWrong;
            //Check the inventory
            bool findItem = false;
            for (int j=0;j<player.listIDItem.Count && !findItem;j++)
            {
                //If we have the item
                if (player.listIDItem[j] == chestScript.getItemToUnlock()[i].GetComponent<ItemScript>().id)
                {
                    if (player.listNumberItem[j] < chestScript.getNumberItemToUnlock()[i])
                    {
                        if(canUnlock)
                        {
                            canUnlock = false;
                        }
                        //Set the red slot
                        transformSlot.GetComponent<Image>().sprite = slotWrong;
                    }
                    //If we have enought item
                    else if(player.listNumberItem[j] >= chestScript.getNumberItemToUnlock()[i])
                    {
                        //Set the red slot
                        transformSlot.GetComponent<Image>().sprite = slotRight;
                    }
                    findItem = true;
                }               
            }
            if(!findItem)
            {
                canUnlock = false;
            }
            //Even if the inventory is empty
            if(player.listIDItem.Count==0)
            {
                if (canUnlock)
                {
                    canUnlock = false;
                }
                transformSlot.GetComponent<Image>().sprite = slotWrong;
            }
            Image slotMoneyImage = panel.transform.GetChild(2).GetComponent<Image>();
            //If we don't have enought money
            if (player.money< chestScript.getGoldToUnlock())
            {
                if (canUnlock)
                {
                    canUnlock = false;
                }
                slotMoneyImage.sprite = slotWrong;
            }
            else
            {
                slotMoneyImage.sprite = slotRight;
            }

        }
        //active the unlock button and change the color of the button
        Transform transformUnlockPanel = panel.transform.GetChild(3);
        if (canUnlock)
        {
            transformUnlockPanel.GetChild(1).gameObject.SetActive(true);
            transformUnlockPanel.GetComponent<Image>().color = Color.white;
            transformUnlockPanel.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            transformUnlockPanel.GetChild(1).gameObject.SetActive(false);
            transformUnlockPanel.GetComponent<Image>().color = new Color32(147, 147, 147,226);
            transformUnlockPanel.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(149, 134, 134,255);
        }
        
    }

    //Unlock the chest with the chestImageScript
    public void unlockChestMenu()
    {
        chestImageScript.unlockNewChest();
        closeUnlock();
    }

    public void showShop()
    {
        shopPanel.SetActive(true);
        shopOpen = true;

    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
        shopOpen = false;
    }

    public bool getShopOpen()
    {
        return shopOpen;
    }

    public void setShopOpen(bool shopOpen)
    {
        this.shopOpen = shopOpen;
    }

    public void setActualSlotShop(GameObject actualSlotShop)
    {
        this.actualSlotShop = actualSlotShop;
    }

    public GameObject getActualSlotShop()
    {
        return actualSlotShop;
    }

    public void IncreaseNumberShop()
    {
        //If the amount is less than the amount in inventory
        if (amounText.GetComponent<amountShopScript>().amoutNumber < player.listNumberItem[player.listIDItem.IndexOf(actualSlotShop.GetComponent<slotShopScript>().itemID)]) 
        {
            amounText.GetComponent<amountShopScript>().amoutNumber++;
            amounText.GetComponent<amountShopScript>().setTextAmount();
        }
    }
    public void DicreaseNumberShop()
    {
        if(amounText.GetComponent<amountShopScript>().amoutNumber>1)
        {
            amounText.GetComponent<amountShopScript>().amoutNumber--;
            amounText.GetComponent<amountShopScript>().setTextAmount();
        }
    }

    public void ConfirmShop()
    {
        actualSlotShop.GetComponent<slotShopScript>().setNumberItem(amounText.GetComponent<amountShopScript>().amoutNumber);
        actualSlotShop.GetComponent<slotShopScript>().setTotalAmount();
        //To deduct the amount of item
        int index = player.listIDItem.IndexOf(actualSlotShop.GetComponent<slotShopScript>().itemID);
        player.listNumberItem[index] -= actualSlotShop.GetComponent<slotShopScript>().numberItem;
        if(player.listNumberItem[index] <= 0)
        {
            ResetSlotInventory(index);
        }
        else
        {
            Inventory.transform.GetChild(1).GetChild(0).GetChild(player.listSlotItem[index]).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + player.listNumberItem[index];
        }
        // A FINIR ICI IL NE FAUT PÄS METTRE LE SLOT DE VENTE RAPIDE
        DisableSlotShop(actualSlotShop);
        SafeShop();
        CloseInventory();
    }

    public void CloseAndPutNothingInShop()
    {
        actualSlotShop.GetComponent<slotShopScript>().makeEmpty();
        CloseInventory();
    }

    public void DisableSlotShop(GameObject actualSlotShop)
    {
        actualSlotShop.GetComponent<Image>().color = new Color32(133, 125, 125, 255);
        actualSlotShop.transform.GetChild(0).GetComponent<Image>().color = new Color32(133, 125, 125, 255);
        actualSlotShop.GetComponent<Button>().interactable = false;
    }
    private void SafeShop()
    {
        player.listSlotItemInShop.Add(actualSlotShop.transform.GetSiblingIndex());
        player.listIDItemInShop.Add(actualSlotShop.GetComponent<slotShopScript>().itemID);
        player.listNumberItemInShop.Add(actualSlotShop.GetComponent<slotShopScript>().numberItem);
        player.listAmountInShop.Add(actualSlotShop.GetComponent<slotShopScript>().totalAmount);
    }

    public void ResetSlotInventory(int index)
    {
        player.listNumberItem.RemoveAt(index);
        player.listIDItem.RemoveAt(index);
        Inventory.transform.GetChild(1).GetChild(0).GetChild(player.listSlotItem[index]).GetChild(0).GetComponent<Image>().sprite = null;
        Inventory.transform.GetChild(1).GetChild(0).GetChild(player.listSlotItem[index]).GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        player.listSlotItem.RemoveAt(index);
    }
}
