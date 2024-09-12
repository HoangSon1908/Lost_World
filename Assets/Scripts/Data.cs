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
}

public class Data : MonoBehaviour
{
    
    
    //Data instance singleton
    public static Data instance { get; private set; }

    //make a list of character in editor
    [Header("Characters")]
    public Character[] characters;
    public Character intro;

    [Header("Card Elements")]
    public TextMeshProUGUI Info1;
    public TextMeshProUGUI TopAnswer;
    public TextMeshProUGUI BottomAnswer;
    public TextMeshProUGUI Character_Name;
    public Image Character_Image;

    private Character Character;
    private Choice Choice;
    private int randomCharacterIndex;
    private int randomChoiceIndex;

    [Header("Intro")]
    private const string FirstTimeKey = "FirstTime";
    private int IntroIndex = 0;
    private bool isFirstTime;

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

        // Kiểm tra xem người chơi có phải lần đầu chơi game hay không
        isFirstTime = PlayerPrefs.GetInt(FirstTimeKey, 1) == 1;


        if (isFirstTime)
        {
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            IntroIndex = 0;
            Intro();
        }
        else
        {
            MakeRandomDecision();
        }
    }

    public void Intro() 
    {
        //walk through the intro list
        if(isFirstTime)
        {
            Character = intro;
            Choice = Character.choices[IntroIndex];

            SetCardElements();
            IntroIndex++;
        }
        else
        {
            // Đánh dấu là người chơi đã chơi game lần đầu
            PlayerPrefs.SetInt(FirstTimeKey, 0);
            PlayerPrefs.Save();
            isFirstTime = false;
            MakeRandomDecision();
        }

    }

    public void MakeRandomDecision()
    {
        if (isFirstTime)
        {
            Intro();
        }
        
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
        Character.choices.RemoveAt(randomChoiceIndex);
        //If there are no more decisions for the character, remove the character from the list
        if (Character.choices.Count == 0)
        {
            List<Character> tempCharacters = new List<Character>(characters);
            tempCharacters.RemoveAt(randomCharacterIndex);
            characters = tempCharacters.ToArray();
        }
    }

    private void SetCardElements()
    {
        // Set the character's name
        Character_Name.text = Character.characterName;

        // Set the character's image
        //Character_Image.sprite = randomCharacter.characterImage;

        // Set the character's color
        Character_Image.color = Character.characterColor;

        // Set the choice's description
        Info1.text = Choice.description;

        // Set the choice's decisions
        TopAnswer.text = Choice.decision1;
        BottomAnswer.text = Choice.decision2;
    }

    private void RandomDecision()
    {
        // Get a random character from the topics list
        randomCharacterIndex = Random.Range(0, characters.Length);
        Character = characters[randomCharacterIndex];

        // Get a random choice from the character's choices list
        randomChoiceIndex = Random.Range(0, Character.choices.Count);
        Choice = Character.choices[randomChoiceIndex];
    }
}
