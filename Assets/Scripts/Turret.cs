using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Weapon {
	public Type turretType = Type.Ground;
	public Transform cannon;
	public SpriteRenderer turretRadius;
	public GameObject projectile;

	[Range(0.1f, 5), Tooltip("Number of shots per second")]
	public float fireRate = 1;
	public float damage = 3;
	public float range = 3.75f;
	public float turnSpeed = 15;

	private Transform target = null;
	private float inverseFireRate;
	public int id { get; private set; }

	void OnEnable() {
		InvokeRepeating("UpdateTarget", 0, 0.5f);
		InvokeRepeating("Fire", 0, inverseFireRate);
	}

	void OnDisable() {
		CancelInvoke("UpdateTarget");
		CancelInvoke("Fire");
	}

	void Start() {
		inverseFireRate = 1 / fireRate;

		float radiusRatio = .2f; // scale is 1:5 between image scale and turret radius
		turretRadius.transform.localScale = new Vector2(radiusRatio * range, radiusRatio * range);
	}

	public void SetID(int num) { id = num; }

	void Fire() {
		if (target == null || !active) return;
		GameObject fired = Instantiate(projectile, cannon.position, transform.rotation);
		fired.tag = turretType.ToString() + "Projectile";
		fired.GetComponent<Projectile>().damage = damage;
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
		float zRotation = Mathf.LerpAngle(curAngle, targetAngle, Time.deltaTime * turnSpeed);
		transform.rotation = Quaternion.Euler(0, 0, zRotation);
	}

	void OnMouseDown() {
		GameManager.instance.FocusTurret(this);
	}

	void OnDrawGizmosSelected() {
		// Display the turret radius when selected
		Gizmos.color = turretType == Type.Ground ? Color.red : Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public void ShowRadius(bool show) {
		turretRadius.enabled = show;
	}
}