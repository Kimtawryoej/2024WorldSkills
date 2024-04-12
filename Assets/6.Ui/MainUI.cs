using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button ranking;
    [FormerlySerializedAs("question")] [SerializeField] private Button howToPlay;
    [SerializeField] private Button howToPlayExit;
    [SerializeField] private Button exit;
    [SerializeField] private Animator howToPlayAni;
    [SerializeField] private Animator cameraAni;
    [SerializeField] private Animator aiAni;
    [SerializeField] private Animator playerAni;
    [SerializeField] private Animator titleAni;
    private void Start()
    {
        start.onClick.AddListener(() => { cameraAni.SetBool($"action", true); titleAni.SetBool($"action", true); });
        ranking.onClick.AddListener(() => SceneManager.LoadScene(4));
        howToPlay.onClick.AddListener(()=>{howToPlayAni.SetBool("On",true);});
        howToPlayExit.onClick.AddListener(()=>{howToPlayAni.SetBool("On",false);});
        exit.onClick.AddListener(() => { Application.Quit();});
        GameManager.Score = 0;
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (cameraAni.GetCurrentAnimatorStateInfo(0).IsName("Camera") == true
        && cameraAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            aiAni.SetBool($"action", true);
            playerAni.SetBool($"action", true);
        }
        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName($"CarMove 3") == true
           && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Debug.Log("��");
            SceneManager.LoadScene(1);
        }
    }
}
