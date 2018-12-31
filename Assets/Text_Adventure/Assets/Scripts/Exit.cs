using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit {

    public string keyString; //the strings that we're looking for (go north, go south, etc.)
    public string exitDescription;
    public Room valueRoom;

}
