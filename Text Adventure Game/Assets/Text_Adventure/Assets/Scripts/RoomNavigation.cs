using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class RoomNavigation : MonoBehaviour {

    public Room currentRoom;
    GameController controller;
    public string testName;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    void Awake() {

        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom() {

        for (int i = 0; i < currentRoom.exits.Length; i++) {
			exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom); 
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

	public void AttemptToChangeRooms(string directionNoun) {
		if (exitDictionary.ContainsKey(directionNoun)) {
			currentRoom = exitDictionary[directionNoun];
			if (directionNoun != "eyes") {
                Save();
				bool isPanda = controller.interactableItems.GetInteractableObjectFromUsableList ("panda");
				//Debug.Log (isPanda);
				if (isPanda) {
                    // ---------------------------------------------------------------------------------------- sound here
					//Debug.Log("music starts");
                    controller.soundEffects[7].Play();
				} else {
                    // ---------------------------------------------------------------------------------------- stop sound here
					//Debug.Log("music ends");
                    controller.soundEffects[7].Stop();
                }
                controller.soundEffects[0].Play();
                controller.LogStringWithReturn ("You head off to the " + directionNoun);
            }
            else{
                controller.soundEffects[2].Stop();
                controller.soundEffects[1].Play();
                controller.LogStringWithReturn("You wake up.");
            }
			controller.DisplayRoomText();
		} else {
			controller.LogStringWithReturn("There is no path to the " + directionNoun);
		}
	}

	public void ClearExits() {
		exitDictionary.Clear();
	}

    //saves data out to a file
    public void Save()  {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ZZZsGetDegreesSave.dat");

        PlayerData data = new PlayerData();
        data.nounsInInventory = controller.interactableItems.nounsInInventory;
        data.roomName = currentRoom.roomName;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        if(File.Exists(Application.persistentDataPath + "/ZZZsGetDegreesSave.dat")){
            controller.soundEffects[2].Stop();
            bool isPanda = controller.interactableItems.GetInteractableObjectFromUsableList("panda");
            if (isPanda) {
                // ---------------------------------------------------------------------------------------- sound here
                controller.soundEffects[7].Play();
            }
            else {
                // ---------------------------------------------------------------------------------------- stop sound here
                controller.soundEffects[7].Stop();
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ZZZsGetDegreesSave.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            controller.interactableItems.nounsInInventory = data.nounsInInventory;
            //AssetDatabase.FindAssets(data.roomName)[0] finds the GUID of the asset with the room name that was saved
            //AssetDatabase.GUIDToAssetPath converts the GUID into a path
            foreach(Room room in controller.listOfRooms){
                if(room.name == data.roomName){
                    currentRoom = room;
                }
            }
            controller.DisplayRoomText();
        }
    }
}

[System.Serializable]
class PlayerData{
    public List<string> nounsInInventory;
    public string roomName;
}
