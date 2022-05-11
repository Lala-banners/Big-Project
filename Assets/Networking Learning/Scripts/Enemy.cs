using UnityEngine;

public class Enemy : MonoBehaviour
{
	private void Update() => transform.position += transform.forward * (Time.deltaTime * 5f);
}