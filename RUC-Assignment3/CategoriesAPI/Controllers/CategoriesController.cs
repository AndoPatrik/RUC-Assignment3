using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoriesAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<Category> CategoriesPersistency = new List<Category>()
        {
            new Category(1,"Beverages"),
            new Category(2,"Condiments"),
            new Category(3, "Confections")
        };

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get() => CategoriesPersistency;

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Category Get(int id) => CategoriesPersistency.Find(c => c.Cid == id);
  

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] Category categoryToInsert) => CategoriesPersistency.Add(new Category(CategoriesPersistency.Count + 1, categoryToInsert.Name));


        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category categoryToUpdate) => CategoriesPersistency.Find(c => c.Cid == id).Name = categoryToUpdate.Name;


        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id) => CategoriesPersistency.Remove(CategoriesPersistency.Find(c => c.Cid == id));
    }
}
