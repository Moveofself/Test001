using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQUtil
{
    public class MSMQHelper
    {
        private string QueuePath;
        public MSMQHelper(string queuePath)
        {
            QueuePath = queuePath;
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queueObj">发送到队列的对象</param>
        /// <param name="queueLable">该发送队列对象的标签</param>
        public void CreateQueue(object queueObj, string queueLable = "")
        {
            try
            {
                using (MessageQueue queue = QueueExist() ? new MessageQueue(QueuePath) : MessageQueue.Create(QueuePath))
                {
                    queue.Label = queueLable;
                    if (queue.CanWrite)
                    {
                        queue.Send(queueObj); 
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error to Create Queue!", ex);
            }
        }
        
        /// <summary>
        /// 获取队列第一条数据，并删除数据
        /// </summary>
        /// <param name="queueTypes"></param>
        /// <returns></returns>
        public object ReceiveOneQueue(Type[] queueTypes)
        {
            object result = null;
            if (QueueExist())
            {
                using (MessageQueue mq = new MessageQueue(QueuePath))
                {
                    try
                    {
                        // 设置消息队列的格式化器
                        mq.Formatter = new XmlMessageFormatter(queueTypes);

                        if (mq.CanRead)
                        {
                            Message oneMessage = mq.Receive(); // 获得消息队列中第一条消息
                            result = oneMessage.Body;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error to query Queue!", ex);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取队列第一条数据，但保留数据
        /// </summary>
        /// <param name="queueTypes"></param>
        /// <returns></returns>
        public object PeekOneQueue(Type[] queueTypes)
        {
            object result = null;
            if (QueueExist())
            {
                using (MessageQueue mq = new MessageQueue(QueuePath))
                {
                    try
                    {
                        // 设置消息队列的格式化器
                        mq.Formatter = new XmlMessageFormatter(queueTypes);

                        if (mq.CanRead)
                        {
                            Message oneMessage = mq.Peek(); // 获得消息队列中第一条消息
                            result = oneMessage.Body; 
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error to query Queue!", ex);
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// 查询队列是否存在
        /// </summary>
        /// <returns></returns>
        public bool QueueExist()
        {
            bool queueExist = false;
            try
            {
                queueExist = MessageQueue.Exists(QueuePath);
            }
            catch//远端访问不支持exist，所有直接查询
            {
                queueExist = true;
            }
            return queueExist;
        }
    }
}
