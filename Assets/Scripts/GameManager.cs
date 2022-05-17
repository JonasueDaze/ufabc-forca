using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Gerencia o jogo da forca.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Prefab da letra no Game.
    public GameObject letter;

    // Objeto que indica o centro da tela.
    public GameObject center;

    // Número máximo de tentativas para forca ou salvação.
    int maxNumTries;

    // Número de tentativas atuais da rodada.
    int numTries;

    // Score atual do jogador.
    int score = 0;

    // Possíveis palavras a serem descobertas.
    string[] words = new string[] { "carro", "elefante", "futebol" };

    // Palavra a ser descoberta.
    string word = "";

    // Indicador de quais letras foram descobertas.
    bool[] foundLetters;

    /**
     * Chamado antes do primeiro frame.
     */
    void Start()
    {
        center = GameObject.Find("screenCenter");
        InitGame();
        InitLetters();

        numTries = 0;
        maxNumTries = 10;
        UpdateNumTries();
        UpdateScore();
    }

    /**
     * Atualização a cada frame.
     */
    void Update()
    {
        CheckKeyboard();
    }

    /**
     * Inicializa as letras do jogo.
     */
    void InitLetters()
    {
        var numLetters = word.Length;
        for (var i = 0; i < numLetters; i++)
        {
            var newPosition = new Vector3(center.transform.position.x + ((i - numLetters / 2f) * 80), center.transform.position.y, center.transform.position.z);
            var l = (GameObject)Instantiate(letter, newPosition, Quaternion.identity);

            // Nomeia na hierarquia a GameObject com letra-(iésima+1), i = 1..numLetters
            l.name = $"letra {i + 1}";

            // Posiciona-se como filho do GameObject Canvas.
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    /**
     * Inicializa o jogo da forca.
     */
    void InitGame()
    {
        // Obtém-se uma palavra do arquivo e transforma-o em maiúscula.
        word = GetWordFromFile().ToUpper();

        // Instancia-se o array bool do indicador de acertos.
        foundLetters = new bool[word.Length];
    }

    /**
     * Verifica as entradas do teclado.
     */
    void CheckKeyboard()
    {
        if (Input.anyKeyDown)
        {
            var inputLetter = Input.inputString.ToCharArray()[0];

            var asciiLetter = System.Convert.ToInt32(inputLetter);
            if (asciiLetter < 97 || asciiLetter > 122) return;

            inputLetter = System.Char.ToUpper(inputLetter);

            numTries++;
            UpdateNumTries();
            if (numTries > maxNumTries)
            {
                SceneManager.LoadScene("Lab1_forca");
            }

            for (var i = 0; i < word.Length; i++)
            {
                if (foundLetters[i]) continue;

                if (word[i] == inputLetter)
                {
                    print("letter found");
                    foundLetters[i] = true;
                    GameObject.Find($"letra {i + 1}").GetComponent<Text>().text = inputLetter.ToString();

                    score = PlayerPrefs.GetInt("score");
                    score++;
                    PlayerPrefs.SetInt("score", score);
                    UpdateScore();
                    CheckWinCondition();
                }
            }
        }
    }

    /**
     * Atualiza o número de tentativas exibida na tela.
     */
    void UpdateNumTries()
    {
        GameObject.Find("numTries").GetComponent<Text>().text = $"{numTries} | {maxNumTries}";
    }

    /**
     * Atualiza o score exibido na tela.
     */
    void UpdateScore()
    {
        GameObject.Find("score").GetComponent<Text>().text = $"Score: {score}";
    }

    /**
     * Verifica a condição de vitória do jogo, i.e. se a palavra foi descoberta antes das tentativas máximas.
     */
    void CheckWinCondition()
    {
        var win = true;
        for (var i = 0; i < word.Length; i++)
        {
            win &= foundLetters[i];
        }

        if (win)
        {
            PlayerPrefs.SetString("word", word);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    /**
     * Obtém uma palavra sorteada a partir de um arquivo de palavras.
     */
    string GetWordFromFile()
    {
        var asset = (TextAsset)Resources.Load("words", typeof(TextAsset));
        var s = asset.text;
        var words = s.Split(' ');

        // Sorteia-se um número dentro do número de palavras do array.
        var randomNumber = Random.Range(0, words.Length);

        // Seleciona-se uma palavra sorteada.
        return words[randomNumber];
    }
}
