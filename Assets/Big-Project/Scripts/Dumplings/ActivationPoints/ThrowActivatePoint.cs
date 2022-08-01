using UnityEngine;
namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    public class ThrowActivatePoint : AbstractActivatePoint
    {
        [SerializeField] private Vector3 throwForce;
        [SerializeField] private float disableDumplingMovementXSeconds;

        public override AbstractActivateCommands GetActivePointCommand(Rigidbody _playerRigidbody)
        {
            return new ThrowCommand(_playerRigidbody, throwForce, disableDumplingMovementXSeconds);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector3 direction = transform.TransformDirection(throwForce) * 0.001f;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
