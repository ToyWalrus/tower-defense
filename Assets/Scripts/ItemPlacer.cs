using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour {
	public GameObject item;
	private int price = 0;
	private bool freelyPlaceable;

	void Update() {
		if (item == null) return;
		Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		item.transform.position = freelyPlaceable ?
			new Vector3(currentPos.x, currentPos.y, 0) :
			new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), 0); // snap-to-grid effect

		if (Input.GetMouseButtonDown(0) && IsValidPlacement(item.transform.position)) {
			GameManager.instance.ItemPlaced(item, price);
			RemoveItemRef();
		} else if (Input.GetKey(KeyCode.Escape)) { // cancel buy
			Destroy(item);
			RemoveItemRef();
		}
	}

	public void SetItem(GameObject item, int price, bool freelyPlaceable) {
		this.item = item;
		this.price = price;
		this.freelyPlaceable = freelyPlaceable;
	}

	private bool IsValidPlacement(Vector2 position) {
		return true;
	}

	private void RemoveItemRef() {
		item = null;
		price = 0;
		freelyPlaceable = false;
	}
}