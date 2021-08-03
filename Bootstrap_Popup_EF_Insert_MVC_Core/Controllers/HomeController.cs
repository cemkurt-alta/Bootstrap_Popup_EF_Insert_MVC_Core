using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Bootstrap_Popup_EF_Insert_MVC_Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace Bootstrap_Popup_EF_Insert_MVC_Core.Controllers
{
    public class HomeController : Controller
    {
        private DBCtx Context { get; }
        private readonly IMapper _mapper;
        public HomeController(DBCtx _context, IMapper mapper)
        {
            _mapper = mapper;
            this.Context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CustomerRequestDto customer)
        {
            var mapper = _mapper.Map(customer, new Customer());

         

            this.Context.Customers.Add(mapper);
            this.Context.SaveChanges();

            //Fetch the CustomerId returned via SCOPE IDENTITY.
            customer.CustomerId = mapper.CustomerId;

            return View(customer);
        }
    }
}
public class CustomerRequestDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    [Required]
    public string Country { get; set; }
}

public class CreateCustomerModelValidator : AbstractValidator<CustomerRequestDto>
{
    public CreateCustomerModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("The first name must be at least 2 character long");

        RuleFor(x => x.Name)
            .MinimumLength(2).
            WithMessage("The first name must be at least 2 character long");

        RuleFor(x => x.Country)
            .MinimumLength(2)
            .WithMessage("The last name must be at least 2 character long");
    }
}

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerRequestDto>()
            .ReverseMap();

        // ForMember is used incase if any field doesn't match  
    }
}