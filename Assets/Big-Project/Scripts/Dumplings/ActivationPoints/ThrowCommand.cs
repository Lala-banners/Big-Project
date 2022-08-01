using Big_Project.Scripts.Dumplings.MovementAndCamera.Motors;
using UnityEngine;
namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    public class ThrowCommand : AbstractActivateCommands
    {
        private Rigidbody playerRigidbody;
        private Vector3 playerForce;
        private float disableDumplingMovementXSeconds;
        
        public ThrowCommand(Rigidbody _playerRigidbody, Vector3 _playerForce, float _disableDumplingMovementXSeconds)
        {
            playerRigidbody = _playerRigidbody;
            playerForce = _playerForce;
            disableDumplingMovementXSeconds = _disableDumplingMovementXSeconds;
        }
        
        public override void ExecuteCommand()
        {
            
            BaseMotorModularBehaviour[] dumplingMotors = playerRigidbody.gameObject.GetComponentsInChildren<BaseMotorModularBehaviour>();

            foreach (BaseMotorModularBehaviour dumplingMotor in dumplingMotors)
            {
                dumplingMotor.ThrowDumplingForXSeconds(disableDumplingMovementXSeconds);
            }
            
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.AddForce(new Vector3(playerForce.x, playerForce.y, playerForce.z));
        }
    }
}
