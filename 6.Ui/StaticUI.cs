using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticUI : MonoBehaviour
{
    [SerializeField] private Text gold;
    [SerializeField] private Animator tab;
    [SerializeField] private GameObject[] dontDesObj = new GameObject[4];
    private void Start()
    {
        foreach (var obj in dontDesObj) 
        { DontDestroyOnLoad(obj); }
    }
    private void Update()
    {
        Tab();
        gold.text = $"{GameManager.Instance.Gold}Gold";
        if (SceneManager.GetActiveScene().buildIndex.Equals(4))
        {
            Debug.Log("ªË¡¶");
            foreach (var obj in dontDesObj)
            { Destroy(obj); }
        }
    }
    private void Tab()
    {
        if (Input.GetKey(KeyCode.Tab)) { tab.SetBool($"On", true); }
        else if (Input.GetKeyUp(KeyCode.Tab)) { tab.SetBool($"On", false); }
    }
}
