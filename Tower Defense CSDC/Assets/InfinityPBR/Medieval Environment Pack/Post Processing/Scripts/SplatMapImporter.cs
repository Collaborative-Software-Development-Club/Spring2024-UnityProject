using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatMapImporter : MonoBehaviour
{
    public Terrain m_terrain;
    public Texture m_splatMap;

    public void ApplySplatMap(Texture splatMap)
    {
        if (m_terrain != null)
        {
            Texture oldSplat = m_terrain.terrainData.GetAlphamapTexture(0);
            Graphics.CopyTexture(splatMap, oldSplat);
            m_terrain.Flush();
        }
    }
}
