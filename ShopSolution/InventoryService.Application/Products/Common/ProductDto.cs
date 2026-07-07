namespace InventoryService.Application.Products.Common;

public record ProductDto(int Id, string Name, string Description, decimal Price, int StockQuantity);
