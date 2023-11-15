using System.Collections.Generic;
using System.Reflection;

namespace EventDrivenStruct.Models; 

public class Mission{

    public static Mission NullMission = new Mission(MissionType.NULL, null, null, null);

    public Mission(MissionType missionType, object targetObject , string methodName, List<object> parameters) {
        this.MissionType = missionType;
        this.TargetObject = targetObject;
        this.MethodName = methodName;
        this.ParameterList = parameters;
    }

    public object TargetObject { get; private set; }

    public MissionType MissionType { get; private set; }

    public string MethodName { get; private set; }

    public List<object> ParameterList { get; private set; }

    public object Execute() {
        if (this.MissionType == MissionType.NULL) return null;
        MethodInfo methodInfo = TargetObject.GetType().GetMethod(this.MethodName, BindingFlags.NonPublic | BindingFlags.Instance);
        object result = methodInfo.Invoke(TargetObject, this.ParameterList.ToArray());
        return result;
    }

}

public enum MissionType {
    NULL,
    SERIAL,
    PARALLEL,
}