// Creator: Kieran
// Creation Time: 2022/06/06 16:41
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Combat
{
    public class CombatBehaviour : ModularBehaviour
    {
        public bool shootInput;

        protected override void OnProcess(UpdatePhase _phase)
        {
            if(shootInput)
            {
                Shoot();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            shootInput = context.action.triggered;
            Debug.Log($"shoot triggered || shootInput = {shootInput}");
        }
        
        private void Shoot()
        {
            Debug.Log("Bang");
            shootInput = false;
        }
        
    }
}