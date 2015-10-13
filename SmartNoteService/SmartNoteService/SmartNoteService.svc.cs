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
            GetAllNoteResponse response = new GetAllNoteResponse();

            List<Note> ret = srv.GetAllNote(input.Author);

            response.Notes = ret;

            return response;
        }

        public InsertNoteResponse InsertNote(InsertNoteRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            InsertNoteResponse response = new InsertNoteResponse();

            bool ret = srv.InsertNote(input.Note);

            response.Success = ret;

            return response;
        }

        public UpdateNoteResponse UpdateNote(UpdateNoteRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            UpdateNoteResponse response = new UpdateNoteResponse();

            bool ret = srv.UpdateNote(input.Note);

            response.Success = ret;

            return response;
        }

        public DeleteNoteResponse DeleteNote(DeleteNoteRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            DeleteNoteResponse response = new DeleteNoteResponse();

            bool ret = srv.DeleteNote(input.Note);

            response.Success = ret;

            return response;
        }
    }
}
