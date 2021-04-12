using System.Collections.Generic;
using ExternalTfsService.Models;

namespace ExternalTfsService
{
  public interface ITfsUserService
  {
    IEnumerable<TfsUser> GetAllUsers();

    IAsyncEnumerable<TfsUser> GetAllUsersAsync();
  }
}