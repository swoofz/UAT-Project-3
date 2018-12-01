using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {

    public int health, dealDamage;      // Create a variables to handle game changes
    public float speed;                 // Create a variable to manage movement speed
    public Pawn pawn;                   // Create a variable to get a pawn controller object
}
