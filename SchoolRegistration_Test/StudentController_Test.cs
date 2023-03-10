using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolRegistration.Controllers;
using SchoolRegistration.Models;
using SchoolRegistration.Repository;
using SchoolRegistration.Repository.Base;

namespace SchoolRegistration_Test
{
    public class StudentController_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllStudent_No_Existing_Records_Returns_NotFound()
        {
            //Arrange
            var mockRepo = new Mock<IStudentRepository>();

            var controller = new Mock<StudentController>(mockRepo.Object);
            var mockDataRepo = new Mock<IDataRepository>();
            mockRepo.Setup(d => d.GetData()).Returns((StudentRegistrationData)null);
            mockRepo.Setup(p => p.GetAll()).Returns((List<Student>)null);
            mockDataRepo.Setup(d => d.GetData()).Returns(It.IsAny<StudentRegistrationData>());

            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            var personController = fixture.Create<StudentController>();
            //Act

            var result = personController.GetAll();

            //Assert
            mockRepo.Verify(p => p.GetAll(), Times.Once);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}