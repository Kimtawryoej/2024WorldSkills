using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Car
{
    [SerializeField] private GameObject CameraSpeedParticle;
    public static Player Instance;
    public Parts parts;
    private List<MeshRenderer> wheel = new List<MeshRenderer>();
    [SerializeField] private float strRotY;
    private void Awake()
    {
        Instance = this;
    }
    protected void Start()
    {        

        TrackManager.Instance.InsPoint(target);
        wheel = gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
        Debug.Log(gameObject.TryGetComponent(out MeshRenderer mesh));
        wheel.Remove(mesh);
        Dele.Instance.PlayerVeloicty = PlayerVelocityRe;
        Dele.Instance.PartsApply = SetPlayer;
        StartCoroutine(SkillUse());
    }
    protected override void Update()
    {
        base.Update();
        Drift();
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReSpawn();
            StartCoroutine(SpeedChange(-inGameSet.CurrentSpeed, false, 1.5f));
            //rig.velocity = Vector3.zero;
        }

        PartsEngine(parts);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Error(() => { PartsWheel(hit, parts); });
        Move();
    }
    protected override void Angle()
    {
        angle += inGameSet.CurrentRotY * Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, strRotY + angle, transform.eulerAngles.z);
    }

    protected override void Move()
    {

        rig.AddForce(transform.right * inGameSet.CurrentSpeed * Input.GetAxis("Vertical"), ForceMode.Acceleration);
        if (rig.velocity.magnitude > 40) { CameraSpeedParticle.SetActive(true); }
        else { CameraSpeedParticle.SetActive(false); }
    }

    protected void Drift()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { inGameSet.CurrentRotY = Mathf.Clamp(inGameSet.CurrentRotY += 0.05f, inGameSet.MinRotY, inGameSet.MaxRotY); }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) { inGameSet.CurrentRotY = inGameSet.MinRotY; }
    }

    private float PlayerVelocityRe()
    {
        return rig.velocity.magnitude;
    }

    private void SetPlayer(ModelCar shop)
    {
        for (int i = 0; i < wheel.Count; i++)
        {
            wheel[i].material.color = shop.Wheel[i].material.color;
        }
        parts = shop.Parts;
    }

    protected override void GameEnd(int index)
    {
        gameEndText[1] = $"Time:{GameManager.Instance.PlayTime.ToString("N4")}";
        gameEndText[2] = $"ScoreBonus: * 99999f";
        gameEndText[3] = $"Score: Time * 99999f =>{Mathf.Round(GameManager.Score += (99999f / GameManager.Instance.PlayTime))}Point";
        base.GameEnd(index);
    }

    private void PartsWheel(RaycastHit hit, Parts part)
    {
        if (hit.collider.gameObject.TryGetComponent(out MapEnum map))
        {

            if (map.MapList.Equals(MapList.DesertMap))
            {
                if (!part.Equals(Parts.DesertWheel))
                    rig.drag = 1.6f;
                else { rig.drag = 1.0f; }
            }
            else { rig.drag = 1.0f; }
        }

    }

    private void PartsEngine(Parts part)
    {
        if (part.Equals(Parts.SixEngine)) { inGameSet.CurrentSpeed = inGameSet.SixEngineSpeed; }
        else if (part.Equals(Parts.EightEngine)) { inGameSet.CurrentSpeed = inGameSet.EightEngineSpeed; }
        else { inGameSet.CurrentSpeed = inGameSet.NormalMaxSpeed; }
    }

    protected override void FindTarget()
    {
        targetCurrentDis = Vector3.Distance(transform.position, target.position);
        if (targetCurrentDis < targetDis)
        {
            target = TrackManager.Instance.GetIndex(target, 0);
            TrackManager.Instance.GetAppearPoint(target);
            TrackManager.Instance.InsPoint(target);
            pointCount += 1;
            if (pointCount.Equals(TrackManager.Instance.Points.Count + (int)GameManager.Instance.stage)) { lap += 1; Dele.Instance.LapUp(lap); pointCount = 1; }
        }

        //if (targetCurrentDis < targetDis) { TrackManager.Instance.GetAppearPoint(target); }
    }


    protected IEnumerator SkillUse()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && Skill.Count >= 1);
            Skill.First()(this);
            Skill.Remove(Skill.First());
            Debug.Log("횟수");
            Dele.Instance.SkillImg(null, 1);
            yield return wait;
        }
    }
}
