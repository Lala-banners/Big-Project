using UnityEngine;

namespace Dumplings.Core.Movement.BaseScripts
{
	public abstract class ModularBehaviour : MonoBehaviour
	{
		public bool Enabled
		{
			get => intEnabled;
			set
			{
				intEnabled = value;
				OnEnabledStateChanged(intEnabled);
			}
		}

		public bool LocalOnly => localOnly;
		
		[SerializeField] private bool localOnly = true;
		[SerializeField] private UpdatePhase updatePhase = UpdatePhase.Update;
		private bool intEnabled = true;

		public virtual void Init(MCCPlayer _player) { }

		/// <summary>
		/// Called in all update functions on the MCCPlayer, with a reference to the phase being called from,
		/// so that the ModularBehaviour can be updated in specific loops.
		/// </summary>
		/// <param name="_phase"> The update loop that this process function is being called from. </param>
		public void Process(UpdatePhase _phase)
		{
			if(_phase != updatePhase || !Enabled)
				return;
			
			OnProcess();
		}

		protected abstract void OnProcess();

		protected virtual void OnEnabledStateChanged(bool _newState) { }
	}
}