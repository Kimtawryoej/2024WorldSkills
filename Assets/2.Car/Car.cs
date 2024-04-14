using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[Serializable]
public struct SetStates
{
    [SerializeField] public float CurrentSpeed;
    [SerializeField] public float SixEngineSpeed;
    [SerializeField] public float EightEngineSpeed;
    [SerializeField] public float NormalMaxSpeed;
    [SerializeField] public float MinRotY;
    [SerializeField] public float MaxRotY;
    [SerializeField] public float CurrentRotY;
}
[RequireComponent(typeof(Rigidbody))]
public abstract class Car : MonoBehaviour
{
    [SerializeField] protected ParticleSystem speedEffectLine;
    public ParticleSystem SpeedEffectLine { get => speedEffectLine; set => speedEffectLine = value; }
    [SerializeField] protected SetStates inGameSet;
    public SetStates InGameSet { get => inGameSet; set => inGameSet = value; }


    [SerializeField] protected int inGameSetIndex;
    [SerializeField] protected Rigidbody rig;
    public Rigidbody Rig { get => rig; set => rig = value; }
    [SerializeField] protected Transform target;
    [SerializeField] protected List<string> gameEndText = new List<string>();
    [SerializeField] protected float targetDis;
    public ParticleSystem HitEffect;
    protected float targetCurrentDis;
    public float TargetCurrentDis => targetCurrentDis;
    [SerializeField] protected float pointCount;
    public float PointCount => pointCount;
    protected float angle;
    protected int lap = 0;
    [SerializeField] protected int MaxLap = 2;

    public int Lap => lap;
    protected float skillCool = 0;
    protected RaycastHit hit;
    protected float hitTime = 0;

    protected MoveMent moveMent = new CarMoveMent();
    protected Rotation rotation = new CarRotationRotation();
    protected SpeedControl speedControl = new CarSpeedManage();
    public SpeedControl SpeedControlPro => speedControl;
    protected virtual void Awake()
    {
        SpeedControlPro.car = this;
    }

    protected virtual void Update()
    {
        FindTarget();
        if (lap.Equals(MaxLap)) { Time.timeScale = 0; GameEnd(inGameSetIndex); }
    }

    protected virtual void FixedUpdate()
    {
        rotation.Rotate();
    }

    protected float FindDir()
    {
        Vector3 dir = target.position - transform.position;
        return Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
    }

    protected virtual void FindTarget()
    {
        targetCurrentDis = Vector3.Distance(transform.position, target.position);
        if (targetCurrentDis < targetDis)
        {
            target = TrackManager.Instance.GetIndex(target, 0);
            pointCount += 1;
            if (pointCount.Equals(TrackManager.Instance.Points.Count + (int)GameManager.Instance.stage)) { lap += 1; Dele.Instance.LapUp(lap); pointCount = 1; }
        }
    }

    public void ReSpawn()
    {
        transform.SetPositionAndRotation(TrackManager.Instance.GetIndex(target, 1).position + new Vector3(0, 6, 0), Quaternion.Euler(new Vector3(0, -FindDir(), 0)));
        //transform.position = TrackManager.Instance.GetIndex(target, 1).position + new Vector3(0, 6, 0);
        //transform.rotation = Quaternion.Euler(new Vector3(0, -FindDir(), 0));
    }

    public void RayCastAction(Action action)
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.8f))
        {
            action();
            hitTime = 0;
        }
        else
        {
            hitTime += Time.fixedDeltaTime;
            if (hitTime > 2)
            {
                hitTime = 0;
                ReSpawn();
            }
        }
    }

    protected virtual void GameEnd(int index)
    {
        Dele.Instance.GameEndUI(gameEndText, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index); });
        lap = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Grass"))
        {
            SpeedControlPro.SpeedChange(-5, false, 1);
        }
    }
    public virtual void collisionFunc() { }

    // throw new NotImplementedException();

}

public class CarMoveMent : MoveMent
{
    public void Move(Action move) { move(); }
}

public class CarRotationRotation : Rotation
{
    public Action RotateFunc { get; set; }

    public void Rotate() { RotateFunc(); }
}

public class CarSpeedManage : SpeedControl
{
    public Car car { get; set; }

    public void SpeedChange(float speed, bool switchbool, float time)
    {
        TimerSystem.Instance.AddTimer(new TimeAgent(time, (TimeAgent) => { SppedManager(speed); if (switchbool) { car.SpeedEffectLine.Play(); } }, (TimeAgent) => { }, (TimeAgent) => { SppedManager(-speed); }));
    }
    public void SppedManager(float speed)
    {
        SetStates updatedSet = car.InGameSet;
        updatedSet.CurrentSpeed += speed;
        updatedSet.NormalMaxSpeed += speed;
        updatedSet.SixEngineSpeed += speed;
        updatedSet.EightEngineSpeed += speed;
        car.InGameSet = updatedSet;
    }
}


