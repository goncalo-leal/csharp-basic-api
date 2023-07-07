using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    readonly DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet(Name="GetAllUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _dapper.LoadData<User>(
            "SELECT * FROM TutorialAppSchema.Users"
        );
    }

    [HttpGet("{userId}", Name="GetUserById")]
    public IActionResult GetUser(int userId)
    {
        User? x = _dapper.LoadDataSingle<User>(
            "SELECT * FROM TutorialAppSchema.Users WHERE UserId = " + userId.ToString()
        );

        if (x == null)
        {
            return NotFound(new KeyNotFoundException("User not found"));
        }
        return Ok(x);
    }

    [HttpPut]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.Users
            SET FirstName = '"+ user.FirstName +
            "', LastName = '"+ user.LastName +
            "', Email = '"+ user.Email +
            "', Gender = '"+ user.Gender +
            "', Active = '"+ user.Active +
            "' WHERE UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    [HttpPost]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.Users (
                FirstName,
                LastName,
                Email,
                Gender,
                Active
            ) VALUES ('" +
                user.FirstName +
                "','" + user.LastName +
                "','" + user.Email +
                "','" + user.Gender +
                "','" + user.Active +
            "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to create user");
    }

    [HttpDelete(Name="DeleteUser")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.Users
            WHERE UserId = "+ userId +
        "";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to delete user");
    }

    // ------------------------

    [HttpGet("{userId}/salary", Name="GetUserSalary")]
    public IActionResult GetUserSalary(int userId)
    {
        UserSalary? x = _dapper.LoadDataSingle<UserSalary>(
            "SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId
        );

        if (x == null)
        {
            return NotFound(new KeyNotFoundException("User not found"));
        }
        return Ok(x);
    }

    [HttpPut("{userId}/salary", Name="UpdateUserSalary")]
    public IActionResult UpdateUserSalary(int userId, UserSalaryDto userSalaryDto)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary
            SET Salary = "+ userSalaryDto.Salary +
            " WHERE UserId = " + userId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User Salary");
    }

    [HttpPost("{userId}/salary", Name="CreateUserSalary")]
    public IActionResult CreateUserSalary(int userId, UserSalaryDto userSalaryDto)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary (
                UserId,
                Salary
            ) VALUES ("
                + userId + ", "
                + userSalaryDto.Salary +
            ")";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Create User Salary");
    }

    [HttpDelete("{userId}/salary", Name="DeleteUserSalary")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserSalary
            WHERE UserId = "+ userId +
        "";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to delete user salary");
    }
}
