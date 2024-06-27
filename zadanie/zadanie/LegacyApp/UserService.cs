using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            // if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            // {
            //     return false;
            // }    
            UserValidator userValidator = new UserValidator();
            if (!userValidator.ValidateNull(firstName, lastName))
            {
                return false;
            }

            if (!userValidator.ValidateEmail(email))
            {
                return false;
            }

            // var now = DateTime.Now;
            // int age = now.Year - dateOfBirth.Year;
            // if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            //
            // if (age < 21)
            // {
            //     return false;
            // }

            if (userValidator.ValidateAge(dateOfBirth))
            {
                return false;
            }

            // var clientRepository = new ClientRepository();
            // var client = clientRepository.GetById(clientId);
            //
            // var user = new User
            // {
            //     Client = client,
            //     DateOfBirth = dateOfBirth,
            //     EmailAddress = email,
            //     FirstName = firstName,
            //     LastName = lastName
            // };

            ClientRetriever clientRetriever = new ClientRetriever(new ClientRepository());
            var client = clientRetriever.retrieveClient(clientId);
            
            UserCreator userCreator = new UserCreator(new ClientRepository());
            var user = userCreator.CreateUser(client, firstName, lastName, email, dateOfBirth);

            // if (client.Type == "VeryImportantClient")
            // {
            //     user.HasCreditLimit = false;
            // }
            // else if (client.Type == "ImportantClient")
            // {
            //     using (var userCreditService = new UserCreditService())
            //     {
            //         int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            //         creditLimit = creditLimit * 2;
            //         user.CreditLimit = creditLimit;
            //     }
            // }
            // else
            // {
            //     user.HasCreditLimit = true;
            //     using (var userCreditService = new UserCreditService())
            //     {
            //         int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            //         user.CreditLimit = creditLimit;
            //     }
            // }

            CreditLimitOperations creditLimitOperations = new CreditLimitOperations(client, user);
            creditLimitOperations.adjustCreditLimit();

            // if (user.HasCreditLimit && user.CreditLimit < 500)
            // {
            //     return false;
            // }

            if (!creditLimitOperations.HasLimit())
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
