using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    Animator animator;
    [SerializeField] RectTransform[] allDeselectedBookmarks;
    [SerializeField] RectTransform[] allSelectedBookmarks;

    [SerializeField] RectTransform previousSelectBm;

    [SerializeField] RectTransform[] sections;


    // Update is called once per frame
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        DisplaySection(previousSelectBm, 0f);
    }
    public void BookmarkNoHover(RectTransform bookmark)
    {

        bookmark.GetComponent<Animator>().Play("No Hover");

    }
    public void BookmarkHover(RectTransform bookmark)
    {

        bookmark.GetComponent<Animator>().Play("Hover");

    }
    IEnumerator BookmarkDeselect(RectTransform bookmark, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (RectTransform deselectBm in allDeselectedBookmarks)
        {
            if (deselectBm.name == bookmark.name)
            {
                deselectBm.gameObject.SetActive(true);
                break;
            }

        }
        previousSelectBm.gameObject.SetActive(false);

        //bookmark.SetParent(deselectedBookmarks);
        //foreach (OldBookmarkPos oldBookmark in oldBookmarksPos)
        //{
        //    if (oldBookmark.bmName == bookmark.name)
        //    {
        //        bookmark.anchoredPosition = new Vector2(oldBookmark.bmPosX, bookmark.anchoredPosition.y - 82);
        //        break;
        //    }
        //}

    }
    IEnumerator BookmarkSelect(RectTransform bookmark, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (RectTransform selectBm in allSelectedBookmarks)
        {
            if (bookmark.name == selectBm.name)
            {
                selectBm.gameObject.SetActive(true);
                previousSelectBm = selectBm;
                break;
            }

        }
        bookmark.gameObject.SetActive(false);
    }
    public void BookmarkSwitch(RectTransform bookmark)
    {
        animator.Play("Switch Section");
        AudioManager.instance.PlayOpenJournal();
        StartCoroutine(BookmarkDeselect(previousSelectBm, 0.2f));
        StartCoroutine(BookmarkSelect(bookmark, 0.2f));
        StartCoroutine(DisplaySection(bookmark, 0.2f));
    }

    public void StopAnimation()
    {
        GetComponent<Animator>().Play("New State");
    }
    IEnumerator DisplaySection(RectTransform bookmark, float delay)
    {
        yield return new WaitForSeconds(delay);
        string secName = bookmark.name.Replace("Bookmark_", "");
        foreach (RectTransform section in sections)
        {
            if (section.name == secName)
            {
                if (section.name.ToLower().Contains("toy"))
                {
                    TutorialManager.instance.UpdateStage(TutorialStage.Press_Q_And_Open_Craft_Toys_Section);
                }
                section.gameObject.SetActive(true);
            }
            else
            {
                section.gameObject.SetActive(false);
            }
        }
        ShowSection(secName);
        
    }
    private void ShowSection(string secName)
    {
        foreach (RectTransform section in sections)
        {
            if (section.name.Contains(secName))
            {
                section.gameObject.SetActive(true);
            }
            else
            {
                section.gameObject.SetActive(false);
            }
        }
    }
}
