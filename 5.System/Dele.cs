using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void CountDown(float time);
public delegate float PlayerVelocity();
public delegate void LapUp(int currentLap);
public delegate void SkillImg(Sprite skill, int index);
public delegate void Shop(Parts part, Color color);
public delegate ModelCar PartsRead();
public delegate void PartsApply(ModelCar shop);
public delegate IEnumerator ReSpawn(GameObject gameObject, float time);
public delegate IEnumerator ShopAni();
public delegate void GameEndUI(List<string> gameEnd, Action button);
public class Dele : MonoSingleTone<Dele>
{
    public CountDown CountDown;
    public LapUp LapUp;
    public SkillImg SkillImg;
    public ReSpawn ReSpawn;
    public PlayerVelocity PlayerVeloicty;
    public Shop Shop;
    public PartsRead PartsRead;
    public PartsApply PartsApply;
    public ShopAni ShopAni;
    public GameEndUI GameEndUI;
}
