using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

     public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
