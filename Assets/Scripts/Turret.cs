using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Weapon {
	public EnemyType turretType = EnemyType.Ground;
	public Transform cannon;
	public GameObject projectile;

	[Range(0.1f, 4)]
	public float fireRate = 1;
	public float damage = 3;
	public float range = 3.75f;

	private Transform target = null;
	public int id { get; private set; }
	

	void Start() {
		InvokeRepeating("UpdateTarget", 0, 0.5f);
	}

	public void SetID(int num) { id = num; }

	void Fire() {
		if (target == null || !active) {
			CancelInvoke("Fire");
			return;
		};
		GameObject fired = Instantiate(projectile, cannon.position, transform.rotation);
		fired.tag = turretType.ToString() + "Projectile";
	}

	void UpdateTarget() {
		if (target != null || !active) return;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(turretType.ToString() + "Enemy");
		Vector3 position = transform.position;

		// target first enemy found
		// later functionality could have user select how targeting works
		foreach (GameObject enemy in enemies) {
			if (Vector3.Distance(enemy.transform.position, position) <= range) {
				target = enemy.transform;
				InvokeRepeating("Fire", 0.4f, fireRate);
				return;
			}
		}

		target = null;
	}

	void Update() {
		if (!active || 
				target == null ||
				Vector3.Distance(target.transform.position, transform.position) > range) {
			target = null;
			return;
		}

		float rads = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x);
		float targetAngle = Mathf.Rad2Deg * rads - 90;
		float curAngle = transform.rotation.eulerAngles.z;
		float zRotation = Mathf.LerpAngle(curAngle, targetAngle, Time.deltaTime * 15);
		transform.rotation = Quaternion.Euler(0, 0, zRotation);
	}

	void OnDrawGizmosSelected() {
		// Display the turret radius when selected
		Gizmos.color = turretType == EnemyType.Ground ? Color.red : Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}