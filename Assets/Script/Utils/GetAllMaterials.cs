// ----------------------------------------------------------------------------
// <author>MaZiJun</author>
// <date>24/04/2020</date>
// ----------------------------------------------------------------------------

namespace Assets.Utils
{
    using System.Collections.Generic;

    using UnityEngine;

    public partial class Utils
    {
        public static Material[] GetAllMaterials(GameObject go)
        {
            var mats = new List<Material>();
            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                mats.Add(renderer.material);
            }

            //向下沿展
            foreach (Transform t in go.transform)
            {
                mats.AddRange(GetAllMaterials(t.gameObject));
            }

            return mats.ToArray();
        }
        
    }
}