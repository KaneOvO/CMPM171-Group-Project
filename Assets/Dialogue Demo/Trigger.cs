using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public NPCConversation conversation;
    
    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        ConversationManager.Instance.StartConversation(conversation);

    }
}
