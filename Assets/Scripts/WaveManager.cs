using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {
	public Button nextWaveButton;
	public EnemySpawner enemySpawner;
	public Wave[] waves;
	public int waveIndex {
		get { return GameManager.instance.roundNumber - 1; }
	}

	public bool waveActive {
		get {
			return GameObject.FindGameObjectsWithTag("GroundEnemy").Length > 0 ||
				GameObject.FindGameObjectsWithTag("AirEnemy").Length > 0;
		}
	}

	private Wave currentWave;

	void OnEnable() {
		nextWaveButton.onClick.AddListener(SpawnWave);
	}

	void OnDisable() {
		nextWaveButton.onClick.RemoveListener(SpawnWave);
	}

	void SpawnWave() {
		currentWave = waves[waveIndex];
		int enemiesToSpawn = currentWave.count;
		float interval = 1f / currentWave.rate;
		
		enemySpawner.StartSpawn(interval, enemiesToSpawn, currentWave.enemyTypes);

		InvokeRepeating("CheckForActiveWave", enemiesToSpawn * interval, 0.5f); // only begin checking after all enemies have spawned
		SetButtonDisabled(true);
	}

	void CheckForActiveWave() {
		if (!waveActive) {
			SetButtonDisabled(false);
			CancelInvoke("CheckForActiveWave");
		}
	}

	void SetButtonDisabled(bool disabled) {
		nextWaveButton.interactable = !disabled;
		if (disabled) {
			SetButtonText("Wave In Progress");
		} else {
			SetButtonText("Spawn Next Wave");
		}			
	}

	public void SetButtonText(string text) {
		Text buttonText = nextWaveButton.GetComponentInChildren<Text>();
		buttonText.text = text;
	}
}
