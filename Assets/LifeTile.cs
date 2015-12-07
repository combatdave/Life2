using UnityEngine;
using System.Collections.Generic;
using System;

public class LifeTile
{
    public int x, y;

    public List<Colony> colonies = new List<Colony>();


    public void Init(int tileX, int tileY)
    {
        x = tileX;
        y = tileY;
    }


    public string GetInfo()
    {
        string info = "";

        foreach (Colony p in colonies)
        {
            info += p.GetInfo() + "\n\n";
        }

        return info;
    }


    public void Debug_AddColony(Colony newColony)
    {
        Debug.Log("Debug add population: " + newColony.GetInfo());

        foreach (Colony p in colonies)
        {
            if (p.species == newColony.species)
            {
                Debug.Log("Population type already exists");
                return;
            }
        }

        colonies.Add(newColony);
    }


    public void Tick(LifeTile[] neighbours)
    {
        foreach (Colony c in colonies)
        {
            c.Tick(this, neighbours);
        }
    }


    public void CompleteTick()
    {
        for (int i = 0; i < colonies.Count; i++)
        {
            Colony c = colonies[i];

            if (c.tickedState.stage < 0)
            {
                colonies.RemoveAt(i);
            }
            else
            {
                c.CompleteTick();
            }
        }
    }


    public Colony GetColonyFromSpecies(SpeciesType species)
    {
        foreach (Colony c in colonies)
        {
            if (c.species == species)
            {
                return c;
            }
        }

        return null;
    }
}
