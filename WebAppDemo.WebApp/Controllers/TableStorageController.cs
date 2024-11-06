using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.ServiceDefaults.Model;
using WebAppDemo.ServiceDefaults.NewFolder;

namespace WebAppDemo.WebApp.Controllers
{
    public class TableStorageController : Controller
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly string _tableName = "ProshoreUser"; // replace with your table name

        public TableStorageController(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Ensure the table is created if it doesn't exist
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            await tableClient.CreateIfNotExistsAsync();

            // Retrieve the entities from the table
            var entities = new List<User>();
            await foreach (var entity in tableClient.QueryAsync<User>())
            {
                entities.Add(entity);
            }

            ViewBag.Users = entities;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(User user)
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            await tableClient.AddEntityAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Retrieve(string partitionKey, string rowKey)
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            var entity = await tableClient.GetEntityAsync<User>(partitionKey, rowKey);

            ViewBag.User = entity.Value; // Passing the single user to the view
            return View();
        }
    }
}
