namespace Workload.Auth.Configuration;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int ExpirationInDays { get; set; }
}

//TODO Add usersecrets according to following:
//Needs following structure in UserSecrets:
//"ConnectionStrings": {
//    "IdentityDb": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=identity;Integrated Security=True"
//  },
//  "JwtSettings": {
//    "Issuer": "https://energibolagetapi.azurewebsites.net",
//    "Secret": "veryVerySuperSecretKey",
//    "ExpirationInDays": 30
//  }

