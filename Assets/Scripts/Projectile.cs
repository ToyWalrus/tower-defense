using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float damage = 2;
	public float speed = 10;

	void Start() {
		Invoke("DestroyProjectile", 3);
	}

	void Update() {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if ((this.tag == "GroundProjectile" && enemy.enemyType == Type.Ground) ||
			(this.tag == "AirProjectile" && enemy.enemyType == Type.Air)) {
			enemy.TakeDamage(damage);
			DestroyProjectile();
		}
	}

	void DestroyProjectile() {
		Destroy(gameObject);
	}
}