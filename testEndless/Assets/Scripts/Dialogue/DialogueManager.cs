using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<Dialogue> sentences;

    public Character[] characters;

    //public CharacterDialogues[] cDialogue;

    CharacterDialogues selectedCDialogue;

    public float letterSpeed;
    public Image charImage, bgImg;
    public Sprite[] bgImages;

    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    
    private void Start()
    {
        int id = -1;
        sentences = new Queue<Dialogue>();

        selectedCDialogue = GameManager.instance.selectedDialogue;

        StartDialogue(id);
    }

    public void StartDialogue(int id)
    {
        Debug.Log("start dialog ");

        sentences.Clear();

        foreach(Dialogue dialogue in selectedCDialogue.dialogues)
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

        if(characters[charSentence.charID].name == "")//narasi
        {
            charImage.gameObject.SetActive(false);
            dialogueName.transform.parent.gameObject.SetActive(false);
        }else 
        {
            if (characters[charSentence.charID].spriteChar == null)
            {
                charImage.gameObject.SetActive(false);
                dialogueName.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                charImage.gameObject.SetActive(true);
                dialogueName.transform.parent.gameObject.SetActive(true);
            }

            dialogueName.text = characters[charSentence.charID].name;
            charImage.sprite = characters[charSentence.charID].spriteChar;
        }

        bgImg.sprite = selectedCDialogue.bgImage;

        
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

        int type = GameManager.instance.selectedChapter;

        if(type == 1)
        {
            GameManager.instance.buttonNavigation.toLevelLand();
        }else if (type == 2)
        {
            GameManager.instance.buttonNavigation.toLevelWater();
        }
        else if(type == 3)
        {
            GameManager.instance.buttonNavigation.toLevelAir();
        }
        else
        {
            GameManager.instance.buttonNavigation.toMenu();
        }

    }
}
