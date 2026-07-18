using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsLoader : MonoBehaviour
{
    [Serializable]
    public class Announcement
    {
        public string Title;
        public string Content;
        public string Date;
        public NewsElement Thumbnail;

        public Announcement(string title, string content, string date, NewsElement thumbnail)
        {
            Title = title;
            Content = content;
            Date = date;
            Thumbnail = thumbnail;
        }
    }

    [Header("=== НАСТРОЙКИ ===")]
    [SerializeField] private TextMeshProUGUI ArticleText;
    [SerializeField] private RectTransform ContentParent;
    [SerializeField] private RectTransform Element;
    [SerializeField] private Button OpenNewsUrlButton;

    private List<Announcement> _announcements;

    private void Start()
    {
        _announcements = new List<Announcement>();

        string title = "DEVELOPMENT 0.1.0";
        string date = "2026-07-18";

        string content =
            "<size=30><b>DEVELOPMENT 0.1.0</b></size>\n" +
            "<size=23><i>July 18, 2026</i></size>\n\n" +
            "<size=17><color=#F0F8FF><size=19><color=#ffe2bd>Hello everyone!</color></size>\n\n" +
            "First ever version of the mod!\n\n" +
            "<size=17><color=#ffe2bd>Changelog</color></size>\n\n" +
            "• Added new props into some Entrance \n\n\n" +
            "Expect more updates soon!\n" +
            "~ Catwithchezzburger, and his team.\n" +
            "</color></size>";

        RectTransform instance = Instantiate(Element, ContentParent);
        NewsElement newsElement = instance.GetComponent<NewsElement>();

        if (newsElement != null)
        {
            if (newsElement.Title != null) newsElement.Title.text = title;
            if (newsElement.Date != null) newsElement.Date.text = date;
            if (newsElement.Content != null) newsElement.Content.text = "First version!";

            newsElement.Id = 0;
            newsElement.transform.localScale = Vector3.one;
        }

        _announcements.Add(new Announcement(title, content, date, newsElement));

        // Показываем самую первую новость
        ShowAnnouncement(0);
    }

    // ДОБАВЛЕННЫЙ МЕТОД: Исправляет ошибку компиляции
    public void ShowAnnouncement(int id)
    {
        if (_announcements == null || id < 0 || id >= _announcements.Count) return;

        Announcement ann = _announcements[id];

        if (ArticleText != null)
        {
            ArticleText.text = ann.Content;
        }

        if (OpenNewsUrlButton != null)
            OpenNewsUrlButton.interactable = false;

        for (int i = 0; i < _announcements.Count; i++)
        {
            NewsElement el = _announcements[i].Thumbnail;
            if (el == null) continue;
            el.transform.localScale = (i == id) ? Vector3.one : new Vector3(0.78125f, 0.78125f, 0.78125f);
        }
    }
}
