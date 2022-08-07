using crud.Entities;
using crud.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace crud.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly DatabaseContext _context;
    
    public UsersController(DatabaseContext context)
    {
        this._context = context;
    }
    /// <summary>
    ///  Retrieve the list of users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        var users = this._context.Users.ToList(); //Where(e => true).ToList();

        return Ok(users);
    }
    
    /// <summary>
    /// Create ane records
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(User user)
    {
        this._context.Users.Add(user);
        this._context.SaveChanges();

        return Ok(user);
    }
    
    /// <summary>
    /// Return one record
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    [HttpGet("{id}")]
    public IActionResult findOne(int id)
    {
        var user =  this._context.Users.Find(id);
        
        if (user == null) throw new KeyNotFoundException("User not found in database");

        return Ok(user);
    }
    
    [HttpGet("/seach")]
    public IActionResult seach( [FromQuery] String? query )
    {
        var user =  this._context.
            Users.Where(u => u.FirsName.ToLower().StartsWith(query.ToLower()) 
                               || u.LasName.ToLower().Contains(query.ToLower()) );
        
        if (user == null) throw new KeyNotFoundException("User not found in database");

        return Ok(user);
    }
    
    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    [HttpPatch("{id}")]
    public IActionResult update(int id, User user)
    {
        var userFound =  this._context.Users.Find(id);
        if (userFound == null) throw new KeyNotFoundException("User not found in database");

        userFound.Address = user.Address;
        userFound.Email = user.Email;
        userFound.FirsName = user.FirsName;
        userFound.LasName = user.LasName;
        userFound.Password = user.Password;

        _context.Users.Update(userFound);

        _context.SaveChanges();

        return Ok(userFound);
    }
    
     
    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    [HttpDelete("{id}")]
    public IActionResult delete(int id)
    {
        var user =  this._context.Users.Find(id);
        
        if (user == null) throw new KeyNotFoundException("User not found in database");

        this._context.Users.Remove(user);
        this._context.SaveChanges();

        return Ok("Suppression réussit");
    }


    
}