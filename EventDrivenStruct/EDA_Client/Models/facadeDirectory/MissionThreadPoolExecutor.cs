using System.Collections.Generic;
using System.Threading;

namespace EventDrivenStruct.Models; 

public class MissionThreadPoolExecutor {
   
    public MissionThreadPoolExecutor(int workerNumber, int maxMissionAmount) {
        this.workerList = new List<Thread>();
        this.missionQue = new List<Mission>();
        this.Locker = new object();
        this.MaxMissionAmount = maxMissionAmount;
        this.InitThreadPool(workerNumber);
    }

    private List<Thread> workerList;

    private List<Mission> missionQue;

    private object Locker;

    private int MaxMissionAmount;
    
    private void InitThreadPool(int workerNumber) {
        for (int i = 0; i < workerNumber; i++) {
            Thread thread = new Thread(Runnable);
            thread.Name = nameof(MissionThreadPoolExecutor) + ":#" + i;
            workerList.Add(thread);
            thread.Start();
        }
    }

    private void Runnable() {
        while (true) {
            lock (Locker) {
                if (this.missionQue.Count == 0) {
                    Monitor.Wait(Locker);
                }
                if(this.missionQue.Count == 0) continue;
                Mission mission = missionQue[0];
                missionQue.RemoveAt(0);
                mission.Execute();
                Monitor.PulseAll(Locker);
            }
        }
    }

    public void EnqueueMission(Mission mission) {
        while (true) {
            lock (Locker) {
                if (this.missionQue.Count >= this.MaxMissionAmount) Monitor.Wait(Locker);
                if (this.missionQue.Count >= this.MaxMissionAmount) continue;
                this.missionQue.Add(mission);
                Monitor.PulseAll(Locker);
                break;
            }
        }
        return;
    }
}