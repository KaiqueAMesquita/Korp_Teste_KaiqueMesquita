using System.Net;
using System.Text.Json;
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
    try
    {
        var response = await client.GetAsync($"/api/Product/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao consultar o serviço de estoque.");

        return await response.Content.ReadFromJsonAsync<StockProductResponse>();
    }
    catch (HttpRequestException)
    {
        throw new HttpRequestException(
            "O serviço de estoque está temporariamente indisponível."
        );
    }
}
public async Task debit(DebitStockRequestDto dto)
{
    try
    {
        var response = await client.PostAsJsonAsync("/api/stock/debit", dto);

        if (response.IsSuccessStatusCode)
            return;

        var message = await getErrorMessage(response);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new ArgumentException(message);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new KeyNotFoundException(message);

        if (response.StatusCode == HttpStatusCode.Conflict)
            throw new InvalidOperationException(message);

        throw new Exception("Erro inesperado no serviço de estoque.");
    }
    catch (HttpRequestException)
    {
        throw new HttpRequestException(
            "O serviço de estoque está temporariamente indisponível."
        );
    }
}

private async Task<string> getErrorMessage(HttpResponseMessage response)
{
    try
    {
        var content = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(content);

        if (json.RootElement.TryGetProperty("message", out var message))
            return message.GetString() ?? "Erro ao processar estoque.";
    }
    catch
    {
    }

    return "Erro ao processar estoque.";
}
}