﻿using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AttackComponentHelper
    {
        /// <summary>
        /// 攻击 CD 计时
        /// </summary>
        /// <param name="self"></param>
        public static void TakeAttack(this UnitSkillComponent self)
        {
            if (self.GetParent<Unit>().GetComponent<RecoverComponent>().isDeath) return;

            GetAttackTarget(self);

            if (self.target != null)
            {
                //DeathSettlement(self.target);

                self.attackDistance = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

                if (self.attackDistance < self.cdDistance)
                {
                    if (!self.startNull)
                    {
                        self.startTime = TimeHelper.ClientNowSeconds();
                        self.startNull = true;
                    }

                    long timeNow = TimeHelper.ClientNowSeconds();

                    if ((timeNow - self.startTime) > self.attcdTime)
                    {
                        self.TakeDamage();

                        self.startNull = false;
                    }
                }
                else
                {
                    if (self.startNull)
                    {
                        self.startTime = 0;
                        self.startNull = false;
                    }
                }
            }           
        }
   
        /// <summary>
        /// 得到 单目标 敌人
        /// </summary>
        /// <param name="unit"></param>
        static void GetAttackTarget(this UnitSkillComponent self)
        {
            Unit unit = self.GetParent<Unit>();

            if (unit.GetComponent<RayUnitComponent>() != null)
            {
                if (unit.GetComponent<RayUnitComponent>().target != null)
                {
                    unit.GetComponent<UnitSkillComponent>().target = unit.GetComponent<RayUnitComponent>().target;
                }
                else
                {
                    ///正前方，5 米内，最近小怪
                }
            }
            else
            {
                if (unit.GetComponent<SeeComponent>() != null && unit.GetComponent<SeeComponent>().target != null)
                {
                    unit.GetComponent<UnitSkillComponent>().target = unit.GetComponent<SeeComponent>().target;
                }
            }
        }

        /// <summary>
        /// 单目标 普通攻击 减伤 TakeDamage
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        public static void TakeDamage(this UnitSkillComponent self)
        {
            Unit my = self.GetParent<Unit>();
            Unit target = self.target;
            if (self.target == null) return;
            if (self.GetParent<Unit>().GetComponent<RecoverComponent>().isDeath) return;
            if (self.target.GetComponent<RecoverComponent>().isDeath) return;

            UnitSkillComponent attack = target.GetComponent<UnitSkillComponent>();
            if (!attack.attackers.Contains(my.Id))
            {
                attack.attackers.Add(my.Id);
            }

            NumericComponent numTarget = target.GetComponent<NumericComponent>();
            NumericComponent numSelf = my.GetComponent<NumericComponent>();
            Random random = new Random();
            int dom = random.Next(0, 99);
            int domhp = 0;
            if (dom < 26)
            {
                domhp = numSelf[NumericType.Attack] * 2;
                numTarget[NumericType.HpAdd] -= domhp; 
            }
            else
            {
                domhp = numSelf[NumericType.Attack];
                numTarget[NumericType.HpAdd] -= domhp;
            }

            Console.WriteLine(" TakeDamage: " + "-" + domhp + " / " + numTarget[NumericType.Hp] + " / " + target.UnitType);
        }
        
        public static void UpdateBuff(this UnitSkillComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            SkillItem[] skillItems = self.GetBuffs();
           


        }
        public static SkillItem[] GetBuffs(this UnitSkillComponent self)
        {
            HashSet<SkillItem> skillItems = new HashSet<SkillItem>();
            foreach (long tem in self.idBuffs.Keys.ToArray())
            {
                int level;
                self.idBuffs.TryGetValue(tem, out level);
                SkillItem skillItem = Game.Scene.GetComponent<SkillComponent>().Get(tem, level);
                skillItems.Add(skillItem);
            }
            return skillItems.ToArray();
        }



    }
}
