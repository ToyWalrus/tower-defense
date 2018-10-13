using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour {
	public CompositeCollider2D enemyPath;

	private GameObject itemToPlace;
	private int price = 0;
	private bool freelyPlaceable;

	void Update () {
		if (itemToPlace == null) return;
		Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 itemPlacement = freelyPlaceable ?
			new Vector3(currentPos.x, currentPos.y, 0) :
			new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), 0); // snap-to-grid effect

		itemToPlace.transform.position  = itemPlacement;
		DisplayValidPlacement(itemPlacement);	

		if (Input.GetMouseButtonDown(0) && IsValidPlacement(itemToPlace.transform.position)) {
			GameManager.instance.ItemPlaced(itemToPlace, price);
			RemoveItemRef();
		} else if (Input.GetKey(KeyCode.Escape)) { // cancel buy
			Destroy(itemToPlace);
			RemoveItemRef();
		}
	}

	public void SetItem(GameObject item, int price, bool freelyPlaceable) {
		this.itemToPlace = item;
		this.price = price;
		this.freelyPlaceable = freelyPlaceable;
	}

	private void DisplayValidPlacement(Vector2 position) {
		SpriteRenderer renderer = itemToPlace.GetComponent<SpriteRenderer>();
		Color color = renderer.color;
		if (IsValidPlacement(position)) {
			color.a = 1f;
		} else {
			color.a = 0.5f;
		}
		renderer.color = color;
	}

	private bool IsValidPlacement(Vector2 position) {
		return freelyPlaceable || !enemyPath.OverlapPoint(position); //!path.bounds.Contains(position);
	}

	private void RemoveItemRef() {
		itemToPlace = null;
		price = 0;
		freelyPlaceable = false;
	}
}