using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia as ações para os botões.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /**
     * Chamada antes do primeiro frame.
     */
    private void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }

    /**
     * Carrega a cena passada como parâmetro.
     */
    public void StartScene(string scene)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scene);
    }

    /**
     * Volta para a cena anterior.
     */
    public void BackScene()
    {
        var previousScene = PlayerPrefs.GetString("previousScene");
        SceneManager.LoadScene(previousScene);
    }
}
