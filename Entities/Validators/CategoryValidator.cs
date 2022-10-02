using Entities.DataTransferObjects;
using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name can't be longer than 50 characters");
            
            RuleFor(c => c.NepaliName)
                .NotEmpty().WithMessage("Category name in Nepali is required")
                .MaximumLength(50).WithMessage("Category name can't be longer than 50 characters");
            
            RuleFor(c => c.Description)
                .MaximumLength(250).WithMessage("Category description can't be longer than 250 characters");
            

        }
    }
}
