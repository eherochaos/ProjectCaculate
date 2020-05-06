using System.Collections;
using System.Collections.Generic;

using Assets.Script.Class;

using UnityEngine;

public class EquipmentConfig:LevelSystem
{
    public string Name;

    public int BattlePower => this.BattlePowerBase + this.BattlePowerAdd * (this.Level - 1);

    public int BattlePowerBase;

    public int BattlePowerAdd;

    public override void LevelUp()
    {
        base.LevelUp();

        //装备数据刷新通知
        this.gameObject.SendMessage(MessageCategory.EquipmentUpdate);
    }
}

