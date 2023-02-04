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
    public GameObject canvas;
    public Image portrait;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public IEnumerator StartDialogue(Dialogue dialogue)
    {
        var fullDataText = dialogue.data.text;
        var dataByLine = fullDataText.Split("\n");
        
        canvas.SetActive(true);
        int imageIndex = 0;
        foreach (var line in dataByLine)
        {
            var seperatedData = line.Split("%");
        
            //var portraitPath = seperatedData[0];
            var characterName = seperatedData[0];
            var newdialogueText = seperatedData[1];

            //portrait.sprite.
            nameText.text = characterName;
            portrait.sprite = dialogue.sprite[imageIndex];
            imageIndex++;

            dialogueText.text = "";
            foreach (var character in newdialogueText.ToCharArray())
            {
                dialogueText.text += character;
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }
        canvas.SetActive(false);
    }
}
