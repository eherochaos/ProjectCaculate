namespace Assets.Utils
{
    using UnityEngine;

    public enum BoundsTest
    {
        Center,
        OnScreen,
        OffScreen,
    }
    public partial class Utils : MonoBehaviour
    {
        public static Bounds BoundsUnion(Bounds b0, Bounds b1)
        {
            if (b0.size == Vector3.zero && b1.size != Vector3.zero)
            {
                return (b1);
            }

            if (b1.size == Vector3.zero)
            {
                return (b0);
            }



            b0.Encapsulate(b1.min);
            b0.Encapsulate(b1.max);
            return b0;
        }

        public static Bounds CombineBoundsOfChildren(GameObject go)
        {
            Bounds b = new Bounds(Vector3.zero, Vector3.zero);
            if (go.GetComponent<Renderer>() != null)
            {
                b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);

            }

            if (go.GetComponent<Collider>() != null)
            {
                b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
            }

            foreach (Transform t in go.transform)
            {
                b = BoundsUnion(b, CombineBoundsOfChildren((t.gameObject)));
            }

            return b;
        }

        private static Bounds _camBounds;
        public static Bounds CamBounds
        {
            get
            {
                if (_camBounds.size == Vector3.zero)
                {
                    SetCameraBounds();
                }

                return _camBounds;
            }
        }

        public static void SetCameraBounds(Camera cam = null)
        {
            if(cam == null) cam = Camera.main;

            var topLeft = new Vector3(0,0,0);
            var bottomRight = new Vector3(Screen.width, Screen.height,0);

            var boundTln = cam.ScreenToWorldPoint(topLeft);
            var boundBrf = cam.ScreenToWorldPoint(bottomRight);

            boundTln.z += cam.nearClipPlane;
            boundBrf.z += cam.farClipPlane;

            var center = (boundTln + boundBrf) / 2f;
            _camBounds = new Bounds(center,Vector3.zero);
            _camBounds.Encapsulate(boundTln);
            _camBounds.Encapsulate(boundBrf);
        }

        public static Vector3 ScreenBoundsCheck(Bounds bnd, BoundsTest test = BoundsTest.Center)
        {
            return BoundsInBoundsCheck(CamBounds, bnd, test);
        }

        public static Vector3 BoundsInBoundsCheck(Bounds bigB, Bounds lilB, BoundsTest test = BoundsTest.OnScreen)
        {
            var pos = lilB.center;

            var off = Vector3.zero;

            switch (test)
            {
                case BoundsTest.Center:
                    if(bigB.Contains(pos)) return Vector3.zero;
                    off.x = CheckForOff(pos.x, bigB.max.x, bigB.min.x);
                    off.y = CheckForOff(pos.y, bigB.max.y, bigB.min.y);
                    off.z = CheckForOff(pos.z, bigB.max.z, bigB.min.z);
                    return off;

                case BoundsTest.OnScreen:
                    if(bigB.Contains(lilB.min) && bigB.Contains(lilB.max)) return Vector3.zero;
                    off.x = CheckForOff(lilB.max.x, lilB.min.x, bigB.max.x, bigB.min.x);
                    off.y = CheckForOff(lilB.max.y, lilB.min.y, bigB.max.y, bigB.min.y);
                    off.z = CheckForOff(lilB.max.z, lilB.min.z, bigB.max.z, bigB.min.z);
                    return off;

                case BoundsTest.OffScreen:
                    bool cMin = bigB.Contains(lilB.min);
                    bool cMax = bigB.Contains(lilB.max);
                    if(cMin|| cMax) return Vector3.zero;;
                    off.x = CheckForOff(lilB.min.x, lilB.max.x, bigB.max.x, bigB.min.x);
                    off.y = CheckForOff(lilB.min.y, lilB.max.y, bigB.max.y, bigB.min.y);
                    off.z = CheckForOff(lilB.min.z, lilB.max.z, bigB.max.z, bigB.min.z);
                    return off;
            }
            return Vector3.zero;
        }

        private static float CheckForOff(float posX, float maxX, float minX)
        {
            if (posX > maxX) return posX - maxX;
            if (posX < minX) return posX - minX;
            return 0;
        }

        private static float CheckForOff(float lMaxX,float lMinX, float maxX, float minX)
        {
            if (lMaxX > maxX) return lMaxX - maxX;
            if (lMinX < minX) return lMinX - minX;
            return 0;
        }

    }
}