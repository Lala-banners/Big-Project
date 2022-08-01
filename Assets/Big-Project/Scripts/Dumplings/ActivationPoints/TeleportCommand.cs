using UnityEngine;
namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    public class TeleportCommand : AbstractActivateCommands
    {
        private Rigidbody playerRigidbody;
        private Transform teleportLocation;
        
        public TeleportCommand(Rigidbody _playerRigidbody, Transform _teleportLocation)
        {
            playerRigidbody = _playerRigidbody;
            teleportLocation = _teleportLocation;
        }
        
        public override void ExecuteCommand()
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.MovePosition(teleportLocation.position);
            playerRigidbody.MoveRotation(teleportLocation.rotation);
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}