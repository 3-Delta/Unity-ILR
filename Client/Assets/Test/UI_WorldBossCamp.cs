using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase { }

public class UIComponent { }

public class CSVBoss
{
    public uint campId;
}

public class CSVBossCamp { }

public class CSVBossStage { }

public class BossInfo
{
    public uint id;
    private CSVBoss _csv;

    public CSVBoss csv {
        get {
            if (_csv == null) {
                
            }
            return _csv;
        }
    }

    public bool unlocked;

    public BossInfo(uint id) {
        this.id = id;
    }
}

public class WorldBossCamp
{
    public uint id;
    private CSVBossCamp _csv;

    public CSVBossCamp csv {
        get {
            if (_csv == null) {
                
            }
            return _csv;
        }
    }
    public bool unlocked;
    
    public WorldBossCamp(uint id) {
        this.id = id;
    }
}

public class SysBoss
{
    public Dictionary<uint, BossInfo> bossDict = new Dictionary<uint, BossInfo>();
    public Dictionary<uint, WorldBossCamp> campDict = new Dictionary<uint, WorldBossCamp>();

    // 请求解锁boss
    public void ReqUnlockBoss(uint bossId) { }

    // 请求解锁阵营
    public void ReqUnlockCamp(uint bossId) { 
    }
    
    // 请求报名
    public void ReqSignUp() { 
    }

    public void RefreshCamp(IList<uint> campList) {
        campDict.Clear();
        // todo
    }
    public void RefreshBoss(IList<uint> bossList) {
        bossDict.Clear();
        // todo
    }
    public void AddCamp(uint campId) {
        if (!campDict.ContainsKey(campId)) {
            WorldBossCamp wbc = new WorldBossCamp(campId);
            campDict.Add(campId, wbc);
        }
    }
    public void AddBoss(uint bossId) {
        if (!bossDict.ContainsKey(bossId)) {
            BossInfo bi = new BossInfo(bossId);
            bossDict.Add(bossId, bi);
        }
    }
}

// 世界boss阵营
public class UI_WorldBossCamp : UIBase
{
    public class Tab : UIComponent
    {
        public uint campId;
        public WorldBossCamp camp;
        
        public void OnBtnClicked() {
            
        }

        public void Refresh(uint campId) {
            this.campId = campId;
        }
    }
}

// 世界boss介绍
public class UI_WorldBossDesc : UIBase
{
    
}
