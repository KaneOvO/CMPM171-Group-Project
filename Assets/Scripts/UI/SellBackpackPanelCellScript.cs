using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class SellBackpackPanelCellScript : BackpackPanelCellScript, IPointerDownHandler
{
    public int currentScene;
    public bool alreadyAdded;


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer");
        if(!alreadyAdded){
            descriptionPanel.transform.parent.GetComponent<WholesaleSellBackpackPanelScript>().addToDisplay(item);
            alreadyAdded = true;
        }
    }
}
