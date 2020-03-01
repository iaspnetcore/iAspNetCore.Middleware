using System;
using System.Collections.Concurrent;

namespace iAspNetCore.Modules.EmailSender
{

    /// <summary>
    /// 邮件队列提供器IMailQueueProvider实现 http://www.cnblogs.com/rocketRobin/p/9294845.html
    /// </summary>
    public class MailQueueProvider
    {
        private static readonly ConcurrentQueue<MailBox> _mailQueue = new ConcurrentQueue<MailBox>();
        public int Count => _mailQueue.Count;
        public bool IsEmpty => _mailQueue.IsEmpty;
        public void Enqueue(MailBox mailBox)
        {
            _mailQueue.Enqueue(mailBox);
        }
        public bool TryDequeue(out MailBox mailBox)
        {
            return _mailQueue.TryDequeue(out mailBox);
        }
    }
}
