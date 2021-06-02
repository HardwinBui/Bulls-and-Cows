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

    struct Code {
        public int[] code;

        public Code(int digit1, int digit2, int digit3, int digit4) {
            code = new int[4];
            code[0] = digit1;
            code[1] = digit2;
            code[2] = digit3;
            code[3] = digit4;
        }

        public int GetCode() {
            var total = 0;
            for(int i = 0; i < code.Length; i++) {
                total += code[i] * (int)Mathf.Pow(10, i);
            }
            return total;
        }

        public bool ContainsDigit(int digit) {
            for(int i = 0; i < code.Length; i++) {
                if(code[i] == digit) {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsBullDigit(int digit, int position) {
            if(position >= code.Length || position < 0) return false;
            return code[position] == digit;
        }

        public bool ContainsCowDigit(int digit, int position) {
            if(position >= code.Length || position < 0) return false;

            for(int i = 0; i < code.Length; i++) {
                if(i != position && code[i] == digit) {
                    return true;
                }
            }
            return false;
        }
    }


    List<Code> possibleCodes = new List<Code>();
    Code currentGuess;

    void Start() {
        // Init the list
        for(int i = 0; i < 10; i++) {
            for(int j = 0; j < 10; j++) {
                if (i == j) continue;
                for(int k = 0; k < 10; k++) {
                    if (i == k || j == k) continue;
                    for(int l = 0; l < 10; l++) {
                        if (i == l || j == l || k == l) continue;
                        Code code = new Code(i, j, k, l);
                        possibleCodes.Add(code);
                    }
                }
            }
        }

        currentGuess = new Code(0,1,2,3);
    }

    public bool CanGuess() {
        return possibleCodes.Count > 0;
    }

    public int GetGuess() {
        return 2;
    }

    public void InputPlayerResponse(int bulls, int cows) {
        // Remove the current asnwer
        possibleCodes.Remove(currentGuess);

        // Remove all codes that don't have correct amount of bulls and cows
        for(int i = 0; i < possibleCodes.Count; i++) {
            if(!CheckCode(possibleCodes[i], bulls, cows)) {
                possibleCodes.Remove(possibleCodes[i]);
            }
        }
    }

    // Checks if given code contains correct amount of bulls and cows
    private bool CheckCode(Code code, int bulls, int cows) {
        int bullCount = 0;
        int cowCount = 0;

        for(int i = 0; i < currentGuess.code.Length; i++) {
            int currentDigit = currentGuess.code[i];
            if(code.ContainsBullDigit(currentDigit, i))
                bullCount += 1;
            else if(code.ContainsCowDigit(currentDigit, i))
                cowCount += 1;
        }
        return bullCount == bulls && cowCount == cows;
    }
    
}
