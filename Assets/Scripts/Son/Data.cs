using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
public class Character
{
    public string characterName;
    //public Sprite characterImage;
    public Color characterColor;
    public bool isIntro;
    public bool isRevive;
    public bool isDie;
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
    public Character DieCard;


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
    public bool Gameover=false;

    [Header("UIColor")]
    public Image MiddleUI;
    public Image TopAndBottomUI;
    public Color DefaultColor;

    public bool canRevive;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        isFirstTime = PlayerPrefs.GetInt(FirstTimeKey, 1) == 1;
        canRevive = PlayerPrefs.GetInt(ShopSystem.instance.reviveEffect, 0) == 1;

        if (isFirstTime)
        {
            currentCharacter = intro;
        }
        else
        {
            currentCharacter = characters[0];
        }

        DefaultColor=MiddleUI.color;
    }

    public void MakeDecision()
    {
        if (currentCharacter.isIntro)
        {
            Intro();
            return;
        }
        if(currentCharacter.isRevive)
        {
            Revive();
            return;
        }
        if(currentCharacter.isDie)
        {
            Kill();
            return;
        }
        
        MakeRandomDecision();
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
        GameManager.Instance.ResetElementStats();
        currentCharacter.isRevive = false;
    }

    private void Kill()
    {
        if (canRevive)
        {
            SetCardElements();
            RevivePlayer();
        }
        else
        {
            SetCardElements();
            Gameover = true;
        }
    }

        public void RevivePlayer()
        {
            currentCharacter = ReviveCard;
            MiddleUI.DOColor(DefaultColor, 1f);
        }

    public void KillPlayer(Choice choice)
    {
        currentCharacter = DieCard;
        currentChoice = choice;
        MiddleUI.DOColor(TopAndBottomUI.color, 1f);
    }


    public Choice CurrentChoice 
    {
        get { return currentChoice; }
    }

    public Character CurrentCharacter 
    {
        get { return currentCharacter; }
    }

    public Choice FindChoiceInDieCard(int DieCardIndex)
    {
        foreach (Choice choice in DieCard.choices)
        {
            if(choice == DieCard.choices[DieCardIndex])
            {
                return choice ;
            }
        }
        return null;
    }
}
