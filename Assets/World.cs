using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public List<List<LifeTile>> tiles;


    void Awake()
    {
        Map m = GetComponent<Map>();

        tiles = new List<List<LifeTile>>();

        for (int x = 0; x < m.mapWidth; x++)
        {
            tiles.Add(new List<LifeTile>());

            for (int y = 0; y < m.mapHeight; y++)
            {
                LifeTile t = new LifeTile();
                t.Init(x, y);
                tiles[x].Add(t);
            }
        }

        GetComponent<Map>().selectedTile = GetLifeTile(0, 0);
    }


    public LifeTile GetLifeTile(int x, int y)
    {
        if (x >= 0 && x < tiles.Count && y >= 0 && y < tiles[x].Count)
        {
            return tiles[x][y];
        }

        return null;
    }


    public void TickWorld()
    {
        Map m = GetComponent<Map>();
        for (int x = 0; x < m.mapWidth; x++)
        {
            for (int y = 0; y < m.mapHeight; y++)
            {
                LifeTile t = tiles[x][y];

                List<LifeTile> neighbours = new List<LifeTile>();
                neighbours.Add(GetLifeTile(x - 1, y));
                neighbours.Add(GetLifeTile(x + 1, y));
                neighbours.Add(GetLifeTile(x, y - 1));
                neighbours.Add(GetLifeTile(x, y + 1));
                neighbours.RemoveAll(tile => tile == null);

                t.Tick(neighbours.ToArray());
            }
        }

                

        foreach (List<LifeTile> row in tiles)
        {
            foreach (LifeTile t in row)
            {
                t.CompleteTick();
            }
        }
    }
}
