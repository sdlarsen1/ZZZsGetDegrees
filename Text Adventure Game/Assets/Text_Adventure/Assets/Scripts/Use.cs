using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("TextAdventure/InputAction/Use"))]
public class Use : InputAction {

	public override void RespondToInput (GameController controller, string[] separatedInputWords){
		if (separatedInputWords.Length > 1) {
			if ((separatedInputWords [1] == "humility") || (separatedInputWords [1] == "social") || (separatedInputWords [1] == "writing")) {
				Debug.Log ("you have attempted to use a skill");
				controller.interactableItems.UseSkills (controller, separatedInputWords);
			} else {
				controller.interactableItems.UseItem (separatedInputWords); 
			}
		} else {
			controller.LogStringWithReturn ("What do you want to use?");
		}
	}
}
