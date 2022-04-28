﻿using System.ComponentModel.DataAnnotations;

namespace INF27507_Boutique_En_Ligne.Models.FormData;

public class UserInfo
{
    [Required, StringLength(32, MinimumLength = 2)]
    public string LastName { get; set; }
    [Required, StringLength(32, MinimumLength = 2)]
    public string Firstname { get; set; }
    
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public double Balance { get; set; }

    public static UserInfo CreateFrom(Client client)
    {
        return new UserInfo()
        {
            Email = client.Email,
            Balance = client.Balance,
            Firstname = client.Firstname,
            LastName = client.Lastname,
            Phone = client.PhoneNumber
        };
    }
}