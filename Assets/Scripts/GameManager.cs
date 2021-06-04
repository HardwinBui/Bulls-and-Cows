using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] UIManager ui;
    [SerializeField] Codebreaker ai;

    public const int digitAmount = 4;
    const int inputOptions = digitAmount + 1;

    // Game variables
    int turn = 1;
    bool inGame;
    int bulls, cows;

    // Start is called before the first frame update
    void Start() {
        if(!ui) ui = FindObjectOfType<UIManager>();
        if(!ai) ai = FindObjectOfType<Codebreaker>();

        PlayAgain();
    }

    // Update is called once per frame
    void Update() {
        // If the game hasn't started, check player input to begin
        if(!inGame && Input.anyKeyDown) {
            inGame = true;
            ui.BeginGame(turn, ai.GetGuess());
        }
    }

#region Public Button Functions

    // Progress the game based on values of bulls and cows
    public void EnterValues() {
        // The game ends when the AI guesses right
        if(bulls >= digitAmount) {
            ui.LoseGame(turn);
        }
        // The game progresses if the AI can still guess
        else {
            ai.InputPlayerResponse(bulls, cows);
            if(ai.CanGuess()) {
                turn += 1;
                ui.UpdateGuessDisplays(turn, ai.GetGuess());

                ResetInputValues();
            }
            // If the AI can't guess anymore, the game ends
            // NOTE: this should only occur if the player failed to input bull and cow values correctly
            else {
                ui.WinGame();
            }
        }
    }

    // Restarts the game once the player is ready
    public void PlayAgain() {
        turn = 1;
        inGame = false;
        ResetInputValues();
        
        ai.ResetAI();
        ui.ResetGame();
    }

    // Allows player to increment/decrement value of bulls
    public void AdjustBullAmount(int value) {
        bulls = (bulls + value + inputOptions) % inputOptions;
        ui.UpdateBullDisplay(bulls);
    }

    // Allows player to increment/decrement value of cows
    public void AdjustCowAmount(int value) {
        cows = (cows + value + inputOptions) % inputOptions;
        ui.UpdateCowDisplay(cows);
    }

#endregion

    // Reset cows and bulls to their default value
    private void ResetInputValues() {
        cows = 0;
        bulls = 0;
        ui.UpdateCowDisplay(cows);
        ui.UpdateBullDisplay(bulls);
    }

}
