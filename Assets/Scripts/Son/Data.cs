﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    //public Sprite characterImage;
    public Color characterColor;
    public bool isIntro;
    public bool isRevive;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string description;
    public string decision1;
    public string decision2;
    
    [Header("Stat for agree")]    
    // Stat effects for decision 1 (agree)
    public int militaryEffect1;
    public int publicEsteem1;
    public int economy1;
    public int spiritualityEffect1;

    [Header("Stat for disagree")]
    // Stat effects for decision 2 (disagree)
    public int militaryEffect2;
    public int publicEsteem2;
    public int economy2;
    public int spiritualityEffect2;
    
    //Ruling days
    public int rulingDays1;
    public int rulingDays2;
}

public class Data : MonoBehaviour
{
    //Data instance singleton
    public static Data instance { get; private set; }

    //make a list of character in editor
    [Header("Characters")]
    public Character[] characters;

    [Header("Story")]
    public Character intro;
    public Character ReviveCard;

    [Header("Card Elements")]
    public TextMeshProUGUI Info1;
    public TextMeshProUGUI TopAnswer;
    public TextMeshProUGUI BottomAnswer;
    public TextMeshProUGUI Character_Name;
    public Image Character_Image;
        
    [Header("Stat UI Elements")]
    private Character currentCharacter;
    private Choice currentChoice;
    private int randomCharacterIndex;
    private int randomChoiceIndex;

    [Header("Intro")]
    private const string FirstTimeKey = "FirstTime";
    private bool isFirstTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isFirstTime = PlayerPrefs.GetInt(FirstTimeKey, 1) == 1;

        if (isFirstTime)
        {
            currentCharacter = intro;
        }
        else
        {
            currentCharacter = characters[0];
        }
    }

    public void MakeDecision()
    {
        if (currentCharacter.isIntro)
        {
            Intro();
        }
        else if(currentCharacter.isRevive)
        {
            Revive();
        }
        else
        {
            MakeRandomDecision();
        }
    }

    public void MakeRandomDecision()
    {
        //Check if the is any character left
        if (characters.Length == 0)
        {
            Debug.Log("No more characters left");
            return;
        }

            RandomDecision();

            SetCardElements();

            DeleteUsingChoice(randomChoiceIndex);

    }

    private void DeleteUsingChoice(int ChoiceIndex)
    {
        //Delete using decision
        currentCharacter.choices.RemoveAt(ChoiceIndex);
        //If there are no more decisions for the character, remove the character from the list
        if (currentCharacter.choices.Count == 0)
        {
            List<Character> tempCharacters = new List<Character>(characters);
            tempCharacters.RemoveAt(randomCharacterIndex);
            characters = tempCharacters.ToArray();
        }
    }

    private void SetCardElements()
    {
        // Set the character's name
        Character_Name.text = currentCharacter.characterName;

        // Set the character's image
        //Character_Image.sprite = randomCharacter.characterImage;

        // Set the character's color
        Character_Image.color = currentCharacter.characterColor;

        // Set the choice's description
        Info1.text = currentChoice.description;

        // Set the choice's decisions
        TopAnswer.text = currentChoice.decision1;
        BottomAnswer.text = currentChoice.decision2;
    }

    private void RandomDecision()
    {
        // Get a random character from the topics list
        randomCharacterIndex = Random.Range(0, characters.Length);
        currentCharacter = characters[randomCharacterIndex];

        // Get a random choice from the character's choices list
        randomChoiceIndex = Random.Range(0, currentCharacter.choices.Count);
        currentChoice = currentCharacter.choices[randomChoiceIndex];
    }

    private void Intro()
    {
        currentChoice = currentCharacter.choices[0];
        SetCardElements();
        DeleteUsingChoice(0);
        if (currentCharacter.choices.Count == 0)
        {
            currentCharacter.isIntro = false;
            PlayerPrefs.SetInt(FirstTimeKey, 0);
            PlayerPrefs.Save();
        }
    }

    private void Revive()
    {
        currentChoice=currentCharacter.choices[0];
        SetCardElements();
        DeleteUsingChoice(0);
        if(currentCharacter.choices.Count == 0)
        {
            GameManager.Instance.ResetElementStats();
            currentCharacter.isRevive = false;
        }
    }

    public void RevivePlayer()
    {
        currentCharacter = ReviveCard;
    }


    public Choice CurrentChoice 
    {
        get { return currentChoice; }
    }

    public Character CurrentCharacter 
    {
        get { return currentCharacter; }
    }
}
