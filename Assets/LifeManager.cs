using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public static LifeManager Instance;

    public bool isBuffed;   // Tengo bufo
    public float buffTime;  // Tiempo que dura el bufo
    public float counter;    // Contador de tiempo del bufo.

    [Tooltip("Slider para la vida del jugador")]
    [SerializeField]
    private Slider LifeSlider;
    [Tooltip("Slider para el bufo")]
    [SerializeField]
    private Slider BuffSlider;

    // Singleton para el life manager.
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        isBuffed = false;
    }

    // Player recibe daño
    public void GetHit(int damage)
    {
        LifeSlider.value -= damage;
        if(LifeSlider.value <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    // Player se cura
    public void GetHealed(int healValue)
    {
        LifeSlider.value += healValue;
    }
    
    // Rellenamos la vida.
    public void FillLifeSlider()
    {
        LifeSlider.value = LifeSlider.maxValue;
    }

    public void BuffPlayer()
    {
        BuffSlider.gameObject.SetActive(true);
        BuffSlider.value = BuffSlider.maxValue;
        isBuffed = true;
    }

    private void Update()
    {
        if (isBuffed)
        {
            counter += Time.deltaTime;
            if(counter >= buffTime)
            {
                isBuffed = false;
                BuffSlider.gameObject.SetActive(false);
            }
            else
            {
                float value = (LifeSlider.maxValue / buffTime) * Time.deltaTime;
                BuffSlider.value -= value;
            }
        }
    }
}
