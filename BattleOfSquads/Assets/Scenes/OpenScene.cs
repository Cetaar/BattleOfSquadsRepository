using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenScene : MonoBehaviour
{
    [SerializeField] private string _nameScene;
    public void OpenSelectedScene()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
