using UnityEngine;

public class TimeTest : MonoBehaviour
{
    public float secound = 10;

    void Update()
    {
        Timing();
    }

    private float nextTime = 1;
    private void Timing()
    {
        if (secound <= 0) return;
        if (Time.time >= nextTime)
        {
            secound -= 1;
            Debug.Log(secound);
            nextTime = Time.time + 1;
        }
    }
}