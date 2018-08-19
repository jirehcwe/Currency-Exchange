using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemAndAnswer {

	private float usdSum;
	private float rielSum;
	private List<Note> usdAnswer;
	private List<Note> rielAnswer;

	public Wallet wallet;

	public float GetUSDSum(){
		return usdSum;
	}

	public float GetRielSum(){
		return rielSum;
	}

	public List<Note> GetUSDAnswer(){
		return usdAnswer;
	}

	public List<Note> GetRielAnswer(){
		return rielAnswer;
	}

	public void setRielSum(float rielsValue){

	}

	public ProblemAndAnswer(float valueInRiels){

		//the problem value must be a multiple of the smallest note denomination so that the problem is exactly solvable. Default is 100 riels.
		if (valueInRiels % GameManager.instance.smallestNoteDenomination == 0){
			rielSum = valueInRiels;
		} else {
			// automatically rounds down the value if it is not.
			rielSum = valueInRiels - valueInRiels % GameManager.instance.smallestNoteDenomination;
		}

		usdSum = rielSum/GameManager.instance.rielsToUSDExchangeRate; //default 4000.
	}


	public void ConvertSumsToAnswers(){
		for (int i=0;i<(usdSum-usdSum%10)/10;i++){
			usdAnswer.Add();
		}

	}

}
