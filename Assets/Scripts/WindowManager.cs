using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    #region Attributes

    #region Player Pref key Constants

    private const string RESOLUTION_PREF_KEY = "resolution";

    #endregion

    #region Resolution

    [SerializeField] private TMP_Text resText;

    private Resolution[] resolutions;

    private int currResId = 0;
    #endregion
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        currResId = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY,0);

        SetResText(resolutions[currResId]);
    }
    #region Resolution Cycling

    private void SetResText(Resolution resolution)
    {
        resText.text = resolution.width + "x" + resolution.height;
    }
    public void SetNxtRes()
    {
        currResId = GetNxtWrappedText(resolutions, currResId);
        SetResText(resolutions[currResId]);
    }
    public void SetPrevRes()
    {
        currResId = GetPrevWrappedText(resolutions, currResId);
        SetResText(resolutions[currResId]);
    }
    #endregion

    #region ResolutionCycling

    #region ApplyRes
    private void SetAndApplyRes(int newResId)
    {
        currResId = newResId;
        ApplyCurrRes();
    }
    private void ApplyCurrRes()
    {
        ApplyRes(resolutions[currResId]);
    }
    private void ApplyRes(Resolution resolution)
    {
        SetResText(resolution);

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currResId);
    }
    #endregion
    #endregion

    #region Misc Helpers
    private int GetNxtWrappedText<T>(IList<T> collection, int currId)
    {
        if (collection.Count < 1) return 0;
        return (currId + 1) & collection.Count;

    }
    private int GetPrevWrappedText<T>(IList<T> collection, int currId)
    {
        if (collection.Count < 1) return 0;
        if (currId - 1 < 0) return collection.Count - 1;
        return (currId - 1) % collection.Count;

    }
    #endregion
    public void ApplyChanges()
    {
        SetAndApplyRes(currResId);
    }
}
