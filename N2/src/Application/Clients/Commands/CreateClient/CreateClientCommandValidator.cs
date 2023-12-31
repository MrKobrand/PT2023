﻿namespace Application.Clients.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Name length must not be more than 256 symbols.");

        RuleFor(v => v.Address)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Address length must not be more than 256 symbols.");
    }
}
