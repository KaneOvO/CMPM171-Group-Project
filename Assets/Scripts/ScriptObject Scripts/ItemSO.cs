using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

[CreateAssetMenu(menuName = "InGameItem/ItemSO")]
public class ItemSO : ScriptableObject
{
    public List<Item> itemList;
}
