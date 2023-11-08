using Mirror;
using Mirror.Examples.MultipleMatch;
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
        if(LocalPlayerData.isOwner == true)
        {
            StartTimer(LocalPlayerData.gametimer);
        }
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
                int minutes = (int)timer / 60;
                int seconds = (int)timer % 60;
                timerText.text = minutes.ToString() + ":" + ((seconds < 10) ? ("0") : ("")) + seconds.ToString();
                //timerText.text = timer.ToString("F1");
                if (timer <= 0)
                {
                    timer = 0;
                    isCounting = false;
                }
            }else if (isCounting == false)
            {

            }
        }
        
    }

    [Command(requiresAuthority = false)]
    public void StartTimer(float startTime)
    {
        timer = startTime;
        //isCounting = true;
        //hasStarted = true;
    }

    private void OnTimerChanged(float oldTimer, float newTimer)
    {
        // Handle UI updates or other actions when the timer value changes
    }
}
