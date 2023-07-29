using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Domain.Entities;

public class InvoiceFile : File
{
    [Column(TypeName = "decimal(18, 2)")] public decimal Price { get; set; }
}