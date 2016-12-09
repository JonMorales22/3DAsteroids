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
		enemy.ChasePlayer ();
		ToOrbitState ();
		ToEvadeState ();
	}
	public void ToAttackState(){

	}
	public void ToOrbitState()
	{
		//Not sure if second if statement is needed
		if (enemy.distance <= enemy.chaseDistance && enemy.distance>=enemy.evadeDistance) 
			enemy.currentState = enemy.orbitState;
	}

	public void ToChaseState()
	{
		Debug.Log ("Chase State can't transition to self!!!");
	}

	public void ToEvadeState()
	{
		if (enemy.distance < enemy.evadeDistance)
			enemy.currentState = enemy.evadeState;
	}
}
