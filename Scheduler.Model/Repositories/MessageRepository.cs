using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories
{
    public class MessageRepository : BaseRepository<Scheduler.Model.EntityModels.Message, SchedulerEntities>, IDisposable
    {
        #region BaseRepository

        public override void Add(Message message)
        {
            Entities.AddToMessages(message);
            Entities.SaveChanges();
        }

        public override IQueryable<Message> Items
        {
            get
            {
                return Entities.Messages;
            }
        }

        #endregion

        int autoIncrementId = 0;


        public Message getMessageById(int id)
        {
            return Items.FirstOrDefault(x => x.id == id);
        }

        public void MarkAsRead(int id)
        {
            var mess = Items.FirstOrDefault(t => t.id == id);
            if (mess != null)
            {
                mess.Status = false;
            }

            Entities.SaveChanges();
        }

        public void MarkAsNotRead(int id)
        {
            var mess = Items.FirstOrDefault(t => t.id == id);
            if (mess != null)
            {
                mess.Status = true;
            }

            Entities.SaveChanges();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return Entities.Messages.ToList();
        }
    }
}
