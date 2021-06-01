using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codebreaker : MonoBehaviour {

    /*
        NOTES:
        - have a hashSet(?) of possible options left 
        - CanGuess() requires hashSet to be of size > 0
        - InputPlayerResponse() updates hashSet
        - GetGuess() picks value that would best reduce the hashSet
    */


    public bool CanGuess() {
        return true;
        return false;
    }

    public int GetGuess() {
        return 2;
    }

    public void InputPlayerResponse(int bulls, int cows) {

    }
    
}
