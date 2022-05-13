using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the adding and removing of spawn points
/// </summary>
public class Lara_SpawnPoint : MonoBehaviour
{
    private void Awake() => Lara_PlayerSpawnSystem.AddSpawnPoint(transform);
    private void OnDestroy() => Lara_PlayerSpawnSystem.RemoveSpawnPoint(transform);

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
