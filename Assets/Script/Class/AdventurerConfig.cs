// ----------------------------------------------------------------------------
// <author>MaZiJun</author>
// <date>04/05/2020</date>
// ----------------------------------------------------------------------------

namespace Assets.Script.Class
{
    using UnityEngine;

    public class AdventurerConfig:LevelSystem
    {
        public int BattlePower => this.BattlePowerBase + this.BattlePowerAdd * (this.Level - 1) + this.BattlePowerEquipment;

        public int BattlePowerEquipment;

        private EquipmentConfig equipmentConfig;

        //==============配置数据==================//

        public string Name;
        
        public int BattlePowerBase;

        public int BattlePowerAdd;

        public EquipmentConfig SetEquipment
        {
            get => this.equipmentConfig;
            set
            {
                //此处应该从List取得对应装备
                this.equipmentConfig = value;
                this.BattlePowerEquipment = this.equipmentConfig.BattlePower;
            }
        }

        public void EquipmentUpdate()
        {
            this.BattlePowerEquipment = this.equipmentConfig.BattlePower;
        }
    }
}