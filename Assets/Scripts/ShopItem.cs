using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShopItem : MonoBehaviour {
	public string itemName;
	public int itemPrice = 10;
	public Turret turret;

	void Start() {		
		Text[] textFields = GetComponentsInChildren<Text>();
		textFields[0].text = itemName;
		textFields[1].text = "$" + itemPrice;

		SpriteRenderer renderer =  turret.GetComponent<SpriteRenderer>();
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
		GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		gameManager.ShopItemClicked(turret, itemPrice);
	}
}
