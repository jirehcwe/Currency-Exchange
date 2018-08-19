using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler,IComparable<Draggable>{

    public Transform parentToReturnTo = null;
    public int valueOfNote;
    public char keyPress = ' ';
    public GameObject placeholder;
    private static int dragSum = 0;
    public bool changedDropzone = false;
    public GameManager gm = GameManager.instance;

     public int CompareTo(Draggable that)
    {
        return -this.valueOfNote.CompareTo(-that.valueOfNote);
    }

    public static int getDragSum()
    {
        int tempSum = dragSum;
        dragSum = 0;
        return tempSum;  
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        placeholder = new GameObject();

        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement Le = placeholder.AddComponent<LayoutElement>();
        Le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        Le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        Le.flexibleHeight = 0;
        Le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        placeholder.name = "placeholder";
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
 

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      //  Debug.Log("Note On Drag");
        this.transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        Destroy(placeholder);
        Debug.Log("Note ended drag");
        this.transform.SetParent(parentToReturnTo);
        int newSiblingIndex = parentToReturnTo.childCount;
        
        if (parentToReturnTo.GetComponentInParent<DropZone>().zone == DropZone.zoneType.Wallet)
        {
            Debug.Log("Card successfully dropped onto wallet");
            if (changedDropzone)
            {
                dragSum -= valueOfNote;
                changedDropzone = false;
            }
            for (int i = 0; i < parentToReturnTo.childCount; i++)
            {
                if (parentToReturnTo.GetChild(i).name == "placeholder")
                {
                    i++;
                }
                if (valueOfNote > parentToReturnTo.GetChild(i).GetComponent<Draggable>().valueOfNote)
                {
                    newSiblingIndex = i;
                    break;
                }
            }
            transform.SetSiblingIndex(newSiblingIndex);
        }
       else if (parentToReturnTo.GetComponentInParent<DropZone>().zone == DropZone.zoneType.Table)
        {
            Debug.Log("Card successfully dropped onto table");
            if (changedDropzone)
            {
                dragSum += valueOfNote;
                changedDropzone= false;
            }
            for (int i = 0; i < parentToReturnTo.childCount; i++)
            {
                Debug.Log(parentToReturnTo.GetChild(i));
                if(parentToReturnTo.GetChild(i).name == "placeholder")
                {
                    i++;
                }
                Debug.Log(parentToReturnTo.GetChild(i));
                if (valueOfNote > parentToReturnTo.GetChild(i).GetComponent<Draggable>().valueOfNote)
                {
                    newSiblingIndex = i;
                    break;
                }
            }
            transform.SetSiblingIndex(newSiblingIndex);

        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
