﻿using ProductManagerApi.Enums;

namespace ProductManagerApi.Dtos;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}