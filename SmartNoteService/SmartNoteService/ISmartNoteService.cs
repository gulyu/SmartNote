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
    [ServiceContract]
    public interface ISmartNoteService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "TestMethod/")]
        TestMethodResponse TestMethod(TestMethodRequest input);
    }
}