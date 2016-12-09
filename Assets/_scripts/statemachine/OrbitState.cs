using UnityEngine;
using System.Collections;

public class OrbitState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public OrbitState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}

	public  void UpdateState ()
	{
		//Debug.Log ("In Orbit mode");
		enemy.OrbitPlayer ();
		ToChaseState ();
		ToEvadeState();
	}

	public void ToOrbitState()
	{
		Debug.Log ("Orbit State can't transition to self!!!");
	}

	public void ToAttackState()
	{
//		if (enemy.distance > enemy.chaseDistance)
//		{
//			enemy.StopOrbitPlayer();
//			enemy.currentState = enemy.attackState;
//		}
	}

	public void ToChaseState()
	{
		if (enemy.distance >= enemy.chaseDistance)
		{
			enemy.StopAttackPlayer();
			enemy.currentState = enemy.chaseState;
		}
	}

	public void ToEvadeState()
	{
		if (enemy.distance <= enemy.evadeDistance)
		{
			enemy.StopAttackPlayer ();
			enemy.currentState = enemy.evadeState;
		}
	}
}
