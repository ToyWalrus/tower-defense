using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {
	public int money { get; private set; }
	public int score { get; private set; }

	private List<Weapon> weapons;

	public Player() {
		weapons = new List<Weapon>();
		money = 100;
		score = 0;
	}

	public bool CanAfford(int cost) {
		return money >= cost;
	}

	public void BuyItem(Weapon weapon, int cost) {
		money -= cost;
		weapons.Add(weapon);
	}

	public void AddMoney(int amount) { money += amount; }

	public List<Weapon> GetTurrets() { return weapons; }
}
