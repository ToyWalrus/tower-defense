using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Weapon {
	public float damage = 2;

	void OnTriggerEnter2D(Collider2D collider) {
		if (!active) return;
		Enemy enemy = collider.GetComponent<Enemy>();
		if (enemy.enemyType == EnemyType.Ground) {
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}