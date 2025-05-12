using UnityEngine;

public class AdTrigger : MonoBehaviour
{
    public GameObject adPanel;
    public AdsYGPlugin adManager;
    public int rewardMoney = 20;
    public GameObject noAd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (adManager.IsAdAvailable)
            {
                adPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                noAd.SetActive(true);
            }
        }
    }

    public void PressNo()
    {
        Time.timeScale = 1;
    }

    public void PressYes()
    {
        adManager.ShowRewardedAd(rewardMoney);
        Time.timeScale = 1;
    }
}
