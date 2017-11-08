using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;
	public Transform Player;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize; // el área de la escena en 2 coordenadas. el vector es x e y, pero nosotros usamos x z (siendo z y en 2)
	public float nodeRadius; // lo necesitamos para saber como de grande debe ser el nodo o cuadradito
	public TerrainType[] walkableRegions;
	public int obstacleProximityPenalty = 10;
	LayerMask walkableMask;
	Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

	private Node[,] grid; // La rejilla de nodos que se ve pintada

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	int penaltyMin = int.MaxValue;
	int penaltyMax = int.MinValue;

	void Awake(){

		//para saber cuantos nodos podemos meter, dependiendo de su tamaño, en el grid
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);//numero de nodos en x
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//numero de nodos en y

		foreach (TerrainType region in walkableRegions) {
			walkableMask.value = walkableMask |= region.terrainMask.value;
			walkableRegionsDictionary.Add ((int) Mathf.Log(region.terrainMask.value, 2), region.terrainPenalty * 2);
		}
		CreateGrid ();


	}

	void Start(){
	}

	//esta funcion permite dibujar el area por cuadraditos, como su fuera un tablero
	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				int movementPenalty = 0;


				Ray ray = new Ray (worldPoint + Vector3.up * 50, Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100f, walkableMask)) {
					walkableRegionsDictionary.TryGetValue (hit.collider.gameObject.layer, out movementPenalty);
				}
				
				if (!walkable) {
					
					movementPenalty += obstacleProximityPenalty;
					Debug.Log (movementPenalty);
				}

				grid [x, y] = new Node (walkable, worldPoint, x, y, movementPenalty * 10);
			}
		}

		BlurPenaltyMap (3);
	}

	private void BlurPenaltyMap(int blurSize){

		Debug.Log ("Dibujando Grid!");

		int kernelSize = blurSize * 2 + 1;
		int kernelExtents = (kernelSize - 1) / 2;

		int[,] penaltiesHorizontalPass = new int[gridSizeX,gridSizeY];
		int[,] penaltiesVerticalPass = new int[gridSizeX,gridSizeY];

		for (int y = 0; y < gridSizeY; y++) {
			for (int x = -kernelExtents; x <= kernelExtents; x++) {
				int sampleX = Mathf.Clamp (x, 0, kernelExtents);
				penaltiesHorizontalPass [0, y] += grid [sampleX, y].movementPenalty;
			}

			for (int x = 1; x < gridSizeX; x++) {
				int removeIndex = Mathf.Clamp (x - kernelExtents - 1, 0, gridSizeX);
				int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX-1);

				penaltiesHorizontalPass [x, y] = penaltiesHorizontalPass [x - 1, y] - grid [removeIndex, y].movementPenalty + grid [addIndex, y].movementPenalty;
			}
		}

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = -kernelExtents; y <= kernelExtents; y++) {
				int sampleY = Mathf.Clamp (y, 0, kernelExtents);
				penaltiesVerticalPass [x, 0] += penaltiesHorizontalPass[x, sampleY];
			}

			int blurredPenalty = Mathf.RoundToInt ((float)penaltiesVerticalPass [x, 0] / (kernelSize * kernelSize));
			grid [x, 0].movementPenalty = blurredPenalty;

			for (int y = 1; y < gridSizeY; y++) {
				int removeIndex = Mathf.Clamp (y - kernelExtents - 1, 0, gridSizeY);
				int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY-1);

				penaltiesVerticalPass [x, y] = penaltiesVerticalPass [x, y-1] - penaltiesHorizontalPass [x, removeIndex] + penaltiesHorizontalPass [x, addIndex];
				blurredPenalty = Mathf.RoundToInt ((float)penaltiesVerticalPass [x, y] / (kernelSize * kernelSize));
				grid [x, y].movementPenalty = blurredPenalty;

			//	Debug.Log (blurredPenalty);

				if (blurredPenalty > penaltyMax) {
					penaltyMax = blurredPenalty;
				}

				if (blurredPenalty < penaltyMin) {
					penaltyMin = blurredPenalty;
				}
			}
		}
	}

	public int MaxSize{
		get { return gridSizeX * gridSizeY; }
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				}

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add (grid [checkX, checkY]);
				}
			}
		}

		return neighbours;
	}

	//esta funcion es para obtener el nodo correspondiende a la posicion del jugador
	public Node NodeFromWorldPoint(Vector3 worldPosition){


		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

		//hacemos esto para que si se esta fuera del escenario por alguna razon no nos devuelva 
		//index invalido del array de nodos
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		//obtenemos los indices del nodo en el que estamos (menos 1 para no salirnos del array!)
		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid [x, y];

	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, 1f, gridWorldSize.y));
		if (grid != null && displayGridGizmos){
			foreach (Node n in grid) {

				Gizmos.color = Color.Lerp (Color.white, Color.black, Mathf.InverseLerp (penaltyMin, penaltyMax, n.movementPenalty));

				Gizmos.color = (n.walkable) ? Gizmos.color: Color.red;
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
			}
		}
	}

	[System.Serializable]
	public class TerrainType{
		public LayerMask terrainMask;
		public int terrainPenalty;
	}
}
