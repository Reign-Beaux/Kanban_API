using Dapper;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.Kanban.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.Kanban.Repositories
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public UserRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<List<User>> GetUsers()
    {
      var spString = "[dbo].[usp_Users_GET]";
      return (await _dbConnection.QueryAsync<User>(spString, transaction: _dbTransaction)).ToList();
    }

    public async Task<User> GetUserById(int id)
    {
      var spString = "[dbo].[usp_Users_GET] @Id";
      return await _dbConnection.QuerySingleOrDefaultAsync<User>(
        spString,
        new { Id = id },
        transaction: _dbTransaction);
    }

    public async Task<User> GetByUserName(string username)
    {
      var spString = "[dbo].[Usp_Users_GET] @Username = @Username";
      return await _dbConnection.QuerySingleOrDefaultAsync<User>(
        spString,
        new { Username = username },
        transaction: _dbTransaction);
    }

    public async Task InsertUser(User user)
    {
      var spString = "[dbo].[usp_Users_INS] @RoleId, @FullName, @Username, @Email, @Password";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          user,
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT User: " + ex.Message);
      }
    }

    public async Task UpdateUser(User user)
    {
      var spString = "[dbo].[usp_Users_UPD] @Id, @RoleId, @FullName, @Username, @Email, @Password";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          user,
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT User: " + ex.Message);
      };
    }

    public async Task DeleteUser(int id)
    {
      var spString = "[dbo].[Usp_Users_DEL] @Id";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { Id = id },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to DELETE User: " + ex.Message);
      }
    }
  }
}
