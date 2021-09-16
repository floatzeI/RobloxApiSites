// I'm way too lazy to make ClientBase work with Multipart and streams. If anyone wants to refactor it to use ClientBase, go ahead...

using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Roblox.Files.Client
{
    public class FilesV1Client : IFilesV1Client
    {
        private HttpClient clientBase { get; set; }
        private string baseUrl { get; set; }
        public FilesV1Client(string baseUrl, string authorization)
        {
            clientBase = new HttpClient();
            this.baseUrl = baseUrl;
            clientBase.DefaultRequestHeaders.Add("roblox-authorization", authorization);
        }

        public async Task<Stream> GetFileById(string fileId)
        {
            return await clientBase.GetStreamAsync(baseUrl + "/v1/GetFile?fileId=" + fileId);
        }

        public async Task<string> UploadFile(string mimeType, Stream fileToUpload)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(mimeType), "mime");
            content.Add(new StreamContent(fileToUpload), "file");
            var response = await clientBase.PostAsync(baseUrl + "/v1/UploadFile", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Unexpected Upload Response: " + response.StatusCode);
            var txt = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Models.UploadResponse>(txt).fileId;
        }
    }
}