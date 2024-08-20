using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginBackend.Models;
public class Decks
{

    [Key]
    public int id { get; set; }
    [Required]
    public string UserId { get; set; }
    public int[] DeckList { get; set; }
    public int[] ExtraDeckList { get; set; }


}


