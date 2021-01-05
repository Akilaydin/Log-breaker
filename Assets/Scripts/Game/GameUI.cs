using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartBtn;
    [SerializeField]
    private GameObject panelKnives;
    [SerializeField]
    private GameObject knifeIcon;
    [SerializeField]
    private Color usedKnifeColor;

    public void ShowRestartButton()
    {
        restartBtn.SetActive(true);
    }

    public void SetKnivesCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(knifeIcon, panelKnives.transform);
        }
    }

    private int knifeIndexToChange = 0;

    public void DecrementKnives()
    {
        panelKnives.transform.GetChild(knifeIndexToChange++).GetComponent<Image>().color = usedKnifeColor;
    }
}
