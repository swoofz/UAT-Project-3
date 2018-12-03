using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Pawn {    // Inherits from Pawn Component

    public override int Attack(int damage) {
        return damage;                          // just return damage
    }

    public override float DoIdle(float speed, float num) {
        float countDown = CountDown(num);                   // Run a count down
        if (countDown < 0) {                                // If countDown hits 0
            LookAround(speed, Random.Range(1, 5));          // Rotate AI
        }
        return countDown;                                   // Return countDown
    }

    public float CountDown(float num) {
        float countDown = num;          // set your count down
        countDown -= Time.deltaTime;    // decrease count down
        return countDown;               // return new num
    }

    public void LookAround(float speed, int num) {
        if (num == 1) {     // if num equal 1
            MoveX(speed);   // Rotate right
        }
        if (num == 2) {     // if num equal 2
            MoveX(-speed);  // Rotate left
        }
        if (num == 3) {     // if num equal 3
            MoveY(speed);   // Rotate up
        }
        if (num == 4) {     // if num equal 4
            MoveY(-speed);  // Rotate down
        }
    }
}
