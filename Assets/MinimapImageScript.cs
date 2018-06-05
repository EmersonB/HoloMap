using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinimapImageScript : MonoBehaviour
{
    public string myURL = "http://ec2-54-197-12-180.compute-1.amazonaws.com:3000/api/alpha/map";


    void Start()
    {
        StartCoroutine(GrabWWWTexture());
    }

    IEnumerator GrabWWWTexture()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(myURL, false);
            www.SetRequestHeader("Accept", "image/*");
            Debug.LogError("Downloading...");
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.LogError(".");
                yield return null;
            }
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                gameObject.GetComponent<Renderer>().materials[0].mainTexture = myTexture;
                Debug.Log("Success");
            }
            yield return new WaitForSeconds(2f);
        }
    }
}