using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] RectTransform[] allBookmarks;
    [SerializeField] RectTransform deselectedBookmarks;
    [SerializeField] RectTransform selectedBookmark;
    List<OldBookmarkPos> oldBookmarksPos;
    public class OldBookmarkPos
    {
        public string bmName = string.Empty;
        public float bmPosX = 0;
        public OldBookmarkPos(string bmName, float bmPosX)
        {
            this.bmName = bmName;
            this.bmPosX = bmPosX;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        oldBookmarksPos = new List<OldBookmarkPos>();
        foreach (var bookmark in allBookmarks)
        {
            OldBookmarkPos name = new OldBookmarkPos(bookmark.name, bookmark.anchoredPosition.x);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BookmarkNoHover(RectTransform bookmark)
    {
        if (bookmark.parent == deselectedBookmarks)
        {
            bookmark.GetComponent<Animator>().Play("No Hover");
        }
    }
    public void BookmarkHover(RectTransform bookmark)
    {
        if (bookmark.parent == deselectedBookmarks)
        {
            bookmark.GetComponent<Animator>().Play("Hover");
        }
    }
    public void BookmarkDeselect(RectTransform bookmark)
    {
        bookmark.SetParent(deselectedBookmarks);
        foreach (OldBookmarkPos oldBookmark in oldBookmarksPos)
        {
            if (oldBookmark.bmName == bookmark.name)
            {
                bookmark.anchoredPosition = new Vector2(oldBookmark.bmPosX, bookmark.anchoredPosition.y - 82);
                break;
            }
        }

    }
    public void BookmarkSelect(RectTransform bookmark)
    {
        if (bookmark.parent == selectedBookmark) return;

        GetComponent<Animator>().Play("Switch Section");
        foreach (RectTransform b in allBookmarks)
        {
            if (b == bookmark)
            {
                b.SetParent(selectedBookmark);
                b.anchoredPosition = new Vector2(-399, b.anchoredPosition.y + 82);
                Debug.Log(b.localPosition);
            }
            else
            {

                BookmarkDeselect(b);
            }

        }
    }
}
