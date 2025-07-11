using Core.ValueObjects;

namespace Core.Dto;

public record CreateUser(string Email,string Password, string RoleType);
