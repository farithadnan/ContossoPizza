using ContossoPizza.Models;
using ContossoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContossoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {

    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => 
        PizzaService.GetAll();


    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if(pizza is null)
            return NotFound();

        return pizza;
    }


    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        // First param of createdAtAction method call represents an action name
        // nameof keyword is used to avoid hard-coding the action name
        // the action name is required to generate a location http response header with url to the newly created pizza
        return CreatedAtAction(nameof(Create), new {id = pizza.Id}, pizza);
    }


    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }


    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}