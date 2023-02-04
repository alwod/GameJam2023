using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueEndEvent : MonoBehaviour
{
    public GameObject dialogueBox;

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.tag == "Finish")
        {
            SceneManager.LoadScene("Main");
        }
    }
}
