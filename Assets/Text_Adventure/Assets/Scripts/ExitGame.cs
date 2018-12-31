using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/ExitGame")]
public class ExitGame : InputAction {

	public override void RespondToInput (GameController controller, string[] seperatedInputWords){
		controller.LogStringWithReturn (seperatedInputWords[0]);
		Application.Quit();
	}
}
