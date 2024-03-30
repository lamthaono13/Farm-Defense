using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateManager : MonoBehaviour
{
    public void Vibate(long timeVibrate)
    {
        if(GameManager.Instance.DataManager.GetSetting(TypeSetting.Haptic))
        {
            Vibrator.Vibrate(timeVibrate);
        }
    }
}
