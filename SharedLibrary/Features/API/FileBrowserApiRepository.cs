using Newtonsoft.Json;
using System.Text.Json;

namespace SharedLibrary.Features;

public class FileBrowserApiRepository<FlatFileEntry> : IFileBrowserApiRepository<FlatFileEntry>
{
    string _controllerName;
    string _rootPath;
    HttpClient _http;

    JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };


    public FileBrowserApiRepository(HttpClient http, string controllerName, string rootPath)
    {
        _http = http;
        _controllerName = controllerName;
        _rootPath = rootPath;
    }


    public async Task<List<FlatFileEntry>> ReadDirectoryAsync(string rootPath)
    {
        try
        {
            if (rootPath != null)
            {
                var url = $"{_controllerName}/ReadDirectoryAsync/{rootPath}";
                var result = await _http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                // Baseline Method
                string responseBody = await _http.GetStringAsync(url);
                var response = System.Text.Json.JsonSerializer.Deserialize<List<FlatFileEntry>>(responseBody, jsonOptions);

                // Abreviated Method
                string responseBody2 = await _http.GetStringAsync(url);
                var response2 = System.Text.Json.JsonSerializer.Deserialize<List<FlatFileEntry>>(responseBody2, jsonOptions);
                
                // Newtonsoft JSON Method
                string responseBody3 = await result.Content.ReadAsStringAsync();
                var response3 = JsonConvert.DeserializeObject<List<FlatFileEntry>>(responseBody3);

                if (result!.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    var _data = new List<FlatFileEntry>();
                    return _data;
                }
            }
            else
            {
                var _data = new List<FlatFileEntry>();
                return _data;
            }
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            var _data = new List<FlatFileEntry>();
            return _data;
        }
    }
}
