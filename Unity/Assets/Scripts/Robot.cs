﻿using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		GetComponent<Animation>().Play ("Robot_animation");
	}
}
