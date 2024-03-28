using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    public Transform[] pathList;



    public List<Transform> GetNodesInPath(int pathInt)
    {
        List<Transform> nodes = new List<Transform>();
        //Debug.Log(pathInt);
        Transform path = pathList[pathInt];

        foreach (Transform node in path)
        {
            nodes.Add(node);
        }

        return nodes;
    }

    public int GetRandomPathInt()
    {
        int randomPathInt = UnityEngine.Random.Range(0, pathList.Length);
        return randomPathInt;
    }

    public Transform GetSpawningPointFromPath(int pathInt)
    {
        Transform path = pathList[pathInt];
        Transform startPoint = path.GetChild(0);
        return startPoint;
    }
}
