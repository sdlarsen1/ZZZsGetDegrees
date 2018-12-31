using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/LeverResponse")]
public class LeverResponse : ActionResponse {

	public override bool DoActionResponse (GameController controller, string[] separatedInputWords)	{
		Debug.Log ("requiredStrings[0] = " + requiredStrings [0]);
		bool donePrev = !controller.puzzleManager.GetLever (requiredStrings [0]);
		Debug.Log (donePrev);
		controller.puzzleManager.Lever (requiredStrings[0]);

		if (donePrev) {
			controller.LogStringWithReturn (successResponse);
		} else {
			controller.LogStringWithReturn (failResponse);
		}

		return donePrev;
	}
}
