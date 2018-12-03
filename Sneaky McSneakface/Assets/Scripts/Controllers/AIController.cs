using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {

    public float fov = 45;          // Create a variable for the AI field of view
    
    private PlayerController target;    // Create a variable to get the target

	// Use this for initialization
	void Start () {
        GameManger.instance.AIList.Add(this);                           // Add this component to a GameManger list
        GameManger.instance.startLocactions.Add(transform.position);    // Add this transform position component to a GameManger list
        GameManger.instance.allController.Add(this);                    // Add this component to a GameManger list
    }

    // Update is called once per frame
    void Update () {
        // TODO:: Add message to the game world canvas element when can see or can hear the target 
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.transform.parent != null) {                                        // If colliding with an gameobject with a parent 
            if (collision.gameObject.transform.parent.tag == "Player") {                            // If colliding with a player object
                target = collision.gameObject.transform.parent.GetComponent<PlayerController>();    // Set target to the player controller
                GameManger.instance.playerTarget = target;                                          // Send the target to gameManger Component
                GameManger.instance.target = this;                                                  // Set target in the gameManger to this gameobject

                if (target != null) {                                                           // If target doesn't equals null
                    GameManger.canHear = pawn.CanHear(collision.gameObject, target.volume);     // Set canHear in the gameManger 
                    GameManger.canSee = pawn.CanSee(collision.gameObject, fov);                 // Set canSee in the gameManger
                } else {                                                                        // Otherwise
                    Debug.LogWarning("No PlayerController on an object with a player tag");     // throw warning
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {                            
        if (collision.gameObject.transform.parent != null) {                // If the gameobject that left the collider has a parent
            if (collision.gameObject.transform.parent.tag == "Player") {    // if the gameobject is a player
                GameManger.canSee = false;                                  // set canSee to false in gameManger
            }
        }
    }
}
