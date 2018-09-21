using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShopItem : MonoBehaviour {
	public string itemName;
	public int itemPrice = 10;
	public GameObject turretPrefab;
	public bool freelyPlaceable = false;

	void Start() {		
		Text[] textFields = GetComponentsInChildren<Text>();
		textFields[0].text = itemName;
		textFields[1].text = "$" + itemPrice;

		SpriteRenderer renderer =  turretPrefab.GetComponent<SpriteRenderer>();
		Image image = GetComponentsInChildren<Image>()[1];
		image.sprite = renderer.sprite;
		image.material = renderer.sharedMaterial;
	}

	void OnEnable() {
		GetComponent<Button>().onClick.AddListener(ItemClicked);
	}

	void OnDisable() {
		GetComponent<Button>().onClick.RemoveListener(ItemClicked);
	}

	void ItemClicked() {
		if (CanBuy()) {
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameManager.instance.ShopItemClicked(Instantiate(turretPrefab, pos, Quaternion.identity), itemPrice, freelyPlaceable);
		}
	}

	private bool CanBuy() {
		return GameManager.instance.player.CanAfford(itemPrice);
	}
}
