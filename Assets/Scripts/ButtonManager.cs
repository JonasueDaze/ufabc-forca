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
     * Carrega a cena do jogo.
     */
    public void StartGame()
    {
        SceneManager.LoadScene("Lab1");
    }
}
