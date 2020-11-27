using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class listItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> listItems;
    List<GameObject> listSortedItem;
    void Start()
    {
        listSortedItem = listItems.OrderBy(o => o.GetComponent<ItemScript>().id).ToList();
    }

   public List<GameObject> getListSortedItem()
   {
        return listSortedItem;
   }
}
