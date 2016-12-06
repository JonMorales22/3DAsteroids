using UnityEngine;
using System.Collections;

public interface IEnemyState {

	//LMAO
	void UpdateState();
	void ToAttackState ();
	void ToChaseState ();
	void ToEvadeState ();

}
