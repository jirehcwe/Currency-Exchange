using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemAndAnswer
{

    private float usdSum;
    private float rielSum;
    private List<Note> usdAnswer;
    private List<Note> rielAnswer;

    public Wallet wallet;

    public float GetUSDSum()
    {
        return usdSum;
    }

    public float GetRielSum()
    {
        return rielSum;
    }

    public List<Note> GetUSDAnswer()
    {
        return usdAnswer;
    }

    public List<Note> GetRielAnswer()
    {
        return rielAnswer;
    }

    public void setRielSum(float rielsValue)
    {

    }

    public ProblemAndAnswer(float valueInRiels)
    {

        //the problem value must be a multiple of the smallest note denomination so that the problem is exactly solvable. Default is 100 riels.
        if (valueInRiels % GameManager.instance.smallestNoteDenomination == 0)
        {
            rielSum = valueInRiels;
        }
        else
        {
            // automatically rounds down the value if it is not.
            rielSum = valueInRiels - valueInRiels % GameManager.instance.smallestNoteDenomination;
        }

        usdSum = rielSum / GameManager.instance.rielsToUSDExchangeRate; //default 4000.

		ConvertSumsToAnswers();
    }


    public void ConvertSumsToAnswers()
    {
        float remainingUSD = usdSum;
        float remainingRiels = rielSum;



        for (int i = 0; i < (remainingUSD - remainingUSD % 10) / 10; i++)
        {
            usdAnswer.Add(wallet.typesOfNotesUSD[0]);
        }

        remainingUSD = remainingUSD - 10 * (remainingUSD - remainingUSD % 10) / 10;

        for (int i = 0; i < (remainingUSD - remainingUSD % 5) / 5; i++)
        {
            usdAnswer.Add(wallet.typesOfNotesUSD[1]);
        }

        remainingUSD = remainingUSD - 5 * (remainingUSD - remainingUSD % 5) / 5;

        for (int i = 0; i < (remainingUSD - remainingUSD % 2) / 2; i++)
        {
            usdAnswer.Add(wallet.typesOfNotesUSD[2]);
        }

        remainingUSD = remainingUSD - 2 * (remainingUSD - remainingUSD % 2) / 2;

        for (int i = 0; i < (int)remainingUSD; i++)
        {
            usdAnswer.Add(wallet.typesOfNotesUSD[3]);
        }
		

        for (int i = 0; i < (remainingRiels - remainingRiels % 5000) / 5000; i++)
        {
            rielAnswer.Add(wallet.typesOfNotesRiels[0]);
        }

        remainingRiels = remainingRiels - 5000 * (remainingRiels - remainingRiels % 5000) / 5000;

        for (int i = 0; i < (remainingRiels - remainingRiels % 1000) / 1000; i++)
        {
            rielAnswer.Add(wallet.typesOfNotesRiels[1]);
        }

        remainingRiels = remainingRiels - 1000 * (remainingRiels - remainingRiels % 1000) / 1000;

        for (int i = 0; i < (remainingRiels - remainingRiels % 500) / 500; i++)
        {
            rielAnswer.Add(wallet.typesOfNotesRiels[2]);
        }

        remainingRiels = remainingRiels - 500 * (remainingRiels - remainingRiels % 500) / 500;

        for (int i = 0; i < remainingRiels/100; i++)
        {
            rielAnswer.Add(wallet.typesOfNotesRiels[3]);
        }





    }

}
