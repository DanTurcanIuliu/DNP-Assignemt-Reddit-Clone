﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class User
{

    public int Id { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    
    // public ICollection<Post> Posts { get; } = new List<Post>();
}