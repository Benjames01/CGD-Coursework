using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{

    // Text fields
    [SerializeField]
    private Text levelText, hitpointText, coinText, upgradeCostText, expText;

    // Logic
    [SerializeField]
    private int currentCharacterSkin = 0;
    [SerializeField]
    private Image characterSelectionSprite;
    [SerializeField]
    private Image weaponSprite;
    [SerializeField]
    private RectTransform expBar;


    // Character Selection
    public void OnArrowClick(bool right)
    {

        currentCharacterSkin = right ? currentCharacterSkin - 1 : currentCharacterSkin + 1;
        currentCharacterSkin = Mathf.Clamp(currentCharacterSkin, 0, GameManager.instance.PlayerSprites.Count-1);

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.PlayerSprites[currentCharacterSkin];

        GameManager.instance.PrefSkin = currentCharacterSkin;
        GameManager.instance.Player.GetComponent<SpriteRenderer>().sprite = characterSelectionSprite.sprite;
        CloseMenu();
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
            
        CloseMenu(); // save data
    }


    private void Pause()
    {
        Time.timeScale = 0.1f;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }

    // Update the character information
    public void UpdateMenu()
    {
        GameManager.instance.MenuManager.GetComponent<UIManager>().Pause(false);
        int weaponLvl = GameManager.instance.Weapon.WeaponLevel;
        // Weapon
        weaponSprite.sprite = GameManager.instance.WeaponSprites[weaponLvl];

        upgradeCostText.text =
            (GameManager.instance.Weapon.WeaponLevel == GameManager.instance.WeaponPrices.Count -1)
            ? "MAX LEVEL" : GameManager.instance.WeaponPrices[weaponLvl].ToString();

        // PlayerSprite
        currentCharacterSkin = GameManager.instance.PrefSkin;
        characterSelectionSprite.sprite = GameManager.instance.PlayerSprites[currentCharacterSkin];

        // Stats
        hitpointText.text = GameManager.instance.Player.Hitpoints.ToString() + " / " + GameManager.instance.Player.MaxHitpoints.ToString();
        coinText.text = GameManager.instance.Coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        int currentLvl = GameManager.instance.GetCurrentLevel();
        // Exp Bar
        if (currentLvl == GameManager.instance.XpTable.Count - 1)
        {
            expBar.localScale = Vector3.one;
            expText.text = "MAX LEVEL";
            Debug.Log("Maxed");
        }
        else
        {
            // exp needed for previous level
            int previousExp = GameManager.instance.GetExpToLevel(currentLvl - 1);
            int currentExp = GameManager.instance.GetExpToLevel(currentLvl);

            int diff = currentExp - previousExp;
            int currentExpToLevel = GameManager.instance.Experience - previousExp;

            // Calculate ratio to make exp bar progress at current speed
            float completionRatio = (float)currentExpToLevel / (float) diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = currentExpToLevel.ToString() + " / " + diff;
        }
    }

    public void CloseMenu()
    {
        GameManager.instance.MenuManager.GetComponent<UIManager>().Resume();
        GameManager.instance.SaveState();
    }

}
