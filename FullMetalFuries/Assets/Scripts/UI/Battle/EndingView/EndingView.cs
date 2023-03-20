using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingView : View
{
    public TextMeshProUGUI endingText { get; private set; }
    public Image background { get; private set; }

    private void Awake()
    {
        endingText = transform.Find("EndingText").GetComponent<TextMeshProUGUI>();
        Debug.Assert(endingText != null);
        background = transform.Find("Background").GetComponent<Image>();
        Debug.Assert(background != null);
    }
}
