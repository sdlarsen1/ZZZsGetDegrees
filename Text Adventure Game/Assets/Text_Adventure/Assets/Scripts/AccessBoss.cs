using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/AccessBoss")]
public class AccessBoss : ChangeRoomResponse {

	/*
	For this ActionResponse requiredStrings[1] matches are sep [2] etc
	Then the [0] has to match the room this can be activated from
	*/


	public override bool ChecksOut (GameController controller, string[] separatedInputWords)
	{ 
			return controller.puzzleManager.checkElevator ();

	}
}
