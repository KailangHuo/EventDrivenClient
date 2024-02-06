using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Integration;

public class CommandExecutor {

    public CommandExecutor() {
        
    }

    public void CallupSoftwareSync(DynamicCommand dynamicCommand) {
        string softwarePath = SystemConfiguration.GetInstance().SystemDirectory + @"/../Client/bin/EDA_Client.exe";
        if(!File.Exists(softwarePath)) ExceptionManager.GetInstance().ThrowExceptionAndExit("CAN NOT FIND SOFTWARE!", 21);
        
        List<string> wakeupCmdParams = MetaCommandManager.GetInstance().GetWakeupCommandParams();
        Dictionary<string, string> kvPairs = new Dictionary<string, string>();
        for (int i = 0; i < wakeupCmdParams.Count; i++) {
            string val = "";
            if (dynamicCommand.CommandParamsMap.ContainsKey(wakeupCmdParams[i])) {
                val = dynamicCommand.CommandParamsMap[wakeupCmdParams[i]];
            }

            kvPairs[wakeupCmdParams[i]] = val;
        }

        var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);
        listener.Start();

        string uuid = Guid.NewGuid().ToString();
        kvPairs["UUID"] = uuid;
        kvPairs["Port"] = ((IPEndPoint)listener.LocalEndpoint).Port + "";
        string base64Args = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(kvPairs)));
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = softwarePath;
        //processStartInfo.Arguments = base64Args;
        processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
        Process.Start(processStartInfo);

        Socket client = listener.AcceptSocket();
        byte[] buffer = new byte[16];
        int len = client.Receive(buffer);
        string received = Encoding.UTF8.GetString(buffer, 0, len);
        if (received != uuid) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("FAILED TO INITIALIZE CLIENT", 1);
        }
    }

    public void InvokeCommand(DynamicCommand dynamicCommand) {
        
    }

}