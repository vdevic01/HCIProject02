using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.DTO
{
    class NavigatorEventDTO
    {
        public object Payload { get; set; }
        public ViewType EventInvoker { get; set; }

        public NavigatorEventDTO(object payload, ViewType eventInvoker)
        {
            Payload = payload;
            EventInvoker = eventInvoker;
        }
    }
}
