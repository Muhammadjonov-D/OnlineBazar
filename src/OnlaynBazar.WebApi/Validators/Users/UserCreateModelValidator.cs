﻿using FluentValidation;
using OnlaynBazar.Service.Helpers;
using OnlaynBazar.WebApi.Models.Users;

namespace OnlaynBazar.WebApi.Validators.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.PhoneNumber)
            .NotNull()
            .WithMessage(user => $"{nameof(user.PhoneNumber)} is not specified");

        RuleFor(user => user.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.Email)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Email)} is not specified");

        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(user => user.Password)
            .Must(ValidationHelper.IsPasswordHard);
    }
}
