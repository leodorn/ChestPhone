using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Image>().sprite == null)
        {
            changeTransparent(0f);
        }
        else
        {
            changeTransparent(255f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Image>().sprite == null)
        {
            changeTransparent(0f);
        }
        else
        {
            changeTransparent(255f);
        }
    }

    private void changeTransparent(float value)
    {
        Color tempColor = GetComponent<Image>().color;
        tempColor.a = value;
        GetComponent<Image>().color = tempColor;
    }
}
