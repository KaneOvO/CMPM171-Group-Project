using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUID : MonoBehaviour
{
    public bool lockedID = false;
    public string id;

    private void OnValidate()
    {
        if (lockedID && id != string.Empty)
        {
            return;
        }

        id = string.Empty;
        id = System.Guid.NewGuid().ToString();
    }
}
