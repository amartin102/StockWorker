using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServices.Consumer
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
