using UnityEngine;
using UnityEngine.Events;

using System.Collections;

namespace BreadAndButter
{
    public abstract class Trigger : MonoBehaviour
    {
        public UnityEvent OnTriggered
        {
            get => onTriggered;
            set => onTriggered = value;
        }

        [SerializeField, Min(0)] protected float delay = 0;
        [SerializeField] protected bool oneTimeUse = false;
        [SerializeField] protected UnityEvent onTriggered = new UnityEvent();

        private bool isTriggered = false;

        public void TriggerEvent()
        {
            //If the trigger can only run one and has already been run,
            //ignore this Trigger Event
            if(isTriggered && oneTimeUse)
            {
                return;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
