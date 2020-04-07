using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ESEC2008
{
    public partial class ESEC2008 : Form
    {

         
        public EqpCommForEsec2008 eqpCommForEsec2008=new EqpCommForEsec2008();

        public ESEC2008()
        {
            InitializeComponent();
            Initial();

            string command = "16 54 0D";
            byte[] commandbyte = command.Split(' ').Select(x => Convert.ToByte(x, 16)).ToArray();
            string s=Encoding.GetEncoding("utf-8").GetString(commandbyte);
        }

        public void Initial()
        {
            connectionSetupCfg.Esec2008BoxRs232 = "COM2,9600,8,None,One";
            connectionSetupCfg.Esec2008BarCodeScanerRs232 = "COM5,115200,8,None,One";
            eqpCommForEsec2008.Init();
            eqpCommForEsec2008.Start();



            //MqttClient client = new MqttClient("127.0.0.1");
            //client.Connect("mqttjs_a85b5ac486");

            mqttcommon.ClientId = "TEST";
            Transaction trans = new Transaction("2.1EQPStateChanged");
            trans.Add("EQPID", "A2800-0015");
            trans.Add("ControlState", "ON-line");
            trans.Add("ProcessState", "START");
 
            mqttcommon.SendMQTT(trans.CreateXMLstring());

            //MqttClient client = new MqttClient("127.0.0.1");

            //client.Connect("TEST");

            
            //mqttcommon.SendMQTT("Hello!");
            //mqttcommon.Subscribe();
            mqttcommon.SendMQTT("Hello!");

            //mqttcommon.ClientId= "mqttjs_a85b5ac486";
            //mqttcommon.hostIP = "127.0.0.1";
            //mqttcommon.hostPort = "8083";
            //mqttcommon.SendMQTT("Hello!");

        }



        public void ShowMessage(string msg, string title)
        {
            this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg, title });
        }

        delegate void MessageBoxShow(string msg, string title);

        void MessageBoxShow_F(string msg, string title)
        {
            MessageBox.Show(msg, "提示信息");
        }


    }
}
