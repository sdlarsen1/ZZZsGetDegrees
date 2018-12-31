using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Help")]
public class Help : InputAction {

	public override void RespondToInput (GameController controller, string[] seperatedInputWords){
		controller.LogStringWithReturn ("Available commands are:" +
			"\nExamine - to look at items or the room again" +
			"\nGo - to move between rooms" +
			"\nTake - to take an item" +
			"\nTalk - to talk to the panda or other living beings" +
			"\nInventory - to check your inventory" +
			"\nUse - to use an item in a room or in your inventory" +
			"\nSkills - to check what skills you've learned" +
			"\nExit - to exit the game");
	}
}
