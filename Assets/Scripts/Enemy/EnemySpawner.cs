using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	private int enemiesToSpawn;
	private int enemiesSpawned = 0;
	private GameObject[] enemyTypes;

	public void StartSpawn(float interval, int numEnemies, GameObject[] enemies) {
		enemiesToSpawn = numEnemies;
		enemiesSpawned = 0;
		enemyTypes = enemies;
		InvokeRepeating("SpawnEnemy", 0, interval);
	}

	GameObject GetRandomEnemy() {
		return enemyTypes[Mathf.FloorToInt(Random.Range(0, enemyTypes.Length))];
	}

	void SpawnEnemy() {
		if (enemiesSpawned == enemiesToSpawn) {
			CancelInvoke("SpawnEnemy");	
			return;
		}

		Instantiate(GetRandomEnemy(), transform.position, transform.rotation);
		GameManager.instance.EnemySpawned();
		enemiesSpawned++;
	}
}