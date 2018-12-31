using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {


	bool lever1, lever2, lever4;
	bool humility, social, writing;
	int skillsUsedCount;

	GameController controller;

	void Awake(){
		controller = GetComponent<GameController> ();
		lever1 = false;
		lever2 = false;
		lever4 = false;
		humility = false;
		social = false;
		writing = false;
		skillsUsedCount = 0;

	}

	public void Lever(string leverName){
		switch (leverName) {
		case("lever1"):
			// ------------------------------------------------------------------ puzzle element solved
			lever1 = true;
            controller.soundEffects[4].Play();
			Debug.Log ("lever1 is true");
			break;
		case("lever2"):
			// ------------------------------------------------------------------ puzzle element solved
			lever2 = true;
            controller.soundEffects[4].Play();
            Debug.Log ("lever2 is true");
			break;
		case("lever4"):
			// ------------------------------------------------------------------ puzzle element solved
			lever4 = true;
            controller.soundEffects[4].Play();
            Debug.Log ("lever4 is true");
			break;
		default:
			Debug.Log ("Tried to use a lever that shouldn't exist.");
			break;
		}
	}

	public bool GetLever(string leverName){
		switch (leverName) {
		case("lever1"):
			return lever1;
		case("lever2"):
			return lever2;
		case("lever4"):
			return lever4;
		}

		return false;
	}

	public bool checkLevers(){
		if (lever1 && lever2 && lever4) {
			controller.soundEffects[7].Stop();
		}
		return lever1 && lever2 && lever4;
	}


	public void Elevator(string weaknessName){
		switch (weaknessName) {
		case("humility"):
			Debug.Log ("humility");
			humility = true;
            break;
		case("social"):
			Debug.Log ("social");
			social = true;
            break;
		case("writing"):
			Debug.Log ("writing");
			writing = true;          
            break;
		default:
			Debug.Log ("Tried to use use a weakness that shouldn't exist.");
			break;
		}
	}

	public bool checkElevator(){
		return humility && social && writing;
	}

	public bool checkHumility(){
		return humility;
	}

	public bool checkSocial(){
		return social;
	}

	public bool checkWriting(){
		return writing;
	}

	public int getSkillsUsedCount() {
		return skillsUsedCount;
	}

	public int updateSkillsUsedCount(GameController controller, string skillUsed) {
		switch (skillUsed) {
		case("humility"):
			controller.soundEffects[5].Play();
			controller.LogStringWithReturn ("Dam, how did you know? I guess other faculties need funding as well.");
			humility = false;
			break;
		case("writing"):
			controller.soundEffects[5].Play();
			controller.LogStringWithReturn ("Look, does anyone actually like going outside?");
			writing = false;
			break;
		case("social"):
			controller.soundEffects[5].Play();
			controller.LogStringWithReturn ("The Dean looks at you awkwardly, not knowing what to say.");
			social = false;
			break;
		}

		return ++skillsUsedCount;
	}
}
