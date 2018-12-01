using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {

    public float fov = 45;
    
    private PlayerController target;

	// Use this for initialization
	void Start () {
        GameManger.instance.AIList.Add(this);
        GameManger.instance.startLocactions.Add(transform.position);
        GameManger.instance.allController.Add(this);
    }

    // Update is called once per frame
    void Update () {
        //if (canHear) {
        //    pawn.DisplayMessage("What was that");
        //}
        //if (canSee) {
        //    pawn.DisplayMessage("I can see you");
        //}
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.transform.parent != null) {
            if (collision.gameObject.transform.parent.tag == "Player") {
                target = collision.gameObject.transform.parent.GetComponent<PlayerController>();
                GameManger.instance.playerTarget = target;
                GameManger.instance.target = this;

                if (target != null) {
                    GameManger.canHear = pawn.CanHear(collision.gameObject, target.volume);
                    GameManger.canSee = pawn.CanSee(collision.gameObject, fov);
                } else {
                    Debug.LogWarning("No PlayerController on an object with a player tag");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.transform.parent != null) {
            if (collision.gameObject.transform.parent.tag == "Player") {
                GameManger.canSee = false;
            }
        }
    }
}
