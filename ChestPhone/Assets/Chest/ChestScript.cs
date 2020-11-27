using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Sprite close, open;
    [SerializeField]
    List<GameObject> listItems;
    List<GameObject> listSortedItem;
    [SerializeField]
    private int days,hours,minutes,secondes;
    [SerializeField]
    List<GameObject> itemForUnlock;
    [SerializeField]
    List<int> numberItemForUnlock;
    [SerializeField]
    private int goldForUnlock;
    
    GameObject dropItem;

    void Start()
    {       
        dropItem = new GameObject();
        
    }

    

    public void getLoot()
    {
        //Sort the item list by there chance to drop
        listSortedItem = listItems.OrderByDescending(o => o.GetComponent<ItemScript>().probability).ToList();
        float randomProbability = Random.Range(0, 100);
        Debug.Log(randomProbability);
        float maxRange = 0;
        bool lootFind = false;
        dropItem = null;
        
        //Calculate and find the item
        for (int i =0;i< listSortedItem.Count && !lootFind;i++)
        {
            maxRange += listSortedItem[i].GetComponent<ItemScript>().probability;
            if (randomProbability <= maxRange)
            {
                dropItem = listSortedItem[i];
                lootFind = true;               
            }
        }
        //Set the loot sprite
        GameObject.Find("Loot").GetComponent<Image>().sprite = dropItem.GetComponent<SpriteRenderer>().sprite;
        
    }


    public int getIDLoot()
    {
        return dropItem.GetComponent<ItemScript>().id;
    }

    
    public int calculateTimer()
    {
        return days * 86400 + hours * 3600 + minutes * 60 + secondes;
    }

    public List<GameObject> getItemToUnlock()
    {
        return itemForUnlock;
    }
    public List<int> getNumberItemToUnlock()
    {
        return numberItemForUnlock;
    }
    public int getGoldToUnlock()
    {
        return goldForUnlock;
    }

    public GameObject getDropLoot()
    {
        return dropItem;
    }


}
