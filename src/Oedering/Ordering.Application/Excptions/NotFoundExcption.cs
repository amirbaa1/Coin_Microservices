using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Excptions
{
    public class NotFoundExcption : ApplicationException
    {
        public NotFoundExcption(string name,object key):base($"Entity {name} :  ({key}) was NotFound")
        {

        }
    }
}
