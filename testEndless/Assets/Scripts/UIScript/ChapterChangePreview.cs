using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterChangePreview : MonoBehaviour
{
    public Image previewImage;
    public Sprite[] selectedImage;

    public Button[] chapterBtn;

    private void Start()
    {
        int unlockedChapter = ((GameManager.instance.currentUnlockedLevel-1) / 5);

        Debug.Log(unlockedChapter);

        for(int i = 0; i < 3; i++)
        {
            if(i > unlockedChapter)
            {
                Debug.Log(i + " Locked");
                chapterBtn[i].interactable = false;
            }
            else
            {
                chapterBtn[i].interactable = true;
            }
        }
    }

    public void ChangePreview(int i)
    {
        if (chapterBtn[i].IsInteractable())
        {
            StartCoroutine(delay(i));
        }
    }

    IEnumerator delay(int i)
    {
        yield return new WaitForSeconds(0.1f);
        previewImage.sprite = selectedImage[i];
    }
}
