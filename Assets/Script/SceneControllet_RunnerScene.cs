using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllet_RunnerScene : MonoBehaviour {
    public bool start = false;
    private bool sleep = true;
    private UIElement uiElement;
    private float time = 0;

    private float timerStart = 1f;

    private float timeSleep = 5f;

    [SerializeField]
    private GameObject Win;

    ShipController shipController;
    JointOverlayer jointOverlayer;
    KeyboardController keyboardController;
    [SerializeField]
    private CamMove camMove;

    [SerializeField]
    private Text scoreWin;
    [SerializeField]
    private Text endText;

    private bool finishFlag = false;
    void Awake()
    {
        uiElement = GetComponent<UIElement>();
        shipController = GetComponent<ShipController>();
        //jointOverlayer = GetComponent<JointOverlayer>();
        keyboardController = GetComponent<KeyboardController>();
    }

	// Use this for initialization
	void Start () {
        uiElement.nameVision();

    }

    // Update is called once per frame
    void Update() {
        if (!finishFlag && start)
        {
                time += Time.deltaTime;
                uiElement.timeVision(((float)(int)(time * 100)) / 100);
        }

    }

    public void finish()
    {
        float t = ((float)(int)(time * 100)) / 100;
        int pos = RunnerInfo.addScore(RunnerInfo.name, t);
        scoreWin.text = pos + ": " + RunnerInfo.name + " " + t + "s";
        if (pos == 1)
        {
            endText.text = "Новый рекорд!";
        } else if (pos <= 3) {
            endText.text = "В тройке лидеров!";
        } else
        {
            endText.text = "Ваш счёт:";
        }
        RunnerInfo.saveHS();
        finishFlag = true;
        Win.SetActive(true);
    }

    public void startRunner()
    {
        camMove.start = true;
        uiElement.deactivateStartMenu();
        start = true;
        //jointOverlayer.startRunner();
        shipController.startRunner();
        keyboardController.startRunner();
    }

    private void timerSleep()
    {
        timeSleep -= Time.deltaTime;
        //Debug.Log(timerStart);
        if (sleep && timeSleep < 0)
        {

            shipController.startRunner();
            sleep = false;
        }
    }

    public void timerStart_Restart()
    {
        timerStart = 1f;
    }

    public void timerStart_Counter()
    {
        timerStart -= Time.deltaTime;
        //Debug.Log(timerStart);
        if(timerStart < 0)
        {
            startRunner();
        }
    }
}
