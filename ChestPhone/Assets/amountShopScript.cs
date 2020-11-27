using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class amountShopScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int amoutNumber = 1;
    
    public void setTextAmount()
    {
        GetComponent<TextMeshProUGUI>().text = "" + amoutNumber;
    }
}
