using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TxtDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public GameObject mainParent;
    private GameObject drag;
    public DragImageController DIcontroller;

    public ImgScript imgGrabber;
    public ImgScript lastImg;
    
    private void Start()
    {
        DIcontroller = GameManager.instance.DIcontroller;
        mainParent = transform.parent.gameObject;

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        this.transform.SetParent(DIcontroller.tempParent.transform);
        if(imgGrabber != null)
        {
            imgGrabber.hasText = false;
            imgGrabber.txtGrabber = null;
            imgGrabber = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.transform.position = Input.mousePosition;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (!eventData.pointerEnter.transform.CompareTag("ImageView") && !eventData.pointerEnter.transform.CompareTag("TextObj"))//kalo misal ngedropnya gk di image, balik lg ke container
        {
            ResetImg();
            ResetParent();
        }

        Debug.Log("End Drag");
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Onclick");
    }

    public void OnDrop(PointerEventData eventData)
    {
        TxtDrag draggedText = eventData.pointerDrag.GetComponent<TxtDrag>();

        Debug.Log("Ondrop " + draggedText.transform.name + " > " + this.name);

        if(draggedText.lastImg == null)
        {
            Debug.Log("Dragged from Container");


            draggedText.SetImg(imgGrabber);
            draggedText.imgGrabber.txtGrabber = draggedText;
            ResetImg();

            draggedText.imgGrabber.SnapText(draggedText.transform);

            ResetParent();
            
        }
        else//nuker antar container
        {
            Debug.Log("Dragged from " + draggedText.lastImg);
            draggedText.lastImg.SwapText(draggedText, this.imgGrabber);

            //draggedText.transform.SetParent(draggedText.imgGrabber.textSnapParent.transform);
            Debug.Log("Img narik " + draggedText.imgGrabber.transform.name);
        }
        
    }

    public void ResetImg()
    {
        imgGrabber = null;
        lastImg = null;
    }

    public void SetImg(ImgScript img)
    {
        imgGrabber = img;
        lastImg = img;
    }

    public void ResetParent()
    {
        transform.SetParent(mainParent.transform);
    }
    
}
