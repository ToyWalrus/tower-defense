using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float spawnInterval;
	public int enemiesToSpawn;

	private int enemiesSpawned = 0;

	// Use this for initialization
	void Start() {
		InvokeRepeating("SpawnEnemy", 0, spawnInterval);
	}

	void SpawnEnemy() {
		if (enemiesSpawned == enemiesToSpawn)
			return;

		Instantiate(enemyPrefab, transform.position, transform.rotation);
		enemiesSpawned++;
	}
}