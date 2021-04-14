using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerPool : MonoBehaviour
{
    public static DaggerPool daggerPoolInstance;

    public GameObject pooledDagger;
    bool poolStatus = true;

    List<GameObject> daggers;

    private void Awake()
    {
        daggerPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        daggers = new List<GameObject>();
    }


    public GameObject GetDagger()
    {
        // Check if any daggers are already in the scene.
        if (daggers.Count > 0)
        {
            // look for daggers that are currently inactive.
            for (int dagger = 0; dagger < daggers.Count; dagger++)
            {
                if (!daggers[dagger].activeInHierarchy)
                {
                    return daggers[dagger];
                }
            }
        }

        // if pool status is empty (no daggers in scene), then instantiate them
        if (poolStatus)
        {
            GameObject dag = Instantiate(pooledDagger);
            dag.transform.parent = transform;
            dag.SetActive(false);
            daggers.Add(dag);
            return dag;
        }

        return null;
    }
}
