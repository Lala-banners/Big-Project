using UnityEngine;

namespace BreadAndButter.AI
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 size = Vector3.zero;
        public Vector3 center = Vector3.zero;

        [SerializeField] private bool floorYPos = false;
        [SerializeField] private bool shouldBossSpawn = false;
        [SerializeField] private Vector2 spawnRate = new Vector2();
        [SerializeField, Range(0, 100)] private float bossSpawnChance = 1;

        [SerializeField] private GameObject bossPrefab = null;
        [SerializeField] private GameObject enemyPrefab = null;

        private float time = 0;
        private float timeStep = 0;

        public void Spawn()
        {
            GameObject prefab = shouldBossSpawn && Random.Range(0, 100) < bossSpawnChance ? bossPrefab : enemyPrefab;

            Vector3 position = transform.position + 
                new Vector3(Random.Range(-size.x, size.x), floorYPos ? 0 : 
                Random.Range(-size.y, size.y), 
                Random.Range(-size.z, size.z)) + center;

            position = transform.InverseTransformPoint(position);

            Instantiate(prefab, position, transform.rotation, transform);
            timeStep = Random.Range(spawnRate.x, spawnRate.y);
        }

        private void Start()
        {
            timeStep = Random.Range(spawnRate.x, spawnRate.y);
        }

        private void Update()
        {
            if(time < timeStep)
            {
                time += Time.deltaTime * Time.timeScale;
            }
            else
            {
                Spawn();
            }
        }

#if UNITY_EDITOR
        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            //Store default Matrix
            Matrix4x4 baseMatrix = Gizmos.matrix;

            //Make gizmos use objects matrix
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;

            //Draw a green, partially transparent cube (slime)
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(center, size);

            //Reset
            Gizmos.matrix = baseMatrix;
        }
#endif
    }
}
