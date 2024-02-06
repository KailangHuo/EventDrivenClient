using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Integration;

public class MetaCommandManager {

    private static MetaCommandManager _instance;

    private MetaCommandManager() {
        Init();
    }

    public static MetaCommandManager GetInstance() {
        if (_instance == null) {
            lock (typeof(MetaCommandManager)) {
                if (_instance == null) {
                    _instance = new MetaCommandManager();
                }
            }
        }

        return _instance;
    }

    private string FilePath;

    private Dictionary<string, MetaCommand> MetaCommands;

    private List<string> WakeupCommandParams;

    private XmlDocument _xmlDocument;

    public MetaCommand GetMetaCommand(string commandName) {
        if (!MetaCommands.ContainsKey(commandName)) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("WRONG INPUT!", 3);
        }

        return MetaCommands[commandName];
    }

    public List<string> GetWakeupCommandParams() {
        List<string> list = new List<string>();
        for (int i = 0; i < WakeupCommandParams.Count; i++) {
            list.Add(WakeupCommandParams[i]);
        }

        return list;
    }

    private void Init() {
        this.FilePath = SystemConfiguration.GetInstance().SystemDirectory + @"\..\Client\ConfigurationFiles\Configuration.xml";
        if (!File.Exists(FilePath)) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("WRONG INSTALL PATH", 21);
        }

        this._xmlDocument = new XmlDocument();
        _xmlDocument.Load(this.FilePath);
        LoadMetaCommands();
        LoadWakeupCommandParams();
    }

    private void LoadMetaCommands() {
        this.MetaCommands = new Dictionary<string, MetaCommand>();
        XmlNode integrationCommandNode = _xmlDocument.SelectSingleNode(@"/Root/IntegrationCommands");
        XmlNodeList childNodes = integrationCommandNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            MetaCommand metaCommand = new MetaCommand();
            XmlAttributeCollection collection = node.Attributes;
            foreach (XmlAttribute xmlAttribute in collection) {
                if (xmlAttribute.Name == "CommandType") {
                    metaCommand.CommandType = (CommandTypeEnum)Enum.Parse(typeof(CommandTypeEnum), xmlAttribute.Value);
                    continue;
                }
                
                if (xmlAttribute.Name == "CommandPriority") {
                    metaCommand.CommandPriority = (CommandPriorityEnum)Enum.Parse(typeof(CommandPriorityEnum), xmlAttribute.Value);
                    continue;
                }
                
                if (xmlAttribute.Name == "CommandName") {
                    metaCommand.CommandName = xmlAttribute.Value;
                    continue;
                }
                
                if (xmlAttribute.Name == "CommandParameterNames") {
                    metaCommand.SetCommandParameterNamesSet(xmlAttribute.Value);
                    continue;
                }
            }
            this.MetaCommands.Add(metaCommand.CommandName, metaCommand);
        }

    }

    private void LoadWakeupCommandParams() {
        this.WakeupCommandParams = new List<string>();
        XmlNode node = _xmlDocument.SelectSingleNode(@"Root/WakeupCommandParameters");
        XmlAttributeCollection attributeCollection = node.Attributes;
        foreach (XmlAttribute xmlAttribute in attributeCollection) {
            string[] strs = xmlAttribute.Value.Split(",");
            for (int i = 0; i < strs.Length; i++) {
                this.WakeupCommandParams.Add(strs[i]);
            }
        }
    }

}