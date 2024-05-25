using UnityEngine;
public class MainCanvas: MonoBehaviour
{
    public static GameObject MainCanvasInstance;
    public static GameObject UserMenu;
    public static GameObject SelectedObject;
    public static GameObject PossibleActions;
    public static GameObject Warning;

    public static GameObject PauseMenu;
    public static GameObject ButtonMenu;


    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _userMenu;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private GameObject _possibleActions;
    [SerializeField] private GameObject _warning;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _buttonMenu;

    void Awake()
    {
        MainCanvasInstance = _mainCanvas;
        UserMenu = _userMenu;
        SelectedObject = _selectedObject;
        PossibleActions = _possibleActions;
        Warning = _warning;
        PauseMenu = _pauseMenu;
        ButtonMenu = _buttonMenu;
    }
}
