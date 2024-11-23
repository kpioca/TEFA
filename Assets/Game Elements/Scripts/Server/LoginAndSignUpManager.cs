/*
   SDK - LootLockerSDK
   Link - https://github.com/LootLocker/unity-sdk
*/


using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LootLocker.Requests;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEditor;

public class LoginAndSignUpManager : MonoBehaviour
{
    #region Pages
    public MainPageStuffs mainPage;
    public LoginPageStuffs loginPage;
    public SignUpStuffs signUpPage;
    public GamePageStuffs gamePage;
    public ResetPassPageStuffs resetPassPage;
    public SaveLoadPageStuffs saveLoadPage;
    public OfflinePageStuffs offlinePage;
    #endregion

    private DataServerProvider _dataProvider;
    private IDataProvider _dataLocalProvider;
    private IPersistentData _persistentData;
    MainMenuBootstrap _mainMenuBootstrap;
    [SerializeField] LeaderboardPanel _leaderboardPanel;
    [SerializeField] GameObject _loadingObj;
    public bool IsInitializedGameSession = false;

    // Start is called before the first frame update

    public void ChangeStateLoadingObj(bool state)
    {
        _loadingObj.SetActive(state);
    }
    public void InitializeLeaderboard()
    {
        Debug.Log("Leaderboard Initialized");
        //_leaderboardPanel.Initialize(_dataLocalProvider, _persistentData);
    }

