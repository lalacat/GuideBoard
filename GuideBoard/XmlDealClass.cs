using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GuideBoard
{
    class XmlDealClass
    {
        private readonly XmlDocument _myXmlDocument;
        private XmlElement _rootElement;
        private string _commandFromElement;

        private XmlNodeList _deviceList;
        private string[] _detailArray;
        private int[] _deviceNum;

        private string _degreeData=null;
        private string _directionData=null ;
        private string _orderData=null;
        private string _contextMessage=null;

        private string _messageTemp;
        private string[] _messageFinish; 
        public XmlDealClass(string str)
        {
            try
            {
                _myXmlDocument = new XmlDocument();
                _myXmlDocument.LoadXml(str);
                GetAllDevice();
            }
            catch
            {
                throw;
            }

        }

        private void GetAllDevice()
        {
            if (_myXmlDocument.DocumentElement != null) _deviceList = _myXmlDocument.DocumentElement.ChildNodes;
            _deviceNum=new int[_deviceList.Count];
            _messageFinish=new string[_deviceList.Count];
            int countTemp = 0;
            foreach (XmlElement xe in _deviceList.Cast<XmlElement>())
            {
                Console.WriteLine(xe.LocalName+ ": "+" ID = "+ xe.GetAttribute("ID"));
                _deviceNum[countTemp] = int.Parse(xe.GetAttribute("ID"));
                
                DealDeviceMessage(xe);
                _messageFinish[countTemp] = "[{" + xe.GetAttribute("ID") + "}," + _messageTemp+ "]";
                _messageTemp = null;
                Console.WriteLine("_messageFinish: {0}",_messageFinish[countTemp]);
                countTemp++;
            }
        }

        private void DealDeviceMessage(XmlElement xe)
        {
            XmlNodeList deviceNodeList = xe.ChildNodes;
            if (deviceNodeList[0].Name == "command")
            {
                int commandTemp = 0;
                _commandFromElement = deviceNodeList[0].InnerText;

                _messageTemp = "{" + _commandFromElement + "}";
              
                Console.WriteLine(_commandFromElement);
                //选命令
                commandTemp = (int) Enum.Parse(typeof(CommandType), _commandFromElement);
                if (2 <= commandTemp && commandTemp <= 5)
                {
                    XmlNodeList contestNodeList = deviceNodeList[1].ChildNodes;
                    int strlen = 0;
                    int countTemp = 0;
                    for (int i = 0; i < contestNodeList.Count; i++)
                    {
                        switch (contestNodeList[i].Name)
                        {
                            case "degree":
                                _degreeData = contestNodeList[i].InnerText;
                                _messageTemp += ",{" + _degreeData+"}";
                                break;
                            case "direction":
                                _directionData = contestNodeList[i].InnerText;
                                _messageTemp += ",{" + _directionData+"}";
                                break;
                            case "order":
                                _orderData = contestNodeList[i].InnerText;
                                _messageTemp += ",{" + _orderData+"}";
                                break;
                            case "detail":
                                if (strlen == 0)
                                {
                                    strlen = contestNodeList.Count - i;
                                    _detailArray = new string[strlen];
                                    countTemp = i;
                                }
                                //颜色+格式+内容
                                XmlNodeList detailChildList = contestNodeList[i].ChildNodes;
                                _detailArray[i - countTemp] = detailChildList[0].InnerText + ";" + detailChildList[1].InnerText + ";" +
                                                              detailChildList[2].InnerText;
                                break;
                            default:
                                Console.WriteLine("格式错误");
                                break;
                        }
                    }
                    foreach (string t in _detailArray)
                        _messageTemp += ",{" + t+"}";
                }else if (5 < commandTemp && commandTemp < 10)
                {
                    if (deviceNodeList[1].Name == "context")
                    {
                        _contextMessage = deviceNodeList[1].InnerText;
                    }
                    _messageTemp = _commandFromElement + "," + _contextMessage;
                }
                else
                {
                    Console.WriteLine("无此命令");
                }
                Console.WriteLine("_completeMessage: {0}", _messageTemp);
            } 
        }

        public string[] GetInformation()
        {
            return _messageFinish;
        }
        
    }
}
