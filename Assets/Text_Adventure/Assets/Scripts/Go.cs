using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Go")]
public class Go : InputAction  {

	public override void RespondToInput(GameController controller, string[] separatedInputWords) {
		if (separatedInputWords.Length > 1) {
			controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);  // pass exit keyword 
		} else {
			controller.LogStringWithReturn ("Where do you want to go?");
		}
	}
}
