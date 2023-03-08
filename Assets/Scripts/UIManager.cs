using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text restoreTimeLabel;
    public TMP_Text fragCountLabel;
    public GameObject[] ballIcons;

    private void Start()
    {
        CubeSpawner.OnFragCountChange += UpdateFragCount;
        BallCannon.OnRestoreTimeChange += UpdateRestoreTime;
        BallCannon.OnBallChange += UpdateBallCount;
    }

    private void UpdateFragCount(int fragCount)
    {
        fragCountLabel.text = fragCount.ToString();
    }

    private void UpdateRestoreTime(float time)
    {
        restoreTimeLabel.text = ((int)(time*10)).ToString();
    }

    private void UpdateBallCount(int ballCount)
    {
        for (int i = 0; i < ballCount; i++)
        {
            ballIcons[i].SetActive(true);
        }
        for (int i = ballCount; i < ballIcons.Length; i++)
        {
            ballIcons[i].SetActive(false);
        }
    }
}
