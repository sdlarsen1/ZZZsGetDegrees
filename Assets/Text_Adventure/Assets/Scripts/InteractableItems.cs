using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

	public List<InteractableObject> usableItemList;

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string> ();
	public Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse> ();

	[HideInInspector] List<string> nounsInRoom = new List<string> ();

	[HideInInspector] public List<string> nounsInInventory = new List<string> ();
	GameController controller;

	void Awake(){
		controller = GetComponent<GameController> ();
	}

	public string GetObjectsNotInInventory(Room currentRoom, int i){
		InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

		if (!nounsInInventory.Contains (interactableInRoom.noun)) {
			nounsInRoom.Add (interactableInRoom.noun);
			return interactableInRoom.description;
		}

		return null;
	}

	public void AddActionResponsesToUseDictionary(){
		
		for (int i = 0; i < nounsInInventory.Count; i++) {
			string noun = nounsInInventory [i];
			//Debug.Log ("There is a " + noun + " in your inventory");

			AddActionResponsesToUseDictionaryInsideLoop (noun);
		}

		for (int i = 0; i < nounsInRoom.Count; i++) {
			string noun = nounsInRoom [i];
			//Debug.Log ("There is a " + noun + " in the room");

			AddActionResponsesToUseDictionaryInsideLoop (noun);
		}

	}

	void AddActionResponsesToUseDictionaryInsideLoop(string noun){
		InteractableObject interactableObjectInArea = GetInteractableObjectFromUsableList (noun);

		if (interactableObjectInArea == null) {
			//Debug.Log (""+noun+" is not in the list of usable objects");
			return;
		}

		//Debug.Log(interactableObjectInArea);
		//Debug.Log (noun+" has "+interactableObjectInArea.interactions.Length + " interactions");
		for (int j = 0; j < interactableObjectInArea.interactions.Length; j++) {
			Interaction interaction = interactableObjectInArea.interactions [j];
			//Debug.Log ("interaction "+j+" = "+interaction.inputAction.keyWord);

			if (interaction.actionResponse == null) {

				//Debug.Log ("    " +noun+" does not have an action response");
				continue;
			}

			if (!useDictionary.ContainsKey(noun)) {
				//Debug.Log ("" +noun+" added to usedictionary");
				//Debug.Log ("" + interaction.actionResponse+" added to usedictionary");
				useDictionary.Add (noun, interaction.actionResponse);
			}
		}
	}

	public InteractableObject GetInteractableObjectFromUsableList(string noun){
		InteractableObject[] items = controller.roomNavigation.currentRoom.interactableObjectsInRoom;
		for (int i = 0; i < items.Length; i++) {
			if (items[i].noun == noun) {
				//Debug.Log(""+items[i]+" is here with "+items[i].interactions.Length+ " interactions");
				return items [i];
			}
		}
		return null;
	}
		
	public Interaction GetInteraction(InteractableObject obj, string key = "use"){
		Interaction ret = null;

		for (int i = 0; i < obj.interactions.Length; i++) {
			if (obj.interactions[i].inputAction.keyWord == key) {
				ret = obj.interactions [i];
			}
		}
		return ret;
	}

	public void DisplayInventory(){
		controller.LogStringWithReturn ("You are carrying: ");

		string compiledInventory = "";

		for (int i = 0; i < nounsInInventory.Count; i++) {
			compiledInventory = compiledInventory + nounsInInventory [i] + "\n";
			// Debug.Log (nounsInInventory [i]);
		}

		if (nounsInInventory.Count == 0) {
			compiledInventory = "Nothing" ;
		}
		controller.LogStringWithReturn (compiledInventory);
	}

	public void ClearCollections(){
		ClearExamine ();
		takeDictionary.Clear ();
		nounsInRoom.Clear ();
	}

	void ClearExamine(){
		List<string> removeList = new List<string>();
		foreach (KeyValuePair<string, string> item in examineDictionary) {
			if (!nounsInInventory.Contains(item.Key)) {
				removeList.Add (item.Key);
			}
		}

		foreach (string key in removeList) {
			examineDictionary.Remove (key);
		}
	}

	public Dictionary<string, string> Take(string[] seperatedInputWords){
		string noun = seperatedInputWords [1];
		if (nounsInRoom.Contains(noun)) {
			if( takeDictionary.ContainsKey (noun)){
				nounsInInventory.Add(noun);
				nounsInRoom.Remove(noun);
				return takeDictionary;
			}else{
				controller.LogStringWithReturn("You cannot take the " + noun + "!");
				return null;
			}
		} else{
			controller.LogStringWithReturn("There is no " + noun + " here to take.");
			return null;
		}
	}

	public void UseItem(string[] seperatedInputWords){
		string nounToUse = seperatedInputWords [1];

		/*
		InteractableObject interactableObject = GetInteractableObjectFromUsableList (nounToUse);
		if (interactableObject.interactions[0].textResponse != null) {
			controller.LogStringWithReturn (interactableObject.interactions [0].textResponse);
		}
		*/
		//AddActionResponsesToUseDictionary (); //Fixme: find a better time to use this than everytime you try to use something

		if (nounsInInventory.Contains(nounToUse) || nounsInRoom.Contains(nounToUse)) {

			Interaction interaction = GetInteraction ( GetInteractableObjectFromUsableList (nounToUse) );
			if (interaction == null) {
				interaction = GetInteraction ( GetInteractableObjectFromUsableList (nounToUse), "talk" );
			}

			//Debug.Log (interaction);

			if (interaction != null && interaction.textResponse != null) {
				controller.LogStringWithReturn (interaction.textResponse);
				//Debug.Log ("Printed textResponse");
			}
			/*
			//Find the right object and print it's description
			Room currentRoom = controller.roomNavigation.currentRoom;
			InteractableObject[] usableItemsInRoom = currentRoom.interactableObjectsInRoom;
			string textResponse = null;
			for (int i = 0; i < usableItemsInRoom.Length; i++) {
				if (usableItemsInRoom[i].noun == nounToUse && usableItemsInRoom[i].interactions[0].textResponse != null) {
					textResponse = usableItemsInRoom [i].interactions[0].textResponse;
					controller.LogStringWithReturn (textResponse);
					Debug.Log ("Printed textResponse");
				}
			}
			*/
			ExecuteActionResponse (interaction, seperatedInputWords);

		} else {
			controller.LogStringWithReturn("There is no " + nounToUse + " around to use");
		}

	}

	public void ExecuteActionResponse(Interaction interaction, string[] seperatedInputWords){
		if (interaction == null) {
			controller.LogStringWithReturn("You can't use the " + seperatedInputWords[1]);
		} else {
			if (interaction.actionResponse != null) {
				bool actionResult = interaction.actionResponse.DoActionResponse (controller, seperatedInputWords);
				if (actionResult) {
					//Debug.Log ("Used "+nounToUse+" successfully");
				} else {
					//Debug.Log ("Used "+nounToUse+" unsuccessfully");
					// I moved success and failure text into the individual actionResponses
				}
			} else {
				if (interaction.textResponse == null) {
					controller.LogStringWithReturn("You can't use the " + seperatedInputWords[1]);
				}
			}
		}

	}

	public void UseSkills(GameController controller, string[] seperatedInputWords) {
		if (seperatedInputWords [1] == "writing") {
			if (controller.puzzleManager.checkWriting ()) {
				controller.puzzleManager.updateSkillsUsedCount (controller, "writing");
			}
		}

		if (seperatedInputWords [1] == "social") {
			if (controller.puzzleManager.checkSocial ()) {
				controller.puzzleManager.updateSkillsUsedCount (controller, "social");
			}
		}

		if (seperatedInputWords [1] == "humility") {
			if (controller.puzzleManager.checkHumility ()) {
				controller.puzzleManager.updateSkillsUsedCount (controller, "humility");
			}
		}

		if (controller.puzzleManager.getSkillsUsedCount() == 3) {
			controller.LogStringWithReturn ("You've defeated the Dean of Beavers!");
		}
	}
}
