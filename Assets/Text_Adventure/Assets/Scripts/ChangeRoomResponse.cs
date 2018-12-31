using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {

	/*
	For this ActionResponse requiredStrings[1] matches are sep [2] etc
	Then the [0] has to match the room this can be activated from
	*/

	public Room roomToChangeTo;

	public override bool DoActionResponse (GameController controller, string[] separatedInputWords)
	{
		bool checksOut = ChecksOut (controller, separatedInputWords);

		if (checksOut) {
			if (successResponse != null) {
				controller.LogStringWithReturn (successResponse);
			}
			controller.roomNavigation.currentRoom = roomToChangeTo;
			controller.roomNavigation.Save ();
			controller.DisplayRoomText ();
			return true;
		} else {
			if (failResponse != null) {
				controller.LogStringWithReturn (failResponse);
				//"Hmm. Nothing happens. Maybe try the XX-XX-XX format from the riddle? Or look around for more clues."
			}
			return false;
		}


	}

	public virtual bool ChecksOut(GameController controller, string[] separatedInputWords){

		bool checksOut = true;

		if (requiredStrings[0] != controller.roomNavigation.currentRoom.roomName) {
			checksOut = false;
		}

		if (separatedInputWords.Length - 2 >= requiredStrings.Length - 1) {
			for (int i = 1; i < requiredStrings.Length; i++) {
				if (requiredStrings [i] != separatedInputWords [i + 1]) {
					checksOut = false;
				}
			}
		} else {
			checksOut = false;
		}

        if(checksOut) {
            controller.soundEffects[6].Play();
        }

		return checksOut;
	}
}
