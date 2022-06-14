using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string eventDescription;
}

public class GatherGameEvent : GameEvent
{
    public string gatherItemName;

    public GatherGameEvent(string name)
    {
        gatherItemName = name;
    }
}
