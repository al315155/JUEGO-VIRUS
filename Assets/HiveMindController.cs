using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindController : MonoBehaviour {

    [Tooltip("Lista de enemigos de la escena")]
    public List<GameObject> enemies = new List<GameObject>();

    [Tooltip("Rango de búsqueda de enemigos cercanos")]
    public float range;

    public static HiveMindController Instance;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public List<GameObject> GetNearEnemies(GameObject go)
    {
        List<GameObject> nearEnemies = new List<GameObject>();
        foreach(GameObject g in enemies)
        {
            if(Vector3.Distance(go.transform.position, g.transform.position) < range){
                nearEnemies.Add(g);
            }
        }

        return nearEnemies;
    }

    public void RemoveEnemy(GameObject g)
    {
        enemies.Remove(g.gameObject);
    }

}
