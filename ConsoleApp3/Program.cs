using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var postData = new PostData
        {
            userId= 5,
            id =55,
            title= "Exploring the Vast Frontiers of Space: A Journey into the Cosmos",
            body= "Space, a realm of mystery and wonder, captivates us with its vastness and beauty. Through exploration, we seek to uncover its secrets and understand our place within it.\r\n"
        };
        string baseUrl = "https://jsonplaceholder.typicode.com";
        var apiClient = new ApiClient<PostData>(baseUrl);

        ApiResponse <PostData> postResult = await apiClient.Post(postData);       
        if (postResult.StatusCode == 500)
        {
            Console.WriteLine("An error has occurred:");
            Console.WriteLine($"POST Result - StatusCode: {postResult.StatusCode} Message:{postResult.Message}");
        }
        else
        {
            Console.WriteLine($"POST Result - StatusCode: {postResult.StatusCode} Message: {postResult.Message}\nData:");
            foreach (var item in postResult.Data)
            {
                Console.WriteLine($"\n{item}");
            }
        }



        Console.WriteLine("------------------------------------------------------------------");
        ApiResponse<PostData> getResult = await apiClient.Get();
        if (getResult.StatusCode == 500)
        {
            Console.WriteLine("An error has occurred:");
            Console.WriteLine($"GET Result - StatusCode: { getResult.StatusCode} Message:{ getResult.Message}");
        }
        else
        {
            Console.WriteLine($"GET Result - StatusCode: {getResult.StatusCode} Message: {getResult.Message}\nData:");
            foreach (var item in getResult.Data)
            {
                Console.WriteLine($"\n{item}");
            }
        }
        Console.WriteLine("------------------------------------------------------------------");
        getResult = await apiClient.Get("100");
        if (getResult.StatusCode == 500)
        {
            Console.WriteLine("An error has occurred:");
            Console.WriteLine($"GET Result - StatusCode: {getResult.StatusCode} Message:{getResult.Message}");
        }
        else
        {
            Console.WriteLine($"GET Result - StatusCode: {getResult.StatusCode} Message: {getResult.Message}\nData:");
            foreach (var item in getResult.Data)
            {
                Console.WriteLine($"{item}");
            }
        }


 ;
        



    }
}