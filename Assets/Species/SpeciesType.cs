using UnityEngine;
using System.Collections.Generic;
using System;

public class SpeciesType : ScriptableObject
{
    public SpeciesStage[] stages;

    public Color lineColor;
}


[System.Serializable]
public class SpeciesStage
{
    public string name;

    public SpeciesLimiter[] requirementsToProgress;
    public SpeciesLimiter[] requirementsToStay;

    public int daysToProgress;
}


public enum RuleType
{
    None,
    CantExistAtAll,
    LessThan,
    LessThanOrEqual,
    Equal,
    GreaterThanOrEqual,
    GreaterThan,
}


public enum RuleArea
{
    None,
    OnlyThis,
    AnyThisAndNeighbours,
    AllThisAndNeighbours,
    AnyNeighbours,
    AllNeighbours,
}


[System.Serializable]
public class SpeciesLimiter
{
    public string debugname;

    public SpeciesType otherSpecies;
    public int otherSpeciesStage;
    public RuleType otherSpeciesStageCantBe;
    public RuleArea areaRequirement;


    public bool RuleOkay(Colony colony, LifeTile thisArea, LifeTile[] neighbouringAreas)
    {
        List<LifeTile> relevantTiles = new List<LifeTile>();

        if (areaRequirement == RuleArea.OnlyThis)
        {
            relevantTiles.Add(thisArea);
        }
        else if (areaRequirement == RuleArea.AllThisAndNeighbours || areaRequirement == RuleArea.AnyThisAndNeighbours)
        {
            relevantTiles.Add(thisArea);
            relevantTiles.AddRange(neighbouringAreas);
        }
        else if (areaRequirement == RuleArea.AnyNeighbours || areaRequirement == RuleArea.AllNeighbours)
        {
            relevantTiles.AddRange(neighbouringAreas);
        }

        List<Colony> relevantColonies = new List<Colony>();

        foreach (LifeTile t in relevantTiles)
        {
            Colony c = t.GetColonyFromSpecies(otherSpecies);
            if (c != null)
            {
                relevantColonies.Add(c);
            }
        }


        if (otherSpeciesStageCantBe == RuleType.CantExistAtAll)
        {
            return relevantColonies.Count == 0;
        }


        

        if (otherSpeciesStageCantBe == RuleType.LessThan || otherSpeciesStageCantBe == RuleType.LessThanOrEqual || otherSpeciesStageCantBe == RuleType.Equal)
        {
            bool gotEnoughColonies = false;

            if (areaRequirement == RuleArea.OnlyThis || areaRequirement == RuleArea.AnyNeighbours || areaRequirement == RuleArea.AnyThisAndNeighbours)
            {
                gotEnoughColonies = relevantColonies.Count >= 1;
            }
            else if (areaRequirement == RuleArea.AllThisAndNeighbours)
            {
                gotEnoughColonies = relevantColonies.Count == neighbouringAreas.Length + 1;
            }
            else if (areaRequirement == RuleArea.AllNeighbours)
            {
                gotEnoughColonies = relevantColonies.Count == neighbouringAreas.Length;
            }

            if (!gotEnoughColonies)
            {
                Debug.Log("Not enough colonies found!");
                return false;
            }
        }


        bool totalFailure = false;

        foreach (Colony other in relevantColonies)
        {
            bool thisColonyFailure = false;

            if (otherSpeciesStageCantBe == RuleType.LessThan)
            {
                thisColonyFailure = other.currentState.stage < otherSpeciesStage;
            }
            else if (otherSpeciesStageCantBe == RuleType.LessThanOrEqual)
            {
                thisColonyFailure = other.currentState.stage <= otherSpeciesStage;
            }
            else if (otherSpeciesStageCantBe == RuleType.Equal)
            {
                thisColonyFailure = other.currentState.stage == otherSpeciesStage;
            }
            else if (otherSpeciesStageCantBe == RuleType.GreaterThan)
            {
                thisColonyFailure = other.currentState.stage > otherSpeciesStage;
            }
            else if (otherSpeciesStageCantBe == RuleType.GreaterThanOrEqual)
            {
                thisColonyFailure = other.currentState.stage >= otherSpeciesStage;
            }

            totalFailure = totalFailure || thisColonyFailure;
        }

        return !totalFailure;
    }
}
