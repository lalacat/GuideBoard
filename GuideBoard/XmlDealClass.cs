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
        
        private int[] _deviceNum;
        private int[] _commandNum;
     
        private ContextInfromation[] _allInfromations; 


        private string _degreeData=null;
        private string _directionData=null ;
        private string _orderData=null;

        private int _countGlobal = 0;
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
            _commandNum=new int[_deviceList.Count];
            _allInfromations = new ContextInfromation[_deviceList.Count];

            foreach (XmlElement xe in _deviceList.Cast<XmlElement>())
            {
                Console.WriteLine(xe.LocalName+ ": "+" ID = "+ xe.GetAttribute("ID"));
                _allInfromations[_countGlobal] = new ContextInfromation {ID = int.Parse(xe.GetAttribute("ID"))};

                DealDeviceMessage(xe);
                _countGlobal++;
            }
            _countGlobal = 0;
        }

        
        private void DealDeviceMessage(XmlElement xe)
        {
            XmlNodeList deviceNodeList = xe.ChildNodes;
            if (deviceNodeList[0].Name == "command")
            {
                int commandTemp = 0;
                _commandFromElement = deviceNodeList[0].InnerText;         
                Console.WriteLine(_commandFromElement);
                //选命令
                commandTemp = (int) Enum.Parse(typeof(CommandType), _commandFromElement);
               
                _allInfromations[_countGlobal].Command= commandTemp;

                if (commandTemp==1)
                {
                   
                    return;
                }
                else if (2 <= commandTemp && commandTemp <= 5)
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
                                _allInfromations[_countGlobal].Degree = _degreeData;
                                break;
                            case "direction":
                                _directionData = contestNodeList[i].InnerText;
                                _allInfromations[_countGlobal].Direction = _directionData;
                                break;
                            case "order":
                                _orderData = contestNodeList[i].InnerText;
                                _allInfromations[_countGlobal].Order = _orderData;
                                break;
                            case "detail":
                                if (strlen == 0)
                                {
                                    strlen = contestNodeList.Count - i;
                                    _allInfromations[_countGlobal].Details=new ContextInfromation.Detail[strlen];
                                }
                                //颜色+格式+内容
                                XmlNodeList detailChildList = contestNodeList[i].ChildNodes;
                                _allInfromations[_countGlobal].Details[countTemp].Color = detailChildList[0].InnerText;
                                _allInfromations[_countGlobal].Details[countTemp].Format = detailChildList[1].InnerText;
                                _allInfromations[_countGlobal].Details[countTemp].Data = detailChildList[2].InnerText;

                                countTemp++;
                                break;
                            default:
                                throw new XmlException("XMLcontext格式错误");
                                break;
                        }
                    }
 
                }else if (5 < commandTemp && commandTemp <= 10)
                {
                    if (deviceNodeList[1].Name == "context")
                    {
                        _allInfromations[_countGlobal].Context = deviceNodeList[1].InnerText;

                    }
                }
                else
                {
                    throw new XmlException("无此命令: "+ _commandFromElement);
                    // Console.WriteLine("无此命令");
                }
            } 
        }

       public ContextInfromation[] GetInfromations
        {
            get { return _allInfromations; }     
        }

    }
}