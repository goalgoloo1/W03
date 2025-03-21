using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtnCoinGroup : MonoBehaviour
{
    private List<Image> _stageBtnCoins = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _stageBtnCoins.Add(transform.GetChild(i).GetComponent<Image>());
        }
    }

    public void ChangeCoinAlpha(int count)
    {
        for (var i = 0; i < count; i++)
        {
            _stageBtnCoins[i].color = new Color(1, 1, 1, 1);
        }
    }
}
