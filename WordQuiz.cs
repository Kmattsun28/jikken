using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class WordQuiz : MonoBehaviour
{
    public TMP_Text questionText;         // 問題文を表示するテキスト
    public TMP_InputField inputField;     // ユーザーが入力するためのフィールド
    public Button submitButton;           // 回答を送信するボタン
    public TMP_Text resultText;           // 結果を表示するテキスト

    private Dictionary<string, string> wordMeanings = new Dictionary<string, string>();
    private List<string> wordList = new List<string>();
    private List<string> incorrectWords = new List<string>(); // 間違えた単語を格納するリスト
    private int currentIndex = 0;
    private string currentWord; // currentWord変数を宣言

    void Start()
    {
        LoadWordsFromCSV("Assets/wordbook.csv");
        ShuffleWordList(); // 単語リストをシャッフル
        submitButton.onClick.AddListener(CheckAnswer);
        DisplayNextWord();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    private void LoadWordsFromCSV(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values.Length == 2)
                    {
                        wordMeanings[values[0]] = values[1];
                        wordList.Add(values[0]);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("CSVファイルの読み込み中にエラーが発生しました: " + e.Message);
        }
    }

    private void ShuffleWordList()
    {
        for (int i = 0; i < wordList.Count; i++)
        {
            int randomIndex = Random.Range(0, wordList.Count);
            string temp = wordList[i];
            wordList[i] = wordList[randomIndex];
            wordList[randomIndex] = temp;
        }
    }

    private void DisplayNextWord()
    {
        if (currentIndex < wordList.Count)
        {
            currentWord = wordList[currentIndex];
            questionText.text = $"単語: {currentWord}";
            inputField.text = "";
            resultText.text = "";
        }
        else
        {
            DisplayIncorrectWords();
            questionText.text = "すべての単語を終了しました。";
            inputField.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
        }
    }

    private void CheckAnswer()
    {
        string userAnswer = inputField.text.Trim(); // ユーザーの入力をトリム
        if (wordMeanings.ContainsKey(currentWord) && string.Equals(wordMeanings[currentWord], userAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            resultText.text = "正解！";
        }
        else
        {
            resultText.text = $"不正解。正しい答えは: {wordMeanings[currentWord]}";
            incorrectWords.Add(currentWord); // 間違えた単語をリストに追加
        }
        currentIndex++;
        DisplayNextWord();
    }

    private void DisplayIncorrectWords()
    {
        if (incorrectWords.Count > 0)
        {
            resultText.text = "間違えた単語:\n";
            foreach (var word in incorrectWords)
            {
                resultText.text += $"{word}: {wordMeanings[word]}\n";
            }
        }
        else
        {
            resultText.text = "すべての単語に正解しました！";
        }
    }
}