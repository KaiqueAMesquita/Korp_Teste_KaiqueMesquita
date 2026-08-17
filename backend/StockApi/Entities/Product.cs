using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace  StockApi.Entities;

public class Product
{
    public Guid Id {get; set;}
    public required string Code {get;set;}

    public required string Description {get;set;}

    [Range(0,int.MaxValue)]
    public int Balance {get; set;}




}