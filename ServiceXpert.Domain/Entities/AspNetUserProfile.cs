﻿namespace ServiceXpert.Domain.Entities;
public class AspNetUserProfile : EntityBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
