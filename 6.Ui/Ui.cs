using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoSingleTone<Ui>
{
    [SerializeField] private Text countDown;
    [SerializeField] private Text time;
    [SerializeField] private Text lap;
    [SerializeField] private Text velocity;
    [SerializeField] private Image velocityImage;
    private float veloz = 0;
    [SerializeField] private Image skill;
    [SerializeField] private Image skill2;

    [SerializeField] private Text ranking;
    [SerializeField] private Text ranking2;

    

    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private Button gameEndButton;
    [SerializeField] private List<Text> gameEndList = new List<Text>();
    private void Start()
    {
        Dele.Instance.CountDown = CountDown;
        Dele.Instance.LapUp = LapUp;
        Dele.Instance.SkillImg = SkillUI;
        Dele.Instance.GameEndUI = GameEndUI;
    }

    private void Update()
    {
        time.text = $"TIME:{GameManager.Instance.PlayTime.ToString("N2")}";
                                                                                                                                 
        RankingUI();
        ChangeVelocity();
    }

    private void CountDown(float time)
    {
        //if (1< time && time<2) { countDown.text = $"GO"; }
        if (((int)time).Equals(1)) { countDown.text = $"GO"; }
        else { countDown.text = Mathf.Floor(time).ToString(); }
        if (time <= 1) { countDown.gameObject.SetActive(false); }
    }
    private void LapUp(int currentLap)
    {
        lap.text = currentLap.ToString();
    }

    private void ChangeVelocity()
    {
        if (Dele.Instance.PlayerVeloicty() > 2)
        {
            velocity.text = Dele.Instance.PlayerVeloicty().ToString("N2");
            velocityImage.transform.rotation =
                Quaternion.Euler(0, 0, -Mathf.Clamp(veloz = (Dele.Instance.PlayerVeloicty() * 2), 0, 180));
        }
        else { velocity.text = $"0"; }
    }
    private void SkillUI(Sprite skillImg, int index)
    {
        if (index.Equals(0))
        {
            if (skill.color.a.Equals(0))
            {
                skill.sprite = skillImg;
                skill.color = Color.white;
            }
            else if (skill2.color.a.Equals(0))
            {
                skill2.sprite = skillImg;
                skill2.color = Color.white;
            }
        }
        else if (index.Equals(1))
        {
            if (skill.color.a.Equals(1))
            {
                if (skill2.color.a.Equals(1))
                { skill.sprite = skill2.sprite; skill2.color = Color.clear; }
                else
                { skill.color = Color.clear; }

            }
        }
    }


    private void RankingUI()
    {
        ranking.text = $"1st:{GameManager.Instance.Cars[0].name}";
        ranking2.text = $"2nd:{GameManager.Instance.Cars[1].name}";
    }

    private void GameEndUI(List<string> gameEnd,Action button)
    {
        gameEndPanel.gameObject.SetActive(true);
        for (int i = 0; i < gameEnd.Count; i++)
        {
            gameEndList[i].text = gameEnd[i];
        }
        gameEndButton.onClick.AddListener(() => button());
    }
}
