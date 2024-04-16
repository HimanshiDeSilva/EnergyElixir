using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.SceneManagement; // Required when Using SceneManagement

public class StartGame_Manager : MonoBehaviour
{
    public string sceneName = "Scenes/MainMenu";
    public TMP_InputField inputField;
    public Button verifyButton;

    void Start() {
        // Adds a listener to the button and invokes a method when the button is clicked.
        //verifyButton.onClick.AddListener(OnVerifyButtonClick); //Other way to execute the method
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnBecameVisible() {
        // 
    }

    // Method to be called when the verify button is clicked
    public void OnVerifyButtonClick() {
        // Takes the text from the input field and prints it to the debug log
        /*Debug.Log(inputField.text);*/
        // Calls the Authenticate method from the APIHandler instance
        if(APIHandler.Instance != null) {
            if(inputField.text != "") {
                StartCoroutine(APIHandler.Instance.Authenticate(inputField.text));
            } else {
                Debug.LogError("API Key is empty.");
            }   
        } else {
            Debug.LogError("APIHandler instance is null.");
        }



/*        LoadScene(sceneName);*/
    }
}
