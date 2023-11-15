using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ConfigurationLoader; 

public class SystemConfiguration {
    private static SystemConfiguration _instance;

    private SystemConfiguration() {
        // 研发环境
        //configFilePath =System.Environment.CurrentDirectory + @"\..\..\..\ConfigurationLoader\ConfigurationFiles\Configuration.xml";
        
        // 产品环境
        configFilePath = System.Environment.CurrentDirectory + @"\ConfigurationFiles\Configuration.xml";
        _document = new XmlDocument();
        
        ConstantAppSet = new HashSet<string>();
        AppInfoMap = new Dictionary<String, AppConfigInfo>();
        ConstantAppList = new List<string>();
        AppList = new List<string>();
        TestStudyAppList = new List<TestStudyAppNode>();
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

    private List<TestStudyAppNode> TestStudyAppList;

    private int ScreenNumber;

    private int StudyNumber;

    private int StudyRemainUnlockNumber;

    private Dictionary<String, AppConfigInfo> AppInfoMap;

    private void init() {
        if (!File.Exists(configFilePath)) {
            MessageBox.Show("ConfigurationFilesNotFound!");
            return;
        }

        _document.Load(configFilePath);
        initScreenNumber();
        initStudyNumber();
        initConstAppList();
        initAppMapAndAppList();
        initStudyAppNodeList();
    }

    private void initScreenNumber() {
        XmlNode ScreenNumberNode = _document.SelectSingleNode(@"/Root/ScreenNumber");
        ScreenNumber = Convert.ToInt32(ScreenNumberNode.FirstChild.Value);
    }

    private void initStudyNumber() {
        XmlNode StudyNumberNode = _document.SelectSingleNode(@"/Root/StudyNumber");
        StudyNumber = Convert.ToInt32(StudyNumberNode.FirstChild.Value);

        XmlNode StudyRemainUnlockNumberNode = _document.SelectSingleNode(@"/Root/StudyRemainUnlockNummber");
        StudyRemainUnlockNumber = Convert.ToInt32(StudyRemainUnlockNumberNode.FirstChild.Value);
    }

    private void initConstAppList() {
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
    }

    private void initAppMapAndAppList() {
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

    private void initStudyAppNodeList() {
        XmlNode testStudiesNode = _document.SelectSingleNode(@"Root/TestStudyData");
        foreach (XmlNode node in testStudiesNode.ChildNodes) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            TestStudyAppNode studyAppNode = new TestStudyAppNode();
            AppModel appModel = null;
            foreach (XmlAttribute attribute in attributeCollection) {
                if (attribute.Name == "patientName") {
                    studyAppNode.patientName = attribute.Value;
                    continue;
                }

                if (attribute.Name == "patientGender") {
                    studyAppNode.patientGender = attribute.Value;
                    continue;
                }

                if (attribute.Name == "patientAge") {
                    studyAppNode.patientAge = attribute.Value;
                    continue;
                }

                if (attribute.Name == "studyUid") {
                    studyAppNode.studyUid = attribute.Value;
                    continue;
                }

                if (attribute.Name == "AppName") {
                    studyAppNode.appName = attribute.Value;
                    continue;
                }

            }
            
            TestStudyAppList.Add(studyAppNode);
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

    public int GetRemainStudyUnlockNumber() {
        return this.StudyRemainUnlockNumber;
    }

    public List<string> GetAppList() {
        return this.AppList;
    }

    public List<TestStudyAppNode> GetTestStudyList() {
        if (TestStudyAppList.Count == 0) {
            TestStudyAppList = new List<TestStudyAppNode>();
            initStudyAppNodeList();
        }
        return this.TestStudyAppList;
    }

    public bool GetTestPanelStatus() {
        XmlNode testPanelStatusNode = _document.SelectSingleNode(@"Root/TestPanelStatus");
        int boolNum = Int32.Parse((string)testPanelStatusNode.FirstChild.InnerText);
        if (boolNum == 1) return true;
        if (boolNum == 0) return false;
        ExceptionManager.GetInstance().ThrowAsyncException("WRONG CONFIGURATION: <TestPanelStatus/>");
        System.Environment.Exit(1);
        return false;
    }

}