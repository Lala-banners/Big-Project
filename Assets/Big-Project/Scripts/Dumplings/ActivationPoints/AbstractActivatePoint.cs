using UnityEngine;
namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    public abstract class AbstractActivatePoint :  MonoBehaviour
    {
        public abstract AbstractActivateCommands GetActivePointCommand(Rigidbody _playerRigidbody);
    }
}
