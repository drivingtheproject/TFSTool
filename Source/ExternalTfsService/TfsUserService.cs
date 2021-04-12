using ExternalTfsService.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalTfsService
{
  public class TfsUserService : ITfsUserService
  {
    private List<TfsUser> _tfsUsers;

    public TfsUserService()
    {
      _tfsUsers = new List<TfsUser>()
      {
        new TfsUser(1, "Steve", "steve@sap.com"),
        new TfsUser(2, "Jessica", "jessica@sap.com"),
        new TfsUser(3, "Nicole", "nicole@sap.com"),
        new TfsUser(4, "Peter", "peter@sap.com"),
        new TfsUser(5, "Jerry", "jerry@sap.com")
      };
    }

    public IEnumerable<TfsUser> GetAllUsers()
    {
      // Initial time to get connected to the TFS
      Thread.Sleep(500);

      foreach (var tfsUser in _tfsUsers)
      {
        // For any reason the complex list creation needs some time...
        Thread.Sleep(10);
        yield return tfsUser;
      }
    }


    public async IAsyncEnumerable<TfsUser> GetAllUsersAsync()
    {
      // Initial time to get connected to the TFS
      await Task.Delay(500).ConfigureAwait(false);

      foreach (var tfsUser in _tfsUsers)
      {
        // For any reason the complex list creation needs some time...
        await Task.Delay(10);
        yield return tfsUser;
      }
    }
  }
}
