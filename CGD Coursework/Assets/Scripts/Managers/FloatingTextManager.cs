using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject TextContainer;
    [SerializeField]
    private GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText text in floatingTexts)
            text.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingTxt = GetFloatingText();
        floatingTxt.Txt.text = msg;
        floatingTxt.Txt.fontSize = fontSize;
        floatingTxt.Txt.color = color;

        floatingTxt.Go.transform.position = Camera.main.WorldToScreenPoint(position); // Convert world space to screen space so its usable in the UI
        floatingTxt.Motion = motion;
        floatingTxt.Duration = duration;

        floatingTxt.Show();
    }
    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.Active);

        if(txt == null)
        {
            txt = new FloatingText();
            txt.Go = Instantiate(textPrefab);
            txt.Go.transform.SetParent(TextContainer.transform);
            txt.Txt = txt.Go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

}
