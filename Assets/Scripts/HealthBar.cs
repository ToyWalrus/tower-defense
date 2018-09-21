using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public Color healthyColor = Color.green;
	public Color midColor = Color.yellow;
	public Color dangerColor = Color.red;

	private float health;
	private float maxHealth;
	private RectTransform bar;
	private RawImage barImage;

	// Use this for initialization
	void Start() {
		bar = transform.GetChild(0).GetComponent<RectTransform>();
		barImage = bar.GetComponent<RawImage>();

		barImage.color = GetHealthColor();
	}

	public void Init(float health) {
		maxHealth = health;
		this.health = health;
	}

	public void UpdateHealth(float health) {
		this.health = health;

		bar.anchorMax = new Vector2(health / maxHealth, 1);
		bar.offsetMax = new Vector2(0, 0);
		barImage.color = GetHealthColor();
	}

	// gives color gradient
	private Color GetHealthColor() {
		float halfHp = maxHealth / 2;
		Color healthColor;
		if (health >= halfHp) {
			float midColorPercent = maxHealth - health;
			float healthyColorPercent = halfHp - midColorPercent;
			healthColor = (healthyColor * healthyColorPercent + midColor * midColorPercent) / halfHp;
		} else {
			float dangerColorPercent = halfHp - health;
			float midColorPercent = health;
			healthColor = (midColor * midColorPercent + dangerColor * dangerColorPercent) / halfHp;
		}

		return healthColor;
	}
}