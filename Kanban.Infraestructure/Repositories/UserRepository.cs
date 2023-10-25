using Dapper;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.Repositories
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public UserRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<List<User>> GetAll()
    {
      var spString = "[dbo].[usp_Users_GET]";
      return (await _dbConnection.QueryAsync<User>(spString, transaction: _dbTransaction)).ToList();
    }

    public async Task<User> GetById(int id)
    {
      var spString = "[dbo].[usp_Users_GET] @UserId";
      return await _dbConnection.QuerySingleOrDefaultAsync<User>(
        spString,
        new { UserId = id },
        transaction: _dbTransaction);
    }

    public async Task<User> GetByUsername(string username)
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
          new
          {
            user.RoleId,
            user.FullName,
            user.Username,
            user.Email,
            user.Password,
          },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT Area: " + ex.Message);
      }
    }

    public async Task UpdateUser(User user)
    {
      var spString = "[dbo].[usp_Users_UPD] @UserId, @RoleId, @FullName, @Username, @Email, @Password";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new
          {
            UserId = user.Id,
            user.RoleId,
            user.FullName,
            user.Username,
            user.Email,
            user.Password,
          },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT Area: " + ex.Message);
      };
    }

    public async Task DeleteUser(int id)
    {
      var spString = "[dbo].[Usp_Users_DEL] @UserId";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { UserId = id },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to DELETE Article: " + ex.Message);
      }
    }
  }
}
