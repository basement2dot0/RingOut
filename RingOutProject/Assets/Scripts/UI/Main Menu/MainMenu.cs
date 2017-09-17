using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button multiplayerButton;
    [SerializeField]
    Button quitButton;
    [SerializeField]
    private GameObject nav;

    private void Awake()
    {
        nav.transform.position = (multiplayerButton.transform.position - new Vector3(150, 0, 0));
    }
    private void Update()
    {
        PauseControls();
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseControls()
    {
        var multiplayer = (multiplayerButton.transform.position - new Vector3(150, 0, 0));
        var quit = (quitButton.transform.position - new Vector3(120, 0, 0));


        if (Navigation() > 0.0f)
            nav.transform.position = multiplayer;
        else if (Navigation() < 0.0f)
            nav.transform.position = quit;
        else if (ConfirmButton())
        {
            if (nav.transform.position == quit)
                QuitGame();
            else if (nav.transform.position == multiplayer)
                LoadLevel("RingMap");
            
        }
    }
    private float Navigation()
    {
        return Input.GetAxis("Nav");
    }
    private bool ConfirmButton()
    {
        bool buttonPressed = new bool();
        if (Input.GetButtonDown("Attack1"))
            buttonPressed = Input.GetButtonDown("Attack1");
        if (Input.GetButtonDown("Attack2"))
            buttonPressed = Input.GetButtonDown("Attack2");
        return buttonPressed;
    }
}
