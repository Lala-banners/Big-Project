using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Big_Project.Scripts.Dumplings.ActivationPoints
{
    [RequireComponent(typeof(Rigidbody))]
    public class DumplingControllerActivatePoints : MonoBehaviour
    {
        [SerializeField] private GameObject activateCanvas;
        private UnityAction activateAction;
        private Rigidbody dumplingRigidbody;
        
        private void Awake()
        {
            dumplingRigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            activateCanvas.SetActive(false);
        }

        private void OnTriggerEnter(Collider _other)
        {
            AbstractActivatePoint activePoint = _other.gameObject.GetComponent<AbstractActivatePoint>();
            
            if (activePoint != null)
            {
                AbstractActivateCommands activeCommand = activePoint.GetActivePointCommand(dumplingRigidbody);
                activateAction += activeCommand.ExecuteCommand;
                activateCanvas.SetActive(true);
            }
        }
        
        private void OnTriggerExit(Collider _other)
        {
            AbstractActivatePoint activePoint = _other.gameObject.GetComponent<AbstractActivatePoint>();
            
            if (activePoint != null)
            {
                activateAction = null;
                activateCanvas.SetActive(false);
            }
        }
		
        public void OnActivatePoint(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                activateAction?.Invoke();
            }
        }
    }
}
