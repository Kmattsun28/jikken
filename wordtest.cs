using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBook : MonoBehaviour
{
    public InputField inputField;
    public Text displayText;
    public Button testButton;
    public Button addButton;
    public Button finishButton;

    private Dictionary<string, string> words = new Dictionary<string, string>();
    private HashSet<string> testedWords = new HashSet<string>();
    private List<string> incorrectWords = new List<string>();

    void Start()
    {
        testButton.onClick.AddListener(TestRandomWord);
        addButton.onClick.AddListener(AddWord);
        finishButton.onClick.AddListener(FinishTest);
    }

    public void AddWord()
    {
        string term = inputField.text;
        if (!string.IsNullOrEmpty(term))
        {
            // ここでAPIを呼び出して単語の意味を取得する
            string meaning = GetWordMeaning(term);
            if (!string.IsNullOrEmpty(meaning))
            {
                words[term] = meaning;
                displayText.text += $"単語: {term}, 意味: {meaning}\n";
            }
            else
            {
                displayText.text += $"単語: {term} の意味を取得できませんでした。\n";
            }
        }
    }

    public void TestRandomWord()
    {
        if (testedWords.Count == words.Count)
        {
            displayText.text += "すべての単語のテストが終了しました。\n";
            return;
        }

        List<string> remainingWords = new List<string>(words.Keys);
        remainingWords.RemoveAll(word => testedWords.Contains(word));

        if (remainingWords.Count == 0)
        {
            displayText.text += "すべての単語のテストが終了しました。\n";
            return;
        }

        string currentWord = remainingWords[Random.Range(0, remainingWords.Count)];
        testedWords.Add(currentWord);

        string userAnswer = inputField.text;
        if (words[currentWord] != userAnswer)
        {
            displayText.text += $"{currentWord}: 不正解\n";
            incorrectWords.Add(currentWord);
        }
        else
        {
            displayText.text += $"{currentWord}: 正解\n";
        }
    }

    public void FinishTest()
    {
        if (incorrectWords.Count > 0)
        {
            displayText.text += "\n不正解だった単語の一覧:\n";
            foreach (string word in incorrectWords)
            {
                displayText.text += $"{word}: {words[word]}\n";
            }
        }
        else
        {
            displayText.text += "\nすべての単語が正解でした。\n";
        }
    }

    private string GetWordMeaning(string word)
    {
        // ここでAPIを呼び出して単語の意味を取得するロジックを実装する
        // 仮の意味を返す
        return "仮の意味";
    }
}