using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int roundNumber { get { return waveManager.roundNumber; } }
	public int livesRemaining = 10;
	public Player player;

	public Text livesRemainingField;
	public Text moneyField;
	public ItemPlacer turretPlacer;
	public WaveManager waveManager;

	private Turret focusedTurret;

	// Use this for initialization
	void Start() {
		DontDestroyOnLoad(gameObject);
		if (instance == null) {
			instance = this;
		}

		if (player == null) {
			player = new Player();
		}

		livesRemainingField.text = "Lives Remaining: " + livesRemaining;
		moneyField.text = "Money: $" + player.money;
	}

	public void ShopItemClicked(GameObject item, int price, bool freelyPlaceable) {
		turretPlacer.SetItem(item, price, freelyPlaceable);
		Turret turret = item.GetComponent<Turret>();
		if (focusedTurret != null)
			focusedTurret.ShowRadius(false);
		focusedTurret = turret;
		if (turret != null)
			turret.ShowRadius(true);
	}

	public void ItemPlaced(GameObject item, int price) {
		Weapon weapon = item.GetComponent<Weapon>();
		player.BuyItem(weapon, price);
		moneyField.text = "Money: $" + player.money;
		weapon.SetActive(true);
	}

	public void FocusTurret(Turret turret) {
		if (turret == null) {
			if (focusedTurret != null) focusedTurret.ShowRadius(false);
			focusedTurret = null;
			return;
		}

		if (focusedTurret != null) focusedTurret.ShowRadius(false);
		focusedTurret = turret;
		focusedTurret.ShowRadius(true);
	}
}