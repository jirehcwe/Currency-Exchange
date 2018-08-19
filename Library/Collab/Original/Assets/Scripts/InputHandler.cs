using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    public GameObject[] buttons;
    public GameManager gm;
    public GameObject Wallet;
    public GameObject Table;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).gameObject;
        }
        if (GameManager.instance != null)
        gm = GameManager.instance;
        
    }
    int value = 0;

    float previousBalance;

    void Update()
    {

        //W_MapKeys();
        //T_MapKeys();
        //Debug.Log(value += W_MapKeys() - T_MapKeys() + DraggableObj.getDragSum() + DropZoneObj.getOffSetValue());

        //Debug.Log(value);
        //Debug.Log("Offset: "+ DropZoneObj.getOffSetValue());
        
        float balance = W_MapKeys() - T_MapKeys() + Draggable.getDragSum();
        if (previousBalance != balance)
            gm.currentBalance = balance;
        gm.balanceText.text = "US$" + gm.currentBalance.ToString();
        previousBalance = balance;
        //gm.currentBalance += W_MapKeys() - T_MapKeys();
        //gm.balanceText.text = "US$" + gm.currentBalance.ToString();
    }
    float T_MapKeys()
    {
        if (Input.inputString != "")
        {

            char key = Input.inputString[0];
            int T_index = -1;
            switch (key)
            {
                case 'q':
                case 'Q':
                    T_index = 0;
                    break;
                case 'w':
                case 'W':
                    T_index = 1;
                    break;
                case 'e':
                case 'E':
                    T_index = 2;
                    break;

                case 'r':
                case 'R':
                    T_index = 3;
                    break;
                case 't':
                case 'T':
                    T_index = 4;
                    break;

            }
            if (T_index == -1)
            {
                return 0;
            }
            int value = Table.transform.GetChild(T_index).GetComponent<Draggable>().note.USDvalue;
            if (T_index < Table.transform.childCount)
            {
                transform.SetSiblingIndex(transform.childCount - 1);
                Table.transform.GetChild(T_index).transform.SetParent(Wallet.transform);
                int newSiblingIndex = 0;
                for (int i = 0; i < Wallet.transform.childCount; i++)
                {
                    if (value > Wallet.transform.GetChild(i).GetComponent<Draggable>().note.USDvalue)
                    {
                        newSiblingIndex = i;
                        break;
                    }
                    else
                    {
                        newSiblingIndex = Wallet.transform.childCount;
                    }
                 }
                Wallet.transform.GetChild(Wallet.transform.childCount - 1).SetSiblingIndex(newSiblingIndex);
                return value;
            }
        }
        return 0;
    }

    float W_MapKeys()
    {
        if (Input.inputString != "")
        {

            char key = Input.inputString[0];
            int W_index = -1;
            switch (key)
            {
                case 'a':
                case 'A':
                    W_index = 0;
                    break;
                case 's':
                case 'S':
                    W_index = 1;
                    break;
                case 'd':
                case 'D':
                    W_index = 2;
                    break;

                case 'f':
                case 'F':
                    W_index = 3;
                    break;
                case 'g':
                case 'G':
                    W_index = 4;
                    break;

            }
            if (W_index == -1)
            {
                return 0;
            }
            float value = Wallet.transform.GetChild(W_index).GetComponent<Draggable>().note.USDvalue;
            if (W_index < Wallet.transform.childCount)
            {   
                int newSiblingIndex = 0;
                for (int i = 0; i < Table.transform.childCount; i++)
                {
                    // Debug.Log("Value of i : " + i);
                    // Debug.Log("Child count: " + Table.transform.childCount);
                    // Debug.Log("TABLE VALUES: " + Table.transform.GetChild(i).GetComponent<Draggable>().valueOfNote);
                    // Debug.Log("Current value: " + value);
                    if (value > Table.transform.GetChild(i).GetComponent<Draggable>().note.USDvalue)
                    {
                        newSiblingIndex = i;
                        break;
                    }
                    else
                    {
                        newSiblingIndex = Table.transform.childCount;
                    }
                    // Debug.Log(Table.transform.GetChild(i));
                    // Debug.Log(newSiblingIndex);
                    // Debug.Log("value " + value);
                }
                transform.SetSiblingIndex(transform.childCount - 1);
                Wallet.transform.GetChild(W_index).transform.SetParent(Table.transform);
                Table.transform.GetChild(Table.transform.childCount - 1).SetSiblingIndex(newSiblingIndex);
                return value;
            }
        }
        return 0;
    }

    
    
}
