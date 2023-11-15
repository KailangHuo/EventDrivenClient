using System.Collections.Generic;

namespace EventDrivenStruct.Models; 

public class MissionExecutionBuffer {

    public MissionExecutionBuffer(int n = 1) {
        this._missionList = new List<Mission>();
        this._maxAmount = n;
        this._locker = new object();
    }

    private List<Mission> _missionList;

    private object _locker;

    private int _maxAmount;

    public bool PutMission(Mission mission) {
        lock (_locker) {
            if (this._missionList.Count < this._maxAmount) {
                this._missionList.Add(mission);
                return true;
            }

            return false;
        }
    }

    public Mission TakeTopMission() {
        lock (_locker) {
            if (this._missionList.Count > 0) {
                Mission mission = this._missionList[0];
                this._missionList.RemoveAt(0);
                return mission;
            }

            return Mission.NullMission;
        }
    }

}