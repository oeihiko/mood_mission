﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusBars : MonoBehaviour {


	public RectTransform healthTransform;
	public RectTransform courTransform;
	public RectTransform compTransform;
	private float cachedYHealth;
	private float minXValueHealth;
	private float maxXValueHealth;
	private float cachedYCour;
	private float minXValueCour;
	private float maxXValueCour;
	private float cachedYComp;
	private float minXValueComp;
	private float maxXValueComp;
	private int currentHealth;
	private int currentCourage;
	private int currentCompassion;


	private int CurrentHealth
	{
		get 
		{ 
			return currentHealth; 
		}
		set 
		{
			currentHealth = value;
			HandleHealth();
		}
	}

	private int CurrentCompassion
	{
		get 
		{ 
			return currentCompassion; 
		}
		set 
		{
			currentCompassion = value;
			HandleCompassion();
		}
	}

	private int CurrentCourage
	{
		get 
		{ 
			return currentCourage; 
		}
		set 
		{
			currentCourage = value;
			HandleCourage();
		}
	}
	public float maxHealth;
	public Text healthText;
	public Image visualHealth; //This field is for setting the color of the healthbar
	public float maxCourage;
	public Text courageText;
	public Image visualCourage;
	public float maxCompassion;
	public Text compassionText;
	public Image visualCompassion;

	public float damageCooldown;
	private bool onCD;
	// Use this for initialization
	void Start () {
		cachedYHealth = healthTransform.position.y;
		maxXValueHealth = healthTransform.position.x;
		minXValueHealth = healthTransform.position.x - healthTransform.rect.width;
		currentHealth = (int)maxHealth;

		cachedYCour = courTransform.position.y;
		maxXValueCour = courTransform.position.x;
		minXValueCour = courTransform.position.x - courTransform.rect.width;

		cachedYComp = compTransform.position.y;
		maxXValueComp = compTransform.position.x;
		minXValueComp = compTransform.position.x - compTransform.rect.width;

		currentCompassion = (int)maxCompassion;
		currentCourage = (int)maxCourage;

		onCD = false;

	}

	
	// Update is called once per frame
	void Update () {

	}

	public void DecreaseStat (int amount, string stat) 
	{

		int counter = 0;
		switch (stat) {
		case "Health":
			if (!onCD && currentHealth > 0)
			{
				StartCoroutine(CoolDownDmg(amount, true, stat));
			}
			break;
		case "Courage":
			if (!onCD && currentCourage > 0)
			{
				StartCoroutine(CoolDownDmg(amount, true, stat));
			}
			break;
		case "Compassion":
			if (!onCD && currentCompassion > 0)
			{
				StartCoroutine(CoolDownDmg(amount, true, stat));
			}
			break;
		}

	}

	public void IncreaseStat (int amount, string stat) 
	{
		int counter = 0;
		if (!onCD && currentHealth < maxHealth)
		{
				StartCoroutine(CoolDownDmg(amount, false, stat));
		}
		switch (stat) {
		case "Health":
			if (!onCD && currentHealth < maxHealth)
			{
				StartCoroutine(CoolDownDmg(amount, false, stat));
			}
			break;
		case "Courage":
			if (!onCD && currentCourage < maxCourage)
			{
				StartCoroutine(CoolDownDmg(amount, false, stat));
			}
			break;
		case "Compassion":
			if (!onCD && currentCompassion < maxCompassion)
			{
				StartCoroutine(CoolDownDmg(amount, false, stat));
			}
			break;
		}
	}
	IEnumerator CoolDownDmg(int counter, bool isDamage, string stat) 
	{
		while(counter > 0) {
			onCD = true;
			switch (stat) {
			case "Health":
				if(isDamage && currentHealth > 0) {
				
					CurrentHealth -= 1;
				}
				else if(!isDamage && currentHealth < maxHealth) {
					CurrentHealth += 1;
				}
				break;
			case "Courage":
				if(isDamage && currentCourage > 0) {
					
					CurrentCourage -= 1;
				}
				else if(!isDamage && currentCourage < maxCourage) {
					CurrentCourage += 1;
				}
			break;
			case "Compassion":
				if(isDamage && currentCompassion > 0) {
					
					CurrentCompassion -= 1;
				}
				else if(!isDamage && currentCompassion < maxCompassion) {
					CurrentCompassion += 1;
				}
			break;
			}

			yield return new WaitForSeconds (damageCooldown);
			onCD = false;
			counter--;
		}
	}

	/*void OnTriggerStay(Collider other) 
	{
		if(other.gameObject.tag == "Player")
		{
			//Debug.Log(onCD + "  " + currentHealth);
			if (!onCD && currentHealth > 0)
			{

				StartCoroutine(CoolDownDmg(0));
				CurrentHealth -= 1;
			}
		}
	}*/


	void HandleHealth()
	{
		healthText.text = "Health: " + currentHealth;

		float currentXValue = MapValues (currentHealth, 0, maxHealth, minXValueHealth, maxXValueHealth);

		healthTransform.position = new Vector3 (currentXValue, cachedYHealth);
	}

	void HandleCompassion()
	{
		compassionText.text = "Compassion: " + currentCompassion;
		
		float currentXValue = MapValues (currentCompassion, 0, maxCompassion, minXValueComp, maxXValueComp);
		
		compTransform.position = new Vector3 (currentXValue, cachedYComp);
	}

	void HandleCourage()
	{
		courageText.text = "Courage: " + currentCourage;
		
		float currentXValue = MapValues (currentCourage, 0, maxCourage, minXValueCour, maxXValueCour);
		
		courTransform.position = new Vector3 (currentXValue, cachedYCour);
	}

	float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
