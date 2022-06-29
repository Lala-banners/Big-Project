// Creator: Kieran
// Creation Time: 2022/06/06 16:41
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Combat
{
    public class CombatBehaviour : ModularBehaviour
    {
        public bool shootInput;

        public override void Init(IMCCPlayer _playerInterface)
        {
            shootInput = false;
            Debug.Log("shoot here");
        }

        protected override void OnProcess(UpdatePhase _phase)
        {
            if(shootInput)
            {
                Shoot();
            }
        }

        public void InputShoot()
        {
            shootInput = true;
        }
        
        private void Shoot()
        {
            Debug.LogWarning("Bang");
            shootInput = false;
        }
    }
}