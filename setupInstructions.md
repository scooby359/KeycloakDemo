- Create a new realm, e.g. myApp
- Create a new client, e.g. myMobileApp
- Set client to allow username / password sign in and return token:
  - Go to client settings
  - Set client protocol to openid-connect
  - Set access type to public
- Set custom audience
  - Go to Client Scopes
  - Create new scope and save
  - Select Mappers and create new
  - Set mapper type to audience
  - Set Included Custom Audience to client name
  - Return to Clients, select client, then select Scopes tab
  - Add newly created scope to Assigned Default Client Scopes 
- Flatten roles claim
  - Default is two keys to differentiate overall realm roles, and client specific roles:
  ```
  realm_access: { 
    roles: []
  },
  resource_access:{
    account: { 
      roles: []
    }
  }
  ```
  - Dotnet expects roles in a single array, so need to flatten these down.
  - In keycloak / client scopes / roles / mappers / realm roles
  - Default value `realm_access.roles`
  - Change to `roles`
  - Repeat for client roles if using, alternatively can manually check claims in Dotnet if want to be specific about client roles


In the .Net API
- Import Nuget Package Microsoft.AspNetCore.Authentication.JwtBearer
- Setup token authentication - in Startup.cs
```
  using using Microsoft.AspNetCore.Authentication.JwtBearer;
  ...
  public void ConfigureServices(IServiceCollection services)
  {
    ... 
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:8080/auth/realms/MyTestApp";
                    options.Audience = "myApi";
                    options.RequireHttpsMetadata = false; // for local host only to allow http
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Tells the mapper to get the Name from the NameIdentifier field
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    ...
    app.UseAuthorization();
    app.UseAuthentication();
  }  
```
