namespace Assets.Script.Class
{
    public class LevelConfig
    {
        public int BattlePower;

        public int Exp;

        public int Gold;
    }

    public static class LevelConfigHelper
    {
        /// <summary>
        /// 计算战斗结果
        /// </summary>
        /// <param name="levelConfig"></param>
        /// <returns></returns>
        public static bool GetResult(this LevelConfig levelConfig)
        {
            return false;
        }
    }
}


