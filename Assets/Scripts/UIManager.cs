using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject startScreen, makerScreen, endScreen;
    [SerializeField] TextMeshProUGUI turnDisplay, guessDisplay;
    [SerializeField] TextMeshProUGUI cows, bulls, endMessage;

#region Functions to adjust the game scene display

    // Begins the game given the AI's first guess
    public void BeginGame(int initialGuess) {
        startScreen.SetActive(false);
        makerScreen.SetActive(true);
        ToggleGuessDisplays(true);
        UpdateGuessDisplays(1, initialGuess);
        ResetBullCowValues();
    }

    // Prompts player with the AI's next guess and reset input UI
    public void NextGuess(int turnCount, int guess) {
        UpdateGuessDisplays(turnCount, guess);
        ResetBullCowValues();
    }

    // Display end screen with number of turns the game lasted
    public void LoseGame(int turns) {
        ToggleGuessDisplays(false);
        makerScreen.SetActive(false);
        endScreen.SetActive(true);

        endMessage.text = "Your number was guessed in " + turns.ToString() + " turn";
        // have 'turn' be plural if it took more than one turn
        if(turns > 1) {
            endMessage.text += "s";
        }
    }

    // Display losing screen for AI if it's unable to guess the value based on player input
    public void WinGame() {
        ToggleGuessDisplays(false);
        makerScreen.SetActive(false);
        endScreen.SetActive(true);

        endMessage.text = "I couldn't guess your answer with the provided input";
    }

    // Returns player back to title screen
    public void ResetGame() {
        startScreen.SetActive(true);
        makerScreen.SetActive(false);
        endScreen.SetActive(false);
        ToggleGuessDisplays(false);
    }

    // Update display values of cows
    public void UpdateCowDisplay(int cowAmount) { 
        cows.text = cowAmount.ToString(); 
    }

    // Update display values of bulls
    public void UpdateBullDisplay(int bullAmount) { 
        bulls.text = bullAmount.ToString(); 
    }

#endregion

    // Update values of turn and guess displays
    private void UpdateGuessDisplays(int turn, int guess) {
        turnDisplay.text = "Turn: " + turn.ToString();
        guessDisplay.text = "Guess: " + guess.ToString();
    }

    // Hide/show turn and guess displays
    private void ToggleGuessDisplays(bool enable) {
        turnDisplay.gameObject.SetActive(enable);
        guessDisplay.gameObject.SetActive(enable);
    }

    // Sets both bull and cow displays to 0
    private void ResetBullCowValues() {
        UpdateCowDisplay(0);
        UpdateBullDisplay(0);
    }
}
