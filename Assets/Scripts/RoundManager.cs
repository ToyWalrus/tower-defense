using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
	[Range(1, 5)]
	public int roundNumber = 1;


	public void NextRound() {
		roundNumber++;
	}

}
