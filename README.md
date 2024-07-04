This repository contains two projects:

- An ASP.NET Core MVC application integrated with Auth0 authentication in the `HealthCheckWebApp` folder.
- An ASP.NET Core Web API protected with Auth0 in the `HealthCheckApi` folder.

Check out the article [Use Private Key JWTs to Authenticate Your .NET Application](https://auth0.com/blog/use-private-key-jwt-to-authenticate-dotnet-app/) for the implementation details.

# Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine
- Visual Studio 2022 (optional)

# To run this application

1. Clone the repo with the following command:

   ```bash
   git clone https://github.com/auth0-blog/dotnet-private-key-jwt-authetication.git
   ```

2. Move to the `HealthCheckApi` folder.

3. Add your Auth0 domain and API identifier to the `appsettings.json` configuration file. Take note of the API identifier.

4. Type `dotnet run` in a terminal window to launch the API.

5. Move to the `HealthCheckWebApp` folder.

6. Create a private and public key pair by running the following command:

  ```bash
  openssl genrsa -out app_keys.pem 2048
  ```

7. Extract the public key with the following command:

   ```bash
   openssl rsa -in app_keys.pem -outform PEM -pubout -out pub_key.pem
   ```

8. Use the public key in the `pub_key.pem` file to [configure Private Key JWT authentication](https://auth0.com/docs/get-started/applications/configure-private-key-jwt) in your Auth0 dashboard.

9. Add your Auth0 domain and client ID to the `appsettings.json` configuration file. Also, assign the API identifier value to the `Audience` key.

7. Type `dotnet run` in a new terminal window to launch the application.

8. Point your browser to the [https://localhost:7062](https://localhost:7062) address.