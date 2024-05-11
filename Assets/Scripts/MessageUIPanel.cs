using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageUIPanel : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    

    private void Start()
    {
        ToggleOpen();
        StartCoroutine(ShowMessage("Find the key"));
    }

    public IEnumerator ShowMessage(string text)
    {
        yield return new WaitForSeconds(1);
        SetText(text);
        ToggleOpen();
        yield return new WaitForSeconds(2);
        ToggleOpen();
    }

    public void ToggleOpen()
    {
        gameObject.SetActive(!gameObject.activeSelf);        
    }

    private void SetText(string text)
    {
        messageText.text = text;
    }
}
