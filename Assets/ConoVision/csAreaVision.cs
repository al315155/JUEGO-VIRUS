using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class csAreaVision : MonoBehaviour {

	public int angulo = 45;                 // Ángulo de visión
	public int rango  = 5;                  // Rango de visión
    private float timeForOutOfSight = 10f;  // Cuánto tiempo tiene que pasar para que nos escapemos.
    private float scapeCounter;             // Contador que nos dice si nos hemos conseguido escapar.
    private bool isScaping;                 // Nos dice si el jugador está escapando del enemigo.

    [SerializeField]
    private ConeDecision m_ConeDecision;
    private StateController m_stateController;
    private ChangeConeMaterial m_changeConeMaterial;

	MeshFilter meshFilter;

	Vector3 oldPosition;
	Quaternion oldRotation;
	Vector3 oldScale;

	Mesh Cono(){
		
		Mesh _cono = new Mesh();
		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> normals  = new List<Vector3>();
		List<Vector2> uv       = new List<Vector2>();

		Vector3 oldPosition,temp;
		oldPosition = temp = Vector3.zero;
		
		vertices.Add(Vector3.zero);
		normals.Add(Vector3.up);
		uv.Add(Vector2.one*0.5f);
		
		int w,s;
		
		for(w=0;w<angulo;w++){
			
			for(s=0;s<rango;s++){
				
				temp.x = Mathf.Cos(Mathf.Deg2Rad*w+Mathf.Deg2Rad*(s/rango))*rango;
				temp.z = Mathf.Sin(Mathf.Deg2Rad*w+Mathf.Deg2Rad*(s/rango))*rango;

				if(oldPosition!=temp){

					oldPosition=temp;
					vertices.Add(new Vector3(temp.x,temp.y,temp.z));
					normals.Add(Vector3.up);
					uv.Add(new Vector2((rango+temp.x)/(rango*2),(rango+temp.z)/(rango*2)));

				}

			}
			
		}
		
		int[] triangles = new int[(vertices.Count-2)*3];
		s = 0;
		
		for(w=1;w<(vertices.Count-2);w++){
			
			triangles[s++] = w+1;
			triangles[s++] = w;
			triangles[s++] = 0;
			
		}
		
		_cono.vertices = vertices.ToArray();
		_cono.normals = normals.ToArray();
		_cono.uv = uv.ToArray();
		_cono.triangles = triangles;
		
		return _cono;
		
	}

	Vector3[] initialPosition;
	Vector2[] initialUV;

	// Use this for initialization
	void Start () {
        isScaping = false;
		meshFilter = this.gameObject.GetComponent<MeshFilter>();
		meshFilter.mesh = Cono();
		initialPosition = meshFilter.mesh.vertices;
		initialUV = meshFilter.mesh.uv;
        m_stateController = transform.parent.gameObject.GetComponent<StateController>();
        m_changeConeMaterial = GetComponent<ChangeConeMaterial>();
        scapeCounter = 0f;
	}

	Mesh areaMesh(Mesh mesh){

		Mesh _mesh = new Mesh();
        int playerFound = 0;
		Vector3[] vertices = new Vector3[mesh.vertices.Length];
		Vector2[] uv       = new Vector2[mesh.uv.Length];

		Vector3 center   = transform.localToWorldMatrix.MultiplyPoint3x4(initialPosition[0]);
		uv[0] = initialUV[0];
		Vector3 worldPoint;

		RaycastHit hit = new RaycastHit();

		for(int i=1;i<vertices.Length;i++){

			worldPoint = transform.localToWorldMatrix.MultiplyPoint3x4(initialPosition[i]);

			if(Physics.Linecast(center,worldPoint, out hit)){
                if(hit.collider.gameObject.name == "Player")
                {
                    //Si una línea choca con el jugador, entonces es que estamos viéndolo.
                    playerFound++;
                    //m_stateController.isPlayerOnSight = true;
                    m_changeConeMaterial.ChangeMaterial(stateMaterial.PURSUIT);
                }
				vertices[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
				uv[i] = new Vector2((rango+vertices[i].x)/(rango*2),(rango+vertices[i].z)/(rango*2));

			} else {
                //m_stateController.isPlayerOnSight = false;
                vertices[i] = initialPosition[i];
				uv[i]       = initialUV[i];
			}
		}

        if (playerFound > 0)
        {
            m_stateController.isPlayerOnSight = true;
            m_stateController.pState = StateController.pursuitState.FOLLOWING;
        }
        else
        {
            // Me han visto y ahora me estoy escapando.
            if (m_stateController.isPlayerOnSight)
            {
                InitializeCounter();    // Inicializo el contador.
                m_stateController.isPlayerOnSight = false;
            }

        }

        _mesh.vertices  = vertices;
		_mesh.uv        = uv;
		_mesh.normals   = mesh.normals;
		_mesh.triangles = mesh.triangles;

		return _mesh;

	}

    // Cuenta el tiempo hasta que me escape.
    private void InitializeCounter()
    {
        isScaping = true;
        scapeCounter = 0f;
    }

    // Update is called once per frame
    void FixedUpdate () {

		if(oldPosition!=transform.position || oldRotation!=transform.rotation || oldScale != transform.localScale){

			oldPosition = transform.position;
			oldRotation = transform.rotation;
			oldScale    = transform.localScale;

			meshFilter.mesh = areaMesh(meshFilter.mesh);

		}

        if (isScaping)
        {
            scapeCounter += Time.deltaTime;
            if(scapeCounter >= timeForOutOfSight)
            {
                m_changeConeMaterial.ChangeMaterial(stateMaterial.PATROL);
                m_stateController.pState = StateController.pursuitState.SCAPED;
                scapeCounter = 0f;
                isScaping = false;
            }
        }
	
	}



}
