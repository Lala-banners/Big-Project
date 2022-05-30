using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.AnimatedValues;

namespace BreadAndButter.AI
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        private Spawner spawner;

        private SerializedProperty sizeProp;
        private SerializedProperty centerProp;

        private SerializedProperty floorYPosProp;
        private SerializedProperty spawnRateProp;

        private SerializedProperty shouldBossSpawnProp;
        private SerializedProperty bossSpawnChanceProp;
        private SerializedProperty bossPrefabProp;
        private SerializedProperty enemyPrefabProp;

        private AnimBool shouldBossSpawn = new AnimBool();

        private BoxBoundsHandle handle;

        // This function is called when the object becomes enabled and active
        private void OnEnable() //Like Start()
        {
            spawner = target as Spawner;

            sizeProp = serializedObject.FindProperty("size");
            centerProp = serializedObject.FindProperty("center");

            floorYPosProp = serializedObject.FindProperty("floorYPos");
            spawnRateProp = serializedObject.FindProperty("spawnRate");

            shouldBossSpawnProp = serializedObject.FindProperty("shouldBossSpawn");
            bossSpawnChanceProp = serializedObject.FindProperty("bossSpawnChance");

            bossPrefabProp = serializedObject.FindProperty("bossPrefab");
            enemyPrefabProp = serializedObject.FindProperty("enemyPrefab");

            shouldBossSpawn.value = shouldBossSpawnProp.boolValue; //Set up anim bool
            shouldBossSpawn.valueChanged.AddListener(Repaint); //Fade effect

            handle = new BoxBoundsHandle();
        }

        /// <summary>
        /// Differentiate objects in inspector.
        /// Looks cool!
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //Set up vertical Layout group visualised as a box
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                //CURLY BRACKETS MAKE COOL INDENT FOR VISUALISATION OF LAYOUT 
                //Draw the center and size props exactly as unity would
                EditorGUILayout.PropertyField(centerProp);
                EditorGUILayout.PropertyField(sizeProp);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(floorYPosProp);

                //Cache the standard of the original value of spawn rate and create label
                Vector2 spawnRate = spawnRateProp.vector2Value;
                string label = $"Spawn Rate Bounds ({spawnRate.x.ToString("0.00")}s - {spawnRate.y.ToString("0.00")}s)";

                //Render the spawn rate as a min max slider and reset the vector
                EditorGUILayout.MinMaxSlider(label, ref spawnRate.x, ref spawnRate.y, 0, 3);
                spawnRateProp.vector2Value = spawnRate;

                EditorGUILayout.Space();

                //Render enemyPrefab and shouldBossSpawn as normal
                EditorGUILayout.PropertyField(enemyPrefabProp);
                EditorGUILayout.PropertyField(shouldBossSpawnProp);

                //Attempt to fade the next variables in and out 
                shouldBossSpawn.target = shouldBossSpawnProp.boolValue;
                if(EditorGUILayout.BeginFadeGroup(shouldBossSpawn.faded))
                {
                    //Only visible when 'shouldBossSpawn' is true.
                    EditorGUI.indentLevel++; //Add indent

                    //Draw boss spawn chance and boss prefab as normal
                    EditorGUILayout.PropertyField(bossSpawnChanceProp);
                    EditorGUILayout.PropertyField(bossPrefabProp);

                    EditorGUI.indentLevel--; //Minus indent
                }
                EditorGUILayout.EndFadeGroup();

            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        // Enables the Editor to handle an event in the scene view
        private void OnSceneGUI() //Like Update()
        {
            //Make the handles colour green
            Handles.color = Color.green;

            //Store default Matrix
            Matrix4x4 baseMatrix = Handles.matrix;

            //Make gizmos use objects matrix
            Matrix4x4 rotationMatrix = spawner.transform.localToWorldMatrix;
            Handles.matrix = rotationMatrix;

            //Set up box bounds handle with spawner values center and size
            handle.center = spawner.center;
            handle.size = spawner.size;

            //Begin listening for changes to the handle, then draw it
            EditorGUI.BeginChangeCheck();
            handle.DrawHandle();

            //Check if any changes have been made
            if(EditorGUI.EndChangeCheck())
            {
                //Take the data from handle and put back in spawner (reset the spawner values to handle values)
                spawner.size = handle.size;
                spawner.center = handle.center;
            }

            //Reset
            Handles.matrix = baseMatrix;
        }
    }
}
