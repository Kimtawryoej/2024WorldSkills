using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Parts { DesertWheel, MountainWheel, CityWheel, SixEngine, EightEngine, None }
public class ShopParts : MonoBehaviour
{
    private Parts buyItem;
   public static bool checkCheat = false;
    [SerializeField] private Button[] ItemList = new Button[5];
    [SerializeField] private Text partExpl;
    [SerializeField] private Button buy;
    [SerializeField] private Text buyText;
    [SerializeField] private Button exit;
    private Dictionary<Parts, (string, Action)> PartDict;
    [SerializeField] private List<Parts> boughtItem = new List<Parts>();
    //[SerializeField] private GameObject saveItems;
    public List<Image> boughtItemImg = new List<Image>();
    //private List<(Sprite, Color)> boughtItemImgImfor = new List<(Sprite, Color)>();
    private void Start()
    {
        //boughtItemImg = saveItems.GetComponentsInChildren<Image>().ToList(); Debug.Log("����");

        //for (int i = 0; i < boughtItemImgImfor.Count; i++)
        //{
        //    Debug.Log(boughtItemImgImfor[i].Item1);
        //    boughtItemImg[i].sprite = boughtItemImgImfor[i].Item1;
        //    boughtItemImg[i].color = boughtItemImgImfor[i].Item2;
        //}

        ItemSet();
        foreach (var item in ItemList)
        {
            item.TryGetComponent(out CarPart part);
            item.onClick.AddListener(() =>
            {
                buyItem = part.Part; ItemFind(part.Part); if (boughtItem.Contains(part.Part)) { buyText.text = $"SoldOut"; }
                else if (!boughtItem.Contains(part.Part)) { buyText.text = $"Buy"; }
            });
        }
        buy.onClick.AddListener(() => { BuyItem(); });
        exit.onClick.AddListener(() => { StartCoroutine(Dele.Instance.ShopAni()); });
        //DontDestroyOnLoad(this);
    }

    private void ItemSet()
    {
        PartDict = new Dictionary<Parts, (string, Action)>()
        {
            {Parts.DesertWheel,($"DesertWheel",()=>{Dele.Instance.Shop(Parts.DesertWheel,Color.red); })},
            {Parts.MountainWheel,($"MountainWheel",()=>{Dele.Instance.Shop(Parts.MountainWheel,Color.green); })},
            {Parts.CityWheel,($"CityWheel",()=>{Dele.Instance.Shop(Parts.CityWheel,Color.blue); })},
            {Parts.SixEngine,($"SixEngine",()=>{Dele.Instance.Shop(Parts.SixEngine,Color.black); })},
            {Parts.EightEngine,($"EightEngine",()=>{Dele.Instance.Shop(Parts.EightEngine,Color.white); })}
        };
    }

    private void ItemFind(Parts parts)
    {
        foreach (var part in PartDict)
        {
            if (part.Key.Equals(parts))
            {
                partExpl.text = part.Value.Item1;
                part.Value.Item2();
            }
        }
    }

    private void BuyItem()
    {
        switch (buyItem)
        {
            case Parts.DesertWheel:
                if (GameManager.Instance.Gold > (1000000) && !boughtItem.Contains(buyItem)) { GameManager.Instance.Gold = -1000000; if (checkCheat) { GameManager.Instance.Gold += 1000000; } Dele.Instance.PartsApply(Dele.Instance.PartsRead()); boughtItem.Add(buyItem); buyText.text = $"SoldOut"; AddPanel(buyItem); }
                else if (boughtItem.Contains(buyItem)) { Dele.Instance.PartsApply(Dele.Instance.PartsRead()); }
                break;
            case Parts.MountainWheel:
                if (GameManager.Instance.Gold > (2000000) && !boughtItem.Contains(buyItem)) { GameManager.Instance.Gold = -2000000; if (checkCheat) { GameManager.Instance.Gold += 2000000; } Dele.Instance.PartsApply(Dele.Instance.PartsRead()); boughtItem.Add(buyItem); buyText.text = $"SoldOut"; AddPanel(buyItem); }
                else if (boughtItem.Contains(buyItem)) { Dele.Instance.PartsApply(Dele.Instance.PartsRead()); }
                break;
            case Parts.CityWheel:
                if (GameManager.Instance.Gold > (3000000) && !boughtItem.Contains(buyItem)) { GameManager.Instance.Gold = -3000000; if (checkCheat) { GameManager.Instance.Gold += 3000000; } Dele.Instance.PartsApply(Dele.Instance.PartsRead()); boughtItem.Add(buyItem); buyText.text = $"SoldOut"; AddPanel(buyItem); }
                else if (boughtItem.Contains(buyItem)) { Dele.Instance.PartsApply(Dele.Instance.PartsRead()); }
                break;
            case Parts.SixEngine:
                if (GameManager.Instance.Gold > (2500000) && !boughtItem.Contains(buyItem)) { GameManager.Instance.Gold = -2500000; if (checkCheat) { GameManager.Instance.Gold += 2500000; } Dele.Instance.PartsApply(Dele.Instance.PartsRead()); boughtItem.Add(buyItem); buyText.text = $"SoldOut"; AddPanel(buyItem); }
                else if (boughtItem.Contains(buyItem)) { Dele.Instance.PartsApply(Dele.Instance.PartsRead()); }
                break;
            case Parts.EightEngine:
                if (GameManager.Instance.Gold > (5000000) && !boughtItem.Contains(buyItem)) { GameManager.Instance.Gold = -5000000; if (checkCheat) { GameManager.Instance.Gold += 5000000; } Dele.Instance.PartsApply(Dele.Instance.PartsRead()); boughtItem.Add(buyItem); buyText.text = $"SoldOut"; AddPanel(buyItem); }
                else if (boughtItem.Contains(buyItem)) { Dele.Instance.PartsApply(Dele.Instance.PartsRead()); }
                break;
        }
    }

    private void AddPanel(Parts parts)
    {
        Debug.Log("파츠구입");
        foreach (var item in ItemList)
        {
            Debug.Log("파츠구입2");
            item.TryGetComponent(out CarPart part);
            if (part.Part.Equals(parts))
            {
                Debug.Log("파츠구입3");
                for (int i = 0; i < boughtItemImg.Count; i++)
                {
                    Debug.Log("파츠구입4");
                    if (ReferenceEquals(boughtItemImg[i].sprite, null))
                    {
                        Debug.Log("파츠구입5");
                        item.transform.GetChild(0).TryGetComponent(out Image image);
                        //boughtItemImgImfor.Add((image.sprite, image.color));
                        boughtItemImg[i].sprite = image.sprite;
                        boughtItemImg[i].color = image.color;
                        break;
                    }
                }
            }
        }
    }

    private void Update()
    {

    }
}
