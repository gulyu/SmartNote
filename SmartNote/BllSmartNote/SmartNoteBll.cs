using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;
using DalSmartNote;
using Microsoft.Data.Entity;
using System.Linq.Expressions;
using Windows.UI.Xaml.Controls;
using Windows.UI.Text;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BllSmartNote
{
    public class SmartNoteBll : ISmartNoteBll
    {
        private static ISmartNoteDal smartNoteDal;

        public SmartNoteBll()
        {
            smartNoteDal = new SmartNoteDalSQLite();
        }

        public bool DeleteNote(Note input)
        {
            return smartNoteDal.DeleteNote(input);
        }

        public List<Note> GetAllNote(User input, int sortBy)
        {
            return smartNoteDal.GetAllNote(input, sortBy);
        }

        public void InitializeSQLiteDatabase()
        {
            // SQLite adatbázis inicializáció.
            using(var db = new SQLiteContext())
            {
                db.Database.Migrate();
            }
        }

        public bool InsertNote(Note input)
        {
            return smartNoteDal.InsertNote(input);
        }

        public bool UpdateNote(Note input)
        {
            return smartNoteDal.UpdateNote(input);
        }

        public List<Note> GetNotesByParams(User user, string title, string content, DateTime? creatinDate, DateTime? modDate, int? priority, 
                                            bool? hasfile, bool? byTitle, bool? byCreationDate, bool? byModifyDate, bool? byPriority, bool? byContent)
        {
            List<Note> ret = null;
            List<Note> allNotes = GetAllNote(user, 0);
            Func<Note, bool> func = (n) => (
                                            //(n.Author.Id == user.Id) &&
                                            (byTitle.Value == true ? n.Title.Contains(title) : true) &&
                                            (hasfile.Value == true ? (n.Attachments != null && n.Attachments.Count > 0) : true) &&
                                            (byCreationDate.Value == true ? n.CreationDate.Date == creatinDate.Value.Date : true) &&
                                            (byModifyDate.Value == true ? n.ModoficationDate.Date == modDate.Value.Date : true) &&
                                            (byPriority.Value == true ? (priority.HasValue && priority == n.Priority) : true) &&
                                            (byContent.Value == true ? n.PlainText.Contains(content) : true)
                                           );

            if (allNotes != null)
            {
                ret = allNotes.Where(func).ToList();
            }

            return ret;
        }

        public async void OpenInAnotherApp(byte[] data, string filename)
        {
            StorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
                {
                    using (DataWriter dataWriter = new DataWriter(outputStream))
                    {
                        dataWriter.WriteBytes(data);
                        await dataWriter.StoreAsync(); // 
                        dataWriter.DetachStream();
                    }
                    await outputStream.FlushAsync();
                }
                await fileStream.FlushAsync();
            }

            await Windows.System.Launcher.LaunchFileAsync(file);
        }
    }
}
