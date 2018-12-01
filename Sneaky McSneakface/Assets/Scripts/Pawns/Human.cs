using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Pawn {

    public override int Attack(int damage) {
        // Yell if player
        if (transform.parent.GetComponent<PlayerController>()) {
            PlayerController player = transform.parent.GetComponent<PlayerController>();
            player.volume = damage;
        } else if (transform.parent.GetComponent<AIController>()) {
            // Do damage
        }

        return 0;
    }
}
