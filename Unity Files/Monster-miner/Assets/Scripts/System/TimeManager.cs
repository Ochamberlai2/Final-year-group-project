using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : SingletonClass<TimeManager> {


    [SerializeField]
    private float DaysPerHour;

    private float timeDelta;

    static GameTime IngameTimeDisplay;

    [SerializeField]
    private Text dateText;

    public override void Awake()
    {
        base.Awake();
        IngameTimeDisplay = new GameTime();
        //the amount of ingame minutes per second to inccrease the minute counter by
        //the amount of in game days to pass per real time hour multiplied by the amount of hours in the day, divided by the amount of minutes in an hour
        timeDelta = (( DaysPerHour * 24) / 60);
        Debug.Log(timeDelta);
    }
    public void Start()
    {
        IngameTimeDisplay.Date = new Vector3(1, 1, 1492);
       StartCoroutine("PassTime");
       // StartCoroutine(PassTime());
    }
    
    public static GameTime IngameTime
    {
        get
        {
            return IngameTimeDisplay;
        }
    }
    public float DeltaTime
    {
        get
        {
            return timeDelta;
        }
    }

    private IEnumerator PassTime()
    {
        yield return null;
        while(true)
        {
        //pass one second
        yield return new WaitForSeconds(0f);
            IngameTimeDisplay.minutes += timeDelta *Time.deltaTime;
            IngameTimeDisplay.timeSinceStart += timeDelta * Time.deltaTime;

            //if the minutes counter has passed 60
            if (IngameTimeDisplay.minutes >= 60)
            {
                //find the delta
                float timeOverMinute = IngameTimeDisplay.minutes - 60;
                IngameTimeDisplay.hours += 1;
                //revert the minutes counter to whatever amount exceeded 60
                IngameTimeDisplay.minutes = timeOverMinute;
                //if the amount of hours gets to one day
                if (IngameTimeDisplay.hours >= 24)
                {
                    IngameTimeDisplay.Date.x += 1;
                    IngameTimeDisplay.hours = 0;
                    //using a lunar month of 28 days for simplicity's sake
                    if (IngameTimeDisplay.Date.x >= 28)
                    {

                        IngameTimeDisplay.Date.y += 1;
                        IngameTimeDisplay.Date.x = 1;
                        //if we get to 1 year
                        if (IngameTimeDisplay.Date.y >= 12)
                        {
                            IngameTimeDisplay.Date.z += 1;
                            IngameTimeDisplay.Date.y = 1;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        string timeOfDay = (IngameTime.hours > 12) ? " pm" : " am";
        dateText.text = IngameTime.hours + " : " + Mathf.RoundToInt(IngameTime.minutes) + timeOfDay + "\n " + IngameTime.Date.x + ", " + IngameTime.Date.y + ", " + IngameTime.Date.z;
    }

}

public struct GameTime
{
    public Vector3 Date;
    public float minutes;
    public int hours;
    public double timeSinceStart;
}
