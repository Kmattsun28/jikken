using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class WordQuiz : MonoBehaviour
{
    // UI Elements
    public TMP_Text questionText;         // 問題文を表示するテキスト
    public TMP_InputField inputField;     // ユーザーが入力するためのフィールド
    public Button submitButton;           // 回答を送信するボタン
    public TMP_Text resultText;           // 結果を表示するテキスト

    // 単語とその日本語の意味を格納する辞書
    private Dictionary<string, string> wordMeanings = new Dictionary<string, string>();

    private List<string> wordList;          // 出題する単語リスト
    private int currentQuestionIndex = 0;   // 現在の問題のインデックス
    private int correctCount = 0;            // 正解数をカウント

    void Start()
    {
        LoadWordsFromCSV("Assets/wordbook1.csv");

        // 単語のリストを取得し、シャッフルする
        wordList = new List<string>(wordMeanings.Keys);
        ShuffleList(wordList); // リストをシャッフル

        // 最初の問題を表示
        ShowNextQuestion();

        // ボタンにクリックイベントを追加
        submitButton.onClick.AddListener(CheckAnswer);
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

    // リストをシャッフルするメソッド
    void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // 次の問題を表示
    void ShowNextQuestion()
    {
        if (currentQuestionIndex < wordList.Count)
        {
            string currentWord = wordList[currentQuestionIndex];
            questionText.text = $"What is the meaning of '{currentWord}' in Japanese?";
            inputField.text = "";  // ユーザー入力をクリア
            resultText.text = "";  // 結果表示をクリア
        }
        else
        {
            // クイズ終了時の結果表示
            questionText.text = "Quiz finished!";
            resultText.text = $"You got {correctCount} out of {wordList.Count} correct.";
            submitButton.interactable = false;  // ボタンを無効にする
        }
    }

    // 回答をチェック
    void CheckAnswer()
    {
        string userAnswer = inputField.text.Trim();  // ユーザーが入力した答えを取得
        string correctAnswer = wordMeanings[wordList[currentQuestionIndex]];

        if (userAnswer == correctAnswer)
        {
            resultText.text = "Correct!";
            correctCount++;
        }
        else
        {
            resultText.text = $"Incorrect! The correct meaning is: {correctAnswer}";
        }

        // 次の問題に進む
        currentQuestionIndex++;
        ShowNextQuestion();
    }
}

