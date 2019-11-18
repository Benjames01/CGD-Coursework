using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    GameObject mHealthPanel;

    float mFill;

    void Update()
    {
        mFill = (float) GameController.Health / GameController.MaxHealth;


        if(mHealthPanel != null)
        {
            mHealthPanel.GetComponent<Image>().fillAmount = mFill;
        }
        
    }
}
