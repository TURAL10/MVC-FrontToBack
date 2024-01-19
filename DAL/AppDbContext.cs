using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.Models.DemoEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4.DAL
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<SliderContent> SliderContents { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<AboutListContent> AboutListContents { get; set; }
        public DbSet<AboutContent> AboutContents { get; set; }

        public DbSet<ExpertContent> ExpertContents { get; set; }
        public DbSet<ExpertImage> ExpertImages { get; set; }
        public DbSet<Experts> Experts { get; set; } 

        public DbSet<BlogContent> BlogContents { get;set;}
        public DbSet<BlogImages> BlogImages { get; set; }

        public DbSet<SaySliderContent> SaySliderContents { get; set;}

        public DbSet<SaySliderImage> SaySliderImages { get; set;}

        public DbSet<InstagramSlider> instagramSliders { get; set; }

        public DbSet<Bio> Bios { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenres> BookGenres { get; set; }

        public DbSet<BookAuthor> BookAuthors {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),

                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),

                }
                );
        }

    }
}

