using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    // Repository for user data access
    private readonly IUserRepository _userRepository;

    // Constructor injecting the user repository
    public CreateUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

    // Handles the creation of a new user
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        try
        {
            // Vérifier si l'email existe déjà
            bool emailExists = await _userRepository.EmailExists(request.Email, ct);

            if (emailExists is false)
                return Result<Guid>.Success(default, "User not found.");

            // Valider et créer l'email
            User newUser = User.Create(request.Email, request.Password);
            newUser.SetFirstname(request.FirstName);
            newUser.SetLastname(request.LastName);
            newUser.SetDisplayName(request.UserName);
            newUser.SetDescription(request.Description);
            
            //TODO: Set properties ProfilePhoto.

            // Ajouter l'utilisateur à la base de données
            await _userRepository.Add(newUser, ct);
            
            // Retourner l'Id du nouvel utilisateur
            return Result<Guid>.Success(newUser.Id, "User created successfully");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error creating user: {ex.Message}");
        }   
    }
}
