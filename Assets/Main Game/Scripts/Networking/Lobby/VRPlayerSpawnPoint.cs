using UnityEngine;

namespace MainGame.Networking.Lobby
{
    public class VRPlayerSpawnPoint : MonoBehaviour
    {
        private void Awake() => VRPlayerSpawnSystem.AddVRSpawnPoint(transform);
        private void OnDestroy() => VRPlayerSpawnSystem.RemoveVRSpawnPoint(transform);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 2f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}
