using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlotScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int nbSlots;
    [SerializeField]
    GameObject prefabSlot;
    Player player;


    private void Awake()
    {

    }
    void Start()
    {
        CreateSlot();
        //Disable the inventory (in the inspector, need to active the inventory before launch the game !!!!)
    }

    

    private void CreateSlot()
    {
        //create all the slots
        for (int i = 0; i < nbSlots; i++)
        {
            GameObject slotCreate = Instantiate(prefabSlot, transform);
            //Set the number to null
            slotCreate.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
        }
        setSlotCreate();
    }

    public void setSlotCreate()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        for (int i = 0; i < player.listSlotItem.Count; i++)
        {
            //Set the slot sprite with the save item
            transform.GetChild(player.listSlotItem[i]).GetComponent<slotScript>().setNewItemSlot(player.listIDItem[i]);
            //Set the slot number with the save number
            transform.GetChild(player.listSlotItem[i]).GetComponent<slotScript>().setText(player.listNumberItem[i]);
        }
        transform.parent.parent.gameObject.SetActive(false);
    }

    
}
