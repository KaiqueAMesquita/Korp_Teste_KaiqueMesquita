using System.Net;
using BillingApi.Clients;
using BillingApi.DTOs;

public class StockClient : IStockClient
{
    private readonly HttpClient client;

public StockClient(IHttpClientFactory factory)
{
    client = factory.CreateClient("StockApi");

}

   

public async Task<StockProductResponse?> getProductById(Guid id)
{
    var response = await client.GetAsync(
        $"/api/Product/{id}"
    );

    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        return null;

    response.EnsureSuccessStatusCode();

    return await response.Content
        .ReadFromJsonAsync<StockProductResponse>();
    }
public async Task debit(DebitStockRequestDto dto)
{
    var response = await client.PostAsJsonAsync(
        "/api/stock/debit",
        dto
    );

    response.EnsureSuccessStatusCode();
}
}