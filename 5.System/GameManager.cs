using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageTYpe { notReturn = 0, retun = 1 }
public class GameManager : MonoSingleTone<GameManager>
{
    [SerializeField] GameObject swcamera;
    public StageTYpe stage;
    static GameObject Swcamera;
    [SerializeField] GameObject InGame;
    public static int gold = 100000000;
    public GameObject CheatUI;
    public int Gold { get => gold; set => gold += value; }
    private float playTime;
    public float PlayTime => playTime += Time.deltaTime;
    public static float Score;
    private float startStopTime = 4;
    private List<Car> cars = new List<Car>();
    public List<Car> Cars => cars;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex.Equals(1)) { Swcamera = swcamera; Debug.Log("저자아아아앙"); }
        Debug.Log(GameManager.Instance.stage);
        Dele.Instance.ReSpawn = ReSpawn;
        Time.timeScale = 0;
        cars = FindObjectsOfType<Car>().ToList<Car>();
        StartCoroutine(StartSet());
    }
    private void Update()
    {
        Ranking();
        CheatKey();
    }
    private IEnumerator ReSpawn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }

    private IEnumerator StartSet()
    {
        while (startStopTime > 1)
        {
            startStopTime -= Time.unscaledDeltaTime;
            Dele.Instance.CountDown(startStopTime);
            yield return null;
        }
        Time.timeScale = 1.0f;

    }


    private void Ranking()
    {
        var sortt = cars.OrderBy(x => x.TargetCurrentDis);
        cars = sortt.ToList();
        sortt = cars.OrderByDescending(x => x.PointCount);
        cars = sortt.ToList();
        sortt = cars.OrderByDescending(x => x.Lap);
        cars = sortt.ToList();
    }

    public void SwitchCamera()
    {
        if (!Swcamera.activeSelf) { Swcamera.SetActive(true); InGame.SetActive(false); }
        else if (Swcamera.activeSelf) { Swcamera.SetActive(false); InGame.SetActive(true); }
    }

    private void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CheatUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ShopParts.checkCheat = true;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (Time.timeScale.Equals(1))
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
