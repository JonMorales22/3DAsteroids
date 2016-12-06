using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public ChaseState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}
	public void UpdateState ()
	{
		Debug.Log ("In Chase mode");
		ToAttackState ();
		ToEvadeState ();
	}

	public void ToAttackState()
	{
		//Not sure if second if statement is needed
		if (enemy.distance <= 10.0f && enemy.distance>=5.0f) 
			enemy.currentState = enemy.attackState;
	}

	public void ToChaseState()
	{
		Debug.Log ("Chase State can't transition to self!!!");
	}

	public void ToEvadeState()
	{
		if (enemy.distance < 5.0f)
			enemy.currentState = enemy.evadeState;
	}
}
