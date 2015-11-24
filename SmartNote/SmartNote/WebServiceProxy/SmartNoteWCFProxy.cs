using Newtonsoft.Json;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNote.WebServiceProxy
{
    public class SmartNoteWCFProxy
    {
        public string connStringBase { get; set; }

        public SmartNoteWCFProxy(string uriString)
        {
            connStringBase = uriString;
        }

        public async Task<List<Note>> GetAllNote(User input)
        {
            GetAllNoteRequest request = new GetAllNoteRequest();
            request.Author = input;

            GetAllNoteResponse res = await Invoke<GetAllNoteRequest, GetAllNoteResponse>(request, "GetAllNote/");

            return res.Notes;
        }

        public async Task<bool> InsertNote(Note input)
        {
            InsertNoteRequest request = new InsertNoteRequest();
            request.Note = input;

            InsertNoteResponse res = await Invoke<InsertNoteRequest, InsertNoteResponse>(request, "InsertNote/");

            return res.Success;
        }

        public async Task<bool> UpdateNote(Note input)
        {
            UpdateNoteRequest request = new UpdateNoteRequest();
            request.Note = input;

            UpdateNoteResponse res = await Invoke<UpdateNoteRequest, UpdateNoteResponse>(request, "UpdateNote/");

            return res.Success;
        }

        public async Task<bool> DeleteNote(Note input)
        {
            DeleteNoteRequest request = new DeleteNoteRequest();
            request.Note = input;

            DeleteNoteResponse res = await Invoke<DeleteNoteRequest, DeleteNoteResponse>(request, "DeleteNote/");

            return res.Success;
        }

        public async Task<bool> DeleteAndInsertAll(List<Note> input, User author)
        {
            DeleteAndInsertAllRequest request = new DeleteAndInsertAllRequest();
            request.Notes = input;
            request.Author = author;

            DeleteAndInsertAllResponse res = await Invoke<DeleteAndInsertAllRequest, DeleteAndInsertAllResponse>(request, "DeleteAndInsertAll/");

            return res.Success;
        }

        private async Task<K> Invoke<T, K>(T request, string method)
        {
            using (var client = new Windows.Web.Http.HttpClient())
            {
                TimeSpan ts = new TimeSpan(1, 0, 0);
                var uri = new Uri(this.connStringBase + method);
                Windows.Web.Http.HttpStringContent input = new Windows.Web.Http.HttpStringContent(JsonConvert.SerializeObject(request), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

                var cts = new System.Threading.CancellationTokenSource();
                cts.CancelAfter(ts);
                var res = await client.PostAsync(uri, input);
                res.EnsureSuccessStatusCode();

                string responseBody = await res.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<K>(responseBody);
            }

        }
    }
}
