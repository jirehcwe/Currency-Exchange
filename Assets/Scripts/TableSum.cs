using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSum : MonoBehaviour {

	public GameObject playerTable;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameManager.instance;
	}
	
	// Update is called once per frame

	void Update()
	{
		CalculateSum(playerTable);
	}
	float CalculateSum(GameObject table){
		float sum = 0;
	
		for (int i=0;i <playerTable.transform.childCount;i++){
			if(table.transform.GetChild(i).GetComponent<Draggable>() != null)
				sum += table.transform.GetChild(i).GetComponent<NoteDisplay>().note.UsdValue;
		}

		return sum;
	
	}

	
}
