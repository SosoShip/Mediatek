using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;
using System;

namespace SVE.Mediatek.DAL.IntegrationTest
{
    public class ManagerRepositoryTest { }
    //{
    //    private IManagerRepository<ManagerEntity> _managerRepository;
    //    private IRepository<StaffEntity> _staffRepository;
    //    private MediatekContext _context;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        var options = new DbContextOptionsBuilder<MediatekContext>()
    //            .UseInMemoryDatabase(databaseName: "TestDatabase") // Creation of an in-memory database
    //            .Options;
    //        _context = new MediatekContext(options);
    //        _managerRepository = new ManagerRepository(_context);
    //        _staffRepository = new Repository<StaffEntity>(_context);
    //    }

    //    [TearDown]
    //    public void Teardown()
    //    {
    //        _context.Database.EnsureDeleted();//Re-initialization of the in-memory database
    //        _context.Dispose(); //Releasing the DbContext at the end of each test to limit memory leaks
    //    }


    //    [Test]
    //    public async Task GetManagerAsync()
    //    {
    //        // Arrange
    //        var staff1 = new ManagerEntity
    //        {
    //            FirsName = "Alice",
    //            Name = "Smith",
    //            Email = "asmith@mediatek.com",
    //            Phone = "0123456789",
    //            Department = Department.Manager,
    //            Password = "password",
    //            Salt = "123"
    //        };
    //        var staff2 = new StaffEntity
    //        {
    //            FirsName = "Bob",
    //            Name = "Johnson",
    //            Email = "bjohnson@mediatek.com",
    //            Phone = "9876543210",
    //            Department = Department.Comptabilité
    //        };

    //        _ = _staffRepository.Add(staff1);
    //        _ = _staffRepository.Add(staff2);

    //        // Act
    //        var theManager = await _managerRepository.GetManager(Department.Manager);

    //        // Assert
    //        theManager.Should().Be(staff1);
    //    }
    //}
}
