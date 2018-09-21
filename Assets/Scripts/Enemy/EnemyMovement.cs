using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float speed = 3;

	private GameObject waypoints;
	private int waypointIndex = 0;
	private Transform target;

	void Start() {
		waypoints = GameObject.FindGameObjectWithTag(GetComponent<Enemy>().enemyType.ToString() +
			"WaypointArray");
		if (waypoints == null) {
			Destroy(gameObject);
			return;
		}

		target = waypoints.transform.GetChild(waypointIndex);
	}

	void Update() {
		Vector2 direction = target.position - transform.position;
		transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

		if (ReachedWaypoint()) {
			waypointIndex++;
			if (waypointIndex == waypoints.transform.childCount) {
				Destroy(gameObject);
				return;
			}
			target = waypoints.transform.GetChild(waypointIndex);
		}
	}

	bool ReachedWaypoint(float dist = 0.1f) {
		Vector2 diff = target.position - transform.position;
		Vector2 epsilon = new Vector2(dist, dist);
		return Mathf.Abs(diff.x) < epsilon.x && Mathf.Abs(diff.y) < epsilon.y;
	}
}