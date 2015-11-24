using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService
{
    [ServiceContract]
    public interface ISmartNoteService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllNote/")]
        Task<GetAllNoteResponse> GetAllNote(GetAllNoteRequest input);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "InsertNote/")]
        InsertNoteResponse InsertNote(InsertNoteRequest input);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateNote/")]
        UpdateNoteResponse UpdateNote(UpdateNoteRequest input);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteNote/")]
        DeleteNoteResponse DeleteNote(DeleteNoteRequest input);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteAndInsertAll/")]
        DeleteAndInsertAllResponse DeleteAndInsertAll(DeleteAndInsertAllRequest input);
    }
}