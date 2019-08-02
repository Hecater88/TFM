using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetConditionReactionPuzzle6 : Reaction {
    public int number;
    // Use this for initialization
    protected override IEnumerator React() {
        yield return new WaitForSeconds(delay);

        // modificamos el estado de la condición
        DrawingLines.DLines.SetConditions(number);
    }
}
