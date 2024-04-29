using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using System;

namespace SVE.Mediatek.DAL.IntegrationTest
{
    public class StaffRepositoryTest
    {
        private StaffRepository _staffRepository;
        private MediatekContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MediatekContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Creation of an in-memory database
                .Options;
            _context = new MediatekContext(options);
            _staffRepository = new StaffRepository(_context);
        }
      
        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();//Re-initialization of the in-memory database
            _context.Dispose(); //Releasing the DbContext at the end of each test to limit memory leaks
        }


        [Test]
        public void TestAddStaff()
        { 
            // Arrange
            var staff = new StaffEntity(
                "Alicia", 
                "Durant", 
                "adurant@mediatek.com", 
                "0256357891", 
                DepartmentEntity.Documentation);

            // Act
            _staffRepository.AddStaff(staff);

            // Assert
            _context.Staffs.Should().Contain(staff);
        }

        [Test]
        public async Task TestGetStafff()
        {
            // Arrange
            var staff1 = new StaffEntity("Alice", "Smith", "asmith@mediatek.com", "0123456789", DepartmentEntity.Documentation);
            var staff2 = new StaffEntity("Bob", "Johnson", "bjohnson@mediatek.com", "9876543210", DepartmentEntity.Comptabilité);

            _staffRepository.AddStaff(staff1);
            _staffRepository.AddStaff(staff2);

            // Act
            var theStaff = await _staffRepository.GetStaff(staff1.Id);

            // Assert
            theStaff.Should().Be(staff1);
        }

        [Test] 
        public async Task TestGetAllStaff()
        {

            // Arrange
            var staff1 = new StaffEntity("Alice", "Smith", "asmith@mediatek.com", "0123456789", DepartmentEntity.Documentation);
            var staff2 = new StaffEntity("Bob", "Johnson", "bjohnson@mediatek.com", "9876543210", DepartmentEntity.Comptabilité);

            _staffRepository.AddStaff(staff1);
            _staffRepository.AddStaff(staff2);

            // Act
            var theStaffs = await _staffRepository.GetAllStaff();

            // Assert
            theStaffs.Should().HaveCount(2);
            theStaffs.Should().Contain(new[] { staff1, staff2 });
        }

        [Test]
        public void TestDeleteStaff()
        {
            // Arrange
            var staff1 = new StaffEntity("Alice", "Smith", "asmith@mediatek.com", "0123456789", DepartmentEntity.Documentation);
            var staff2 = new StaffEntity("Bob", "Johnson", "bjohnson@mediatek.com", "9876543210", DepartmentEntity.Comptabilité);

            _staffRepository.AddStaff(staff1);
            _staffRepository.AddStaff(staff2);

            // Act
            _staffRepository.DeleteStaff(staff2.Id);

            // Assert
            _context.Should().NotBe(staff2);
        }

        [Test]
        public void TestUpdateStaff()
        {
            // Arrange
            var staff = new StaffEntity("Alice", "Smith", "asmith@mediatek.com", "0123456789", DepartmentEntity.Documentation);
            _staffRepository.AddStaff(staff);

            // Act
            staff.Name = "Alice";
            staff.FirsName = "Smith";
            staff.Email = "asmith@mediatek.com";
            staff.Phone = "0725987413";
            staff.Department = DepartmentEntity.Documentation;

            _staffRepository.UpdateStaff(staff); 

            // Assert
            var staffInDatabase = _context.Staffs.Find(staff.Id);
            staffInDatabase.Phone.Should().Be(staff.Phone);
        }
    }
}