﻿using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class PatrolComponentHelper 
    {
        #region 行为树模式
        /// <summary>
        /// 小怪巡逻
        /// </summary>
        /// <param name="self"></param>
        public static void UpdatePatrol(this PatrolComponent self)
        {          
            if (self.isIdle)
            {
                //开始休息 4秒
                if (!self.startNull)
                {
                    self.startTime = TimeHelper.ClientNowSeconds();
                    self.startNull = true;
                }
                long timeNow = TimeHelper.ClientNowSeconds();                
                if ((timeNow - self.startTime) > self.idleResTime)
                {
                    self.startNull = false;
                    self.isStartWalk = true;
                    self.isIdle = false;
                }
            }
            else
            {
                //发送坐标，开始行走，开始发送巡逻目标点坐标
                if (self.isStartWalk)
                {
                    self.SendPatrolPosition();
                    self.isStartWalk = false;
                }

                //如果到达目标点,开始休息,并计时4秒后 重置巡逻目标点
                float sqr = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.goalPoint);
                if (sqr < 0.1f)
                {
                    self.isStartWalk = true;
                    self.isIdle = true;
                    self.patolNull = false;
                }                

                //如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点
                if (!self.patolNull)
                {
                    self.patolTimer = TimeHelper.ClientNowSeconds();
                    self.patolNull = true;
                }
                long timeNow = TimeHelper.ClientNowSeconds();
                if ((timeNow - self.patolTimer) > self.lifeCdTime)
                {
                    self.patolNull = false;
                    self.isStartWalk = true;
                }

            }
        }

        /// <summary>
        /// 发送巡逻目标点坐标 消息
        /// </summary>
        /// <param name="self"></param>
        static void SendPatrolPosition(this PatrolComponent self)
        {
            /// 休息时间到 开始发送巡逻目标点坐标 消息
            self.patrolMap = self.GetPatrolMap();
            self.goalPoint = self.patrolPoint;
            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.GetParent<Unit>().Id);
            actorLocationSender.Send(self.patrolMap);
        }

        static Patrol_Map GetPatrolMap(this PatrolComponent self)
        {
            self.patrolPoint = TargetPositon(self);
            Patrol_Map patrol_Map = new Patrol_Map() { Id = self.GetParent<Unit>().Id, X = self.patrolPoint.x, Y = self.patrolPoint.y, Z = self.patrolPoint.z };
            return patrol_Map;
        }

        static Vector3 TargetPositon(this PatrolComponent self)
        {
            Random ran = new Random(self.coreRan);
            self.coreRan += 1;
            if (self.coreRan > (self.coreDis * 2 - 1))
            {
                self.coreRan = 0;
            }
            int h = ran.Next(0, self.coreDis * 2); 
            int v = ran.Next(0, self.coreDis * 2); 
            Vector3 offset = new Vector3(h - self.coreDis, 0, v - self.coreDis);
            offset.y = self.spawnPosition.y;
            Vector3 endVec = offset + self.spawnPosition;
            int limitXZ = Game.Scene.GetComponent<AoiGridComponent>().mapWide / 2 - 5;
            if (endVec.x < -limitXZ)
            {
                endVec.x = -limitXZ;
            }
            if (endVec.x > limitXZ)
            {
                endVec.x = limitXZ;
            }
            if (endVec.z < -limitXZ)
            {
                endVec.z = -limitXZ;
            }
            if (endVec.z > limitXZ)
            {
                endVec.z = limitXZ;
            }
            return endVec;
        }

        #endregion

        /// <summary>
        /// 小怪巡逻
        /// </summary>
        /// <param name="self"></param>
        //public static void UpdatePatrol(this PatrolComponent self)
        //{
        //    if (!self.isPatrol)
        //    {
        //        self.isStartWalk = true;
        //        self.isIdle = false;
        //        self.patolNull = false;
        //        self.startNull = false;
        //        return;
        //    }

        //    if (self.isIdle)
        //    {
        //        //开始休息 4秒
        //        if (!self.startNull)
        //        {
        //            self.startTime = TimeHelper.ClientNowSeconds();
        //            self.startNull = true;
        //        }
        //        long timeNow = TimeHelper.ClientNowSeconds();
        //        if ((timeNow - self.startTime) > self.idleResTime)
        //        {
        //            self.startNull = false;
        //            self.isStartWalk = true;
        //            self.isIdle = false;
        //        }
        //    }
        //    else
        //    {
        //        //发送坐标，开始行走，开始发送巡逻目标点坐标
        //        if (self.isStartWalk)
        //        {
        //            self.SendPatrolPosition();
        //            self.isStartWalk = false;
        //        }

        //        //如果到达目标点,开始休息,并计时4秒后 重置巡逻目标点
        //        float sqr = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.goalPoint);
        //        if (sqr < 0.1f)
        //        {
        //            self.isStartWalk = true;
        //            self.isIdle = true;
        //            self.patolNull = false;
        //        }

        //        //如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点
        //        if (!self.patolNull)
        //        {
        //            self.patolTimer = TimeHelper.ClientNowSeconds();
        //            self.patolNull = true;
        //        }
        //        long timeNow = TimeHelper.ClientNowSeconds();
        //        if ((timeNow - self.patolTimer) > self.lifeCdTime)
        //        {
        //            self.patolNull = false;
        //            self.isStartWalk = true;
        //        }

        //    }
        //}


    }
}
