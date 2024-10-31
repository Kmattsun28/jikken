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
    private string currentWord;

    void Start()
    {
        LoadWordsFromCSV("Assets/wordbook.csv");
        submitButton.onClick.AddListener(CheckAnswer);
        DisplayNextWord();
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
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("CSVファイルの読み込み中にエラーが発生しました: " + e.Message);
        }
    }

    private void DisplayNextWord()
    {
        if (wordMeanings.Count > 0)
        {
            int index = Random.Range(0, wordMeanings.Count);
            currentWord = new List<string>(wordMeanings.Keys)[index];
            questionText.text = $"単語: {currentWord}";
            inputField.text = "";
            resultText.text = "";
        }
        else
        {
            questionText.text = "単語がありません。";
        }
    }

    private void CheckAnswer()
    {
        string userAnswer = inputField.text;
        if (wordMeanings.ContainsKey(currentWord) && wordMeanings[currentWord] == userAnswer)
        {
            resultText.text = "正解！";
        }
        else
        {
            resultText.text = $"不正解。正しい答えは: {wordMeanings[currentWord]}";
        }
        DisplayNextWord();
    }
}

