using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Graphics")]
    RenderTexture renderTexture;
    [SerializeField] int minResMulti = 15;
    public int currResMulti = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            //currResMulti = minResMulti + (int)Mathf.Pow(PlayerPrefs.GetFloat("graphicsLvl"), 3);
            //Debug.Log(currResMulti);
            //Vector2 size = new Vector2(16, 9);
            //RenderTexture snapShot = new RenderTexture((int)size.x * currResMulti, (int)size.y * currResMulti, 16, RenderTextureFormat.ARGB32);
            //Camera cam = Camera.main;
            //cam.targetTexture = snapShot;
            //cam.aspect = size.x / size.y;
            //RawImage rawImg = FindAnyObjectByType<RawImage>();
            //rawImg.texture = snapShot;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Home))
        {

            SceneManager.LoadScene(0);

        }
    }

}
