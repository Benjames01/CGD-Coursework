using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    [SerializeField]
    private bool active;

    [SerializeField]
    private GameObject go;

    [SerializeField]
    private Text txt;

    [SerializeField]
    private Vector3 motion;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float lastShown;
    public bool Active { get => active; set => active = value; }
    public GameObject Go { get => go; set => go = value; }
    public Text Txt { get => txt; set => txt = value; }
    public Vector3 Motion { get => motion; set => motion = value; }
    public float Duration { get => duration; set => duration = value; }
    public float LastShown { get => lastShown; set => lastShown = value; }

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;

        // If (current time - time it was last shown) is greater than the duration hide the text
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
