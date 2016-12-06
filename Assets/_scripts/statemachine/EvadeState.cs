using UnityEngine;
using System.Collections;

public class EvadeState : IEnemyState {


	private readonly StatePatternEnemy enemy;

	public EvadeState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}
	public void UpdateState ()
	{
		Debug.Log ("In Evade mode");
		ToAttackState ();
		ToChaseState ();
	}

	public void ToAttackState()
	{
		//Not sure if second if statement is needed
		if (enemy.distance >= 5.0f && enemy.distance<=10.0f) 
			enemy.currentState = enemy.attackState;
	}

	public void ToChaseState()
	{
		if (enemy.distance > 10.0f)
			enemy.currentState = enemy.attackState;
	}

	public void ToEvadeState()
	{
		Debug.Log ("Evade State can't transition to self!!!");
	}
}
