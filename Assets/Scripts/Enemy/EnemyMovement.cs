using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float speed = 3;

	private GameObject waypoints;
	private int waypointIndex = 0;
	private Transform target;

	private Transform aimSpot;

	void Start() {
		waypoints = GameObject.FindGameObjectWithTag(GetComponent<Enemy>().enemyType.ToString() +
			"WaypointArray");
		if (waypoints == null) {
			Destroy(gameObject);
			return;
		}

		target = waypoints.transform.GetChild(waypointIndex);
		aimSpot = GetComponent<Enemy>().aimSpot;
		
		Vector2 dir = target.position - transform.position;
		RotateTowardWaypoint(dir.normalized);
	}

	void Update() {
		Vector2 direction = target.position - transform.position;
		transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

		if (ReachedWaypoint()) {
			waypointIndex++;
			if (waypointIndex == waypoints.transform.childCount) {
				GameManager.instance.OnEnemyReachedEnd(gameObject.GetComponent<Enemy>());
				Destroy(gameObject);
				return;
			}
			target = waypoints.transform.GetChild(waypointIndex);
			Vector2 newDir = target.position - transform.position;
			RotateTowardWaypoint(newDir.normalized);
		}
	}

	bool ReachedWaypoint(float dist = 0.1f) {
		Vector2 diff = target.position - transform.position;
		return Mathf.Abs(diff.x) < dist && Mathf.Abs(diff.y) < dist;
	}

	void RotateTowardWaypoint(Vector2 dirVector) {
		float colliderRadius = GetComponent<CircleCollider2D>().radius;
		aimSpot.transform.localPosition = dirVector * colliderRadius;
	}
}