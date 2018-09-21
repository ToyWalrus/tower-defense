using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {
	public int roundNumber = 1;
	public float spawnInterval = 0.5f;
	public Button nextWaveButton;
	public GameObject enemySpawners;
	public bool waveActive {
		get {
			return GameObject.FindGameObjectsWithTag("GroundEnemy").Length > 0 ||
				GameObject.FindGameObjectsWithTag("AirEnemy").Length > 0;
		}
	}

	private EnemySpawner[] spawners;

	void Start() {
		spawners = enemySpawners.GetComponentsInChildren<EnemySpawner>();
	}

	void OnEnable() {
		nextWaveButton.onClick.AddListener(SpawnWave);
	}

	void OnDisable() {
		nextWaveButton.onClick.RemoveListener(SpawnWave);
	}

	void SpawnWave() {
		int enemiesToSpawn = roundNumber * 3;
		foreach (EnemySpawner spawner in spawners) {
			spawner.StartSpawn(spawnInterval, enemiesToSpawn);
		}

		InvokeRepeating("CheckForActiveWave", enemiesToSpawn * spawnInterval, 0.5f); // only begin checking after all enemies have spawned
		SetButtonDisabled(true);
	}

	void CheckForActiveWave() {
		if (!waveActive) {
			SetButtonDisabled(false);
			roundNumber++;
			CancelInvoke("CheckForActiveWave");
		}
	}

	void SetButtonDisabled(bool disabled) {
		nextWaveButton.interactable = !disabled;
		Text buttonText = nextWaveButton.GetComponentInChildren<Text>();
		if (disabled) {
			buttonText.text = "Wave In Progress";
		} else {
			buttonText.text = "Spawn Next Wave";
		}			
	}
}
