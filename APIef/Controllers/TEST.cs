
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIef.Models;
using APIef.Services;
using APIef.Data;

namespace APIef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TEST : ControllerBase
    {
        ExternalApiService externalApiService = new ExternalApiService();
        DataContext _dbContext;

        public TEST (DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        
        
        
        

    }
}
