using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class AnimatedGifDrawer : MonoBehaviour
{

    public Vector2 drawPosition;
    public GameObject obj;
    public Texture2D tex;
    public string progress;
    public TextMesh text;
    int numRepeat = 3;
    string data;
    int indexData = 0;
    List<string> urls;
    List<string> paths = new List<string>();
    public string url = "http://192.168.0.14/";
    private List<UniGif.GifTexture> m_gifTexList = new List<UniGif.GifTexture>();
    [SerializeField]
    private bool m_outputDebugLog;
    [SerializeField]
    private TextureWrapMode m_wrapMode = TextureWrapMode.Clamp;
    // Textures filter mode

    [SerializeField]
    private FilterMode m_filterMode = FilterMode.Point;
    void Awake()
    {
        urls = new List<string>();
        StartCoroutine(getIndex());

    }

    IEnumerator getFile()
    {
        indexData = UnityEngine.Random.Range(1, urls.Count);
        m_gifTexList.Clear();
        WWW www = new WWW(url + urls[indexData]);
        while (!www.isDone)
        {
            yield return null;

        }
        text.text = "File:" + urls[indexData] + "(" + indexData + "/" + urls.Count + ")";

        int loop, w, h;
        try
        {
            m_gifTexList = UniGif.GetTextureList(www.bytes, out loop, out w, out h, m_filterMode, m_wrapMode, m_outputDebugLog);

            StartCoroutine(GifLoopCoroutine());
        }
        catch (IndexOutOfRangeException)
        {
            StartCoroutine(getFile());
        }

    }
    private IEnumerator GifLoopCoroutine()
    {


        for (int j = 0; j < numRepeat; j++)
        {



            for (int i = 0; i < m_gifTexList.Count; i++)
            {
                // Change texture

                obj.GetComponent<Renderer>().material.mainTexture = m_gifTexList[i].m_texture2d;
                // Delay
                float delayedTime = Time.time + m_gifTexList[i].m_delaySec;
                while (delayedTime > Time.time)
                {
                    yield return 0;
                }
                // Pause

            }

        }

        obj.GetComponent<Renderer>().material.mainTexture = tex;
        text.text = "Loading...";
        try {
            StartCoroutine(getFile());
        }
        catch (NullReferenceException) {
            StartCoroutine(GifLoopCoroutine());
        }
       

    }

    IEnumerator getIndex()
    {
        WWW www = new WWW(url);
        //yield return www;

        while (!www.isDone)
        {

            yield return null;
        }
        data = www.text;
        int cancler = 0;
        int i;
        for (i = 0; i < data.Length - 9; i++)
        {
            string t = data.Substring(i, 4);
            if (string.Equals(t, "HREF"))
            {

                int st = i + 7;
                int c = 0;
                int j;
                for (j = i + 6; j < data.Length - 20; j++)
                {
                    c++;
                    string t2 = data.Substring(j, 4);
                    if (string.Equals(t2, ".gif"))
                    {
                        i = j;
                        break;

                    }
                }
                if (cancler == 0)
                {
                    cancler = 1;

                }
                else
                {
                    string fi = data.Substring(st, c + 2);
                    urls.Add(fi);

                }

            }

        }
        StartCoroutine(getFile());

    }







}