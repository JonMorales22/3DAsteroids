using UnityEngine;
using System.Collections;

public class ApplyDamage : MonoBehaviour {
	public int damage;

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Player"))
			PlayerStats.Instance.TakeDamage (damage);
	}
}
