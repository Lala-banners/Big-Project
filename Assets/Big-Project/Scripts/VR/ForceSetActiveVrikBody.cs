using System.Collections;
using UnityEngine;

namespace Big_Project.Scripts.VR
{
    public class ForceSetActiveVrikBody : MonoBehaviour
    {
        [SerializeField] private GameObject VRIKBody;
        void Start()
        {
            Invoke(nameof(TurnOnBodyAfterFrame),0.1f);
            Invoke(nameof(TurnOnBodyAfterFrame),1f);
            Invoke(nameof(TurnOnBodyAfterFrame),3f);
            Invoke(nameof(TurnOnBodyAfterFrame),5f);
            Invoke(nameof(TurnOnBodyAfterFrame),10f);
        }

        public void TurnOnBodyAfterFrame()
        {
            FindObjectOfType<ForceSetActiveVrikBody>().VRIKBody.SetActive(true);
        }
    }
}
