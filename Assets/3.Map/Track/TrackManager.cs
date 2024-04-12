using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrackManager : MonoSingleTone<TrackManager>
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private GameObject appearPoint;
    public List<GameObject> appearPoints = new List<GameObject>();
    public List<Transform> Points => points;
    [SerializeField] private Vector3 drawCubeSize;
    [SerializeField] private float appearPointVector;
    private void OnDrawGizmos()
    {
        points = transform.GetComponentsInChildren<Transform>().ToList();
        points.Remove(gameObject.transform);
        points.ForEach(x => { Gizmos.DrawWireCube(x.position, drawCubeSize); });
    }
    public Transform GetIndex(Transform currentPoint, int index)
    {
        int value = 0;
        if (index.Equals(0)) { value = ((points.IndexOf(currentPoint) + 1).Equals(points.Count)) ? 0 : points.IndexOf(currentPoint) + 1;  /*Debug.Log();*/ }
        else if (index.Equals(1)) { value = ((points.IndexOf(currentPoint) - 1) < 0) ? points.Count - 1 : points.IndexOf(currentPoint) - 1; }
        return points[value];
    }

    public void InsPoint(Transform currentPoint)
    {
        if (appearPoints.Count.Equals(points.Count))
        {
            appearPoints.Clear();
        }
        GameObject gobject = Instantiate(appearPoint, currentPoint.transform.position - new Vector3(0, appearPointVector, 0), Quaternion.identity);
        if (appearPoints.Contains(gobject))
        {
            Destroy(gobject);
        }
        else if (!appearPoints.Contains(gobject))
        {
            appearPoints.Add(gobject);
        }
    }

    public void GetAppearPoint(Transform currentPoint)
    {
        Debug.Log(currentPoint.name);
        int value = ((points.IndexOf(currentPoint) - 1) < 0) ? points.Count - 1 : points.IndexOf(currentPoint) - 1;
        Debug.Log(value);
        Destroy(appearPoints[value]);
    }
}
