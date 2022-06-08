using UnityEngine;

namespace Lara
{
    public class ActivateWristUI : MonoBehaviour
    {
        public GameObject wristUI;
        public Transform wristAttach;

        private void Update()
        {
            wristUI.transform.SetParent(wristAttach, false);
        }
    }
}
