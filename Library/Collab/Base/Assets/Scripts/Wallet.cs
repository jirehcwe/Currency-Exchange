using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour {
    // Use this for initialization
    public List<Note> typesOfNotesUSD;
    public List<Note> typesOfNotesRiels;  
    private GameObject newNote;
    public Transform parentToReturnTo = null;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createNoteInWallet (ProblemAndAnswer answer)
    {
        for (int i = 0;  i < answer.GetUSDAnswer().Length; i++)
        {
            createNote(parentToReturnTo.gameObject, answer.GetUSDAnswer()[i]);
        }
        for (int j = 0; j < answer.GetRielAnswer().Length; j++)
        {
            createNote(parentToReturnTo.gameObject, answer.GetRielAnswer()[j]);
        }
    }


    private List<Note> addRandomNote(List<Note> noteList)
    {   
        for (int j = 0; j < Random.Range(2,6); j++)
        {
//            noteList.
        }
        return noteList;


    }

    private void createNote(GameObject wallet, Note note)
    {

        Instantiate(newNote);
        newNote.transform.SetParent(wallet.transform.parent);
        newNote.GetComponent<DropZone>().zone = DropZone.zoneType.Wallet;
        LayoutElement Le = newNote.AddComponent<UnityEngine.UI.LayoutElement>();
        Le.preferredWidth = wallet.GetComponent<LayoutElement>().preferredWidth;
        Le.preferredHeight = wallet.GetComponent<LayoutElement>().preferredHeight;
        Le.flexibleHeight = 0;
        Le.flexibleWidth = 0;

        newNote.GetComponent<NoteDisplay>().note = note;
    }
}
