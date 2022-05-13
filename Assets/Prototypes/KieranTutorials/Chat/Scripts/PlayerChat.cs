// Creator: Kieran
// Creation Time: 2022/05/11 20:24
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainProject.Prototypes.KieranTutorials.Chat
{
	public class PlayerChat : NetworkBehaviour
	{
		[SerializeField] private Vector3 movement = new Vector3();

		[Client]
		private void Update()
		{
			if(!hasAuthority) return;

			if(Input.GetKeyDown(KeyCode.Space))
				CmdMove();
		}

		[Command]
		private void CmdMove()
		{
			RpcMove();
		}

		[ClientRpc]
		private void RpcMove() => transform.Translate(movement);
	}
}