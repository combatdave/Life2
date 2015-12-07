using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public struct ColonyData
{
    public int stage;
    public int daysInStage;
}


public class Colony
{
    public SpeciesType species;

    public ColonyData currentState;
    public ColonyData tickedState;


    public bool AtFinalStage
    {
        get
        {
            return currentState.stage >= species.stages.Length - 1;
        }
    }

    public string GetInfo()
    {
        string info = species.name + "\n";
        info += "Stage " + currentState.stage + ": " + species.stages[currentState.stage].name + "\n";
        info += "Progress: " + currentState.daysInStage + "/" + GetDaysToProgressForCurrentStage() + " days complete";

        return info;
    }


    public void Debug_AddPopulation(float number)
    {

    }


    public int GetDaysToProgressForCurrentStage()
    {
        return species.stages[currentState.stage].daysToProgress;
    }


    public void Tick(LifeTile tile, LifeTile[] neighbours)
    {
        tickedState.daysInStage += 1;

        Debug.Log("Ticked " + species + " from " + currentState.daysInStage + " to " + tickedState.daysInStage);


        if (!CanStayAtCurrentStage(tile, neighbours))
        {
            GoToPreviousStage();
        }


        if (!AtFinalStage && tickedState.daysInStage > GetDaysToProgressForCurrentStage())
        {
            if (CanProgress(tile, neighbours))
            {
                GoToNextStage();
            }
        }
    }


    private bool CanStayAtCurrentStage(LifeTile tile, LifeTile[] neighbours)
    {
        Debug.Log("Checking if we can stay at current stage...");

        foreach (SpeciesLimiter rule in species.stages[tickedState.stage].requirementsToStay)
        {
            Debug.Log("Checking rule " + rule.debugname);

            bool ruleOkay = rule.RuleOkay(this, tile, neighbours);

            Debug.Log("Rule okay: " + ruleOkay);

            if (!ruleOkay)
            {
                return false;
            }
        }

        return true;
    }


    private bool CanProgress(LifeTile tile, LifeTile[] neighbours)
    {
        Debug.Log("Checking if we can progress...");

        foreach (SpeciesLimiter rule in species.stages[tickedState.stage].requirementsToProgress)
        {
            Debug.Log("Checking rule " + rule.debugname);

            bool ruleOkay = rule.RuleOkay(this, tile, neighbours);

            Debug.Log("Rule okay: " + ruleOkay);

            if (!ruleOkay)
            {
                return false;
            }
        }

        return true;
    }


    private void GoToNextStage()
    {
        tickedState.stage += 1;
        tickedState.daysInStage = 0;
    }


    private void GoToPreviousStage()
    {
        tickedState.stage -= 1;
        tickedState.daysInStage = 0;
    }


    public void CompleteTick()
    {
        currentState.stage = tickedState.stage;
        currentState.daysInStage = tickedState.daysInStage;
    }
}
