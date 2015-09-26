using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartNoteService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartNoteService : ISmartNoteService
    {
        public TestMethodResponse TestMethod(TestMethodRequest input)
        {
            TestMethodResponse response = new TestMethodResponse();
            response.List = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                response.List.Add(i);
            }

            return response;
        }
    }
}
