﻿using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour 
{
	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	public void StartGame()
	{
        Application.LoadLevel("IntroCine");
	}

    public void Survival()
    {
        Application.LoadLevel("FirstGame");
    }

    public void Multiplayer()
    {
        Application.LoadLevel("Multijoueur");
    }

	public void ExitGame()
	{
		Application.Quit ();
	}

    public void settingGame()
    {

    }

    public void website()
    {
		Application.OpenURL("http://cosmichumanoids.free.fr/");
    }

}
