using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Examine")]
public class Examine : InputAction {

	public override void RespondToInput (GameController controller, string[] separatedInputWords){
		InteractableItems ii = controller.interactableItems;
		if (separatedInputWords.Length > 1) {
			if (separatedInputWords [1] == "room") {
				controller.DisplayRoomText ();
			} else {
				controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (ii.examineDictionary, separatedInputWords [0], separatedInputWords [1]));
				InteractableObject item = ii.GetInteractableObjectFromUsableList (separatedInputWords [1]);
				if (item != null) {
					ii.ExecuteActionResponse (ii.GetInteraction (item, "examine"), separatedInputWords);
				}
			}
		} else {
			controller.LogStringWithReturn ("What do you want to examine?");
		}
	}
}
