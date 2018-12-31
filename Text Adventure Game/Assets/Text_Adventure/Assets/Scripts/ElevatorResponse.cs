using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ElevatorResponse")]
public class ElevatorResponse : ActionResponse {

	/*
	For this ActionResponse requiredStrings[1] matches are sep [2] etc
	Then the [0] has to match the room this can be activated from
	*/


	public override bool DoActionResponse (GameController controller, string[] separatedInputWords)
	{
		Debug.Log ("Inside the elevator response");
		controller.puzzleManager.Elevator (requiredStrings [0]);

		return true;

	}
}