    public void Initialize(DataServerProvider dataProvider, IDataProvider dataLocalProvider, IPersistentData persistentData, MainMenuBootstrap mainMenuBootstrap)
    {
        _dataProvider = dataProvider;
        _dataLocalProvider = dataLocalProvider;
        _persistentData = persistentData;
        _mainMenuBootstrap = mainMenuBootstrap;
    }
    public void InitializeGameSession()
    {
        #region CheckingGameSession
        // To prevent showing login page everytime when player start the game
        IsInitializedGameSession = true;
        ChangeStateLoadingObj(true);
        _mainMenuBootstrap.StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                Debug.Log("Internet Available!");
                LootLockerSDKManager.CheckWhiteLabelSession(response =>
                {
                    if (response)
                    {
                        Debug.Log("session is valid, you can start a game session");
                        StartSessionForExistingAccount((bool callback) => { if(callback) InitializeLeaderboard(); ChangeStateLoadingObj(false); });
                    }
                    else
                    {
                        if(_persistentData.saveData.Login != "")
                        UserLogin(_persistentData.saveData.Login, _persistentData.saveData.Password, true, (bool callback) =>
                        {
                            if(!callback)
                            {
                                mainPage.MainPage.SetActive(true);
                                Debug.Log("session is NOT valid, we should show the login form");
                                return;
                            }
                            InitializeLeaderboard();
                        });
                        else
                        {
                            mainPage.MainPage.SetActive(true);
                            Debug.Log("session is NOT valid, we should show the login form");
                            ChangeStateLoadingObj(false);
                        }
                    }
                });
            }
            else
            {
                offlinePage.OfflinePage.SetActive(true);
                offlinePage.Info.text = "You are not online!";
                ChangeStateLoadingObj(false);
            }
        }));
        #endregion
    }

    private void SaveAccountInfo(string login, string password)
    {
        _persistentData.saveData.Login = login;
        _persistentData.saveData.Password = password;
        _dataLocalProvider.Save();
    }


    private void OnEnable()
    {
        mainPage.MainPageLoginBtn.onClick.AddListener(OpenLoginPage);
        mainPage.MainPageSignUpBtn.onClick.AddListener(OpenSignupPage);
        mainPage.ResetPassBtn.onClick.AddListener(OpenResetPage);

        signUpPage.SignUpBtn.onClick.AddListener(UserSignUp);
        signUpPage.ExitNameBtn.onClick.AddListener(OpenMainPage);
        loginPage.LoginBtn.onClick.AddListener(LoginThroughBtn);
        loginPage.ExitNameBtn.onClick.AddListener(OpenMainPage);


        gamePage.SetNameBtn.onClick.AddListener(SetPlayerNickName);
        gamePage.ExitNameBtn.onClick.AddListener(EndSession);
        resetPassPage.ResetBtn.onClick.AddListener(ResetThroughBtn);
        resetPassPage.ExitNameBtn.onClick.AddListener(OpenMainPage);

        saveLoadPage.ExitBtn.onClick.AddListener(EndSession);
        saveLoadPage.SaveBtn.onClick.AddListener(Save);
        saveLoadPage.LoadBtn.onClick.AddListener(Load);

        offlinePage.CheckConnection.onClick.AddListener(checkConnectionThroughBtn);
    }

    private void OnDisable()
    {
        mainPage.MainPageLoginBtn.onClick.RemoveAllListeners();
        mainPage.MainPageSignUpBtn.onClick.RemoveAllListeners();
        mainPage.ResetPassBtn.onClick.RemoveAllListeners();

        signUpPage.SignUpBtn.onClick.RemoveAllListeners();
        signUpPage.ExitNameBtn.onClick.RemoveAllListeners();
        loginPage.LoginBtn.onClick.RemoveAllListeners();
        loginPage.ExitNameBtn.onClick.RemoveAllListeners();


        gamePage.SetNameBtn.onClick.RemoveAllListeners();
        gamePage.ExitNameBtn.onClick.RemoveAllListeners();
        resetPassPage.ResetBtn.onClick.RemoveAllListeners();
        resetPassPage.ExitNameBtn.onClick.RemoveAllListeners();

        saveLoadPage.ExitBtn.onClick.RemoveAllListeners();
        saveLoadPage.SaveBtn.onClick.RemoveAllListeners();
        saveLoadPage.LoadBtn.onClick.RemoveAllListeners();

        offlinePage.CheckConnection.onClick.RemoveAllListeners();

        ClearAllInfo();
    }
    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            action(false);
        }
        else
        {
            Debug.Log("Success");
            action(true);
        }
    }

    void StartSessionForExistingAccount(Action<bool> callback)
    {
        LootLockerSDKManager.StartWhiteLabelSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                callback(false);
                return;
            }

            Debug.Log("session started successfully");
            OpenSaveLoadPage();
            GetPlayerName((string callback) => {});
            callback(true);
        });
    }

    void StartGuestSession(Action<bool> callback)
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                callback(false);
                return;
            }
            callback(true);
            Debug.Log("successfully started LootLocker session");
        });
    }
    void EndSession()
    {
        LootLockerSDKManager.EndSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error ending LootLocker session");

                return;
            }

            OpenMainPage();
            Debug.Log("session ended successfully");
        });
    }

    #region Signup and Login
    void UserSignUp()
    {
        string email = signUpPage.SignUpEmailInput.text;
        string password = signUpPage.SignUpPasswordInput.text;

        ChangeStateLoadingObj(true);
        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            ChangeStateLoadingObj(false);
            if (!response.success)
            {
                Debug.Log("error while creating user");
                signUpPage.Info.text = response.errorData.message.ToUpper();
                return;
            }

            Debug.Log("user created successfully");
            UserLogin(email, password, true, (bool callback) => { });

        });
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    void UserLogin(string email,string password , bool rememberMe, Action<bool> callback)
    {
        ChangeStateLoadingObj(true);
        LootLockerSDKManager.WhiteLabelLogin(email, password, rememberMe, (response) =>
        {
            if (!response.success)
            {
                ChangeStateLoadingObj(false);
                Debug.Log("error while logging in");
                loginPage.Info.text = response.errorData.message.ToUpper();
                callback(false);
                return;
            }

            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                ChangeStateLoadingObj(false);
                if (!response.success)
                {
                    Debug.Log("error starting LootLocker session");
                    loginPage.Info.text = response.errorData.message.ToUpper();
                    callback(false);
                    return;
                }

                Debug.Log("session started successfully");
                callback(true);
                SaveAccountInfo(email, password);
                ClearAllInfo();
                GetPlayerName((string callback) => {
                    if (callback == "")
                        OpenGamePage();
                    else OpenSaveLoadPage();
                });
            });
        });
    }

    void LoginThroughBtn()
    {
        string email = loginPage.LoginEmailInput.text;
        string password = loginPage.LoginPasswordInput.text;
        bool remember = true;
        UserLogin(email, password, remember, (bool callback) => { });
    }

    void checkConnectionThroughBtn()
    {
        ChangeStateLoadingObj(true);
        _mainMenuBootstrap.StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                Debug.Log("Internet Available!");
                LootLockerSDKManager.CheckWhiteLabelSession(response =>
                {
                    if (response)
                    {
                        Debug.Log("session is valid, you can start a game session");
                        StartSessionForExistingAccount((bool action) => { if (action) InitializeLeaderboard(); ChangeStateLoadingObj(false); });

                    }
                    else
                    {
                        Debug.Log("session is NOT valid, we should show the login form");

                        if (_persistentData.saveData.Login != "")
                            UserLogin(_persistentData.saveData.Login, _persistentData.saveData.Password, true, (bool callback) =>
                            {
                                if (!callback)
                                {
                                    OpenMainPage();
                                    Debug.Log("session is NOT valid, we should show the login form");
                                    return;
                                }
                                InitializeLeaderboard();
                            });
                        else
                        {
                            OpenMainPage();
                            Debug.Log("session is NOT valid, we should show the login form");
                            ChangeStateLoadingObj(false);
                        }
                    }
                });
            }
            else
            {
                offlinePage.Info.text = "You are not online!";
                ChangeStateLoadingObj(false);
            }
        }));
    }
    #endregion

    #region Save and Load
    void Save()
    {
        _dataProvider.Save((string callback) => {
            saveLoadPage.Info.text = callback;
        });
    }

    public void ClearAllInfo()
    {
        loginPage.Info.text = "";
        signUpPage.Info.text = "";
        gamePage.Info.text = "";
        resetPassPage.Info.text = "";
        saveLoadPage.Info.text = "";
    }

    void Load()
    {
        _dataProvider.TryLoad((string callback) => {
            saveLoadPage.Info.text = callback;
            _mainMenuBootstrap.InitializeInventory();
            _mainMenuBootstrap.InitializeMenuManager();
            _mainMenuBootstrap.InitializeShop();
        });
    }
    #endregion
    #region PlayerName
    void SetPlayerName(string name)
    {
        LootLockerSDKManager.SetPlayerName(name, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
                OpenSaveLoadPage();
                GetPlayerName((string callback) => { });
            }
            else
            {
                Debug.Log("Error setting player name");
            }
        });
    }

    void ResetThroughBtn()
    {
        string email = resetPassPage.EmailInput.text;
        if(IsValidEmail(email))
            ResetPassword(email);
        else
        {
            resetPassPage.Info.text = "Incorrect format of email";
        }
    }

    void ResetPassword(string email)
    {
        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error requesting password reset");
                resetPassPage.Info.text = response.errorData.message.ToUpper();
                return;
            }

            Debug.Log("requested password reset successfully");
            resetPassPage.Info.text = "Succesfull!\nCheck your email";
        });
    }
    void SetPlayerNickName()
    {
        SetPlayerName(gamePage.UsernameInput.text);
    }

    void GetPlayerName(Action<string> callback)
    {
        string username = "";
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player name: " + response.name);
                username = response.name;
                gamePage.Username.text = "Player Name: " + username; 
                saveLoadPage.Username.text = "Player Name: " + username;
                callback(username);

            }
            else
            {
                Debug.Log(response.errorData.message);
                Debug.Log("Error getting player name");
                callback("");
            }
        });
    }

    #endregion

    #region PageOpenerFuntions

    void OpenSaveLoadPage()
    {
        ClearAllInfo();
        gamePage.GamePage.SetActive(false);
        mainPage.MainPage.SetActive(false);
        saveLoadPage.SaveLoadPage.SetActive(true);
        loginPage.LoginPage.SetActive(false);
        offlinePage.OfflinePage.SetActive(false);
    }
    #region OpenLoginPage
    void OpenLoginPage()
    {
        ClearAllInfo();
        _mainMenuBootstrap.StartCoroutine(OnOpenLoginPage());
    }
    IEnumerator OnOpenLoginPage()
    {
        yield return null;
        mainPage.MainPage.SetActive(false);
        signUpPage.SignUpPage.SetActive(false);
        loginPage.LoginPage.SetActive(true);
    }
    #endregion

    #region OpenGamePage
    void OpenGamePage()
    {
        _mainMenuBootstrap.StartCoroutine(OnOpenGamePage());
    }

    void OpenMainPage()
    {
        ClearAllInfo();
        mainPage.MainPage.SetActive(true);
        loginPage.LoginPage.SetActive(false);
        signUpPage.SignUpPage.SetActive(false);
        gamePage.GamePage.SetActive(false);
        resetPassPage.ResetPage.SetActive(false);
        offlinePage.OfflinePage.SetActive(false);
        saveLoadPage.SaveLoadPage.SetActive(false);
    }
    IEnumerator OnOpenGamePage()
    {
        yield return null;
        mainPage.MainPage.SetActive(false);
        loginPage.LoginPage.SetActive(false);
        signUpPage.SignUpPage.SetActive(false);
        gamePage.GamePage.SetActive(true);
    }
    #endregion

    #region OpenSignUpPage
    void OpenSignupPage()
    {
        ClearAllInfo();
        _mainMenuBootstrap.StartCoroutine(OpOpenSignupPage());
    }
    IEnumerator OpOpenSignupPage()
    {
        yield return null;
        mainPage.MainPage.SetActive(false);
        signUpPage.SignUpPage.SetActive(true);
    }
    #endregion

    void OpenResetPage()
    {
        ClearAllInfo();
        resetPassPage.ResetPage.SetActive(true);
        mainPage.MainPage.SetActive(false);
        signUpPage.SignUpPage.SetActive(false);
        loginPage.LoginPage.SetActive(false);

    }
    #endregion
}

