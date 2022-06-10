using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Completed,
    Failed,
}

public enum GoalType
{
    Gather,
    Hide,
}

[System.Serializable]
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;
    public GoalType goalType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;
    
    [Header("Player")]
    public ItemData item;
    private GameObject waypointCanvas;

    public abstract bool isCompleted();
}
