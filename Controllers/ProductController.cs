using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DataAccess;
using Project1.Models;

namespace Project1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ProductController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("Products")]
    [Authorize(Roles = "Admin, User")]
    public IActionResult Get()
    {
        var products = _context.Products.ToList();

        return Ok(products);
    }

    [HttpGet("Products/{id}")]
    [Authorize(Roles = "Admin, User")]
    public IActionResult Get([FromRoute] int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound("Can't find object with id " + id);
        }

        return Ok(product);
    }

    [HttpPost("Products")]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges(); // ! if you don't have this line, it will not update the db

        return Ok(_context.Products);
    }

    [HttpPut("Products")]
    [Authorize(Roles = "Admin")]
    public IActionResult Put([FromBody] Product product)
    {
        var find = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

        if (find == null)
        {
            return NotFound("Can't find object with id " + product.Id);
        }
        
        _context.Products.Update(product);
        _context.SaveChanges();
        
        return Ok(_context.Products);
    }

    [HttpDelete("Products")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete([FromQuery] int id)
    {
        var del = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

        if (del == null) {
            return NotFound("Can't find object with id " + id);
        }
        
        _context.Products.Remove(del);
        _context.SaveChanges();

        return Ok(_context.Products);
    }
}
