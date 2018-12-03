using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : Pawn {

    public Text text;               // Create a variable to send a message to
    public GameObject textbox;      // Create a variable to get the entired text area

    public override int Attack(int damage) {
        // Yell if player
        if (transform.parent.GetComponent<PlayerController>()) {                            // If the player is attacking
            PlayerController player = transform.parent.GetComponent<PlayerController>();    // store player is variable for easy access
            player.volume = damage;                                                         // change players volume
        } else if (transform.parent.GetComponent<AIController>()) {     // if its and AI
            // Do damage
        }

        return 0;
    }

    public override void DisplayMessage(string message) {
        if (textbox != null) {          // if textbox is not null
            textbox.SetActive(true);    // activate textbox
            text.text = message;        // change the text to message
        } else {
            Debug.LogWarning("No textbox is linked to the pawn component");     // if no text box throw warning
        }
    }

    public override void DisableMessage() {
        textbox.SetActive(false);               // Disable the text box
    }
}
