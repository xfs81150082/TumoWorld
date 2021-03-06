﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class SeekComponent : Component
    {
        public bool isSee = false;                                           //是否在追击敌人
        public Unit target;                                                  //选定的目标，追击或攻击的目标必定是选定的目标
        public float targetDistance { get; set; } = float.PositiveInfinity;
        public float enterSeeDis { get; set; } = 225.0f;

        //public bool startNull = false;
        public long delTime = 0;
        public long seeTimer = 0;
        public long resTime = 40;
        public long speed = 2;

        public bool isSeePath = false;                                        //表示是否有追击路径
        public Vector3 seePoint;
        public Vector3 goalPosition { get; set; } = Vector3.zero;             //最后一次目标坐标准保存下来
        public See_Map seeMap { get; set; }
    }
}