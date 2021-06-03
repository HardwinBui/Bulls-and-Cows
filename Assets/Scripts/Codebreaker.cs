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

    // A struct used to represent any 4 digit number, making it easier to compare each digit
    struct Code {
        int[] code;

        // Hard-coded constructor to only support 4 digit codes
        public Code(int digit1, int digit2, int digit3, int digit4) {
            code = new int[4];
            code[0] = digit1;
            code[1] = digit2;
            code[2] = digit3;
            code[3] = digit4;
        }

        // Returns the code's digit at a given position
        public int GetDigit(int position) {
            if(position >= code.Length || position < 0) return -1;
            return code[position];
        }

        // Returns the amount of digits in the code
        public int GetLength() {
            return code.Length;
        }

        // Returns the numerical value of the code
        public int GetCode() {
            var total = 0;
            for(int i = 0; i < code.Length; i++) {
                total += code[code.Length-1-i] * (int)Mathf.Pow(10, i);
            }
            return total;
        }

        // Returns true if the code contains a given digit at the specified position
        public bool ContainsBullDigit(int digit, int position) {
            if(position >= code.Length || position < 0) return false;
            return code[position] == digit;
        }

        // Returns true if the code contains a given digit that's not at the specified position
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

    Code currentGuess;
    List<Code> allCodes, possibleCodes;

    void Start() {
        // Init the list of all possible codes
        allCodes = new List<Code>();
        for(int i = 0; i < 10; i++) {
            for(int j = 0; j < 10; j++) {
                if (i == j) continue;
                for(int k = 0; k < 10; k++) {
                    if (i == k || j == k) continue;
                    for(int l = 0; l < 10; l++) {
                        if (i == l || j == l || k == l) continue;
                        Code code = new Code(i, j, k, l);
                        allCodes.Add(code);
                    }
                }
            }
        }
    }

    // Resets the set of currently possible codes to default
    public void ResetAI() {
        possibleCodes = new List<Code>(allCodes);
    }

    // Returns true if the AI can still continue guessing
    public bool CanGuess() {
        print(possibleCodes.Count);
        return possibleCodes.Count > 0;
    }

    // Sends the AI's next guess assuming that the game hasn't ended
    public int GetGuess() {
        currentGuess = possibleCodes[0];
        return currentGuess.GetCode();
    }

    // Takes in player input and eliminates incorrect codes in the current set
    public void InputPlayerResponse(int bulls, int cows) {
        // Remove the current asnwer
        possibleCodes.Remove(currentGuess);

        // Remove all codes that don't have correct amount of bulls and cows
        for(int i = 0; i < possibleCodes.Count; i++) {
            if(!CheckCode(possibleCodes[i], bulls, cows)) {
                possibleCodes.Remove(possibleCodes[i]);
                i -= 1;
            }
        }
    }

    // Checks if given code contains correct amount of bulls and cows
    private bool CheckCode(Code code, int bulls, int cows) {
        int bullCount = 0;
        int cowCount = 0;

        for(int i = 0; i < currentGuess.GetLength(); i++) {
            int currentDigit = currentGuess.GetDigit(i);
            if(code.ContainsBullDigit(currentDigit, i))
                bullCount += 1;
            else if(code.ContainsCowDigit(currentDigit, i))
                cowCount += 1;
        }
        return bullCount == bulls && cowCount == cows;
    }
    
}
