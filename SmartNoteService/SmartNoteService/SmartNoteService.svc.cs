using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SrvSmartNote;

namespace SmartNoteService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartNoteService : ISmartNoteService
    {
        public GetAllNoteResponse GetAllNote(GetAllNoteRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            GetAllNoteResponse response = null;

            return response;
        }

        public InsertNoteResponse InsertNote(InsertNoteRequest input)
        {
            throw new NotImplementedException();
        }

        public UpdateNoteResponse UpdateNote(UpdateNoteRequest input)
        {
            throw new NotImplementedException();
        }

        public DeleteNoteResponse DeleteNote(DeleteNoteRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
