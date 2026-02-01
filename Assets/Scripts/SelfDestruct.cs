using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private float selfDestructDelay;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    void Start()
    {
        Invoke("selfDestruct", selfDestructDelay);
        if(source != null && clip != null)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }
}
