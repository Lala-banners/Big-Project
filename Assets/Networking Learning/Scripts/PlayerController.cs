using NetworkGame.Networking;
using System;

using UnityEngine;

namespace NetworkGame
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float speed = 2f;

		// Update is called once per frame
		private void Update()
		{
			float currentSpeed = speed * (MatchManager.instance.doubleSpeed ? 1 : 2);
			
			transform.position += transform.right * Time.deltaTime * currentSpeed * Input.GetAxis("Horizontal");
			transform.position += transform.forward * Time.deltaTime * currentSpeed * Input.GetAxis("Vertical");
		}
	}
}