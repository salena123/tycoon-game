using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class SimpleAlembicLooper : MonoBehaviour
{
    [Header("Alembic Player")]
    public AlembicStreamPlayer player;

    [Header("Playback Settings")]
    public bool playOnStart = true;
    public bool loop = true;
    public bool isPlaying = true;
    public float speed = 1.0f;

    private float time = 0f;

    private void Start()
    {
        time = 0f;
        player.UpdateImmediately(time);

        if (!playOnStart)
            isPlaying = false;
    }

    private void Update()
    {
        if (!isPlaying || player == null || player.Duration <= 0f)
            return;

        time += Time.deltaTime * speed;

        if (loop && time > player.Duration)
            time %= player.Duration; // вместо обнулени€ сохран€ем плавность цикла

        time = Mathf.Clamp(time, 0f, player.Duration);
        player.UpdateImmediately(time);
    }

    public void Play() => isPlaying = true;
    public void Pause() => isPlaying = false;
    public void ResetPlayback()
    {
        time = 0f;
        player.UpdateImmediately(time);
    }
}
