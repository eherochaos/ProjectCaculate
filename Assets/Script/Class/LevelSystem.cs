// ----------------------------------------------------------------------------
// <author>MaZiJun</author>
// <date>04/05/2020</date>
// ----------------------------------------------------------------------------

namespace Assets.Script.Class
{
    using UnityEngine;

    public class LevelSystem :MonoBehaviour
    {
        public int Level;

        public int CurrentExp;

        public int LevelMax => this.ExpCurve.Length + 1;

        public int[] ExpCurve;

        public virtual void LevelUp()
        {
            this.Level += 1;
            this.CurrentExp = 0;
        }

    }
    public static class LevelSystemHelper
    {
        /// <summary>
        /// 增加目标经验值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="increment"></param>
        public static void AddExp(this LevelSystem target, int increment)
        {
            var maxLevel = target.LevelMax;
            var fromLv = target.Level;
            while (increment > 0)
            {
                var currentLv = target.Level;
                if (currentLv == maxLevel)
                {
                    break;
                }

                var upgradeExp = target.ExpCurve[currentLv];
                var dExp = Mathf.Min(upgradeExp - target.CurrentExp, increment);
                increment -= dExp;
                target.CurrentExp += dExp;
                if (target.CurrentExp >= upgradeExp)
                {
                    target.LevelUp();
                }
            }
        }
    }
}