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
            var absence = new AbsenceEntity(
                new DateOnly(2024, 02, 25), 
                new DateOnly(2024, 02, 26),
                ReasonEntity.RRT);

            // Act
            _abscenceRepository.AddAbsence(absence);

            // Assert
            _context.Absence.Should().Contain(absence);
        }

        [Test]
        public async Task TestGetAbsence()
        {
            // Arrange
            var absence1 = new AbsenceEntity(
                 new DateOnly(2024, 02, 25),
                 new DateOnly(2024, 02, 26),
                 ReasonEntity.RRT);
            var absence2 = new AbsenceEntity(
                 new DateOnly(2024, 05, 24),
                 new DateOnly(2024, 05, 28),
                 ReasonEntity.Maladie);

            _abscenceRepository.AddAbsence(absence1);
            _abscenceRepository.AddAbsence(absence2);

            // Act
            var theAbsence = await _abscenceRepository.GetAbsence(absence1.Id);

            // Assert
            theAbsence.Should().Be(absence1);
        }

        [Test]
        public async Task TestGetAllAbsence()
        {

            // Arrange
            var absence1 = new AbsenceEntity(
                new DateOnly(2024, 02, 25),
                new DateOnly(2024, 02, 26),
                ReasonEntity.RRT);
            var absence2 = new AbsenceEntity(
                 new DateOnly(2024, 05, 24),
                 new DateOnly(2024, 05, 28),
                 ReasonEntity.Maladie);

            _abscenceRepository.AddAbsence(absence1);
            _abscenceRepository.AddAbsence(absence2);

            // Act
            var theStaffs = await _abscenceRepository.GetAllAbsence();

            // Assert
            theStaffs.Should().HaveCount(2);
            theStaffs.Should().Contain(new[] { absence1, absence2 });
        }

        [Test]
        public void TestDeleteAbsence()
        {
            // Arrange
            var absence1 = new AbsenceEntity(
                new DateOnly(2024, 02, 25),
                new DateOnly(2024, 02, 26),
                ReasonEntity.RRT);
            var absence2 = new AbsenceEntity(
                 new DateOnly(2024, 05, 24),
                 new DateOnly(2024, 05, 28),
                 ReasonEntity.Maladie);

            _abscenceRepository.AddAbsence(absence1);
            _abscenceRepository.AddAbsence(absence2);

            // Act
            _abscenceRepository.DeleteAbsence(absence2.Id);

            // Assert
            _context.Should().NotBe(absence2);
        }

        [Test]
        public void TestUpdateAbsence()
        {
            // Arrange
            var absence = new AbsenceEntity(
                new DateOnly(2024, 02, 25),
                new DateOnly(2024, 02, 26),
                ReasonEntity.RRT);
            _abscenceRepository.AddAbsence(absence);



            // Act
            new DateOnly(2024, 02, 25);
            new DateOnly(2024, 02, 26);
            absence.Reason = ReasonEntity.Famille;

            _abscenceRepository.UpdateAbsence(absence);

            // Assert
            var absenceInDatabase = _context.Absence.Find(absence.Id);
            absenceInDatabase.Reason.Should().Be(absence.Reason);
        }
    }
}
