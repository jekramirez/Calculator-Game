using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasFunctions : MonoBehaviour
{
    public GameObject[] buttons;

    bool inGame = true;

    [SerializeField]
    public int movesLeft;
    public int goalNumber;
    public int currentNumber;
    public int addedLastDigit;
    public int digitToReplace;
    public int replacementDigit;

    public Text currentNumberText;
    public Text movesLeftText;
    public Text goalNumberText;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        movesLeftText.text = "Moves: " + movesLeft.ToString();
        goalNumberText.text = "Goal: " + goalNumber.ToString();

        if (inGame)
        {
            currentNumberText.text = currentNumber.ToString();
        }

        if (movesLeft == 0 && currentNumber != goalNumber)
        {
            StartCoroutine(Delay());
            DeactivateAllButtons();
        }

        if (currentNumber == goalNumber)
        {
            StartCoroutine(Delay());
            DeactivateAllButtons();
        }
    }
    void GameOver()
    {
        currentNumberText.text = "You Lose!";
        StartCoroutine(RestartAfterDelay());
    }
    void YouWin()
    {
        currentNumberText.text = "You Win!";
        StartCoroutine(RestartAfterDelay());
    }
    IEnumerator RestartAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(3);

        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);

        if (movesLeft == 0 && currentNumber != goalNumber)
        {
            inGame = false;
            GameOver();
        }

        if (currentNumber == goalNumber)
        {
            inGame = false;
            YouWin();
        }
    }
    public void Button1()
    {
        currentNumber = currentNumber + 10;
        movesLeft = movesLeft - 1;
    }
    public void Button2()
    {
        currentNumber = currentNumber * 2;
        movesLeft = movesLeft - 1;
    }
    public void Button3()
    {
        currentNumber = currentNumber - 5;
        movesLeft = movesLeft - 1;
    }
    public void Button4()
    {
        // Removes the last digit.
        currentNumber = currentNumber / 10;
        movesLeft = movesLeft - 1;
    }
    public void Button5()
    {
        // Adds a number on the last.
        currentNumber = currentNumber * 10 + addedLastDigit;
        movesLeft = movesLeft - 1;
    }
    public void Button6()
    {
        // Reverse the digits.
        string numberString = currentNumber.ToString();
        char[] charArray = numberString.ToCharArray();
        Array.Reverse(charArray);
        string reversedNumber = new string(charArray);
        currentNumber = int.Parse(reversedNumber);
        movesLeft = movesLeft - 1;
    }
    public void Button7()
    {
        // Transform digits to another digits.
        string numberString = currentNumber.ToString();
        numberString = numberString.Replace(digitToReplace.ToString(), replacementDigit.ToString());
        currentNumber = int.Parse(numberString);
        movesLeft = movesLeft - 1;
    }
    public void ButtonClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DeactivateAllButtons()
    {

        buttons = GameObject.FindGameObjectsWithTag("Buttons");

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
}
