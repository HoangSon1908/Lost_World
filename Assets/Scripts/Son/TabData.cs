using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class CharacterUIElement
{
    public string name;
    public Color color;
    public bool hasEncountered;
}

public class TabData : MonoBehaviour
{
    // Singleton pattern
    public static TabData instance;

    [Header("Shop System")]
    public Button[] itemButtons;

    public string reviveEffect;
    public string prophecyEffect;
    public string NoAdsEffect;

    private string[] itemKeys;

    [HideInInspector] public bool canRevive;
    [HideInInspector] public bool seeTheFuture;

    [Header("Processing system")]
    public GameObject[] CharacterUIList;
    public CharacterUIElement[] characterUIElements;
    // Dictionary to map character names to their UI elements
    private Dictionary<string, CharacterUIElement> characterDictionary;

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

        //Logic for shop system
        itemKeys = new string[] { reviveEffect, prophecyEffect, NoAdsEffect };
        LoadPurchaseState();
        canRevive = PlayerPrefs.GetInt(reviveEffect, 0) == 1;
        seeTheFuture = PlayerPrefs.GetInt(prophecyEffect, 0) == 1;

        //Logic for processing system
        // Initialize the dictionary
        characterDictionary = new Dictionary<string, CharacterUIElement>();
        foreach (var characterElement in characterUIElements)
        {
            characterDictionary[characterElement.name] = characterElement;
            // Load the saved "encountered" status for each character from PlayerPrefs
            characterElement.hasEncountered = PlayerPrefs.GetInt(characterElement.name, 0) == 1;
        }
        LoadEncounteredCharacter();

    }

    #region Shop Logic
    // Function to load purchase state from PlayerPrefs
    void LoadPurchaseState()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            // Check if the item has been purchased
            if (PlayerPrefs.GetInt(itemKeys[i], 0) == 1)
            {
                DisableButton(itemButtons[i]);
            }
            else
            {
                // Add a listener to the button to handle the purchase when clicked
                int index = i;//save the i value from current loop
                itemButtons[i].onClick.AddListener(() => PurchaseItem(index));
                SetButtonName(itemButtons[i], itemKeys[i]);
            }
        }
    }

    //function to set the button name
    void SetButtonName(Button button, string buttonName)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text = buttonName;
        }
    }

    // Function to handle purchasing the item
    void PurchaseItem(int itemIndex)
    {
        // Mark the item as purchased in PlayerPrefs
        PlayerPrefs.SetInt(itemKeys[itemIndex], 1);
        PlayerPrefs.Save();

        DisableButton(itemButtons[itemIndex]);
    }

    // Function to disable the button, and update its text to "Purchased"
    void DisableButton(Button button)
    {
        button.interactable = false;

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = "Purchased";
        }
    }
    #endregion

    // Function to assign name and color to each GameObject in CharacterUIList.
    public void LoadEncounteredCharacter()
    {
        for (int i = 0; i < CharacterUIList.Length; i++)
        {
            // Get the Text and Image components from each GameObject in CharacterUIList.
            TMP_Text nameText = CharacterUIList[i].GetComponentInChildren<TMP_Text>();
            Image characterImage = CharacterUIList[i].GetComponentInChildren<Image>();

            if (characterUIElements[i].hasEncountered)
            {
                nameText.text = characterUIElements[i].name;
                characterImage.color = characterUIElements[i].color;
            }
            else
            {
                // If character is not encountered, set the default values.
                nameText.text = "????";
                characterImage.color = Color.white;
            }
        }
    }

    // Function to mark a character as encountered using string name (with check to prevent redundant calls)
    public void EncounterCharacter(string characterName)
    {
        if (characterDictionary.ContainsKey(characterName))
        {
            CharacterUIElement characterElement = characterDictionary[characterName];
            Debug.Log("Encountering character: " + characterName);

            // Only mark as encountered if not already encountered
            if (!characterElement.hasEncountered)
            {
                // Mark the character as encountered
                characterElement.hasEncountered = true;

                // Save the encounter status in PlayerPrefs
                PlayerPrefs.SetInt(characterName, 1);
                PlayerPrefs.Save();

                // Update the UI with the new encounter status
                LoadEncounteredCharacter();
            }
            else
            {
                Debug.Log("Character already encountered: " + characterName);
            }
        }
        else
        {
            Debug.LogWarning("Character not found: " + characterName);
        }
    }

}
