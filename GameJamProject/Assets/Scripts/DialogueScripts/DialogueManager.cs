using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        
        _sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
           _sentences.Enqueue(sentence); 
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (var character in sentence.ToCharArray())
        {
            dialogueText.text += character;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        
    }
}
