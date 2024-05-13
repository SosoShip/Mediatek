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
        private IRepository<StaffEntity> _staffRepository;
        private MediatekContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MediatekContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Creation of an in-memory database
                .Options;
            _context = new MediatekContext(options);
            _staffRepository = new Repository<StaffEntity>(_context);
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
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };

            // Act
            _ = _staffRepository.Add(staff);

            // Assert
            _context.Staffs.Should().Contain(staff);
        }

        [Test]
        public async Task TestGetStafff()
        {
            // Arrange
            var staff1 = new StaffEntity 
            { 
                FirsName = "Alice", 
                Name = "Smith", 
                Email = "asmith@mediatek.com", 
                Phone = "0123456789", 
                Department =  Department.Documentation 
            };
            var staff2 = new StaffEntity
            { 
                FirsName = "Bob", 
                Name = "Johnson", 
                Email = "bjohnson@mediatek.com", 
                Phone = "9876543210", 
                Department =  Department.Comptabilité 
            };

            _ = _staffRepository.Add(staff1);
            _ = _staffRepository.Add(staff2);

            // Act
            var theStaff = await _staffRepository.Get(staff1.Id);

            // Assert
            theStaff.Should().Be(staff1);
        }

        [Test] 
        public async Task TestGetAllStaff()
        {

            // Arrange
            var staff1 = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };
            var staff2 = new StaffEntity
            {
                FirsName = "Bob",
                Name = "Johnson",
                Email = "bjohnson@mediatek.com",
                Phone = "9876543210",
                Department = Department.Comptabilité
            };

            _ = _staffRepository.Add(staff1);
            _ = _staffRepository.Add(staff2);

            // Act
            var theStaffs = await _staffRepository.GetAll();

            // Assert
            theStaffs.Should().HaveCount(2);
            theStaffs.Should().Contain(new[] { staff1, staff2 });
        }

        [Test]
        public void TestDeleteStaff()
        {
            // Arrange
            var staff1 = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };
            var staff2 = new StaffEntity
            {
                FirsName = "Bob",
                Name = "Johnson",
                Email = "bjohnson@mediatek.com",
                Phone = "9876543210",
                Department = Department.Comptabilité
            };

            _ = _staffRepository.Add(staff1);
            _ = _staffRepository.Add(staff2);

            // Act
            _ = _staffRepository.Delete(staff2.Id);

            // Assert
            _context.Should().NotBe(staff2);
        }

        [Test]
        public void TestUpdateStaff()
        {
            // Arrange
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };
            _ = _staffRepository.Add(staff);

            // Act
            staff.Name = "Alice";
            staff.FirsName = "Smith";
            staff.Email = "asmith@mediatek.com";
            staff.Phone = "0725987413";
            staff.Department = Department.Documentation;

            _ = _staffRepository.Update(staff); 

            // Assert
            var staffInDatabase = _context.Staffs.Find(staff.Id);
            staffInDatabase.Phone.Should().Be(staff.Phone);
        }
    }
}