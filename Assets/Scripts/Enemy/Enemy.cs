using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float health = 10;
	public int value = 10;
	public Type enemyType = Type.Ground;
	public HealthBar healthBar;

	void Start() {
		healthBar.Init(health);
	}

	public void TakeDamage(float damage) {
		health -= damage;
		healthBar.UpdateHealth(health);

		if (health <= 0) {
			// play sound ?
			GameManager.instance.OnEnemyDestroyed(this);
			Destroy(gameObject);			
		}
	}

}

public enum Type {
	Ground,
	Air
}