using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    [SerializeField] private Item itemType;
    private void OnEnable()
    {
        itemType = (Item)Random.Range(1, 7);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player car))
        {
            foreach (var item in ItemManager.Instance.ItemList)
            {
                if (item.Key.Equals(itemType))
                {
                    if (car.Skill.Count <= 2) { car.Skill.Add(item.Value.Item2); Dele.Instance.SkillImg(item.Value.Item1, 0); Debug.Log(car.Skill.Count); }
                    break;
                }
            }
            Debug.Log("¾ÆÀÌÅÛ");
            GameManager.Instance.StartCoroutine(Dele.Instance.ReSpawn(gameObject, 3));
            gameObject.SetActive(false);
        }
    }
}
