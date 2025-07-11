using Core.Entity;

namespace Core.Dto;

public record UpdateUser(string? Email, string? Password, Role? Role);