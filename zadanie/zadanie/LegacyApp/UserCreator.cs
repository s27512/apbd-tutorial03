using System;

namespace LegacyApp
{
    public class UserCreator
    {
        private ClientRepository clientRepository;

        public UserCreator(ClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public User CreateUser(Client client, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            return user;
        }
    }
}