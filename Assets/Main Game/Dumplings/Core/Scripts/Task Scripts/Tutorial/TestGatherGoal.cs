using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGatherGoal : TestQuest.TestTaskGoal
{
    public string item;

    public override string GetDescription()
    {
        return $"Gather an {item}";
    }

    public override void Initialise()
    {
        base.Initialise();
        EventManager.Instance.AddListener<GatherGameEvent>(OnGather);
    }

    private void OnGather(GatherGameEvent eventInfo)
    {
        if (eventInfo.gatherItemName == item)
        {
            curAmount++;
            Evaluate();
        }
    }
}
