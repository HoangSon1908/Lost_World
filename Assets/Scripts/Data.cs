using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    public Sprite characterImage;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string description;
    public string decision1;
    public string decision2;
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

    private Character randomCharacter;
    private Choice randomChoice;
    private int randomCharacterIndex;
    private int randomChoiceIndex;

    public void MakeRandomDecision()
    {
            RandomDecision();

            SetCardElements();
    }

    private void SetCardElements()
    {
        // Set the character's name
        Character_Name.text = randomCharacter.characterName;

        // Set the character's image
        Character_Image.sprite = randomCharacter.characterImage;

        // Set the choice's description
        Info1.text = randomChoice.description;

        // Set the choice's decisions
        TopAnswer.text = randomChoice.decision1;
        BottomAnswer.text = randomChoice.decision2;
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
