using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows throttle and speed of the player ship.
/// </summary>
public class SpeedUI : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null && BoatInputHuman2.playerBoat != null && BoatEngine.playerBoat != null)
        {
            text.text = string.Format("THR: {0}\nSPD: {1}", (BoatInputHuman2.playerBoat.Throttle * 100.0f).ToString("000"), BoatEngine.playerBoat.Velocity.magnitude.ToString("000"));
        }
    }
}
