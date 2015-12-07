using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Map : MonoBehaviour
{
    public int mapWidth = 10;
    public int mapHeight = 10;
    public GameObject tilePrefab;

    public Text locationText;
    public Text infoText;

    private List<List<GameObject>> visualTiles;

    public LifeTile selectedTile;


	// Use this for initialization
	void Awake()
    {
	    visualTiles = new List<List<GameObject>>();

        for(int x=0; x<mapWidth; x++)
        {
            visualTiles.Add(new List<GameObject>());

            for (int y=0; y<mapHeight; y++)
            {
                GameObject tileGO = Instantiate(tilePrefab);
                tileGO.transform.position = new Vector3(x - (mapWidth / 2.0f), y - (mapHeight / 2.0f), 0);
                tileGO.transform.parent = transform;
                tileGO.name = "Tile " + x + " " + y;
                visualTiles[x].Add(tileGO);

                VisualTile tile = tileGO.GetComponent<VisualTile>();
                tile.x = x;
                tile.y = y;
                tile.OnClicked += OnTileClicked;
            }
        }
    }
	
	
    void OnTileClicked(int x, int y)
    {
        selectedTile = GetComponent<World>().GetLifeTile(x, y);

        UpdateUI();
    }


    void Update()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        if (selectedTile != null)
        {
            locationText.text = "[" + selectedTile.x + ", " + selectedTile.y + "]";

            string info = selectedTile.GetInfo();
            infoText.text = info;
        }
    }


    void OnGUI()
    {
        if (selectedTile != null)
        {
            GetComponent<DebugUI>().DrawGraphs(selectedTile);
        }
    }
}
