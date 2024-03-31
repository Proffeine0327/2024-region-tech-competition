using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class How2PlayUI : MonoBehaviour
{
    [System.Serializable]
    public class PageInfo
    {
        public Vector2 time;
        [TextArea] public string explain;
    }

    public List<PageInfo> pageInfos;
    public GameObject bg;
    public Text explain;
    public Button prev;
    public Button next;
    public Button exit;

    private int page;
    private VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.time = 5f;

        prev.onClick.AddListener(() =>
        {
            page--;
            page = Mathf.Clamp(page, 0, pageInfos.Count);
            player.time = pageInfos[page].time.x;
            player.Play();
        });
        next.onClick.AddListener(() =>
        {
            page++;
            page = Mathf.Clamp(page, 0, pageInfos.Count);
            player.time = pageInfos[page].time.x;
            player.Play();
        });
        exit.onClick.AddListener(() => Hide());
    }

    private void Update()
    {
        if (player.time > pageInfos[page].time.y)
        {
            player.time = pageInfos[page].time.x;
            player.Play();
        }
        explain.text = pageInfos[page].explain;
        prev.gameObject.SetActive(page != 0);
        next.gameObject.SetActive(page < pageInfos.Count - 1);
    }

    public void Display()
    {
        bg.gameObject.SetActive(true);
        player.time = pageInfos[page].time.x;
        player.Play();
    }

    public void Hide()
        => bg.gameObject.SetActive(false);
}
