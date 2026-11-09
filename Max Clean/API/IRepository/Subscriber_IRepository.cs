using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IRepository
{
    interface Subscriber_IRepository
    {
        int Add(Subscriber_Model ObjSubscriber);
        int Update(Subscriber_Model ObjSubscriber);
        int Delete(int SubscriberID);
        Subscriber_Model GetSubscriberByID(int SubscriberID);
        List<Subscriber_Model> GetActiveSubscribers();
        List<Subscriber_Model> GetAllSubscribers();
    }
}
