using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SrvSmartNote;
using System.Threading.Tasks;

namespace SmartNoteService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartNoteService : ISmartNoteService
    {
        public async Task<GetAllNoteResponse> GetAllNote(GetAllNoteRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            GetAllNoteResponse response = new GetAllNoteResponse();

            List<Note> ret = await srv.GetAllNote(input.Author);

            if (ret != null)
            {
                response.Notes = ret;
                response.Success = true;
            }
            
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

        public DeleteAndInsertAllResponse DeleteAndInsertAll(DeleteAndInsertAllRequest input)
        {
            SrvSmartNote.SmartNoteService srv = new SrvSmartNote.SmartNoteService();
            DeleteAndInsertAllResponse response = new DeleteAndInsertAllResponse();

            response.Success = srv.DeleteAndInsertAll(input.Notes, input.Author);

            return response;
        }
    }
}
