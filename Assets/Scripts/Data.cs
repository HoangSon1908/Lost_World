using System.Collections;
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
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string description;
    public string decision1;
    public string decision2;

    // Stat effects for decision 1 (agree)
    public int militaryEffect1;
    public int publicEsteem1;
    public int economy1;
    public int spiritualityEffect1;

    // Stat effects for decision 2 (disagree)
    public int militaryEffect2;
    public int publicEsteem2;
    public int economy2;
    public int spiritualityEffect2;

    // Elements' name
    public string element1;
    public string element2;
    public string element3;
    public string element4;
}

public class Data : MonoBehaviour
{
    //Data instance singleton
    public static Data instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //make a list of character in editor
    [Header("Characters")]
    public Character[] characters;

    [Header("Card Elements")]
    public TextMeshProUGUI Info1;
    public TextMeshProUGUI TopAnswer;
    public TextMeshProUGUI BottomAnswer;
    public TextMeshProUGUI Character_Name;
    public Image Character_Image;

    [Header("Stat UI Elements")]
    public Image militaryPowerStatBar;
    public Image publicEsteemStatBar;
    public Image economyStatBar;
    public Image spiritualityStatBar;

    private Character randomCharacter;
    private Choice randomChoice;
    public PreviewStatChange previewStatChange;
    private int randomCharacterIndex;
    private int randomChoiceIndex;

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

            DeleteUsingChoice();

    }

    private void DeleteUsingChoice()
    {
        //Delete using decision
        randomCharacter.choices.RemoveAt(randomChoiceIndex);
        //If there are no more decisions for the character, remove the character from the list
        if (randomCharacter.choices.Count == 0)
        {
            List<Character> tempCharacters = new List<Character>(characters);
            tempCharacters.RemoveAt(randomCharacterIndex);
            characters = tempCharacters.ToArray();
        }
    }

    private void SetCardElements()
    {
        // Set the character's name
        Character_Name.text = randomCharacter.characterName;

        // Set the character's image
        //Character_Image.sprite = randomCharacter.characterImage;

        // Set the character's color
        Character_Image.color = randomCharacter.characterColor;

        // Set the choice's description
        Info1.text = randomChoice.description;

        // Set the choice's decisions
        TopAnswer.text = randomChoice.decision1;
        BottomAnswer.text = randomChoice.decision2;

        StartCoroutine(previewStatChange.FlickerStatChange(militaryPowerStatBar, randomChoice.militaryEffect1));
        StartCoroutine(previewStatChange.FlickerStatChange(economyStatBar, randomChoice.economy1));
        StartCoroutine(previewStatChange.FlickerStatChange(publicEsteemStatBar, randomChoice.publicEsteem1));
        StartCoroutine(previewStatChange.FlickerStatChange(spiritualityStatBar, randomChoice.spiritualityEffect1));
    }

    private void RandomDecision()
    {
        // Get a random character from the topics list
        randomCharacterIndex = Random.Range(0, characters.Length);
        randomCharacter = characters[randomCharacterIndex];

        // Get a random choice from the character's choices list
        randomChoiceIndex = Random.Range(0, randomCharacter.choices.Count);
        randomChoice = randomCharacter.choices[randomChoiceIndex];
    }
}
