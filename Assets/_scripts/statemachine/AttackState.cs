using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public AttackState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}

	public  void UpdateState ()
	{
		Debug.Log ("In Attack mode");
		enemy.StartAttackPlayer ();
		ToChaseState ();
		ToEvadeState ();
	}

	public void ToAttackState()
	{
		Debug.Log ("Attack State can't transition to self!!!");
	}

	public void ToOrbitState()
	{
		
	}

	public void ToChaseState()
	{
		if (enemy.distance > enemy.chaseDistance)
		{
			enemy.StopAttackPlayer();
			enemy.currentState = enemy.chaseState;
		}
	}

	public void ToEvadeState()
	{
		if (enemy.distance < enemy.evadeDistance)
		{
			enemy.StopAttackPlayer ();
			enemy.currentState = enemy.evadeState;
		}
	}
}
