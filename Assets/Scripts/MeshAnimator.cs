using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class MeshAnimator : MonoBehaviour
{
    public float frameRate = 30f;
    public bool loop = true;

    private Mesh[] meshes;
    private float timer = 0f;
    private int currentFrame = 0;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        meshes = Resources.LoadAll<Mesh>("Meshes");

        if (meshes == null || meshes.Length == 0)
        {
            Debug.LogError("MeshAnimator: Не найдено мешей в Resources/Meshes");
            enabled = false;
            return;
        }

        meshes = meshes.OrderBy(m =>
        {
            string digits = System.Text.RegularExpressions.Regex.Match(m.name, @"\d+").Value;
            return int.TryParse(digits, out int num) ? num : 0;
        }).ToArray();

        meshFilter.mesh = meshes[0];
    }

    void Update()
    {
        if (meshes == null || meshes.Length <= 1) return;

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame++;

            if (currentFrame >= meshes.Length)
            {
                if (loop) currentFrame = 0;
                else currentFrame = meshes.Length - 1;
            }

            meshFilter.mesh = meshes[currentFrame];
        }
    }
}
