using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
	public int roundNumber = 1;
	public Round[] rounds;
	public Wave[] currentRoundWaves {
		get { return rounds[roundNumber - 1].roundWaves; }
	}

	public void NextRound() {
		roundNumber++;
	}

}
