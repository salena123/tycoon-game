using UnityEngine;
using YG;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class AdsYGPlugin : MonoBehaviour
{
    private static AdsYGPlugin _instance;

    public static AdsYGPlugin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AdsYGPlugin>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("AdsYGPlugin");
                    _instance = obj.AddComponent<AdsYGPlugin>();
                }
            }
            return _instance;
        }
    }

    private int rewardMoney;
    private const int rewardId = 1;
    private bool isAdCooldown = false;

    public GameObject noAd;
    public float adCooldownTime = 60f;
    private float remainingCooldown;

    public TMP_Text cooldownText;
    public bool IsAdAvailable => !isAdCooldown;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += OnRewarded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnRewarded;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedBindAdTrigger());
    }

    public void ShowRewardedAd(int money)
    {
        if (isAdCooldown)
        {
            noAd.SetActive(true);
            return;
        }

        if (!YandexGame.SDKEnabled)
        {
            Debug.Log("Yandex SDK не инициализирован. Реклама не будет показана.");
            return;
        }

        rewardMoney = money;

        try
        {
            YandexGame.RewVideoShow(rewardId);
            Debug.Log("Попытка показать рекламу.");
            StartCoroutine(AdCooldownRoutine());
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при попытке показать рекламу: {e.Message}");
        }
    }

    private void OnRewarded(int id)
    {
        if (id == rewardId)
        {
            MoneyManager.AddMoney(rewardMoney);
            Debug.Log($"Реклама просмотрена. Выдано {rewardMoney} монет.");
        }
    }

    private IEnumerator AdCooldownRoutine()
    {
        isAdCooldown = true;
        remainingCooldown = adCooldownTime;

        while (remainingCooldown > 0)
        {
            if (cooldownText != null)
            {
                cooldownText.text = $"Реклама будет доступна через {Mathf.CeilToInt(remainingCooldown)} сек.";
            }

            yield return new WaitForSecondsRealtime(1f);
            remainingCooldown -= 1f;
        }

        isAdCooldown = false;
        if (cooldownText != null)
        {
            cooldownText.text = "Реклама доступна!";
        }
    }

    public void SetCooldown(float cooldown)
    {
        remainingCooldown = cooldown;
        if (remainingCooldown > 0)
        {
            StartCoroutine(AdCooldownRoutine());
        }
    }

    public float GetRemainingCooldown()
    {
        return remainingCooldown;
    }

    public void SetUI(GameObject noAdPanelObj, TMP_Text cooldownTextObj)
    {
        noAd = noAdPanelObj;
        cooldownText = cooldownTextObj;
    }

    private void Start()
    {
        //StartCoroutine(DelayedBindAdTrigger());
    }



    private IEnumerator DelayedBindAdTrigger()
    {
        yield return new WaitForSeconds(1.9f);

        if (noAd == null)
        {
            GameObject foundPanel = GameObject.Find("noAd");
            if (foundPanel != null)
                noAd = foundPanel;
        }

        if (cooldownText == null)
        {
            GameObject foundText = GameObject.Find("cooldownText");
            if (foundText != null)
                cooldownText = foundText.GetComponent<TMP_Text>();
        }

        SetUI(noAd, cooldownText);

        AdTrigger trigger = FindObjectOfType<AdTrigger>();

        StartCoroutine(AddTrigger(trigger));
    }

    private IEnumerator AddTrigger (AdTrigger trigger)
    {
        yield return new WaitForSeconds(1f);

        if (trigger != null)
        {
            trigger.adManager = this;

            if (trigger.noAd == null && noAd != null)
            {
                trigger.noAd = noAd;
            }

            Debug.Log("AdTrigger успешно привязан к AdsYGPlugin.");
        }
        else
        {
            Debug.LogWarning("AdTrigger не найден на сцене.");
        }
    }

}
