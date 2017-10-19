using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMov : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float rotationX, rotationY, rotationZ;

    // Use this for initialization
    void Start()
    {
        //Obtenemos el offset de la cámara mientras sigue al prota.
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}