using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WordBook : MonoBehaviour
{
    public InputField wordInputField;
    public InputField meaningInputField;
    public Text wordListText;
    public Button addWordButton;
    public Button saveToCSVButton;

    private Dictionary<string, string> words = new Dictionary<string, string>();

    void Start()
    {
        addWordButton.onClick.AddListener(AddWord);
        saveToCSVButton.onClick.AddListener(SaveToCSV);
    }

    public void AddWord()
    {
        string word = wordInputField.text;
        string meaning = meaningInputField.text;

        if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(meaning))
        {
            words[word] = meaning;
            UpdateWordListText();
        }
    }

    private void UpdateWordListText()
    {
        wordListText.text = "";
        foreach (var entry in words)
        {
            wordListText.text += $"単語: {entry.Key}, 意味: {entry.Value}\n";
        }
    }

    public void SaveToCSV()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "wordbook1.csv");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in words)
            {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
        Debug.Log($"単語帳が {filePath} に保存されました。");
    }
}