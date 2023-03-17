using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    //it is defined for using TextMeshProUGUI as a data type 
public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private TextMeshProUGUI Cheriestext;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            Cheriestext.text = "Cherries:" + cherries;
        }
        
    }
}
