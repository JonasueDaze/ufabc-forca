using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Exibe a última palavra oculta da forca.
/// </summary>
public class ShowLastWord : MonoBehaviour
{
    /** 
     * Chamado antes do primeiro frame.
     */
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("word");
    }
}
