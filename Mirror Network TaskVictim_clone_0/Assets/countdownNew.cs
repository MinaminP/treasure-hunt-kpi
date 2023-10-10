using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class countdownNew : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTimerChanged))]
    public float timer = 60.0f; // Waktu countdown awal

    private bool isCounting = true;
    public bool hasStarted = false;

    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (isCounting)
            {
                //scoreTarget -= Time.deltaTime;

                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F1");
                if (timer <= 0)
                {
                    timer = 0;
                    isCounting = false;
                }

            }
        }
        
    }

    private void OnTimerChanged(float oldTimer, float newTimer)
    {
        // Handle UI updates or other actions when the timer value changes
    }
}
