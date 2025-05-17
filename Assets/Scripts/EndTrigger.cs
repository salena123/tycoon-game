using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject chest;
    private Animator chestAnimator;
    public Rigidbody ChestLock;
    void Start()
    {
        if (chest != null)
        {
            chestAnimator = chest.GetComponent<Animator>();

            if (PlayerPrefs.GetInt("ChestOpened", 0) == 1)
            {
                chestAnimator.Play("chestOpen");
                ChestLock.isKinematic = false;
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("Chest GameObject не назначен в инспекторе!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && chestAnimator != null)
        {
            chestAnimator.SetTrigger("Open");
            ChestLock.isKinematic = false;
            PlayerPrefs.SetInt("ChestOpened", 1);
            PlayerPrefs.Save();
            gameObject.gameObject.SetActive(false);
        }
    }
}
