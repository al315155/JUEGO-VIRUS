﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public void BotonPlay()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void BotonSalir()
    {
        Application.Quit();
    }
}
