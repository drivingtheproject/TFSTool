using System;

namespace ExternalTfsService.Models
{
  public class TfsUser
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }


    public TfsUser(int id, string name, string email)
    {
      if (id <= 0)
        throw new ArgumentOutOfRangeException(nameof(id));

      Id = id;
      Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
      Email= string.IsNullOrWhiteSpace(email) ? throw new ArgumentNullException(nameof(email)) : email;
    }
  }
}
