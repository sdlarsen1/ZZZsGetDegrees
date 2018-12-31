using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Skills")]
public class Skills : InputAction {

	// Use this for initialization
	public override void RespondToInput (GameController controller, string[] separatedInputWords)
	{
		if (controller.puzzleManager.checkHumility () || controller.puzzleManager.checkSocial () || controller.puzzleManager.checkWriting ()) {
			controller.LogStringWithReturn ("You have the following skills:");
		}
		else {controller.LogStringWithReturn ("You have no skills yet.");}
		if (controller.puzzleManager.checkHumility ()) {
			controller.LogStringWithReturn ("Humility");
		}
		else if (controller.puzzleManager.checkSocial ()) {
			controller.LogStringWithReturn ("Social");
		}
		else if (controller.puzzleManager.checkWriting ()) {
			controller.LogStringWithReturn ("Writing");
		}
	}

}
