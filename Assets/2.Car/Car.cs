using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct SetStates
{
    public float CurrentSpeed;
    public float SixEngineSpeed;
    public float EightEngineSpeed;
    public float NormalMaxSpeed;
    public float MinRotY;
    public float MaxRotY;
    public float CurrentRotY;
}
[RequireComponent(typeof(Rigidbody))]
public abstract class Car : MonoBehaviour
{
    [SerializeField] protected ParticleSystem speedEffectLine;
    [SerializeField] protected SetStates inGameSet;
    [SerializeField] protected int inGameSetIndex;
    [SerializeField] protected Rigidbody rig;
    public Rigidbody Rig { get => rig; set => rig = value; }
    [SerializeField] protected Transform target;
    [SerializeField] protected List<string> gameEndText = new List<string>();
    [SerializeField] protected float targetDis;
    protected float targetCurrentDis;
    public float TargetCurrentDis => targetCurrentDis;
    [SerializeField] protected float pointCount;
    public float PointCount => pointCount;
    protected float angle;
    protected int lap = 0;
    [SerializeField] protected int MaxLap = 2;
    
    public int Lap => lap;
    public List<Action<Car>> Skill = new List<Action<Car>>();
    protected float skillCool = 0;
    protected RaycastHit hit;
    protected float hitTime = 0;

    protected virtual void Update()
    {
        FindTarget();
        

        if (lap.Equals(MaxLap)) { Time.timeScale = 0; GameEnd(inGameSetIndex); }
    }

    protected virtual void FixedUpdate()
    {
        Angle();

    }

   
    protected abstract void Move();

    protected abstract void Angle();

    public float FindDir()
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
            if (pointCount.Equals(TrackManager.Instance.Points.Count + (int)GameManager.Instance.stage)) { lap += 1; Dele.Instance.LapUp(lap); pointCount = 1; Debug.Log(gameObject.name); }
        }
    }

    public void ReSpawn()
    {
        transform.position = TrackManager.Instance.GetIndex(target, 1).position + new Vector3(0, 6, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, -FindDir(), 0));
    }

    public void Error(Action action)
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.8f))
        {
            action();
            Debug.Log("´êÀ½");
            hitTime = 0;
        }
        else
        {
            //Debug.Log(hitTime);
            hitTime += Time.fixedDeltaTime;
            if (hitTime > 2)
            {
                //Debug.Log("Àç¼ÒÈ¯");
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

    public IEnumerator SpeedChange(float speed, bool switchbool, float tinm)
    {
        SppedManager(speed);
        if (switchbool) { speedEffectLine.Play(); }
        yield return new WaitForSeconds(tinm);
        SppedManager(-speed);
    }

    public void SppedManager(float speed)
    {
        inGameSet.CurrentSpeed += speed;
        inGameSet.NormalMaxSpeed += speed;
        inGameSet.SixEngineSpeed += speed;
        inGameSet.EightEngineSpeed += speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Grass"))
        {
            Debug.Log(other.gameObject.name);
            StartCoroutine(SpeedChange(-5, false, 1));
        }
    }
}
