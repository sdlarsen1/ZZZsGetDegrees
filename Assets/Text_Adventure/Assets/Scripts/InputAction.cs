using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Base class for input actions
 */

public abstract class InputAction : ScriptableObject {

	public string keyWord;

	public abstract void RespondToInput(GameController controller, string[] separatedInputWords);
}
