namespace API.Data
{
  public class Seed
  {
    public static async Task SeedUsers(DataContext context) {
      if (await context.Users.AnyAsync()) return;

      var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

      var users = JsonSerializer.Deserialize<List<AppUser>>(UserData);

      foreach (var user in users) {
        using var hmac = new HMACSHA512();
        user.UserName = user.UserName.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
        user.PasswordSalt = hmac.key;

        context.Users.Add(user);
      }

      await context.SaveChangesAsync();
    }
  }
}