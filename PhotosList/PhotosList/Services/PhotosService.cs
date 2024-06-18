using PhotosList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhotosList.Services
{
    public class PhotosService
    {
        private HttpClient client;
        private Photo photo;
        private List<Photo> photos;
        private JsonSerializerOptions _serializerOptions;


        public PhotosService() { 
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Photo>> getPhotosAsync()
        {
            Uri uri = new Uri("https://jsonplaceholder.typicode.com/photos");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    photos = JsonSerializer.Deserialize<List<Photo>>(content, _serializerOptions);
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return photos;
        }
    }
}
