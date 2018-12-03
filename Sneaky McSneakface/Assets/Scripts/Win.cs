using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") { // if trigger is a player
            GameManger.instance.win = true;         // change the game state / win the game
        }
    }
}
