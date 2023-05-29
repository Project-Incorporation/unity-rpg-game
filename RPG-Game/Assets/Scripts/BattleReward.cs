using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;
    
    public GameObject rewardScreen;
    public TMP_Text xpText, itemText;

    public string[] rewardItems;
    public int xpEarned;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OpenRewardScreen(100, new string[] { "Iron Sword", "Iron Armor" });
        }
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewardItems = rewards;

        xpText.text = "Zdobyto " + xpEarned + " XP!";
        itemText.text = "";

        for(int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewards[i] + "\n";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        /*for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddEXP(xpEarned);
            }
        }*/

        GameManager.instance.playerStats[0].AddEXP(xpEarned);

        for(int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }


        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;
    }
}
