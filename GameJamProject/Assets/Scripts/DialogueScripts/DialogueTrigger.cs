using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject player;

    private void Update()
    {
        var playerPosition = player.transform.position;
        var distance = Vector3.Distance(playerPosition, transform.position);
        
        if (distance <= 10 && Input.GetKey(KeyCode.Return))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
