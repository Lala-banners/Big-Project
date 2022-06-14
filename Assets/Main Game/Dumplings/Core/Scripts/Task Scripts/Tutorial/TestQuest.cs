using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TestQuest : ScriptableObject
{
    [System.Serializable]
    public struct Information
    {
        public string name;
        public string description;
        public Sprite icon;
    }

    [Header("Information")] 
    public Information info;

    //Rewards for dumplings
    [System.Serializable]
    public struct Stat
    {
        public int currency, exp;
    }

    [Header("Rewards")] 
    public Stat rewards = new Stat { currency = 10, exp = 5};

    public bool TestCompleted { get; protected set; }
    public TaskCompletedEvent taskCompleted;
    
    //Contains info about what it takes to complete each task
    public abstract class TestTaskGoal : ScriptableObject
    {
        protected string description;
        public int curAmount { get; protected set; }
        public int reqAmount = 3;
        
        public bool TestCompleted { get; protected set; }
        [HideInInspector] public UnityEvent taskCompleted;

        public virtual string GetDescription()
        {
            return description;
        }

        public virtual void Initialise()
        {
            TestCompleted = false;
            taskCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if (curAmount >= reqAmount)
            {
                TestComplete();
            }
        }

        private void TestComplete()
        {
            TestCompleted = true;
            taskCompleted.Invoke();
            taskCompleted.RemoveAllListeners();
        }
    }

    public List<TestTaskGoal> goals;

    public void Initialise()
    {
        TestCompleted = false;
        taskCompleted = new TaskCompletedEvent();

        foreach (var goal in goals)
        {
            goal.Initialise();
            goal.taskCompleted.AddListener(delegate { CheckTasks(); });
        }
    }

    private void CheckTasks()
    {
        TestCompleted = goals.All(g => g.TestCompleted);
        if (TestCompleted)
        {
            //Give reward
            taskCompleted.Invoke(this);
            taskCompleted.RemoveAllListeners();
        }
    }
}

public class TaskCompletedEvent : UnityEvent<TestQuest>{}


#if UNITY_EDITOR
[CustomEditor(typeof(TestQuest))]
public class TaskEditor : Editor
{
    private SerializedProperty questInfoProperty;
    private SerializedProperty questStatProperty;

    private List<string> taskType;
    private SerializedProperty taskTypeListProperty;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateTask()
    {
        var newTask = CreateInstance<TestQuest>();
        
        ProjectWindowUtil.CreateAsset(newTask, "quest.asset");
    }

    private void OnEnable()
    {
        questInfoProperty = serializedObject.FindProperty(nameof(TestQuest.info));
        questStatProperty = serializedObject.FindProperty(nameof(TestQuest.rewards));

        taskTypeListProperty = serializedObject.FindProperty(nameof(TestQuest.goals));

        var lookup = typeof(TestQuest.TestTaskGoal);
        taskType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    /// <summary>
    /// Ensures all fields of a task object are being displayed,
    /// as well as list of tasks.
    /// </summary>
    public override void OnInspectorGUI()
    {
        var child = questInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);
        
        EditorGUILayout.LabelField("Task Info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        child = questStatProperty.Copy();
        depth = child.depth;
        child.NextVisible(false);
        
        EditorGUILayout.LabelField("Task Reward", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add new task", -1, taskType.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(taskType[choice]);
            
            AssetDatabase.AddObjectToAsset(newInstance, target);
            
            taskTypeListProperty.InsertArrayElementAtIndex(taskTypeListProperty.arraySize);
            taskTypeListProperty.GetArrayElementAtIndex(taskTypeListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;
        }

        Editor editor = null;
        int toDelete = -1;
        for (int i = 0; i < taskTypeListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = taskTypeListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);
            
            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref editor);
            
            editor.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = taskTypeListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item,true);
            
            //Need to do it twice, first time nullify the entry, second time actually remove it
            taskTypeListProperty.DeleteArrayElementAtIndex(toDelete);
            taskTypeListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif