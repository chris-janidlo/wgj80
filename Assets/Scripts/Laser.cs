using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Laser : MonoBehaviour
{
	public float Speed;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward.normalized * Speed;
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.collider.tag == "Player")
		{
			Debug.Log("game over");
		}
		Destroy(gameObject);
	}
}
