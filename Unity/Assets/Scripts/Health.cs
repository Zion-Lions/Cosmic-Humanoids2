﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

	public int health;

	void Start ()
    {
		if (gameObject.name == "Robot Kyle")
				health = Random.Range(30, 50);
		else
				health = 100;
	}
	
	void Update ()
    {
	    
	}

    public void ApplyDamage(int Damage)
    {
        health -= Damage;

        if (health <= 0)
        {
            Destroy(gameObject);
			Application.LoadLevel("TheEnd");
        }
    }
}
