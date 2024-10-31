using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WordTest : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInputField;
    public Button submitButton;
    public TMP_Text resultText;
    public Button nextButton;
    public TMP_Text incorrectWordsText;

    private Dictionary<string, string> words = new Dictionary<string, string>();
    private List<string> wordList = new List<string>();
    private List<string> incorrectWords = new List<string>();
    private string currentWord;
    private string currentMeaning;

    void Start()
    {
        LoadFromCSV();
        nextButton.onClick.AddListener(DisplayNextWord);
        submitButton.onClick.AddListener(CheckAnswer);
        DisplayNextWord();
    }

    private void LoadFromCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "wordbook1.csv");
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] entries = line.Split(',');
                    if (entries.Length == 2)
                    {
                        words[entries[0]] = entries[1];
                        wordList.Add(entries[0]);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりません。");
        }
    }

    private void DisplayNextWord()
    {
        if (wordList.Count == 0)
        {
            questionText.text = "単語帳が空です。";
            return;
        }

        int randomIndex = Random.Range(0, wordList.Count);
        currentWord = wordList[randomIndex];
        currentMeaning = words[currentWord];

        questionText.text = $"単語: {currentWord}";
        answerInputField.text = "";
        resultText.text = "";
    }

    private void CheckAnswer()
    {
        string userAnswer = answerInputField.text;
        if (userAnswer == currentMeaning)
        {
            resultText.text = "正解！";
        }
        else
        {
            resultText.text = $"不正解。正しい意味は: {currentMeaning}";
            incorrectWords.Add($"{currentWord}: {currentMeaning}");
        }

        // 次の単語を表示
        DisplayNextWord();
    }

    public void DisplayIncorrectWords()
    {
        if (incorrectWords.Count > 0)
        {
            incorrectWordsText.text = "不正解だった単語の一覧:\n";
            foreach (string word in incorrectWords)
            {
                incorrectWordsText.text += $"{word}\n";
            }
        }
        else
        {
            incorrectWordsText.text = "すべての単語が正解でした。";
        }
    }
}