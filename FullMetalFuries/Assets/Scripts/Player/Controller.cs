using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private readonly string xAxisName = "Horizontal";
    private readonly string yAxisName = "Vertical";

    private readonly string evadeName = "Evade Ability";
    private readonly string powerName = "Power Ability";

    private const int LEFT_MOUSE_BUTTON = 0;
    private const int RIGHT_MOUSE_BUTTON = 1;

    public float x { get; private set; }
    public float y { get; private set; }

    public bool evade { get; private set; }
    public bool power { get; private set; }
    public bool attack { get; private set; }
    public bool sec { get; private set; }

    public bool change { get; private set; }

    void Update()
    {
        x = y = 0f;
        evade = power = change = attack = sec = false;

        x = Input.GetAxis(xAxisName);
        y = Input.GetAxis(yAxisName);

        evade = Input.GetButton(evadeName);
        power = Input.GetButtonDown(powerName);
        attack = Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON);
        sec = Input.GetMouseButtonDown(RIGHT_MOUSE_BUTTON);
    }
}
