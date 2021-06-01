using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] UIManager ui;
    [SerializeField] Codebreaker ai;

    const int digitAmount = 4;
    const int inputOptions = digitAmount + 1;

    // Game variables
    int turn = 1;
    bool inGame;
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
        ui.LoseGame(turn);
    }

    public void IncorrectGuess() {
        //ui.WrongGuess();

        // TODO: check if can guess again, if not then GG

        ui.NextGuess(0,0);
    }

    // Progress the game based on values of bulls and cows
    public void EnterValues() {
        if(bulls >= digitAmount) {
            ui.LoseGame(turn);
        }
        else {
            print("poopy");
        }
    }

    public void PlayAgain() {
        inGame = false;
        ResetInputValues();
        ui.ResetGame();
    }

    public void AdjustBullAmount(int value) {
        bulls = (bulls + value + inputOptions) % inputOptions;
        ui.UpdateBullDisplay(bulls);
    }

    public void AdjustCowAmount(int value) {
        cows = (cows + value + inputOptions) % inputOptions;
        ui.UpdateCowDisplay(cows);
    }

#endregion

    private void ResetInputValues() {
        cows = 0;
        bulls = 0;
    }

}
