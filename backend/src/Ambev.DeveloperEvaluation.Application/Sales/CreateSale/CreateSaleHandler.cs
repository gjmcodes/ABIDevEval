using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;


        public Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            //var validator = new CreateSaleCommandValidator();
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);

            //if (!validationResult.IsValid)
            //    throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException($"User with email {command.Email} already exists");

            var user = _mapper.Map<User>(command);
            user.Password = _passwordHasher.HashPassword(command.Password);

            var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
            var result = _mapper.Map<CreateUserResult>(createdUser);
            return result;
        }
    }
}
