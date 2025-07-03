using System.Security.Claims;
using BlazorProductStore.DAL;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(string connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        // This is our main "upsert" method
        public User FindOrCreateUser(ClaimsPrincipal claimsPrincipal)
        {
            // 1. Extract the required claims from the logged-in user
            var googleId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var name = claimsPrincipal.FindFirstValue(ClaimTypes.Name);

            // A user must have a Google ID to be valid
            if (string.IsNullOrEmpty(googleId))
            {
                throw new InvalidOperationException("User does not have a valid Google ID.");
            }

            // 2. Check if the user already exists in our database
            var user = _userRepository.FindByGoogleId(googleId);

            // 3. If the user does not exist, create a new one
            if (user == null)
            {
                var newUser = new User
                {
                    GoogleId = googleId,
                    Email = email ?? string.Empty, // Ensure email/name are not null
                    Name = name ?? string.Empty
                };
                user = _userRepository.Add(newUser);
            }

            // 4. Return the found or newly created user
            return user;
        }
    }
}