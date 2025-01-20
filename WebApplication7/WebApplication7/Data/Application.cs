using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using WebApplication7.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data;

public class Application : IdentityDbContext
{
    public Application (DbContextOptions<Application> options) : base(options)
    {
        
    }

public DbSet<WebApplication7.Models.EventModel> EventModel { get; set; } = default!;


    
}