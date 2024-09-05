using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Canvas levelUpMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerScript;
    private int levelUpOption;
    private int baseLevelNeed = 10;
    private int level = 1;

    public int nextLevel = 10;

    private enum levelUpOptions {Damage, FireRate, MovementSpeed};

    private levelUpOptions[] choices = new levelUpOptions[3]; 

    public enum gameStates {Running, Paused, GameOver, LevelUp};

    public gameStates myGameState = gameStates.Running;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        Time.timeScale = 0;
        myGameState = gameStates.LevelUp;
        levelUpMenu.gameObject.SetActive(true);
        nextLevel += baseLevelNeed * level;
        level++;

        SetUpgradeOptions();
        SetButtonTexts();
    }

    private void SetUpgradeOptions()
    {
        choices[0] = (levelUpOptions)Random.Range(0, 3);
        choices[1] = (levelUpOptions)Random.Range(0, 3);
        while (choices[1] == choices[0])
        {
            choices[1] = (levelUpOptions)Random.Range(0, 3);
        }
        choices[2] = (levelUpOptions)Random.Range(0, 3);
        while (choices[2] == choices[0] || choices[2] == choices[1])
        {
            choices[2] = (levelUpOptions)Random.Range(0, 3);
        }
    }

    private void SetButtonTexts()
    {
        for(int i = 0; i < choices.Length; i++)
        {
            if(choices[i] == levelUpOptions.Damage)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Damage: +5";
            }
            else if(choices[i] == levelUpOptions.FireRate)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Fire rate: +50";
            }
            else if (choices[i] == levelUpOptions.MovementSpeed)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Movement speed: + 2";
            }
        }
    }

    public void SetOption(int option)
    {
        levelUpOption = option;
    }

    public void ConfirmLevelUp()
    {
        if(levelUpOption != 0)
        {
            //Upgrade player here
            Debug.Log("Leveled up with option:" + levelUpOption);
            levelUpOptions chosenLevelUp = choices[levelUpOption - 1];
            if(chosenLevelUp == levelUpOptions.Damage)
            {
                playerScript.gunDamage += 5;
            }
            else if (chosenLevelUp == levelUpOptions.FireRate)
            {
                playerScript.fireRate += 50;
            }
            else if (chosenLevelUp == levelUpOptions.MovementSpeed)
            {
                playerScript.movementSpeed += 2;
            }

            levelUpOption = 0;
            levelUpMenu.gameObject.SetActive(false);
            myGameState = gameStates.Running;
            Time.timeScale = 1;
        }
    }
}
