using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class JerryHaMuerto : MonoBehaviour {

	// Use this for initialization
	public void BotonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BotonSalir()
    {
        Application.Quit();
    }
}