#region Pages Class
[Serializable]
public class MainPageStuffs
{
    public GameObject MainPage;
    public Button MainPageLoginBtn;
    public Button MainPageSignUpBtn;
    public Button ResetPassBtn;
}
[Serializable]
public class LoginPageStuffs
{
    public GameObject LoginPage;
    public TMP_Text Info;
    public TMP_InputField LoginEmailInput;
    public TMP_InputField LoginPasswordInput;
    public Button LoginBtn;
    public Button ExitNameBtn;
}
[Serializable]
public class SignUpStuffs
{
    public GameObject SignUpPage;
    public TMP_Text Info;
    public TMP_InputField SignUpEmailInput;
    public TMP_InputField SignUpPasswordInput;
    public Button SignUpBtn;
    public Button ExitNameBtn;
}

[Serializable]
public class GamePageStuffs
{
    public GameObject GamePage;
    public TMP_Text Info;
    public TMP_Text Username;
    public Button SetNameBtn;
    public Button ExitNameBtn;
    public TMP_InputField UsernameInput;
}

[Serializable]
public class SaveLoadPageStuffs
{
    public GameObject SaveLoadPage;
    public TMP_Text Info;
    public TMP_Text Username;
    public Button SaveBtn;
    public Button LoadBtn;
    public Button ExitBtn;
}

[Serializable]
public class OfflinePageStuffs
{
    public GameObject OfflinePage;
    public TMP_Text Info;
    public Button CheckConnection;
}

[Serializable]
public class ResetPassPageStuffs
{
    public GameObject ResetPage;
    public TMP_Text Info;
    public TMP_InputField EmailInput;
    public Button ResetBtn;
    public Button ExitNameBtn;
}
#endregion