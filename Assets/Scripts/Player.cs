using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {
	public int money { get; private set; }
	public int score { get; private set; }

	public List<Turret> turrets;

	public Player() {
		turrets = new List<Turret>();
		money = 100;
		score = 0;
	}

	public bool BuyItem(Turret turret, int cost) {
		if (cost > money) return false;
		money -= cost;
		turrets.Add(turret);
		return true;
	}
}
