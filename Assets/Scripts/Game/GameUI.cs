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
    [SerializeField]
    private Text levelText;
    private int knifeIndexToChange;

    public void ShowRestartButton()
    {
        restartBtn.SetActive(true);
    }

    public void SetKnivesCount(int count)
    {
        DestroyKnivesInPanel();
        for (int i = 0; i < count; i++)
        {
            Instantiate(knifeIcon, panelKnives.transform);
        }
    }

    private void DestroyKnivesInPanel()
    {
        try
        {
            knifeIndexToChange = 0;
            for (int i = 0; i < panelKnives.GetComponentsInChildren<Image>().Length; i++)
            {
                foreach (var img in panelKnives.GetComponentsInChildren<Image>())
                {
                    Destroy(img.gameObject);
                }
            }
        }
        catch
        {
            Debug.Log("Smth went wrong while destroing knives");
            return;
        }

    }

    
    public void DecrementKnives()
    {
        panelKnives.transform.GetChild(knifeIndexToChange).GetComponent<Image>().color = usedKnifeColor;
        knifeIndexToChange++;
    }

    public void IncreaseLevel()
    {

        levelText.text = "Level " + GameController.instance.levelIndex;
    }
}
