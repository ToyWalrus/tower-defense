using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	private int enemiesToSpawn;
	private int enemiesSpawned = 0;

	public void StartSpawn(float interval, int numEnemies) {
		enemiesToSpawn = numEnemies;
		enemiesSpawned = 0;
		InvokeRepeating("SpawnEnemy", 0, interval);
	}

	void SpawnEnemy() {
		if (enemiesSpawned == enemiesToSpawn) {
			CancelInvoke("SpawnEnemy");	
			return;
		}

		Instantiate(enemyPrefab, transform.position, transform.rotation);
		enemiesSpawned++;
	}
}