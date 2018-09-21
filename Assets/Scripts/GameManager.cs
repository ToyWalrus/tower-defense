using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int roundNumber = 1;
	public int livesRemaining = 10;
	public Player player;

	public Text livesRemainingField;
	public Text moneyField;
	public Button nextWaveButton;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		if (player == null) {
			player = new Player();
		}

		livesRemainingField.text = "Lives Remaining: " + livesRemaining;
		moneyField.text = "Money: $" + player.money;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShopItemClicked(Turret turret, int price) {
		if (player.BuyItem(turret, price)) {
			moneyField.text = "Money: $" + player.money;
			// play sound or something
		} else {
			// play other sound
		}
	}
}
