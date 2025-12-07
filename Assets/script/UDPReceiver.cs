using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using TMPro;



public class UDPReceiver : MonoBehaviour
{
    UdpClient udpClient;
    IPEndPoint remoteEndPoint;

    public int maxobj = 0;
    public TMP_Text warnText;


    //Json
    [System.Serializable]
    public class Data
    {
        public string Class;
        public int x;
        public int y;
        public int w;
        public int h;

    }
    [System.Serializable]
    public class DataList
    {
        public Data[] data;
    }

    [System.Serializable]
    public class ListOfWord
    {
        public string key;
        public List<string> words;

    }

    [System.Serializable]

    public class Word
    {
        public List<ListOfWord> AllWord;
    }

    public DataList dataList = new DataList();
    public Word ListOfAllWords = new Word();

    //Find Words

    void Start()
    {
        udpClient = new UdpClient(5011);  // Listen on port 5010
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 5011);

        // Start listening on a separate thread
        udpClient.BeginReceive(ReceiveData, null);



    }
    public List<string> addobj = new List<string>();




    bool find;

    void ReceiveData(IAsyncResult result)
    {
        try
        {
            byte[] receivedData = udpClient.EndReceive(result, ref remoteEndPoint);
            string jsonString = Encoding.UTF8.GetString(receivedData);

            if (string.IsNullOrWhiteSpace(jsonString))  // Check for empty data
            {
                Debug.LogError("Received empty UDP message.");
                return;
            }
            // Debug.Log($"Received UDP Data: {jsonString}");  // Debug output
            try
            {
                //กำหนดข้อมูล
                dataList = JsonUtility.FromJson<DataList>(jsonString);
                if (dataList.data.Length > 0)
                {
                     



                    foreach (var values in dataList.data)
                    {

                        // 1) นับจำนวน object (ไม่ใช่ person)
                        if (values.Class != "person")
                        {

                            int count = addobj.Count(x => x == values.Class);

                        if (count < 5)
                        {
                            addobj.Add(values.Class);
                        }



                        }
                            




                        for (int i = 0; i < ListOfAllWords.AllWord.Count; i++)
                        {
                            if (ListOfAllWords.AllWord[i].key == values.Class)
                            {
                                if (ListOfAllWords.AllWord[i].words.Count < 5)
                                {

                                    ListOfAllWords.AllWord[i].words.Add(values.Class);

                                }

                                find = true;
                            }
                        }
                        if (!find)
                        {
                            ListOfAllWords.AllWord.Add(new ListOfWord { key = values.Class, words = new List<string> { values.Class } });
                        }

                        find = false;

                    }
                    
                    
                

                }


            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Parsing Error: {ex.Message}\nReceived Data: {jsonString}");
            }

            // Continue listening
            udpClient.BeginReceive(ReceiveData, null);
        }
        catch (Exception ex)
        {
            warnText.text = $"UDP Error: {ex.Message}";
            Debug.LogError($"UDP Error: {ex.Message}");
        }
    }




    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}




