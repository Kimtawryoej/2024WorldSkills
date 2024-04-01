using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCamera : MainCamera
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform[] moveTarget = new Transform[3];
    [SerializeField] private Button[] moveTargetButton = new Button[3];
    [SerializeField] private Animator ani;
    [SerializeField] private GameObject allShopUI;
    private int index = 0;
    private bool isPlaying = false;
    private float inTime = 1.0f;
    private float outTime = 3f;

    private void Start()
    {
        //DontDestroyOnLoad(this);
        moveTargetButton[0].onClick.AddListener(() => { index = 0; });
        moveTargetButton[1].onClick.AddListener(() => { index = 1; });
        moveTargetButton[2].onClick.AddListener(() => { index = 2; });
        //for (int i = 0; i < moveTarget.Length; i++)
        //{
        //    moveTargetButton[i].onClick.AddListener(() => { index = i; Debug.Log(index); });
        //}
        Dele.Instance.ShopAni = OutShop;
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ani.SetBool($"On", true);
        StartCoroutine(InShop());
    }

    private void Update()
    {
        if (isPlaying)
        {
            CameraMove(moveTarget[index].transform.position);
        }
            RotCamera(lookTarget.transform.position);
    }

    private IEnumerator InShop()
    {
        inTime = 1.0f;
        yield return (new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).IsName("Door") == true
        && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f));
        while (inTime > 0)
        {
            CameraMove(moveTarget[0].transform.position);
            inTime -= Time.unscaledDeltaTime;
            yield return null;
        }
        ani.SetBool($"On", false);
        allShopUI.gameObject.SetActive(true);
        isPlaying = true;
    }
    private IEnumerator OutShop()
    {
        outTime = 3.0f;
        allShopUI.gameObject.SetActive(false);
        ani.SetBool($"On", true);
        yield return (new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).IsName($"Door") == true
        && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f));
        isPlaying = false;
        while (outTime > 0)
        {
            CameraMove(moveTarget[3].transform.position/*+new Vector3(30,0,0)*/);
            outTime -= Time.unscaledDeltaTime;
            yield return null;
        }
        ani.SetBool($"On", false);
        yield return (new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).IsName($"CloseDoor") == true
        && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1));
        GameManager.Instance.SwitchCamera(); Time.timeScale = 1;
    }
}
