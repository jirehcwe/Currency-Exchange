﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{

    public enum zoneType { Wallet, Table };
    public zoneType zone = zoneType.Table;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log(eventData.pointerDrag.name + " On Drop to " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        // Debug.Log("Current Parent: " + d.parentToReturnTo);
        // Debug.Log("Dropping parent: " + this.transform);
        if (d != null && (d.parentToReturnTo.transform != this.transform))
        {
            d.parentToReturnTo = this.transform;
            d.changedDropzone = true;

            //            Debug.Log("ParentToReturnTo is set");

        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    static public void EmptyTableToWallet(GameObject table, GameObject wallet)
    {
        if (table.GetComponent<DropZone>().zone == zoneType.Table)
        {
            int childcount = table.transform.childCount;
            for (int i = 0; i < childcount; i++)
            {
                table.transform.GetChild(0).SetParent(wallet.transform);
            }

            // SortZone(wallet.GetComponent<DropZone>());
        }
    }

    static public void ClearDropzone(GameObject dropzoneToClear){
        int childcount = dropzoneToClear.transform.childCount;
        // Debug.Log(dropzoneToClear + " : " + childcount);
            for (int i = 0; i < childcount; i++)
            {
                Destroy(dropzoneToClear.transform.GetChild(i).gameObject);
            }
    }

    public static float SumOfZoneInRiels(DropZone zone)
    {
        float sum = 0;
        Transform zoneTransform = zone.transform;
        for (int i = 0; i < zoneTransform.childCount; i++)
        {
            if (zoneTransform.GetChild(i).GetComponent<Draggable>() != null)
            {
                sum += zoneTransform.GetChild(i).GetComponent<NoteDisplay>().note.UsdValue*GameManager.instance.rielsToUSDExchangeRate;
            }
        }
        return sum;
    } 

        public static float SumOfZoneInUSD(DropZone zone)
    {
        float sum = 0;
        Transform zoneTransform = zone.transform;
        for (int i = 0; i < zoneTransform.childCount; i++)
        {
            if (zoneTransform.GetChild(i).GetComponent<Draggable>() != null)
            {
                sum += zoneTransform.GetChild(i).GetComponent<NoteDisplay>().note.UsdValue;
            }
        }
        return sum;
    } 

    public static void SortZone(DropZone zone){
        List<Transform> cardList = new List<Transform>();
        Transform zoneTransform = zone.transform;
        for (int i = 0; i < zoneTransform.childCount; i++)
        {           
            cardList.Add(zoneTransform.GetChild(i));
            
        }

        cardList.Sort();

        foreach (Transform t in cardList){
            t.SetSiblingIndex(cardList.IndexOf(t));
        }

    } 
}