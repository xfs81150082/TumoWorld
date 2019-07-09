﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class AoiGrid : Component
    {
        public long gridId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int minX { get; set; }
        public int maxX { get; set; }
        public int minY { get; set; }
        public int maxY { get; set; }
        public HashSet<long> seeGrids = new HashSet<long>();
        public HashSet<long> players = new HashSet<long>();
        public HashSet<long> enemys = new HashSet<long>();
        public HashSet<long> npcers = new HashSet<long>();
        public AoiGrid() {   }
        public AoiGrid(long id,int x ,int y)
        {
            this.gridId = id;
            this.X = x;
            this.Y = y;
        }
        
    }
}