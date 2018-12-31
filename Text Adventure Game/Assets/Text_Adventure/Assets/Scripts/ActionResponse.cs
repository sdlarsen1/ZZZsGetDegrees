using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject {

	public string[] requiredStrings;
	[TextArea]
	public string successResponse = null;
	[TextArea]
	public string failResponse = null;

	public abstract bool DoActionResponse(GameController controller, string[] separatedInputWords);
}
