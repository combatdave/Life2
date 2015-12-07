using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Collections;

public class DebugUI : MonoBehaviour
{
    public SpeciesType antSpecies;
    public SpeciesType antEaterSpecies;

    public bool autoTick;


    void Start()
    {
        StartCoroutine(AutoTick());
    }


    public void AddFox()
    {
        AddColonyToSelectedTile(antSpecies);
    }


    public void AddRabbit()
    {
        AddColonyToSelectedTile(antEaterSpecies);
    }


    public void AddColonyToSelectedTile(SpeciesType species)
    {
        LifeTile t = GetComponent<Map>().selectedTile;
        if (t != null)
        {
            Colony colony = new Colony();
            colony.species = species;
            t.Debug_AddColony(colony);
        }
    }


    public void Tick()
    {
        GetComponent<World>().TickWorld();
    }


    public void DrawGraphs(LifeTile t)
    {
        //float graphWidth = 400f;
        //float graphHeight = 260f;

        //float xMin = (Screen.width / 2f) - (graphWidth / 2f);
        //float xMax = (Screen.width / 2f) + (graphWidth / 2f);
        //float yMax = Screen.height - 40f;
        //float yMin = Screen.height - (40f + graphHeight);

        //Drawing.DrawLine(new Vector2(xMin, yMax), new Vector2(xMax, yMax), Color.white);
        //Drawing.DrawLine(new Vector2(xMin, yMin), new Vector2(xMin, yMax), Color.white);

        //float maxPop = 1f;
        //int maxPoints = 0;

        //foreach (Colony p in t.populations)
        //{
        //    if (p.history.Count > maxPoints)
        //    {
        //        maxPoints = p.history.Count;
        //    }
        //}

        //int totalMaxPoints = 50;
        //if (maxPoints > totalMaxPoints) maxPoints = totalMaxPoints;

        //foreach (Colony p in t.populations)
        //{
        //    //if (p.population > maxPop)
        //    //{
        //    //    maxPop = p.population;
        //    //}
        //    int start = p.history.Count - totalMaxPoints;
        //    if (start < 0) start = 0;
        //    for (int i = start; i < p.history.Count; i++)
        //    {
        //        if (p.history[i] > maxPop)
        //        {
        //            maxPop = p.history[i];
        //        }
        //    }
        //}




        //foreach (Colony p in t.populations)
        //{
        //    float prevX = xMin;
        //    float prevY = yMax;

        //    int start = p.history.Count - totalMaxPoints;
        //    if (start < 0) start = 0;

        //    for (int i=start; i<p.history.Count; i++)
        //    {
        //        float xPos = xMin + (graphWidth * (i - start) / (float)maxPoints);
        //        float yPos = yMax - (graphHeight * p.history[i] / maxPop);

        //        if (i - start != 0)
        //        {
        //            Drawing.DrawLine(new Vector2(prevX, prevY), new Vector2(xPos, yPos), p.species.lineColor);
        //        }

        //        prevX = xPos;
        //        prevY = yPos;
        //    }
        //}
    }


    IEnumerator AutoTick()
    {
        while (true)
        {
            if (autoTick)
            {
                Tick();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void OnAutoTickChanged(bool state)
    {
        autoTick = state;
    }
}
