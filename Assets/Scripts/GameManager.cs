using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board gameBoard;

    // player list
    [Header("player settings")]
    public List<Player> playerList = new List<Player>();
    public int currentPlayerIndex = 0;

    // player info panel 
    public GameObject playerInfoPrefab;
    public Transform playerPanel;

    // game setting values
    [Header("golbal game settings")]
    
    public int maxTurnesInJail = 3;
    
    public int startMoney = 2000;
    public int passGoMoney = 200;



    // ai info lists
    [Header("Game Info assets")]
    public List<string> aiNames = new List<string>();
    public List<Sprite> token_img = new List<Sprite>();
    public List<GameObject> tokens = new List<GameObject>();

    // dice roll info
    int[] rolledDice;
    bool rolled_a_double;
    int doubleCount;



    private void Awake()
    {
        instance = this;
    }




    // Start is called before the first frame update
    void Start()
    {
        Init();
        playerList[currentPlayerIndex].playerInfo.ToggleTurn();
        if (playerList[currentPlayerIndex].playerType == Player.PlayerType.AI)
        {
            // ai player
            RollDice();
        }
        else
        {
            // humman player
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject infoObj = Instantiate(playerInfoPrefab, playerPanel, false);
            PlayerInfo info = infoObj.GetComponent<PlayerInfo>();
            playerList[i].Init(gameBoard.route[0], startMoney, info);
            GameObject newToken = Instantiate(playerList[i].MyToken, playerList[i].currentNode.transform, false);
            playerList[i].setToken(newToken);
        }
    }


    public void RollDice()
    {
        rolledDice = new int[2];
        rolledDice[0] = Random.Range(1, 7);
        rolledDice[1] = Random.Range(1, 7);
        rolled_a_double = rolledDice[0] == rolledDice[1];
        print(playerList[currentPlayerIndex].name+ " Rolled "+ rolledDice[0]+ " and "+ rolledDice[1]);
     
        StartCoroutine(DelayBeforeMove(rolledDice[0] + rolledDice[1]));

    }

    IEnumerator DelayBeforeMove(int roll)
    {
        
        yield return new WaitForSeconds(2f);
      
        gameBoard.MovePlayerToken(roll, playerList[currentPlayerIndex]);

    }

    public void SwitchPlayer()
    {
        currentPlayerIndex++;
        // rolled doubles

        // over flow check
        if (currentPlayerIndex >= playerList.Count) 
        {
            currentPlayerIndex = 0;
        }
        // check if in jail

        // if player is ai
        if (playerList[currentPlayerIndex].playerType == Player.PlayerType.AI)
        {
            RollDice();
        }


        // if humman
            //show ui

    }

    public int PlayerPassedGo()
    {
        return passGoMoney;
    }
}
