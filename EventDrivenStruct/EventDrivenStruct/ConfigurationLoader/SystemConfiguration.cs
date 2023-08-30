using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ConfigurationLoader; 

public class SystemConfiguration {
    private static SystemConfiguration _instance;

    private SystemConfiguration() {
        // 研发环境
        configFilePath = @"..\..\..\ConfigurationLoader\ConfigurationFiles\Configuration.xml";
        
        // 产品环境
        //configFilePath = @"..\ConfigurationLoader\ConfigurationFiles\Configuration.xml";
        _document = new XmlDocument();
        init();
    }

    public static SystemConfiguration GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemConfiguration)) {
                if (_instance == null) {
                    _instance = new SystemConfiguration();
                }
            }
        }

        return _instance;
    }

    private string configFilePath;

    private XmlDocument _document;

    private HashSet<string> ConstantAppSet;

    private List<string> ConstantAppList;

    private List<string> AppList;

    private int ScreenNumber;

    private int StudyNumber;

    private Dictionary<String, AppConfigInfo> AppInfoMap;

    private void init() {
        ConstantAppSet = new HashSet<string>();
        AppInfoMap = new Dictionary<String, AppConfigInfo>();
        ConstantAppList = new List<string>();
        AppList = new List<string>();
        _document.Load(configFilePath);
        XmlNode ScreenNumberNode = _document.SelectSingleNode(@"/Root/ScreenNumber");
        ScreenNumber = Convert.ToInt32(ScreenNumberNode.FirstChild.Value);

        XmlNode StudyNumberNode = _document.SelectSingleNode(@"/Root/StudyNumber");
        StudyNumber = Convert.ToInt32(StudyNumberNode.FirstChild.Value);

        XmlNode constantAppListNode = _document.SelectSingleNode(@"/Root/ConstantAppList");
        XmlNodeList childNodes = constantAppListNode.ChildNodes;
        foreach (XmlNode node in childNodes) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            foreach (XmlAttribute  xmlAttribute in attributeCollection) {
                if (xmlAttribute.Name == "Name") {
                    ConstantAppSet.Add(xmlAttribute.Value);
                    ConstantAppList.Add(xmlAttribute.Value);
                }
            }
        }

        XmlNode appConfigListNode = _document.SelectSingleNode(@"/Root/AppConfiguration");
        foreach (XmlNode node in appConfigListNode.ChildNodes) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            AppConfigInfo appConfigInfo = new AppConfigInfo();
            foreach (XmlAttribute attribute in attributeCollection) {
                if (attribute.Name == "Name") appConfigInfo.AppName = attribute.Value;
                if (attribute.Name == "MaxConfigScreenNumber")
                    appConfigInfo.MaxConfigScreenNumber = Convert.ToInt32(attribute.Value);
            }
            AppInfoMap.Add(appConfigInfo.AppName, appConfigInfo);
            if (appConfigInfo.MaxConfigScreenNumber != 0) {
                AppList.Add(appConfigInfo.AppName);
            }
            
        }
        
    }

    public AppConfigInfo GetAppConfigInfo(string appName) {
        if (AppInfoMap.ContainsKey(appName)) {
            return AppInfoMap[appName];
        }

        throw new NullReferenceException();
    }

    public bool IsConstantApp(string appName) {
        return ConstantAppSet.Contains(appName);
    }

    public int GetScreenNumber() {
        return ScreenNumber;
    }

    public List<string> GetConstantAppList() {
        return this.ConstantAppList;
    }

    public int GetMaxStudyNumber() {
        return this.StudyNumber;
    }

    public List<string> GetAppList() {
        return this.AppList;
    }

}