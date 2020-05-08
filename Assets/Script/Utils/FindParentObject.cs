namespace Assets.Utils
{
    using UnityEngine;

    public partial class Utils : MonoBehaviour
    {
        public static GameObject FindTaggedParent(GameObject go)
        {
            while (true)
            {
                if (go.tag != "Untagged")
                {
                    return go;
                }

                if (go.transform.parent == null)
                {
                    return null;
                }

                go = go.transform.parent.gameObject;
            }
        }

        public static GameObject FindTaggedParent(Transform t)
        {
            return FindTaggedParent(t.gameObject);
        }
    

    }
}
