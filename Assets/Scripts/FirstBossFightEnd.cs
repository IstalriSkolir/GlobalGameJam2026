using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstBossFightEnd : MonoBehaviour
{
    [SerializeField]
    private UnityEvent fightEndEvent;
    [SerializeField]
    private List<GelatinousCube> bosses;
    [SerializeField]
    private MusicManager music;

    private void Start()
    {
        //music = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    public void ClearBossesList()
    {
        foreach (GelatinousCube cube in bosses)
            Destroy(cube.gameObject);
        bosses.Clear();
    }

    public void UpdateBossesList(GelatinousCube newBoss, bool add)
    {
        if (add && !bosses.Contains(newBoss)) bosses.Add(newBoss);
        else if (!add && bosses.Contains(newBoss)) bosses.Remove(newBoss);
        Invoke("checkIfAnyCubesLeft", 1f);
    }

    private void checkIfAnyCubesLeft()
    {
        if (bosses.Count == 0)
        {
            GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().UpdateMusicMode(MusicMode.Alter);
            fightEndEvent.Invoke();
        }
    }
}
