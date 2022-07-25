using UnityEngine;
using UnityEngine.UIElements;
using FusedVR.Web3;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class FusedLogin : MonoBehaviour
{
    public UIDocument uIDocument;

    // Start is called before the first frame update
    void Start() {
        Button ui = uIDocument.rootVisualElement.Q<Button>("Submit");
        ui.clicked += Ui_onClick;
    }

    private async void Ui_onClick() {
        TextField ui = uIDocument.rootVisualElement.Q<TextField>("Email");
        string email = ui.text;
        if (IsEmail(email)) {
            bool loggedIn = await Web3Manager.Login(email, "test");
            if (loggedIn) {
                SceneManager.LoadScene(1);
            }
        } else {
            Debug.LogError("Invalid Email Address");
        }
    }

    public const string MatchEmailPattern =
    @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
      + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    /// <summary>
    /// Checks whether the given Email-Parameter is a valid E-Mail address.
    /// </summary>
    /// <param name="email">Parameter-string that contains an E-Mail address.</param>
    /// <returns>True, wenn Parameter-string is not null and contains a valid E-Mail address;
    /// otherwise false.</returns>
    public static bool IsEmail(string email) {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
}
