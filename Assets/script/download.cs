using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class download : MonoBehaviour
{
    public TMP_InputField IpInput;
    public TMP_Text StatusText;


    private void Start() {
        IpInput.text = PlayerPrefs.GetString("serverIp");
    }
    public void SetNewServerIp()
    {
        PlayerPrefs.SetString("serverIp", IpInput.text);
        SceneManager.LoadScene("main");
    }
}

