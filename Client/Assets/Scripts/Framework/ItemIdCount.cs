using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdCount {
    public uint id { get; protected set; }
    public int count { get; protected set; }

    public virtual bool IsEnough(int targetCount) {
        return count >= targetCount;
    }

    public ItemIdCount() { }
    public ItemIdCount(uint id, int count) {
        Reset(id, count);
    }
    public ItemIdCount Reset(uint id, int count) {
        this.id = id;
        this.count = count;
        return this;
    }
}

public class ItemGuidCount : ItemIdCount {
    public uint guid { get; protected set; }
    
    public ItemGuidCount() { }
    public ItemGuidCount(ulong guid, uint id, int count) {
        Reset(guid, id, count);
    }
    public ItemGuidCount Reset(ulong guid, uint id, int count) {
        this.guid = id;
        this.Reset(id, count);
       
        return this;
    }
}
