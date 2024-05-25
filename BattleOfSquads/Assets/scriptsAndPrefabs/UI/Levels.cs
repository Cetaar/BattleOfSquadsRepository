using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Levels : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string namelevel;
    private void Start()
    {
        _text.text = namelevel;
    }
    public void OpenLevel()
    {
        SceneManager.LoadScene(namelevel);
    }
}
