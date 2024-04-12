using System.Security.Cryptography.X509Certificates;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }
        //GET All action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        //GET by Id Action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id){
            var pizza = PizzaService.Get(id);
            if(pizza == null)
                return NotFound();
            return Ok(pizza);
        }

        //POST Action
        [HttpPost]
        public IActionResult Create(Pizza pizza){
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        //PUT Action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza){
            if(id != pizza.Id)
                BadRequest();
            
            var existingPizza = PizzaService.Get(id);
            if(existingPizza is null)
                NotFound();
            
            PizzaService.Update(id,pizza);
            return NoContent();
        }

        //DELETE Action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            
            var existingPizza = PizzaService.Get(id);
            if( existingPizza is null) 
                NotFound();

            PizzaService.Delete(id);
            return NoContent();
        }
}

