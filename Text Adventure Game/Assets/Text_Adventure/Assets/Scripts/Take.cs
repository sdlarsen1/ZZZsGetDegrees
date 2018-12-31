﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Take")]
public class Take : InputAction {

	public override void RespondToInput (GameController controller, string[] seperatedInputWords){
		if (seperatedInputWords.Length > 1) {
			Dictionary<string, string> takeDictionary = controller.interactableItems.Take (seperatedInputWords);

			if (takeDictionary != null) {
				controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (takeDictionary, seperatedInputWords [0], seperatedInputWords [1]));
			}
		} else {
			controller.LogStringWithReturn ("What do you want to take?");
		}	
	}
}


