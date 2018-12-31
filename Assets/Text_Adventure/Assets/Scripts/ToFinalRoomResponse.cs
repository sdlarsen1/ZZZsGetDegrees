using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ToFinalRoom")]
public class ToFinalRoomResponse : ChangeRoomResponse {

	public override bool ChecksOut(GameController controller, string[] separatedInputWords){
		Debug.Log (controller);
		Debug.Log (controller.puzzleManager);
		Debug.Log (controller.puzzleManager.getSkillsUsedCount ());
		if (controller.puzzleManager.getSkillsUsedCount () >= 3) {
            controller.soundEffects[3].Play();
			return true;
		} else {
			return false;
		}
	}
}
