
using FluentValidation;

namespace ECommerceAPI.Application.Validations.Product;

public class CreateProductValidator 
{
    // public CreateProductValidator()
    // {
    //     : AbstractValidator<VM_CreateProduct>
    //     RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Ürünün ismini giriniz")
    //         .MaximumLength(150).MinimumLength(2).WithMessage("Ürün adı 2-150 karakter arası olmak zorundadır.");
    //     RuleFor(x => x.Stock).NotNull().NotEmpty().Must(stock => stock != "0")
    //         .WithMessage("Stok adedi 0'dan büyük olmak zorundadır.")
    //         .NotEmpty().WithMessage("Stok adedini giriniz.");
    //     RuleFor(x => x.Price).NotNull().WithMessage("Ürünün fiyatını giriniz.")
    //         .Must(x => x >= 0).WithMessage("Ürün fiyatı negatif olamaz");
    // }
}