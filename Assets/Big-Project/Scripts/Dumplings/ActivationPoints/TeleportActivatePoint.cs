using System;
using UnityEngine;
namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    public class TeleportActivatePoint : AbstractActivatePoint
    {
        [SerializeField] private Transform teleportPointToComeOutOf;
        
        public override AbstractActivateCommands GetActivePointCommand(Rigidbody _playerRigidbody)
        {
            return new TeleportCommand(_playerRigidbody, teleportPointToComeOutOf);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 direction = transform.TransformDirection(Vector3.forward) * 0.1f;
            Gizmos.DrawRay(transform.position, direction);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, teleportPointToComeOutOf.position);
        }
    }
}
