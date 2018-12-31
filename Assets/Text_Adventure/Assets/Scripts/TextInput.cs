using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    public InputField inputField;

    GameController controller;

	int quickFinish = 0;

    void Awake() {
        controller = GetComponent<GameController>();
		Debug.Log (inputField);
		Debug.Log (inputField.onEndEdit);
        inputField.onEndEdit.AddListener(AcceptStringInput);     // listener for user input
    }

    void AcceptStringInput(string userInput) {
        userInput = userInput.ToLower();             // take user input
        controller.LogStringWithReturn(userInput);

		char[] delimiterCharacters = { ' ' };        // delimiters
		string[] separatedInputWords = userInput.Split(delimiterCharacters);  // split string

		bool validInput = false;

		for (int i = 0; i < controller.inputActions.Length; i++) {
			InputAction inputAction = controller.inputActions[i];

			if (inputAction.keyWord == separatedInputWords[0]) {       // if first word is a key, respond
				validInput = true;
				inputAction.RespondToInput(controller, separatedInputWords);
			} 

		}

		if (!validInput) {
			if (separatedInputWords [0] == "carry" && separatedInputWords [1] == "the" && separatedInputWords [2] == "two") {
				QuickFinish ();
			} else {
				controller.LogStringWithReturn ("Command could not be recognized");
			}
		}

		InputComplete();                             // reset input field
    }

    void InputComplete() {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }

	void QuickFinish(){
		quickFinish++;
		if (quickFinish == 1) {
			controller.LogStringWithReturn ("Command could not be recognized");
		}
		if (quickFinish == 2) {
			controller.LogStringWithReturn ("You feel like you're in a dream... that's strange.");
		}
		if (quickFinish == 3) {
			controller.LogStringWithReturn ("Wait! This is a dream!");
		}

		if (quickFinish >= 3) {
			controller.soundEffects[3].Play();
			controller.roomNavigation.currentRoom = controller.listOfRooms[39];
			controller.roomNavigation.Save ();
			controller.DisplayRoomText ();
		}
	}
}
