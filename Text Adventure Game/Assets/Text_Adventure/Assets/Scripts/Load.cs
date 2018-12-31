using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/LoadGame")]
public class Load : InputAction {

	public override void RespondToInput (GameController controller, string[] seperatedInputWords){
		controller.roomNavigation.Load();
	}
}

