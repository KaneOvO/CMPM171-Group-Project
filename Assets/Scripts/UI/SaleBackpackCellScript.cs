using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class SaleBackpackCellScript : BackpackPanelCellScript, IPointerDownHandler
{
    public bool alreadyAdded;
    public SaleBackpackPanelScript saleBackpackPanelScript;
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!alreadyAdded)
        {
            saleBackpackPanelScript.addToDisplay(item, this);
            alreadyAdded = true;
        }
    }
}
