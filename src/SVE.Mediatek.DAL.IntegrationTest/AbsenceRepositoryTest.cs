using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.DAL.Repository;

namespace SVE.Mediatek.DAL.IntegrationTest
{
    internal class AbsenceRepositoryTest
    {
        private AbsenceRepository _abscenceRepository;
        private MediatekContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MediatekContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Creation of an in-memory database
                .Options;
            _context = new MediatekContext(options);
            _abscenceRepository = new AbsenceRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();//Re-initialization of the in-memory database
            _context.Dispose(); //Releasing the DbContext at the end of each test to limit memory leaks
        }


        [Test]
        public void TestAddAbsence()
        {
            // Arrange
            var absence = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 02, 25),
                EndDate = new DateOnly(2024, 02, 26),
                Reason = Reason.RRT
            };
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };


            // Act
            _abscenceRepository.AddAbsence(staff.Id, absence);

            // Assert
            _context.Absence.Should().Contain(absence);
        }

        [Test]
        public async Task TestGetAbsence()
        {
            // Arrange
            var absence1 = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 02, 25),
                EndDate = new DateOnly(2024, 02, 26),
                Reason = Reason.RRT
            };

            var absence2 = new AbsenceEntity
                 {
                BeginDate = new DateOnly(2024, 05, 25),
                EndDate = new DateOnly(2024, 05, 26),
                Reason = Reason.Maladie
            };

            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };

            _abscenceRepository.AddAbsence(staff.Id, absence1);
            _abscenceRepository.AddAbsence(staff.Id, absence2);

            // Act
            var theAbsence = await _abscenceRepository.Get(absence1.Id);

            // Assert
            theAbsence.Should().Be(absence1);
        }

        [Test]
        public async Task TestGetAllAbsence()
        {

            // Arrange
            var absence1 = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 02, 25),
                EndDate = new DateOnly(2024, 02, 26),
                Reason = Reason.RRT
            };

            var absence2 = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 05, 25),
                EndDate = new DateOnly(2024, 05, 26),
                Reason = Reason.Maladie
            };
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };


            _abscenceRepository.AddAbsence(staff.Id, absence1);
            _abscenceRepository.AddAbsence(staff.Id, absence2);

            // Act
            var theAbsence = _abscenceRepository.GetAll();

            // Assert
            theAbsence.Should().HaveCount(2);
            theAbsence.Should().Contain(new[] { absence1, absence2 });
        }

        [Test]
        public void TestDeleteAbsence()
        {
            // Arrange
            var absence1 = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 02, 25),
                EndDate = new DateOnly(2024, 02, 26),
                Reason = Reason.RRT
            };

            var absence2 = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 05, 25),
                EndDate = new DateOnly(2024, 05, 26),
                Reason = Reason.Maladie
            };
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };


            _abscenceRepository.AddAbsence(staff.Id, absence1);
            _abscenceRepository.AddAbsence(staff.Id, absence2);

            // Act
            _abscenceRepository.Delete(absence2.Id);

            // Assert
            _context.Should().NotBe(absence2);
        }

        [Test]
        public void TestUpdateAbsence()
        {
            // Arrange
            var absence = new AbsenceEntity
            {
                BeginDate = new DateOnly(2024, 02, 25),
                EndDate = new DateOnly(2024, 02, 26),
                Reason = Reason.RRT
            };
            var staff = new StaffEntity
            {
                FirsName = "Alice",
                Name = "Smith",
                Email = "asmith@mediatek.com",
                Phone = "0123456789",
                Department = Department.Documentation
            };

            _abscenceRepository.AddAbsence(staff.Id, absence);


            // Act
            new DateOnly(2024, 02, 25);
            new DateOnly(2024, 02, 26);
            absence.Reason = Reason.Famille;

            _abscenceRepository.Update(absence);

            // Assert
            var absenceInDatabase = _context.Absence.Find(absence.Id);
            absenceInDatabase.Reason.Should().Be(absence.Reason);
        }
    }
}
