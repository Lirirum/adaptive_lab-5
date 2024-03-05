using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class ApiResponse<T>
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public List<T> Data { get; set; }
}





public class ApiClient<T>
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private  string baseUrl ;
    public ApiClient( string baseUrl )
    {
        this.baseUrl = baseUrl;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(this.baseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ApiResponse<T>> Get()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"posts/");
            response.EnsureSuccessStatusCode();
            var responseData= await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseData);
            List<T> data = JsonSerializer.Deserialize<List<T>>(responseData);
            return new ApiResponse<T> {Message= "Success", StatusCode = (int)response.StatusCode, Data = data };

        }
        catch (Exception ex)
        {
            return new ApiResponse<T> { StatusCode = 500, Message = ex.Message };
        }
    }

    public async Task<ApiResponse<T>> Get(string id )
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"posts/{id}");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseData);
            List<T> data = [JsonSerializer.Deserialize<T>(responseData),];
            return new ApiResponse<T> { Message = "Success", StatusCode = (int)response.StatusCode, Data = data };

        }
        catch (Exception ex)
        {
            return new ApiResponse<T> { StatusCode = 500, Message = ex.Message };
        }
    }



    public async Task<ApiResponse<T>> Post(PostData payload)
    {
        try
        {
            string jsonPayload = JsonSerializer.Serialize(payload);
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");          
            HttpResponseMessage response = await _httpClient.PostAsync("posts", content);
            response.EnsureSuccessStatusCode();
                
            string responseBody = await response.Content.ReadAsStringAsync();
            T data = JsonSerializer.Deserialize<T>(responseBody);            
            return new ApiResponse<T> { Message= "Success", StatusCode = (int)response.StatusCode, Data = new List<T> { data } };
        }
        catch (Exception ex)
        {
            return new ApiResponse<T> { StatusCode = 500, Message = ex.Message };
        }
    }
}
