using System;

namespace EventDrivenStruct.Models; 

public class Mission {

    public Mission(Action action) {
        this.Action = action;
    }

    private Action Action;

    public void Execute() {
        this.Action.Invoke();
    }

}