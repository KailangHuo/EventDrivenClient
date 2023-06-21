using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EventDrivenStruct.ViewModels; 

public class TCP_Sender {
    //更新详见github推送信息
    private volatile static TCP_Sender _instance;

    private TCP_Sender() {
        
    }

    public static TCP_Sender GetInstance() {
        if (_instance == null) {
            lock (typeof(TCP_Sender)) {
                if (_instance == null) {
                    _instance = new TCP_Sender();
                    
                }
            }
        }

        return _instance;
    }
    
    
    public void SendOpen(List<string> studyUid, string appName, int attachScreenNumber, int number) {
        string listInfo = "";
        for (int i = 0; i < studyUid.Count - 1; i++) {
            listInfo += studyUid[i] + ", ";
        }

        listInfo += studyUid[studyUid.Count - 1];
        
        Debug.WriteLine("Open optimes " + number +" | Uids: " + listInfo + " | 应用名: " + appName + " | 屏幕号: " + attachScreenNumber);
    }
    public void SendHide(List<string> studyUid, string appName, int number) {
        string listInfo = "";
        for (int i = 0; i < studyUid.Count - 1; i++) {
            listInfo += studyUid[i] + ", ";
        }

        listInfo += studyUid[studyUid.Count - 1];
        
        Debug.WriteLine("Hide optimes " + number +" | Uids: " + listInfo + " | 应用名: " + appName );
    }
    public void SendClose(List<string> studyUid, string appName, int number) {
        string listInfo = "";
        for (int i = 0; i < studyUid.Count - 1; i++) {
            listInfo += studyUid[i] + ", ";
        }

        listInfo += studyUid[studyUid.Count - 1];
        
        Debug.WriteLine("Close optimes " + number +" | Uids: " + listInfo + " | 应用名: " + appName );
    }

}