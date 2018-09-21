using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour {
	public GameObject turret;
	private int price = 0;
	private bool freelyPlaceable;

	void Update () {
		if (turret == null) return;
		Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		turret.transform.position = freelyPlaceable ?
			new Vector3(currentPos.x, currentPos.y, 0) :
			new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), 0); // snap-to-grid effect

		if (Input.GetMouseButtonDown(0) && IsValidPlacement(turret.transform.position)) {
			GameManager.instance.ItemPlaced(turret, price);
			RemoveItemRef();
		} else if (Input.GetKey(KeyCode.Escape)) { // cancel buy
			Destroy(turret);
			RemoveItemRef();
		}
	}

	public void SetItem(GameObject turret, int price, bool freelyPlaceable) {
		this.turret = turret;
		this.price = price;
		this.freelyPlaceable = freelyPlaceable;
	}

	private bool IsValidPlacement(Vector2 position) {
		return true;
	}

	private void RemoveItemRef() {
		turret = null;
		price = 0;
		freelyPlaceable = false;
	}
}
