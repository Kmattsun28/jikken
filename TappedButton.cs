using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro;
public class ButtonManager : MonoBehaviour
{
    public TMP_Text textbox;

    public void TappedButton1()
    {
        SceneManager.LoadScene("quiz"); 
    }

    public void TappedButton2()
    {
        textbox.text = "ボタン２が押されました";
    }

    public void TappedButton3()
    {
        textbox.text = "ボタン３が押されました";
    }

}