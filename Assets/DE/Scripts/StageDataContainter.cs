using System;
using System.Collections.Generic;
using Enums;
namespace GameData
{
    public class StageDataContainter
    {
        [Serializable]
        public class StageList
        {
            public List<StageData> Stages = new List<StageData>();
        }
    
        [Serializable]
        public class StageData
        {
            public float finalTotalScore;
            public RankType finlaRankType;
            public float finalTotalTime;
        }
    }

}
