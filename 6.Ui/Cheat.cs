using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] private Item itemType;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cheatUi;

    private void Start()
    {
        Debug.Log(itemType);
    }
    public void ClickCheat()
    {
        if (player.TryGetComponent(out Player car))
        {
            foreach (var item in ItemManager.Instance.ItemList)
            {
                if (item.Key.Equals(itemType))
                {
                    Debug.Log(itemType);
                    if (car.Skill.Count <= 2) { car.Skill.Add(item.Value.Item2); Dele.Instance.SkillImg(item.Value.Item1, 0); Time.timeScale = 1; cheatUi.gameObject.SetActive(false); }
                    break;
                }
            }
        }
    }
}
