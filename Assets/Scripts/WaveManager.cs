using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {
	public Button nextWaveButton;
	public EnemySpawner enemySpawner;
	public int waveIndex = 0;
	private Wave currentWave;	

	private Wave[] waves {
		get { return GameManager.instance.roundManager.currentRoundWaves; }
	}


	void OnEnable() {
		nextWaveButton.onClick.AddListener(SpawnWave);
	}

	void OnDisable() {
		nextWaveButton.onClick.RemoveListener(SpawnWave);
	}

	void SpawnWave() {
		currentWave = waves[waveIndex++];
		int enemiesToSpawn = currentWave.count;
		float interval = 1f / currentWave.rate;
		
		enemySpawner.StartSpawn(interval, enemiesToSpawn, currentWave.enemyTypes);

		InvokeRepeating("CheckForActiveWave", enemiesToSpawn * interval, 0.5f); // only begin checking after all enemies have spawned
		SetButtonDisabled(true);
	}

	public bool WaveIsActive() {
		return GameManager.instance.aliveEnemiesCount > 0;		
	}

	void CheckForActiveWave() {
		if (!WaveIsActive()) {
			SetButtonDisabled(false);
			CancelInvoke("CheckForActiveWave");
		}
	}

	void SetButtonDisabled(bool disabled) {
		nextWaveButton.interactable = !disabled;
		if (disabled) {
			SetButtonText("Wave In Progress");
		} else {
			if (waveIndex == waves.Length) {
				SetButtonText("Begin Next Round");
				GameManager.instance.UpdateRound();
				waveIndex = 0;
			} else {
				SetButtonText("Spawn Next Wave");
			}
		}			
	}

	void SetButtonText(string text) {
		Text buttonText = nextWaveButton.GetComponentInChildren<Text>();
		buttonText.text = text;
	}
}
