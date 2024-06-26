﻿using System.ComponentModel.DataAnnotations;

namespace filmsAPI.Models;

public class MovieTheater
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "The Name of the Movie Theather is mandatory")]
    public string Name { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
    public virtual ICollection<Section> Sections { get; set; }
}
