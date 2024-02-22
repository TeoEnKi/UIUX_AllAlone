using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("Start Menu")]
    [SerializeField] Sprite[] continueSprites;
    [SerializeField] Button[] startMenuBtns;
    [SerializeField] Image startmenuImg;
    [SerializeField] Color disableMenuColor;

    [Header("Settings")]
    [SerializeField] RectTransform settingsGO;
    [SerializeField] float maxYSettings = 34;
    [SerializeField] float minYSettings = -1000;
    [SerializeField] Slider graphicsSlider;
    [SerializeField] Slider audioSlider;

    [Header("Managers")]
    //AudioManager audioManager
    public DataManager dataManager;

    private void Awake()
    {

    }

    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();

        if (PlayerPrefs.HasKey("graphicsLvl") == true && graphicsSlider != null)
        {
            int number = (int)PlayerPrefs.GetFloat("graphicsLvl");
            graphicsSlider.value = number;
        }
    }

    public void NewGame()
    {
        //disable start menu
        foreach (Button btn in startMenuBtns)
        {
            btn.gameObject.SetActive(false);
        }
        StartCoroutine(FadeOutStartMenu());
    }
    IEnumerator FadeOutStartMenu()
    {
        while (true)
        {
            Color currentColour = startmenuImg.color;
            currentColour.r -= 0.1f;
            currentColour.g -= 0.1f;
            currentColour.b -= 0.1f;

            startmenuImg.color = currentColour;
            if (startmenuImg.color == Color.black)
            {
                SceneManager.LoadScene(1);
            }

            yield return new WaitForSeconds(.04f);
        }
    }

    public void OpenSettingsPage()
    {
        //disable start menu
        foreach (Button btn in startMenuBtns)
        {
            btn.gameObject.SetActive(false);
        }
        startmenuImg.color = disableMenuColor;


        settingsGO.gameObject.SetActive(true);
        StartCoroutine(RaiseSettingsPage());
    }

    IEnumerator RaiseSettingsPage()
    {
        while (settingsGO.anchoredPosition.y < maxYSettings)
        {
            //Vector3 settingsPageNewPos = settingsGO.position;
            //++settingsPageNewPos.y;
            //settingsGO.position = settingsPageNewPos;
            Vector3 settingsPageNewPos = Camera.main.WorldToViewportPoint(settingsGO.position);
            settingsPageNewPos.y += 2;
            settingsGO.position = Vector3.MoveTowards(settingsGO.position, Camera.main.ViewportToWorldPoint(settingsPageNewPos), 10f);


            yield return new WaitForSeconds(0);
        }
    }

    public void CloseSettingsPage()
    {
        //enable start menu
        foreach (Button btn in startMenuBtns)
        {
            btn.gameObject.SetActive(true);
        }
        startmenuImg.color = Color.white;

        StartCoroutine(LowerSettingsPage());
    }

    IEnumerator LowerSettingsPage()
    {
        while (settingsGO.gameObject.activeSelf)
        {
            Vector3 settingsPageNewPos = Camera.main.WorldToViewportPoint(settingsGO.position);
            settingsPageNewPos.y--;
            settingsGO.position = Vector3.MoveTowards(settingsGO.position, Camera.main.ViewportToWorldPoint(settingsPageNewPos), 40f);

            if (Camera.main.WorldToViewportPoint(settingsGO.transform.position).y < minYSettings)
            {
                settingsGO.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void SetGraphicsLevel(Slider graphicsSlider)
    {
        PlayerPrefs.SetFloat("graphicsLvl", graphicsSlider.value);

    }
    public void SetAudioLevel(Slider audioSlider)
    {
        PlayerPrefs.SetFloat("audioLvl", audioSlider.value);
        float unit = audioSource.maxDistance / audioSlider.maxValue;
        audioSource.volume = audioSlider.value*unit;
    }
}
