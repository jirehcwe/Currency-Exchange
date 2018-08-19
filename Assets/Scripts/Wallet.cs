using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour {
    // Use this for initialization
    public List<Note> typesOfNotesUSD;
    public List<Note> typesOfNotesRiels;
    public GameObject notePrefab;
    public Settings settings;
    public int maxAddUsdNum;
    public int maxAddReilsNum;
    public int minAddUsdNum;
    public int minAddReilsNum;
    public RectTransform parentToReturnTo;

    public Note testNote;
    // Update is called once per frame

    public void createNoteInWalletMixed(ProblemAndAnswer answer)
    {
        maxAddUsdNum = settings.MaxAddUsdNum;
        minAddUsdNum = settings.MinAddUsdNum;
        maxAddReilsNum = settings.MaxAddRielsNum;
        minAddReilsNum = settings.MinAddRielsNum;
        List<Note> notesToDisplay = addRandomNote(answer.GetMixedAnswer(), maxAddUsdNum, maxAddReilsNum);
        for (int i = 0; i < notesToDisplay.Count; i++)
        {
            createNote(notesToDisplay[i]);
        }
    }
    public void createNoteInWalletRiels(ProblemAndAnswer answer)
    {
        maxAddUsdNum = settings.MaxAddUsdNum;
        maxAddReilsNum = settings.MaxAddRielsNum;
        minAddUsdNum = 0;
        minAddReilsNum = settings.MinAddRielsNum;
        List<Note> notesToDisplay = addRandomNote(answer.GetRielAnswer(), 0, maxAddReilsNum);
        foreach (Note note in notesToDisplay){
            // Debug.Log("Note passed: " + note);
            createNote(note);
        }

        // for (int i=0; i<answer.GetRielAnswer().Count;i++){
        //     createNote(testNote);
        // }
        
    }
    public void createNoteInWallet(ProblemAndAnswer answer)    //This is the main function that will be used
    {
        List<Note> notesToDisplay = addRandomNote(mergeAnswers(answer.GetMixedAnswer(), answer.GetRielAnswer()), maxAddUsdNum,maxAddReilsNum);
        for (int i = 0; i < notesToDisplay.Count; i++)
        {
            createNote(notesToDisplay[i]);
        }
    }


    private List<Note> mergeAnswers(List<Note> usd, List<Note> reils)
    {
        for (int i = 0; i < reils.Count; i++)
        {
            usd.Add(reils[i]);
        }
        return usd;
    }


    private List<Note> addRandomNote(List<Note> noteList, int maxAddUsdNum, int maxAddReilsNum)               //Private function that takes in a list of correct answer notes, then add random notes for difficulty
    {   
        for (int j = 0; j < Random.Range(minAddUsdNum,maxAddUsdNum); j++)
        {

            int index = 0;
            Note noteToAdd = typesOfNotesUSD[Random.Range(0, 3)];
            for (int a = 0; a < noteList.Count; a++)
            {
                if (noteList[a].UsdValue > noteToAdd.UsdValue)
                {
                    index++;
                }
                else break;
            }

            noteList.Insert(index, noteToAdd);
        }
        for (int k = 0; k < Random.Range(minAddReilsNum,maxAddReilsNum); k++)
        {
            int index = 0;
            Note noteToAdd = typesOfNotesRiels[Random.Range(0, 3)];
            for (int a = 0; a < noteList.Count; a++)
            {
                if (noteList[a].UsdValue > noteToAdd.UsdValue)
                {
                    index++;
                }
                else break;
            }

            noteList.Insert(index,noteToAdd);
        }
        //noteList.Sort();
// TODO: implement some sorting function

        return noteList;
    }

/*
    class Note_SortByValue : IComparer<Note> {
        public int Compare(Note x, Note y)
        {
            if (x.USDvalue < y.USDvalue) return 1;
            else if (x.USDvalue > y.USDvalue) return -1;
            else return 0;
        }
    }
*/
    
    private void createNote(Note notePassed)                                  //creates notes in the wallet itself
    {
        GameObject newNote = Instantiate(notePrefab, parentToReturnTo);
        newNote.GetComponent<NoteDisplay>().StartNote(notePassed);
    }
}
