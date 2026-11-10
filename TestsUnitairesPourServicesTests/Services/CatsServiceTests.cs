using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsUnitairesPourServices.Data;
using TestsUnitairesPourServices.Models;
using TestsUnitairesPourServices.Exceptions;

namespace TestsUnitairesPourServices.Services.Tests
{
    [TestClass()]
    public class CatsServiceTests
    {
        ApplicationDBContext _dbContext;

        private const int RACIST_HOUSE_ID = 1;
        private const int RETARD_HOUSE_ID = 2; 

private const int CHAT_AVEC_MAISON_ID = 1;
        private const int CHAT_SANS_MAISON_ID = 2;
      
        public CatsServiceTests()
        {
        }



        [TestInitialize]
        public void Init() {

            DbContextOptions<ApplicationDBContext> options = new DbContextOptionsBuilder<ApplicationDBContext>()
                    .UseInMemoryDatabase(databaseName: "CatDatabase").UseLazyLoadingProxies(true).Options;

  _dbContext = new ApplicationDBContext(options);
            

            Cat cat1 = new Cat()
            {
                Id = CHAT_SANS_MAISON_ID, 
                Name = "Homeless Cat",
                Age = 4,
              
            };

            _dbContext.Cat.Add(cat1);
            _dbContext.SaveChanges();

            House house1 = new House()
            {
                Id = RACIST_HOUSE_ID, 
                Address = "Ohio Street", 
                OwnerName = "Gaku"

            };

            _dbContext.House.Add(house1);
            _dbContext.SaveChanges();

            Cat cat2 = new Cat()
            {
                Id = CHAT_AVEC_MAISON_ID,
                Name = "Rich ass Cat",
                Age = 5,
                House = house1
            };

            _dbContext.Cat.Add(cat2);
            _dbContext.SaveChanges();

            House house2 = new House()
            {
                Id = RETARD_HOUSE_ID,
                Address = "Retard Street",
                OwnerName = "Gaku"
            };
            _dbContext.Add(house2);
            _dbContext.SaveChanges();










        }


        [TestCleanup]
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        [TestMethod()]
        public void MoveTest()
        {
            CatsService catservice = new CatsService(_dbContext);

            var maison1 = _dbContext.House.Find(RACIST_HOUSE_ID);
            var maison2 = _dbContext.House.Find(RETARD_HOUSE_ID);

            var newhouse = catservice.Move(CHAT_AVEC_MAISON_ID, maison1, maison2);

          

            Assert.IsNotNull(newhouse);
            

         
        }

        [TestMethod()]
        public void MoveUnknownCat()
        {
            CatsService catservice = new CatsService(_dbContext);

            var maison1 = _dbContext.House.Find(RACIST_HOUSE_ID);
            var maison2 = _dbContext.House.Find(RETARD_HOUSE_ID);

            //Cat unknownCat = new Cat()
            //{
            //    Id = 3,
            //    Name = "Boonk gang Cat",
            //    Age = 3,
            //    House = maison1
            //};



            var newhouse = catservice.Move(3, maison1, maison2);

            Assert.IsNull(newhouse); 

        }

        [TestMethod]
        public void MoveCatNoHouse()
        {
            CatsService catservice = new CatsService(_dbContext);

            var maison1 = _dbContext.House.Find(RACIST_HOUSE_ID);
            var maison2 = _dbContext.House.Find(RETARD_HOUSE_ID);

            var HomelessCat = 

            Assert.ThrowsExactly<WildCatException>(() =>
            {
                catservice.Move(CHAT_SANS_MAISON_ID, maison1, maison2); 
            }); 
        }


      


    }
}