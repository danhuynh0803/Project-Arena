﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 2.0f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.
	
	
	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
//	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	SpriteRenderer render; 
	bool lostAlpha; 
	
	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
//		enemyHealth = GetComponent<EnemyHealth>();
//		anim = GetComponent <Animator> ();
		render = player.GetComponent<SpriteRenderer> (); 
	}
	
	
	void OnTriggerEnter2D (Collider2D other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}
		
	
	void OnTriggerExit2D (Collider2D other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		if (timer >= timeBetweenAttacks && lostAlpha) 
		{
			player.GetComponent<Renderer>().material.color += new Color(0, 0, 0, .50f);
			lostAlpha = false;
		}
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange && lostAlpha == false) //&& enemyHealth.currentHealth > 0)
		{
			Attack ();
			player.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, .50f); 
			lostAlpha = true; 
		}
		
		// If the player has zero or less health...
		if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			// anim.SetTrigger ("PlayerDead");
		}
	}
	
	void Attack ()
	{
		// Reset the timer.
		timer = 0f;
		
		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackDamage);
		}
	}
}

	
	