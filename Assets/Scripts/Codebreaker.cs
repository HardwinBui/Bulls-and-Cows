using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codebreaker : MonoBehaviour {

    // A struct used to represent any 4 digit number, making it easier to compare each digit
    struct Code {
        int[] code;
        HashSet<int> digits;

        // Hard-coded constructor to only support 4 digit codes
        public Code(int digit1, int digit2, int digit3, int digit4) {
            code = new int[4];
            code[0] = digit1;
            code[1] = digit2;
            code[2] = digit3;
            code[3] = digit4;

            digits = new HashSet<int>();
            for(int i = 0; i < code.Length; i++) {
                digits.Add(code[i]);
            }
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
            if(ContainsBullDigit(digit, position)) return false;
            return digits.Contains(digit);
        }
    }

    Code currentGuess, nextGuess;
    HashSet<Code> allCodes, possibleCodes;
    List<Code> eliminatedCodes = new List<Code>();

#region Core functions to be called by GameManager.cs
    // Resets the set of currently possible codes to default
    public void ResetAI() {
        if(allCodes == null) {
            InitAllPossibleCodes();
        }
        nextGuess = new Code(9,8,7,6);
        possibleCodes = new HashSet<Code>(allCodes);
    }

    // Returns true if the AI can still continue guessing
    public bool CanGuess() {
        return possibleCodes.Count > 0;
    }

    // Sends the AI's next guess assuming that the game hasn't ended
    public int GetGuess() {
        currentGuess = nextGuess;
        return currentGuess.GetCode();
    }

    // Takes in player input and eliminates incorrect codes in the current set
    public void InputPlayerResponse(int bulls, int cows) {
        // Remove the current answer
        possibleCodes.Remove(currentGuess);

        // Find all codes that need to be removed based on given bulls and cows
        eliminatedCodes.Clear();
        foreach(Code code in possibleCodes) {
            if(!CheckCode(code, bulls, cows)) {
                eliminatedCodes.Add(code);
            }
            // Use last valid code as the next guess
            else { nextGuess = code; }
        }

        // Remove all undesired codes from the hashset
        for(int i = 0; i < eliminatedCodes.Count; i++)
            possibleCodes.Remove(eliminatedCodes[i]);
    }
#endregion

    // Init the list of all possible codes
    private void InitAllPossibleCodes() {
        allCodes = new HashSet<Code>();
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
