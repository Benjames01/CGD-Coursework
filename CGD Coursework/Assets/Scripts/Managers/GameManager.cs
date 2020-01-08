using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        // Singleton pattern implementation
        if(GameManager.instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += LoadState;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(hud);
            Destroy(menu);
            Destroy(ftManager);
            Destroy(menuManager);
            Destroy(pauseMenu);
        }
    }

    // Resources
    [SerializeField]
    private List<Sprite> playerSprites;
    [SerializeField]
    private List<Sprite> weaponSprites;
    [SerializeField]
    private List<int> weaponPrices;
    [SerializeField]
    private List<int> xpTable;
    [SerializeField]
    private GameObject[] magicAttackPrefab;

    public List<Sprite> PlayerSprites { get => playerSprites; set => playerSprites = value; }
    public List<Sprite> WeaponSprites { get => weaponSprites; set => weaponSprites = value; }
    public List<int> WeaponPrices { get => weaponPrices; set => weaponPrices = value; }
    public List<int> XpTable { get => xpTable; set => xpTable = value; }
    public GameObject[] MagicAttackPrefab { get => magicAttackPrefab; set => magicAttackPrefab = value; }

    // References
    [SerializeField]
    private Player player;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private FloatingTextManager floatingTextManager;
    [SerializeField]
    private RectTransform hpBar;
    [SerializeField]
    private GameObject hud;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject ftManager;
    [SerializeField]
    private Animator deathMenuAnim;
    [SerializeField]
    private GameObject menuManager;
    [SerializeField]
    private GameObject pauseMenu;

    public Player Player { get => player; set => player = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public FloatingTextManager FloatingTextManager { get => floatingTextManager; set => floatingTextManager = value; }
    public Animator DeathMenuAnim { get => deathMenuAnim; set => deathMenuAnim = value; }
    public GameObject MenuManager { get => menuManager; }

    // Logic
    [SerializeField]
    private int coins;
    [SerializeField]
    private int experience;
    [SerializeField]
    private int prefSkin = 0;
    [SerializeField]
    private int weaponLevel;

    public int Coins { get => coins; set => coins = value; }
    public int Experience { get => experience; set => experience = value; }
    public int PrefSkin { get => prefSkin; set => prefSkin = value; }

    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Weapon Upgrades
    public bool TryUpgradeWeapon()
    {
        // is weapon highest level?
        if (weaponPrices.Count-1 <= weapon.WeaponLevel)
            return false;
        if (coins >= WeaponPrices[weapon.WeaponLevel])
        {
            coins -= weaponPrices[weapon.WeaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
            
        return false;
    }

    // Experience system

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;
            if(r == xpTable.Count - 1) // Highest level
            return r;
            
        }

        return r;
    }
    public int GetExpToLevel(int level)
    {
        int r = 0;
        int exp = 0;
        while(r < level)
        {
            exp += xpTable[r];
            r++;
        }

        return exp;
    }

    public void GainExp(int exp)
    {
        int currentLevel = GetCurrentLevel();
        experience += exp;
        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }

    private void OnLevelUp()
    {
        Debug.Log("Level Up!!");       
        player.OnLevelUp();
    }

    // Health bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.Hitpoints / (float)player.MaxHitpoints;
        hpBar.localScale = new Vector3(1, ratio, 1);
    }


    // On Scene load
    private void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Death menu & respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("HubScene");

        player.Respawn();
    }

    // Save State
    /*
     * 
     *  INT prefSkin        (0)
     *  INT coins           (1)          
     *  INT experience      (2)
     *  INT weaponLevel     (3)
     * 
     */

    public void SaveState()
    {
        string s = "";

        s += prefSkin + "|";
        s += Coins.ToString() + "|";
        s += Experience.ToString() + "|";
        s += weapon.WeaponLevel.ToString();

        // just to update in inspector
        weaponLevel = weapon.WeaponLevel;

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (player == null)
            player = GameObject.Find("Player").GetComponent<Player>();

        if (floatingTextManager == null)
            floatingTextManager = GameObject.Find("UIContainer").GetComponentInChildren<FloatingTextManager>();

        if (weapon == null)
            weapon = GameObject.Find("Player").GetComponentInChildren<Weapon>();

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        // Get save data from PlayerPrefs and split using the '|' symbol
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        prefSkin = int.Parse(data[0]);
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[prefSkin];

        coins = int.Parse(data[1]);

        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        weaponLevel = int.Parse(data[3]);
        weapon.WeaponLevel = weaponLevel;       
        weapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[weaponLevel];

    }
}

