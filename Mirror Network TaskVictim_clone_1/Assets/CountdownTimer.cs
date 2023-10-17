using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTimerChanged))]
    public float timer = 60.0f; // Waktu countdown awal

    [SyncVar(hook = nameof(OnScoreTargetChanged))] public float scoreTarget = 5f;

    private bool isCounting = true;

    public TextMeshProUGUI timerText;

    public bool hasStarted = false;

    public GameObject winnertext;

    public ScoreBoardNew ScoreBoardNew;

    public GameObject lobby;
    public GameObject ReadyButton;

    public InputField scoreTargetField;

    public TextMeshProUGUI targetText;
    
    public GameObject targettt;
    public GameObject goall;
    public GameObject addButton;
    public GameObject sumButton;

    public GameObject fadeBlack;


    private void Start()
    {
        ScoreBoardNew = GameObject.FindWithTag("canvas").GetComponent<ScoreBoardNew>();
    }
    // Update is called once per frame
    void Update()
    {
        if (targetText.enabled == true)
        {
            targetText.text = scoreTarget.ToString("F1");
        }

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

            }else if (isCounting == false)
            {
                timerText.text = "Times Up!";
                fadeBlack.SetActive(true);
                summaryMuncul();
            }
        }

        //Debug.Log(ScoreBoardNew.playerCountTotal);
        if(ScoreBoardNew.playerReadyTotal > 0 && ScoreBoardNew.playerReadyTotal >= ScoreBoardNew.playerCountTotal)
        {
            hasStarted = true;
            if (lobby != null)
            {
                //hancurkanLobby();
                lobby.SetActive(false);
                targettt.SetActive(false);
                goall.SetActive(false);
                addButton.SetActive(false);
                sumButton.SetActive(false);
            }
            else
            {
                Debug.Log("do nothing");
            }

            if (ReadyButton != null)
            {
                ReadyButton.SetActive(false);
            }
            else
            {
                Debug.Log("do nothing");
            }

        }

        //timerText.text = timer.ToString("F1");
    }

    [Command(requiresAuthority = false)]
    void hancurkanLobby()
    {
        //score += 1f;
        NetworkServer.Destroy(lobby);
        
        
        //NetworkServer.Destroy(ReadyButton);
    }

    [Command(requiresAuthority = false)]
    void hancurkanTombolLobby()
    {
        //score += 1f;
        //NetworkServer.Destroy(lobby);
        
        NetworkServer.Destroy(ReadyButton);
        
        
    }

    [Command(requiresAuthority = false)]
    public void endTimer()
    {
        timer = 0;
    }

    [Command(requiresAuthority = false)]
    void summaryMuncul()
    {
        GetComponent<Animator>().Play("summaryappear");

    }

    public void setTargetScore()
    {
        scoreTarget = float.Parse(scoreTargetField.text);
    }

    public void StartTimer(float startTime)
    {
        timer = startTime;
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    private void OnTimerChanged(float oldTimer, float newTimer)
    {
        // Handle UI updates or other actions when the timer value changes
    }

    private void OnScoreTargetChanged(float oldTarget, float newTarget)
    {

    }
}
