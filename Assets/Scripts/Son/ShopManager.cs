using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    // Array of buttons for each item in the shop
    public Button[] itemButtons;

    // Unique keys for each item in PlayerPrefs
    public string[] itemKeys;

    void Start()
    {
        LoadPurchaseState();
    }

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
                int index = i;
                itemButtons[i].onClick.AddListener(() => PurchaseItem(index));
                SetButtonName(itemButtons[i], itemKeys[i]);
            }
        }
    }

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
        Debug.Log("Item purchased: " + itemKeys[itemIndex]);

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
}
