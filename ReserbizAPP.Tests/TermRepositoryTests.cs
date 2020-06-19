using System.Collections.Generic;
using NUnit.Framework;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class TermRepositoryTests
    {
        [TestCase(1, "T-0004")]
        [TestCase(2, "T-0005")]
        [TestCase(3, "T-0006")]
        public void Should_ReturnFalse_WhenTermIdBelongsToDifferentExistingTerm_And_TermCodeDoesExistsToAnyTerm(int termId, string termCode)
        {
            // Arrange
            var termRepository = new TermRepository();
            var termlist = TermListTestData();

            // Act 
            var result = termRepository.CheckTermCodeIfExists(termlist, termId, termCode);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(1, "T-0001")]
        [TestCase(2, "T-0002")]
        [TestCase(3, "T-0003")]
        public void Should_ReturnFalse_WhenTermIdAndCodeBelongsToTheSameExistingTerm(int termId, string termCode)
        {
            // Arrange
            var termRepository = new TermRepository();
            var termlist = TermListTestData();

            // Act 
            var result = termRepository.CheckTermCodeIfExists(termlist, termId, termCode);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(1, "T-0002")]
        [TestCase(2, "T-0003")]
        [TestCase(3, "T-0001")]
        public void Should_ReturnTrue_WhenTermIdBelongsToDifferentTerm_And_TermCodeBelongsToDifferentTerm(int termId, string termCode)
        {
            // Arrange
            var termRepository = new TermRepository();
            var termlist = TermListTestData();

            // Act 
            var result = termRepository.CheckTermCodeIfExists(termlist, termId, termCode);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(0, "T-0004")]
        [TestCase(-1, "T-0005")]
        [TestCase(-2, "T-0006")]
        public void Should_ReturnFalse_WhenTermIdDoesNotExists_And_TermCodeDoesNotExists(int termId, string termCode)
        {
            // Arrange
            var termRepository = new TermRepository();
            var termlist = TermListTestData();

            // Act 
            var result = termRepository.CheckTermCodeIfExists(termlist, termId, termCode);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(0, "T-0001")]
        [TestCase(-1, "T-0002")]
        [TestCase(-2, "T-0003")]
        public void Should_ReturnTrue_WhenTermIdDoesNotExists_And_TermCodeDoesExists(int termId, string termCode)
        {
            // Arrange
            var termRepository = new TermRepository();
            var termlist = TermListTestData();

            // Act 
            var result = termRepository.CheckTermCodeIfExists(termlist, termId, termCode);

            // Assert
            Assert.IsTrue(result);
        }

        private IList<Term> TermListTestData()
        {
            var termlist = new List<Term>();
            termlist.Add(new Term
            {
                Id = 1,
                Code = "T-0001"
            });
            termlist.Add(new Term
            {
                Id = 2,
                Code = "T-0002"
            });
            termlist.Add(new Term
            {
                Id = 3,
                Code = "T-0003"
            });

            return termlist;
        }
    }
}