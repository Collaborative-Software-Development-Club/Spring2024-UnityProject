using UnityEngine;
using UnityEditor;

public class RandomObjectPlacementMods : MonoBehaviour
{

    [MenuItem("Window/Infinity PBR/Rotate Random Y #%r")]
    static void RotateRandomY()
    {

        foreach (GameObject o in Selection.gameObjects)
        {
            if (o.GetComponent<Transform>())
                RandomTransform(o.GetComponent<Transform>());
        }
    }
    
    [MenuItem("Window/Infinity PBR/Nudge X & Z #%e")]
    static void NudgeXZ()
    {

        foreach (GameObject o in Selection.gameObjects)
        {
            if (o.GetComponent<Transform>())
                NudgeTransform(o.GetComponent<Transform>());
        }
    }

    static void RandomTransform(Transform o)
    {
        Vector3 tempAngles = o.eulerAngles;
        tempAngles.y = Random.Range(-360f, 360f);
        o.eulerAngles = tempAngles;
    }

    static void NudgeTransform(Transform o)
    {
        Vector3 tempPos = o.position;
        tempPos.x += Random.Range(-0.1f, 0.1f);
        tempPos.z += Random.Range(-0.1f, 0.1f);
        o.position = tempPos;
    }
}
