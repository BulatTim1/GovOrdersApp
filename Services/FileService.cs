using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using GovOrdersApp.Data.DB;
using MongoDB.Bson;

namespace GovOrdersApp.Services
{
	public class FileService
    {
        static GridFSBucket gridFS = DBConnection.gridFS;

        public async Task UploadImageToDbAsync(string name, Stream fs)
        {
            await gridFS.UploadFromStreamAsync(name, fs);
        }

        public async Task<string> UploadFileAsync(string name, Stream fs)
        {
            return (await gridFS.UploadFromStreamAsync(name, fs)).ToString();
        }
        
        public string? IsExsists(string id)
        {
            try
            {
                return gridFS.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefault()?.Filename;
            }
            catch
            {
                return null;
            }
        }

        public async void DeleteFile(string id)
        {
            await gridFS.DeleteAsync(new ObjectId(id));
        }

        public async Task<Stream> DownloadFileAsync(string id)
        {
            return await gridFS.OpenDownloadStreamAsync(new ObjectId(id));
        }

        public void DownloadToLocal(string name)
        {
            using (FileStream fs = new FileStream($"{Directory.CreateDirectory("./wwwroot/images/")}{name}", FileMode.Create))
            {
                gridFS.DownloadToStreamByName(name, fs);
            }
        }

        public string[] GetAllNames()
        {
            var filter = Builders<GridFSFileInfo>.Filter.Empty;
            string[] fileNames = new string[] { };
            using (var cursor = gridFS.Find(filter))
            {
                fileNames = cursor.ToList().Select(x => x.Filename).ToArray();
            }
            return fileNames;
        }

        public void DeleteImage(string name)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", name);
            using (var cursor = gridFS.Find(filter))
            {
                var id = cursor.ToList().Select(x => x.Id).FirstOrDefault();
                gridFS.Delete(id);
            }
        }
    }
}
