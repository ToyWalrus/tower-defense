using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float health = 10;
	public EnemyType enemyType = EnemyType.Ground;
	public HealthBar healthBar;

	void Start() {
		healthBar.Init(health);
	}

	void Update() {
		if (health <= 0) {
			Destroy(gameObject);
			Debug.Log("Kaboom went the enemy");
		}
	}

	public void TakeDamage(float damage) {
		health -= damage;
		healthBar.UpdateHealth(health);
		// play sound
	}

}

public enum EnemyType {
	Ground,
	Air
}