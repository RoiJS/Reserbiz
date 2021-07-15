using NUnit.Framework;
using System;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.Tests.Comparers;
using ReserbizAPP.LIB.Models;
using System.Collections.Generic;
using ReserbizAPP.LIB.BusinessLogic;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class PaymentRepositoryTests
    {
        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToAll()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.All
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 12
                },
                new PaymentBreakdown {
                    Id = 11
                },
                new PaymentBreakdown {
                    Id = 10
                },
                new PaymentBreakdown {
                    Id = 9
                },
                new PaymentBreakdown {
                    Id = 8
                },
                new PaymentBreakdown {
                    Id = 7
                },
                new PaymentBreakdown {
                    Id = 6
                },
                new PaymentBreakdown {
                    Id = 5
                },
                new PaymentBreakdown {
                    Id = 4
                },
                new PaymentBreakdown {
                    Id = 3
                },
                new PaymentBreakdown {
                    Id = 2
                },
                new PaymentBreakdown {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToRental()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.Rental
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 3
                },
                new PaymentBreakdown {
                    Id = 2
                },
                new PaymentBreakdown {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToRental_And_SortOrderAscending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.Rental,
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 1
                },
                new PaymentBreakdown {
                    Id = 2
                },
                new PaymentBreakdown {
                    Id = 3
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToElectricBill()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.ElectricBill
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 5
                },
                new PaymentBreakdown {
                    Id = 4
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToElectricBill_And_SortOrderAscending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.ElectricBill,
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 4
                },
                new PaymentBreakdown {
                    Id = 5
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToWaterBill()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.WaterBill
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 6
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToWaterBill_And_SortOrderAscending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.WaterBill,
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 6
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToMiscellaneousFee()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 8
                },
                new PaymentBreakdown {
                    Id = 7
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToMiscellaneousFee_And_Ascending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 7
                },
                new PaymentBreakdown {
                    Id = 8
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToPenalty()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.Penalty
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 12
                },
                new PaymentBreakdown {
                    Id = 11
                },
                new PaymentBreakdown {
                    Id = 10
                },
                new PaymentBreakdown {
                    Id = 9
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }
        
        [Test]
        public void Test_GetFilteredPayments_WhenFilterPaymentForTypeIsSetToPenalty_And_SortOrderAscending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                PaymentForType = PaymentForTypeEnum.Penalty,
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 9
                },
                new PaymentBreakdown {
                    Id = 10
                },
                new PaymentBreakdown {
                    Id = 11
                },
                new PaymentBreakdown {
                    Id = 12
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterSortOrderIsSetToAscending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 1
                },
                new PaymentBreakdown {
                    Id = 2
                },
                new PaymentBreakdown {
                    Id = 3
                },
                new PaymentBreakdown {
                    Id = 4
                },
                new PaymentBreakdown {
                    Id = 5
                },
                new PaymentBreakdown {
                    Id = 6
                },
                new PaymentBreakdown {
                    Id = 7
                },
                new PaymentBreakdown {
                    Id = 8
                },
                new PaymentBreakdown {
                    Id = 9
                },
                new PaymentBreakdown {
                    Id = 10
                },
                new PaymentBreakdown {
                    Id = 11
                },
                new PaymentBreakdown {
                    Id = 12
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredPayments_WhenFilterSortOrderIsSetToDescending()
        {
            // Arrange
            var paymentBreakdownRepository = new PaymentBreakdownRepository();
            var payments = GetPayments();
            var comparer = new PaymentComparer();
            var paymentFilter = new PaymentFilter
            {
                SortOrder = SortOrderEnum.Descending
            };

            // Act
            var actualResult = paymentBreakdownRepository.GetFilteredPayments(payments, paymentFilter);

            // Assert
            var expectedResult = new List<PaymentBreakdown> {
                new PaymentBreakdown {
                    Id = 12
                },
                new PaymentBreakdown {
                    Id = 11
                },
                new PaymentBreakdown {
                    Id = 10
                },
                new PaymentBreakdown {
                    Id = 9
                },
                new PaymentBreakdown {
                    Id = 8
                },
                new PaymentBreakdown {
                    Id = 7
                },
                new PaymentBreakdown {
                    Id = 6
                },
                new PaymentBreakdown {
                    Id = 5
                },
                new PaymentBreakdown {
                    Id = 4
                },
                new PaymentBreakdown {
                    Id = 3
                },
                new PaymentBreakdown {
                    Id = 2
                },
                new PaymentBreakdown {
                    Id = 1
                },
            };


            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        private List<PaymentBreakdown> GetPayments()
        {

            var payments = new List<PaymentBreakdown>();

            payments.Add(new PaymentBreakdown
            {
                Id = 1,
                PaymentForType = PaymentForTypeEnum.Rental,
                Amount = 3000,
                DateTimeReceived = new DateTime(2021, 06, 12)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 2,
                PaymentForType = PaymentForTypeEnum.Rental,
                Amount = 2500,
                DateTimeReceived = new DateTime(2021, 06, 15)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 3,
                PaymentForType = PaymentForTypeEnum.Rental,
                Amount = 1500,
                DateTimeReceived = new DateTime(2021, 06, 18)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 4,
                PaymentForType = PaymentForTypeEnum.ElectricBill,
                Amount = 150,
                DateTimeReceived = new DateTime(2021, 06, 21)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 5,
                PaymentForType = PaymentForTypeEnum.ElectricBill,
                Amount = 150,
                DateTimeReceived = new DateTime(2021, 06, 24)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 6,
                PaymentForType = PaymentForTypeEnum.WaterBill,
                Amount = 100,
                DateTimeReceived = new DateTime(2021, 06, 27, 10, 30, 0)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 7,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                Amount = 1000,
                DateTimeReceived = new DateTime(2021, 06, 27, 14, 20, 0)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 8,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                Amount = 500,
                DateTimeReceived = new DateTime(2021, 06, 27, 19, 35, 0)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 9,
                PaymentForType = PaymentForTypeEnum.Penalty,
                Amount = 50,
                DateTimeReceived = new DateTime(2021, 06, 28)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 10,
                PaymentForType = PaymentForTypeEnum.Penalty,
                Amount = 50,
                DateTimeReceived = new DateTime(2021, 06, 29)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 11,
                PaymentForType = PaymentForTypeEnum.Penalty,
                Amount = 50,
                DateTimeReceived = new DateTime(2021, 06, 30)
            });

            payments.Add(new PaymentBreakdown
            {
                Id = 12,
                PaymentForType = PaymentForTypeEnum.Penalty,
                Amount = 50,
                DateTimeReceived = new DateTime(2021, 07, 01)
            });

            return payments;
        }
    }
}