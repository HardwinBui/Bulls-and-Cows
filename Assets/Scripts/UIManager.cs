using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject startScreen, breakerScreen, makerScreen, endScreen;
    [SerializeField] GameObject restartButton;
    [SerializeField] TextMeshProUGUI turnDisplay, guessDisplay;
    [SerializeField] TextMeshProUGUI cows, bulls, endMessage;

#region Functions to adjust the game scene display

    // Begins the game given the AI's first guess
    public void BeginGame(int initialGuess) {
        startScreen.SetActive(false);
        restartButton.SetActive(true);
        breakerScreen.SetActive(true);
        ToggleGuessDisplays(true);
        UpdateGuessDisplays(1, initialGuess);
    }

    // Prompts player with the AI's next guess
    public void NextGuess(int turnCount, int guess) {
        UpdateGuessDisplays(turnCount, guess);
        makerScreen.SetActive(false);
        breakerScreen.SetActive(true);
    }

    // Changes screen to allow players to input bulls and cows
    public void WrongGuess() {
        breakerScreen.SetActive(false);
        makerScreen.SetActive(true);
        UpdateCowDisplay(0);
        UpdateBullDisplay(0);
    }

    // Display end screen with number of turns the game lasted
    public void LoseGame(int turns) {
        ToggleGuessDisplays(false);
        breakerScreen.SetActive(false);
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

        endMessage.text = "I can't guess your answer with the provided input";
    }

    // Returns player back to title screen
    public void ResetGame() {
        startScreen.SetActive(true);
        restartButton.SetActive(false);
        breakerScreen.SetActive(false);
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
}
