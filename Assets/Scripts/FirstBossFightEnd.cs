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

    public void UpdateBossesList(GelatinousCube[] newBosses, bool add)
    {
        if (newBosses != null && newBosses.Length > 0)
        {
            if (add)
            {
                foreach (GelatinousCube cube in newBosses)
                {
                    if (!bosses.Contains(cube))
                    {
                        bosses.Add(cube);
                    }
                }
            }
            else
            {
                foreach (GelatinousCube cube in newBosses)
                {
                    if (bosses.Contains(cube))
                    {
                        bosses.Remove(cube);
                    }
                }
            }
        }

        if (transform.childCount == 0)
            fightEndEvent.Invoke();
    }
}
