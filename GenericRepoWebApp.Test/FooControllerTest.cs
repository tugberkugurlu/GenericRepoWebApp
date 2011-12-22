using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GenericRepoWebApp.Data.DataAccess.Infrastructure;
using GenericRepoWebApp.Data.DataAccess.SqlServer;
using GenericRepoWebApp.Controllers;
using System.Web.Mvc;

namespace GenericRepoWebApp.Test {

    [TestClass]
    public class FooControllerTest {

        private IFooRepository fooRepo;

        [TestInitialize]
        public void Initialize() {

            //Mock repository creation
            Mock<IFooRepository> mock = new Mock<IFooRepository>();
            mock.Setup(m => m.GetAll()).Returns(new[] { 
                new Foo { FooId = 1, FooName = "Fake Foo 1" },
                new Foo { FooId = 2, FooName = "Fake Foo 2" },
                new Foo { FooId = 3, FooName = "Fake Foo 3" },
                new Foo { FooId = 4, FooName = "Fake Foo 4" }
            }.AsQueryable());

            mock.Setup(m => 
                m.GetSingle(
                    It.Is<int>(i => 
                        i == 1 || i == 2 || i == 3 || i == 4
                    )
                )
            ).Returns<int>(r => new Foo { 
                FooId = r,
                FooName = string.Format("Fake Foo {0}", r)
            });

            fooRepo = mock.Object;
        }

        [TestMethod]
        public void is_index_returns_model_type_of_iqueryable_foo() {
            
            //Arrange
            //Create the controller instance
            FooController fooController = new FooController(fooRepo);

            //Act
            var indexModel = fooController.Index().Model;

            //Assert
            Assert.IsInstanceOfType(indexModel, typeof(IQueryable<Foo>));
        }

        [TestMethod]
        public void is_index_returns_iqueryable_foo_count_of_4() {

            //Arrange
            //Create the controller instance
            FooController fooController = new FooController(fooRepo);

            //Act
            var indexModel = (IQueryable<object>)fooController.Index().Model;

            //Assert
            Assert.AreEqual<int>(4, indexModel.Count());
        }

        [TestMethod]
        public void is_details_returns_type_of_ViewResult() {

            //Arrange
            //Create the controller instance
            FooController fooController = new FooController(fooRepo);

            //Act
            var detailsResult = fooController.Details(1);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(ViewResult));
        }

        [TestMethod]
        public void is_details_returns_type_of_HttpNotFoundResult() { 

            //Arrange
            //Create the controller instance
            FooController fooController = new FooController(fooRepo);

            //Act
            var detailsResult = fooController.Details(5);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(HttpNotFoundResult));
        }
    }
}