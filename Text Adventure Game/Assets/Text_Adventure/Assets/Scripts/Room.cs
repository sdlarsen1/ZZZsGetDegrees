using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TextAdventure/Room")] //create asset instances of this object
[System.Serializable]
public class Room : ScriptableObject {

    [TextArea] //makes text area for description bigger 
    public string description; 
    public string roomName;
    public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;

}
