﻿// Creator: Kieran & James
// Creation Time: 2022/04/11 09:01
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController
{
	/// <summary>
	/// This is the manager that will handle all the behaviours attached to this player.
	/// Requires a Rigid body to manipulate and apply force to. 
	/// </summary>
	[RequireComponent(typeof(Rigidbody))]
	// ReSharper disable once InconsistentNaming
	public class MCCPlayer : MonoBehaviour, IMCCPlayer
	{
		public PlayerInput Input => input;
		public Collider Collider => collider;
		public Rigidbody Rigidbody { get; set; }
		public Transform Transform => transform;
		[SerializeField] private new Collider collider;
		[SerializeField] private PlayerInput input;
		[SerializeField] private List<ModularBehaviour> behaviours = new List<ModularBehaviour>();

		/// <summary>
		/// This trys to get any "Behaviours" in the list of behavoiurs under the player.
		/// </summary>
		/// <param name="_found"> The reference of the behaviour found.</param>
		/// <typeparam name="BEHAVIOUR">The behaviour you are trying to get. </typeparam>
		/// <returns> True if the behaviour was found. False if it isn't in the list.</returns>
		public bool TryGetBehaviour<BEHAVIOUR>(out BEHAVIOUR _found) where BEHAVIOUR : ModularBehaviour
		{
			// Goes through all the behaviours on the player.
			foreach(ModularBehaviour behaviour in behaviours)
			{
				// If you find an the behaviour in the players list of behaviours.
				if(behaviour.GetType() == typeof(BEHAVIOUR))
				{
					_found = (BEHAVIOUR)behaviour;
					return true;
				}
			}
			// If it goes through the whole list and can't find the behaviour out null.
			_found = null;
			// Return false as there wasn't an instance of the behaviour.
			return false;
		}

		/// <summary>
		/// Add this behaviour to the the player.
		/// </summary>
		/// <param name="_behaviour"> This is the behaviour to be added to the player. </param>
		public void RegisterBehaviour(ModularBehaviour _behaviour)
		{
			// If the behaviour isn't on the player already.
			if(!behaviours.Contains(_behaviour))
				// Add the behaviour to the list of behaviours.
				behaviours.Add(_behaviour);
		}

		/// <summary>
		/// Remove this behaviour from the player.
		/// </summary>
		/// <param name="_behaviour"> The behaviour to remove from the player. </param>
		public void DeregisterBehaviour(ModularBehaviour _behaviour)
		{
			// If the behaviour is on the player.
			if(behaviours.Contains(_behaviour))
				// Remove it from the player.
				behaviours.Remove(_behaviour);
		}

		/// <summary>
		/// Called before any Update functions.
		/// </summary>
		private void Start()
		{
			Rigidbody = gameObject.GetComponent<Rigidbody>();
			Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			
			if(collider == null)
				collider = gameObject.GetComponent<Collider>();

			if(collider == null)
			{
				collider = gameObject.AddComponent<BoxCollider>();
				Debug.LogWarning("No collider attached to MCC Player, adding standard box collider... Is this intended?", gameObject);
			}
			
			behaviours.ForEach(_behaviour =>
			{
				_behaviour.Init(this);
			});
		}

		/// <summary>
		/// Called each Update.
		/// Cycle through all the behaviours and run any processes(Update).
		/// </summary>
		private void Update() => behaviours.ForEach(_behaviour => _behaviour.Process(UpdatePhase.Update));

		/// <summary>
		/// Called each FixedUpdate.
		/// Cycle through all the behaviours and run any processes(FixedUpdate).
		/// </summary>
		private void FixedUpdate() => behaviours.ForEach(_behaviour => _behaviour.Process(UpdatePhase.FixedUpdate));

		/// <summary>
		/// Called each LateUpdate.
		/// Cycle through all the behaviours and run any processes(LateUpdate).
		/// </summary>
		private void LateUpdate() => behaviours.ForEach(_behaviour => _behaviour.Process(UpdatePhase.LateUpdate));

		/// <summary>
		/// Called when component is attached to object.
		/// </summary>
		private void Reset()
		{
			GetAttachedColliderOrAddOne();
			GetInChildrenSelectedBehaviours();
			GetInChildrenPlayerInput();
		}
		
		private void GetInChildrenPlayerInput()
		{
			if(input == null)
			{
				input = GetComponentInChildren<PlayerInput>();
				if(input == null)
					Debug.Log($"Please added a Player Input",this);
			}
		}

		[Button]
		private void GetInChildrenSelectedBehaviours()
		{
			behaviours = new List<ModularBehaviour>();
			ModularBehaviour[] behavioursInChildren = GetComponentsInChildren<ModularBehaviour>();
			foreach (ModularBehaviour modularBehaviourBehaviour in behavioursInChildren)
			{
				if (modularBehaviourBehaviour.toBeUsedInMMCParent)
				{
					behaviours.Add(modularBehaviourBehaviour);
					Debug.Log($"Added attached {modularBehaviourBehaviour} to {behaviours}", this);
				}
			}
		}
		
		private void GetAttachedColliderOrAddOne()
		{
			if (collider == null)
			{
				collider = gameObject.GetComponent<Collider>();

				if (collider == null)
				{
					collider = gameObject.AddComponent<BoxCollider>();
					Debug.Log($"No collider found. \nAdded basic box collider = {collider}", this);
				}
			}
		}
	}
}