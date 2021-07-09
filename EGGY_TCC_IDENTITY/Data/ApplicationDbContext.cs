using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EGGY_TCC_IDENTITY.Models;
using System.Collections.Generic;
using System.Text;

namespace EGGY_TCC_IDENTITY.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TB_USUARIO> TB_USUARIO { get; set; }
        public DbSet<TB_STATUS_USUARIO> TB_STATUS_USUARIO { get; set; }
        public DbSet<TB_ONG> TB_ONG { get; set; }
        public DbSet<TB_STATUS_ONG> TB_STATUS_ONG { get; set; }
        public DbSet<TB_IMAGEM> TB_IMAGEM { get; set; }
        public DbSet<TB_NOTICIA> TB_NOTICIA { get; set; }
        public DbSet<TB_APOIADOR> TB_APOIADOR { get; set; }
        public DbSet<TB_ONG_APOIADOR> TB_ONG_APOIADOR { get; set; }
        public DbSet<TB_NIVEL_ACESSO> TB_NIVEL_ACESSO { get; set; }
    }
}
