using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] UIManager ui;
    [SerializeField] Codebreaker ai;

    const int maxBullCowValue = 5;

    // Game variables
    int turn = 1;
    bool inGame, isOver;
    int bulls, cows;

    // Start is called before the first frame update
    void Start() {
        ui.ResetGame();
    }

    // Update is called once per frame
    void Update() {
        if(!inGame && Input.anyKeyDown) {
            inGame = true;
            ui.BeginGame(0);
        }
    }

#region Public Button Functions

    // Reset the game if the guess was right
    public void CorrectGuess() {
        isOver = true;
        ui.LoseGame(turn);
    }

    public void IncorrectGuess() {
        cows = 0;
        bulls = 0;
        ui.WrongGuess();
    }

    // Allows player to end game and restart at any given time
    public void RestartGame() {
        turn = 1;
        inGame = false;
        ui.ResetGame();
    }

    public void PlayAgain() {
        inGame = false;
        isOver = false;
        ui.ResetGame();
    }

    public void AdjustBullAmount(int value) {
        bulls = (bulls + value + maxBullCowValue) % maxBullCowValue;
        ui.UpdateBullDisplay(bulls);
    }

    public void AdjustCowAmount(int value) {
        cows = (cows + value + maxBullCowValue) % maxBullCowValue;
        ui.UpdateCowDisplay(cows);
    }

#endregion

}
