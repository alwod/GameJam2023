using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject player;
    
    private float _delayTime = 0.0f;
    private bool hasStarted = false;

    private void Update()
    {
        var playerPosition = player.transform.position;
        var distance = Vector3.Distance(playerPosition, transform.position);

        _delayTime = _delayTime - Time.deltaTime;
        
        if (distance <= 5 && Input.GetKey(KeyCode.Return) && _delayTime <= 0 && !hasStarted)
        {
            _delayTime = 0.2f;
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        hasStarted = true;
        StartCoroutine(FindObjectOfType<DialogueManager>().StartDialogue(dialogue));
    }
}
