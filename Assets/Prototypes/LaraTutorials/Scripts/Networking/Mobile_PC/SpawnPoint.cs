using UnityEngine;

/// <summary>
/// This script manages the adding and removing of spawn points
/// </summary>
namespace Lara
{
    public class SpawnPoint : MonoBehaviour
    {
        private void Awake() => PlayerSpawnSystem.AddSpawnPoint(transform);
        private void OnDestroy() => PlayerSpawnSystem.RemoveSpawnPoint(transform);

        /// <summary>
        /// Visualise spawn points and positions 
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 1);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}
