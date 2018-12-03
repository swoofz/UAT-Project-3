using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Pawn {

    public override int Attack(int damage) {
        // Yell if player
        if (transform.parent.GetComponent<PlayerController>()) {                            // If the player is attacking
            PlayerController player = transform.parent.GetComponent<PlayerController>();    // store player is variable for easy access
            player.volume = damage;                                                         // change players volume
        } else if (transform.parent.GetComponent<AIController>()) {
            // Do damage
        }

        return 0;
    }
}
