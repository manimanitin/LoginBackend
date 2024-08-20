using LoginBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DecksController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    public DecksController(
        UserManager<IdentityUser> userManager,
        IConfiguration config,
        SignInManager<IdentityUser> signInManager,
        ApplicationDbContext dbContext
        )
    {
        _userManager = userManager;
        _config = config;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }
    


    [HttpGet("{userId}")]
    public async Task<ActionResult<Decks>> GetDecks(string userId)
    {
        var decks = _dbContext.Decks.Where(d => d.UserId == userId).ToList();

        if (decks == null)
        {
            return NotFound();
        }

        return Ok(decks);
    }

    [HttpPost]
    public async Task<ActionResult<Decks>> PostDecks(Decks decks)
    {
        if (DecksExists(decks.UserId))
        {
            var existingEntity = await _dbContext.Decks.FirstOrDefaultAsync(e => e.UserId == decks.UserId);
            existingEntity.ExtraDeckList = decks.ExtraDeckList;
            existingEntity.DeckList = decks.DeckList;
            await _dbContext.SaveChangesAsync();

            return Ok(existingEntity);


        }
        else
        {

            _dbContext.Decks.Add(decks);

        }
        await _dbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpDelete()]
    public async Task<ActionResult<Decks>> DeleteDecks(Decks decks)
    {
        var myEntity = await _dbContext.Decks.FirstOrDefaultAsync(e => e.UserId == decks.UserId);
        if (myEntity == null)
        {
            return NotFound();
        }

        Console.WriteLine(myEntity);
        _dbContext.Decks.Remove(myEntity);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }


    private bool DecksExists(string userId)
    {
        Console.WriteLine(_dbContext.Decks.Any(e => e.UserId == userId));
        return _dbContext.Decks.Any(e => e.UserId == userId);
    }

}


