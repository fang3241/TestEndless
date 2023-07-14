using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<Dialogue> sentences;

    public Character[] characters;


    public float letterSpeed;
    public Image charImage;
    public Text dialogueName;
    public Text dialogueText;

    private void Start()
    {
        sentences = new Queue<Dialogue>();
        
    }

    public void StartDialogue(Dialogue[] dialogues)
    {
        Debug.Log("start dialog ");

        sentences.Clear();

        foreach(Dialogue dialogue in dialogues)
        {
            sentences.Enqueue(dialogue);
        }

        //dialogueName.text = dialogue.charID.ToString() ;

        //foreach(string sentence in dialogue.sentences)
        //{
        //    sentences.Enqueue(sentence);
        //}

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        

        Dialogue charSentence = sentences.Dequeue();


        charImage.sprite = characters[charSentence.charID].spriteChar;
        dialogueName.text = characters[charSentence.charID].name;
        //dialogueText.text = charSentence.sentences;

        Debug.Log(characters[charSentence.charID].name + " : " + charSentence.sentences);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(charSentence.sentences));

        //Debug.Log(sentence);

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }


    public void EndDialogue()
    {
        Debug.Log("end dialogue");
    }
}
