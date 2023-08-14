using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImgScript : MonoBehaviour, IDropHandler
{
    public RectTransform textSnapParent;
    public RectTransform pos;
    public TxtDrag txtGrabber;

    public bool hasText;

    private void Start()
    {
        hasText = false;
    }


    public void OnDrop(PointerEventData eventData)
    {
        
        Debug.Log(eventData.pointerDrag.name);

        if (eventData.pointerDrag.CompareTag("TextObj"))
        {
            if (!hasText)//naro ke image
            {
                hasText = true;
                txtGrabber = eventData.pointerDrag.GetComponent<TxtDrag>();
                SnapText(eventData.pointerDrag.transform);
                txtGrabber.SetImg(this);
                Debug.Log(eventData.pointerDrag.name + " is dropped on " + transform.name);
            }
            else//nuker
            {
                Swapping(eventData);
            }
        }
        
    }

    public void Swapping(PointerEventData eventData)
    {
        TxtDrag txtObject = eventData.pointerDrag.GetComponent<TxtDrag>();//yg di drag

        if (txtObject.lastImg != null)//nuker antar image
        {
            ImgScript targetImg = eventData.pointerEnter.transform.GetComponent<ImgScript>();
            ImgScript objectImg = eventData.pointerDrag.transform.GetComponent<TxtDrag>().lastImg;//yg di drag

            Debug.Log("Check Target " + eventData.pointerEnter.transform.parent.name + (targetImg == null));
            Debug.Log("Check Object " + (objectImg == null));
            SwapText(txtObject, targetImg);
        }
        else//nuker dari container
        {
            txtGrabber.ResetImg();
            txtGrabber.ResetParent();

            txtGrabber = txtObject;
            txtGrabber.SetImg(this);

            SnapText(txtGrabber.transform);
        }
    }
    
    public void SnapText(Transform txt)
    {
        txt.SetParent(textSnapParent.transform);
        txt.GetComponent<RectTransform>().localPosition = pos.localPosition;
    }

    public void SwapText(TxtDrag draggedText, ImgScript targetImg)
    {
        ImgScript objectImg = draggedText.lastImg;

        objectImg.hasText = true;

        TxtDrag txtObject = draggedText;
        Debug.Log(targetImg.name);
        TxtDrag txtTarget = targetImg.textSnapParent.GetComponentInChildren<TxtDrag>();//target 

        //ngegrab text masing masing
        targetImg.txtGrabber = txtObject;
        objectImg.txtGrabber = txtTarget;

        //nuker posisi text masing masing
        targetImg.SnapText(targetImg.txtGrabber.transform);
        objectImg.SnapText(objectImg.txtGrabber.transform);
        
        //nuker image grabber
        txtTarget.imgGrabber = objectImg;
        txtObject.imgGrabber = targetImg;

        //assign last img
        txtTarget.lastImg = objectImg;
        txtObject.lastImg = targetImg;
    }
}
