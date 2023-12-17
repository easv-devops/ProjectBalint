using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.DataModels.Enums;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Route("/api/getAccounts")]
    public ResponseDto GetAllAccounts()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all accounts.",
            ResponseData = _accountService.GetAllAccounts()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createAccount")]
    public ResponseDto CreateAccount([FromBody] CreateAccountRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a account.",
            ResponseData = _accountService.CreateAccount(dto.Name, dto.Email, dto.Password,
                (AccountRank)Enum.ToObject(typeof(AccountRank), dto.Rank))
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateAccount")]
    public ResponseDto UpdateAccount([FromBody] UpdateAccountRequestDto dto)
    {
        var userRankClaim = User.Claims.FirstOrDefault(c => c.Type == "rank")?.Value; //receives null, fix later, most likely cors problem (will not be present in live env)
        //Console.WriteLine(userRankClaim);
        if (true )//!string.IsNullOrEmpty(userRankClaim) && IsAllowedToDeleteAccount(userRankClaim)) [develop later]
        {
            var account = new AccountQuery
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Rank = dto.Rank
            };
            _accountService.UpdateAccount(account);
            return new ResponseDto()
            {
                MessageToClient = "Successfully updated account.",
            };
        }
        else
        {
            return new ResponseDto()
            {
                MessageToClient = "You're not authorized to update this account."
            };
        }
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteAccount/{id:int}")]
    public ResponseDto DeleteAccount([FromRoute] int id)
    {
        //var userRankClaim = User.Claims.FirstOrDefault(c => c.Type == "rank")?.Value;
        
        if (true)//!string.IsNullOrEmpty(userRankClaim) && IsAllowedToDeleteAccount(userRankClaim))
        {
            _accountService.DeleteAccount(id);
            return new ResponseDto()
            {
                MessageToClient = "Successfully deleted account."
            };
        }
        else
        {
            return new ResponseDto()
            {
                MessageToClient = "You're not authorized to delete this account."
            };
        }
    }

    private bool IsAllowedToDeleteAccount(string userRankClaim)
    {
        return true;
    }

    [HttpPut]
    [Route("/api/checkPassword")]
    public ResponseDto CheckPassword([FromBody] CredentialCheckRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully checked passwords.",
            ResponseData = _accountService.CheckCredentials(dto.Username, dto.Password)
        };
    }
}