using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/MazeExit")]
public class MazeExitResponse : ChangeRoomResponse {

	public override bool ChecksOut(GameController controller, string[] separatedInputWords){
		//Debug.Log (controller);
		//Debug.Log (controller.puzzleManager);
		//Debug.Log (controller.puzzleManager.checkLevers ());
		return controller.puzzleManager.checkLevers ();
	}
}
