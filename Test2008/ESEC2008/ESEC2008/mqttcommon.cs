using log4net;
using System;
using System.Configuration;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ESEC2008
{
	public class mqttcommon
	{

		public static string hostIP = ConfigurationManager.AppSettings["hostIP"];

		public static string hostPort = ConfigurationManager.AppSettings["hostPort"];

		public static string TopicID = ConfigurationManager.AppSettings["TopicID"];

		private static mqttcommon instance;

		private static MqttClient client = new MqttClient(mqttcommon.hostIP);

		private static object lockHelper = new object();

        public static string ClientId;

        public static System.Timers.Timer timerReconnect =new System.Timers.Timer();
		public static mqttcommon InstanceSingleTon()
		{
			if (mqttcommon.instance == null)
			{
				lock (mqttcommon.lockHelper)
				{
					if (mqttcommon.instance == null)
					{
						mqttcommon.instance = new mqttcommon();
					}
				}
			}
			return mqttcommon.instance;
		}

		public mqttcommon()
		{
            try
            {
                try
                {
                    client.Connect(ClientId);
                    client.MqttMsgPublished += Client_MqttMsgPublished;
                    client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                    
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e);
                }

                timerReconnect.Interval = 60 * 1000;
                timerReconnect.Elapsed += TimerReconnect_Elapsed;
            }
            catch(Exception  e)
            {
                Log.Logger.Error(e);
                timerReconnect.Start();
            }
        }

        private void TimerReconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (reconnect()) timerReconnect.Stop();
        }


        private static bool reconnect()
        {
            try
            {
                if (client.IsConnected)
                {
                    mqttcommon.client.MqttMsgPublished += Client_MqttMsgPublished;
                    mqttcommon.client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                }
                else
                {
                    mqttcommon.client.Connect(ClientId);
                    mqttcommon.client.MqttMsgPublished += Client_MqttMsgPublished;
                    mqttcommon.client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                }
                return true;
            }
            catch(Exception e)
            {
                Log.Logger.Error(e);
                timerReconnect.Start();
                return false;
            }
        }
        public static void SendMQTT(string msg)
		{
			try
			{
				mqttcommon.InstanceSingleTon();                
                mqttcommon.client.Publish(mqttcommon.TopicID, System.Text.Encoding.UTF8.GetBytes(msg), 2, false);
            }
			catch (System.Exception ex)
			{
				Log.Logger.ErrorFormat(ex.Message, new object[0]);
                reconnect();
			}
		}

        public static void Subscribe( )
        {
            try
            {
                mqttcommon.InstanceSingleTon();
                mqttcommon.client.Subscribe(new string[] { mqttcommon.TopicID }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            }
            catch (System.Exception ex)
            {
                Log.Logger.ErrorFormat(ex.Message, new object[0]);
                reconnect();
            }
        }

        public static void Subscribe(string sTopicID)
        {
            try
            {
                mqttcommon.InstanceSingleTon();
                mqttcommon.client.Subscribe(new string[] { mqttcommon.TopicID }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch (System.Exception ex)
            {
                Log.Logger.ErrorFormat(ex.Message, new object[0]);
                reconnect();
            }
        }


        private static void Client_MqttMsgPublished(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishedEventArgs e)
        {
            Log.Logger.DebugFormat("{0}:{1}", e.MessageId, e.IsPublished);
        }


        private static void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            Log.Logger.DebugFormat("{0}   {1}   {2}    {3}", System.Text.Encoding.Default.GetString(e.Message), e.Topic, e.Retain,e.DupFlag);

        }


        public static void Close()
        {
            timerReconnect.Close();
            client.Disconnect();          
        }


    }
}
